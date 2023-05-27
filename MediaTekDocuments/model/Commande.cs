using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaTekDocuments.model
{
    /// <summary>
    /// Classe métier Commande
    /// </summary>
    public class Commande
    {
        /// <summary>
        /// id d'une commande
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// date d'une commande
        /// </summary>
        public DateTime DateCommande { get; set; }
        /// <summary>
        /// montant d'une commande
        /// </summary>
        public double Montant { get; set; }
        /// <summary>
        /// Initialisation d'un nouvel objet Commande
        /// </summary>
        /// <param name="id"></param>
        /// <param name="dateCommande"></param>
        /// <param name="montant"></param>
        public Commande(string id, DateTime dateCommande, double montant)
        {
            this.Id = id;
            this.DateCommande = dateCommande;
            this.Montant = montant;
        }
    }
}
