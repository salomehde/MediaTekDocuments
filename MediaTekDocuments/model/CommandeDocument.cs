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
        public int NbExemplaire { get; set; }
        public string IdLivreDvd { get; set; }
        public int IdSuivi { get; set; }
        public string LibelleSuivi { get; set; }
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
