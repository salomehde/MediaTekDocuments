using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaTekDocuments.model
{
    /// <summary>
    /// Classe métier FinAbonnement
    /// </summary>
    public class FinAbonnement
    {
        /// <summary>
        /// date de fin de l'abonnement
        /// </summary>
        public DateTime DateFinAbonnement { get; }
        /// <summary>
        /// id de la revue
        /// </summary>
        public string IdRevue { get; }
        /// <summary>
        /// titre de la revue
        /// </summary>
        public string RevueTitre { get; }
        /// <summary>
        /// Initialisation d'un nouvel objet Abonnement
        /// </summary>
        /// /// <param name="dateFinAbonnement"></param>
        /// <param name="idRevue"></param>
        /// <param name="RevueTitre"></param>
        public FinAbonnement(DateTime dateFinAbonnement, string idRevue, string RevueTitre)
        {
            this.DateFinAbonnement = dateFinAbonnement;
            this.IdRevue = idRevue;
            this.RevueTitre = RevueTitre;
        }
    }
}
