using BussinesLogic.DTO_s;
using BussinesLogic.Interfaces;
using Dapper;
using Data.Interfaces;
using Data.Models;
using NpgsqlTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace BussinesLogic.Services
{
    public class StrojeviService : IStrojeviService
    {
        private readonly IDbService _dbService;

        public StrojeviService(IDbService dbService)
        {
            _dbService = dbService;
        }

        public async Task AddStroj(UpdateCreateStrojeviDTO stroj)
        {
            if(await DoesStrojNazivExist(stroj.Naziv) is true)
            {
                throw new ArgumentException("Name already exists");
            }

            string command = "INSERT INTO strojevi  (Naziv)" +
                "VALUES (@Naziv)";
            await _dbService.CreateEntity<Strojevi>(command, stroj);
        }

        public async Task DeleteStroj(int strojeviId)
        {
            string command = "DELETE FROM strojevi WHERE StrojeviId = @StrojeviId";
            await _dbService.DeleteEntity(command, new { strojeviId });
        }

        public async Task<List<StrojeviDTO>> GetAllStrojevi()
        {
            string command = "SELECT * FROM strojevi";
            var strojevi = await _dbService.GetAllAsync<StrojeviDTO>(command, null);
            return strojevi;
        }

        public async Task<StrojDetailsResponseDTO> GetStrojeviById(int strojeviId)
        {
            string strojcommand = "SELECT * FROM strojevi WHERE StrojeviId = @StrojeviId";
            var strojResult = await _dbService.GetByIdAsync<StrojeviDTO>(strojcommand, new { strojeviId });

            string kvarcommand = "SELECT * FROM kvarovi WHERE StrojeviId = @StrojeviId";
            var kvarovi = await _dbService.GetAllAsync<KvaroviDTO>(kvarcommand, new { strojeviId });

            var averageKvarTime = kvarovi.Select(kavg => kavg.Vrijeme_zavrsetak - kavg.Vrijeme_pocetak).Average(ts => ts.TotalMinutes);

            return new StrojDetailsResponseDTO {
                StrojeviId = strojResult.StrojeviId,
                Naziv = strojResult.Naziv,
                ProsjecnoTrajanjeKvara = averageKvarTime,
                KvaroviList = kvarovi
                
            };

        }

        public async Task UpdateStrojevi(int strojeviId, UpdateCreateStrojeviDTO strojeviDTO)
        {
            if(await DoesStrojNazivExist(strojeviDTO.Naziv) == true)
            {
                throw new ArgumentException("Name already exists");
            }

            string command = "UPDATE strojevi SET Naziv = @Naziv WHERE StrojeviId = @StrojeviId";
            await _dbService.UpdateEntity<StrojeviDTO>(command, new { strojeviId, strojeviDTO.Naziv });
        }

        public async Task<bool> DoesStrojNazivExist(string naziv)
        {
            string command = "SELECT * FROM strojevi WHERE Naziv = @Naziv";
            var result = await _dbService.GetAllAsync<StrojeviDTO>(command, new {naziv });

            if(result == null)
            {
                return false;
            }
            return true;
        }
    }
}
