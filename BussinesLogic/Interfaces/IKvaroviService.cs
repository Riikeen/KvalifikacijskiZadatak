using BussinesLogic.DTO_s;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinesLogic.Interfaces
{
    public interface IKvaroviService
    {
        Task AddKvar(UpdateCreateKvaroviDTO stroj);
        Task DeleteKvar(int kvaroviId);
        Task<List<KvaroviDTO>> GetAllKvarovi(int pagesize, int pagenumber);
        Task<KvaroviDTO> GetKvaroviById(int kvaroviId);
        Task UpdateKvar(int kvaroviId, UpdateCreateKvaroviDTO kvaroviDTO);
        Task UpdateKvarStatus(int id, bool isResolved);
    }
}
