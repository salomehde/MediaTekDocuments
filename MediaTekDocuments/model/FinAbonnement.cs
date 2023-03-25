using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaTekDocuments.model
{
    public class FinAbonnement
    {
        public DateTime DateFinAbonnement { get; }
        public string IdRevue { get; }
        public string RevueTitre { get; }

        public FinAbonnement(DateTime dateFinAbonnement, string idRevue, string RevueTitre)
        {
            this.DateFinAbonnement = dateFinAbonnement;
            this.IdRevue = idRevue;
            this.RevueTitre = RevueTitre;
        }
    }
}
