using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaTekDocuments.model
{
    /// <summary>
    /// Classe métier Abonnement hérite de commande
    /// </summary>
    public class Abonnement : Commande
    {
        /// <summary>
        /// Date de la fin de l'abonnement
        /// </summary>
        public DateTime DateFinAbonnement { get; }
        /// <summary>
        /// L'identifiant de la revue
        /// </summary>
        public string IdRevue { get; }
        /// <summary>
        /// Initialisation d'un nouvel objet Abonnement
        /// </summary>
        /// <param name="id">Id de l'abonnement</param>
        /// <param name="dateCommande">Date de la commande de l'abonnement</param>
        /// <param name="montant">Montant de l'abonnement</param>
        /// <param name="dateFinAbonnement">Date de fin de l'abonnement</param>
        /// <param name="idRevue">Id de la revue concernée par l'abonnement</param>
        public Abonnement(string id, DateTime dateCommande, double montant, DateTime dateFinAbonnement, string idRevue) : base(id, dateCommande, montant)
        {
            this.DateFinAbonnement = dateFinAbonnement;
            this.IdRevue = idRevue;
        }
    }
}
