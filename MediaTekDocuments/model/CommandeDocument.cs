using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaTekDocuments.model
{
    /// <summary>
    /// Classe métier CommandeDocument hérite de Commande
    /// </summary>
    public class CommandeDocument : Commande
    {
        /// <summary>
        /// nombre d'exemplaires du document
        /// </summary>
        public int NbExemplaire { get; set; }
        /// <summary>
        /// id du livre/dvd
        /// </summary>
        public string IdLivreDvd { get; set; }
        /// <summary>
        /// id du suivi de la commande
        /// </summary>
        public int IdSuivi { get; set; }
        /// <summary>
        /// libelle du suivi de la commande
        /// </summary>
        public string LibelleSuivi { get; set; }
        /// <summary>
        /// Initialisation d'un nouvel objet ComandeDocument
        /// </summary>
        /// <param name="id"></param>
        /// <param name="dateCommande"></param>
        /// <param name="montant"></param>
        /// <param name="nbExemplaire"></param>
        /// <param name="idLivreDvd"></param>
        /// <param name="idSuivi"></param>
        /// <param name="libelleSuivi"></param>
        public CommandeDocument(string id, DateTime dateCommande, double montant, int nbExemplaire, string idLivreDvd, int idSuivi, string libelleSuivi)
            : base(id, dateCommande, montant)
        {
            this.NbExemplaire = nbExemplaire;
            this.IdLivreDvd = idLivreDvd;
            this.IdSuivi = idSuivi;
            this.LibelleSuivi = libelleSuivi;
        }
    }
}
