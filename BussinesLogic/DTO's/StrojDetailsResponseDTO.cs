using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinesLogic.DTO_s
{
    public class StrojDetailsResponseDTO
    {
        public int StrojeviId { get; set; }
        public string Naziv { get; set; }
        public double ProsjecnoTrajanjeKvara { get; set; }
        public List<KvaroviDTO> KvaroviList { get; set; }
    }
}
