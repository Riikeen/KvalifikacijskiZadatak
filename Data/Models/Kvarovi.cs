using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    public enum Prioritet
    {
        Nizak,
        Srednji,
        Visok
    }

    public class Kvarovi
    {
        public int KvaroviId { get; set; }
        public string Naziv { get; set; }
        public Prioritet Prioritet { get; set; }

        public DateTime Vrijeme_pocetak { get; set; }
        public DateTime Vrijeme_zavrsetak { get; set; }
        public string Opis { get; set; }
        public int StrojeviId { get; set; }
        public bool IsResolved { get; set; }

    }
}
