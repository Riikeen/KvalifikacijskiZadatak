using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Interfaces
{
    public interface IDbService
    {
        Task<T> GetByIdAsync<T>(string command,object parameters);
        Task<List<T>> GetAllAsync<T>(string command, object parameters);
        Task CreateEntity<T>(string command, object parameters);
        Task DeleteEntity(string command, object parameters);
        Task UpdateEntity<T>(string command, object parameters);
        
    }
}
