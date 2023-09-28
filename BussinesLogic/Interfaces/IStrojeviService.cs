using BussinesLogic.DTO_s;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinesLogic.Interfaces
{
    public interface IStrojeviService
    {
        Task AddStroj(UpdateCreateStrojeviDTO stroj);
        Task DeleteStroj(int kvaroviId);
        Task<List<StrojeviDTO>> GetAllStrojevi();
        Task<StrojDetailsResponseDTO> GetStrojeviById(int kvaroviId);
        Task UpdateStrojevi(int kvaroviId, UpdateCreateStrojeviDTO strojeviDTO);
    }
}
