using BussinesLogic.DTO_s;
using BussinesLogic.Interfaces;
using Data.Interfaces;
using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinesLogic.Services
{
    public class KvaroviService : IKvaroviService
    {
        private readonly IDbService _dbService;
        public KvaroviService(IDbService dbService)
        {
            _dbService = dbService;
        }

        public async Task AddKvar(UpdateCreateKvaroviDTO kvaroviDTO)
        {
            if(kvaroviDTO.Opis == null){
                throw new ArgumentException("Opis nije unesen");
            }

            if(await IsPreviousKvarResolved(kvaroviDTO.StrojeviId) is false)
            {
                throw new Exception("Postoji neotklonjeni kvar");
            }


            string command = "INSERT INTO kvarovi (Naziv, Prioritet, Vrijeme_pocetak, Vrijeme_zavrsetak, Opis, StrojeviId, IsResolved)" +
                                "VALUES (@Naziv, @Prioritet::prioritet, @Vrijeme_pocetak, @Vrijeme_zavrsetak, @Opis, @StrojeviId, @IsResolved)";

            await _dbService.CreateEntity<Kvarovi>(command, new
            {
                kvaroviDTO.Naziv,
                Prioritet=kvaroviDTO.Prioritet.ToString(),
                kvaroviDTO.Vrijeme_pocetak,
                kvaroviDTO.Vrijeme_zavrsetak,
                kvaroviDTO.Opis,
                kvaroviDTO.StrojeviId,
                kvaroviDTO.IsResolved

            });

        }

        public async Task DeleteKvar(int kvaroviId)
        {
            string command = "DELETE FROM kvarovi WHERE KvaroviId = @KvaroviId";
            await _dbService.DeleteEntity(command, new { kvaroviId });
        }

        public async Task<List<KvaroviDTO>> GetAllKvarovi(int pagesize,int pagenumber)
        {
            var offset = CalculateOffset(pagesize, pagenumber);
            string command = "SELECT * FROM kvarovi" +
                " OFFSET @offset ROWS FETCH NEXT @pagesize ROWS ONLY";
            var response = await _dbService.GetAllAsync<KvaroviDTO>(command, new { offset, pagesize });
            return response.OrderByDescending(o => o.Vrijeme_pocetak).OrderBy(o => o.Prioritet).ToList();
        }

        public async Task<KvaroviDTO> GetKvaroviById(int kvaroviId)
        {
            string command = "SELECT * FROM kvarovi WHERE KvaroviId = @KvaroviId";
            return await _dbService.GetByIdAsync<KvaroviDTO>(command, new { kvaroviId });
        }

        public async Task UpdateKvar(int id, UpdateCreateKvaroviDTO kvaroviDTO)
        {
            string command = "UPDATE kvarovi SET Naziv = @Naziv, Prioritet = @Prioritet::prioritet, Vrijeme_pocetak = @Vrijeme_pocetak, Vrijeme_zavrsetak = @Vrijeme_zavrsetak, Opis = @Opis, StrojeviId = @StrojeviId WHERE KvaroviId = @KvaroviId, IsResolved = @IsResolved";

            await _dbService.UpdateEntity<Kvarovi>(command, new
            {
                KvaroviId = id,
                kvaroviDTO.Naziv,
                Prioritet = kvaroviDTO.Prioritet.ToString(),
                kvaroviDTO.Vrijeme_pocetak,
                kvaroviDTO.Vrijeme_zavrsetak,
                kvaroviDTO.Opis,
                kvaroviDTO.StrojeviId,
                kvaroviDTO.IsResolved
            }) ;
        }

        public async Task UpdateKvarStatus(int kvaroviId, bool isResolved)
        {
            string command = "UPDATE kvarovi SET IsResolved = @IsResolved Where KvaroviId = @KvaroviId";
            await _dbService.UpdateEntity<Kvarovi>(command, new { kvaroviId, isResolved });
        }
        public static int CalculateOffset(int pagesize, int pagenumber)
        {
            return (pagenumber - 1) * pagesize;
        }

        public async Task<bool> IsPreviousKvarResolved(int strojeviId)
        {
            const string command = "SELECT * FROM kvarovi WHERE StrojeviId = @StrojeviId AND IsResolved = false";
            var result = await _dbService.GetAllAsync<Kvarovi>(command, new {strojeviId });
            if(result.Count < 1)
            {
                return true;
            }
            return false;
        }
    }
}
