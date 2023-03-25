using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaTekDocuments.model
{
    /// <summary>
    /// Classe métier Suivi
    /// </summary>
    public class Suivi
    {
        public int Id { get; set; }
        public string Libelle { get; set; }
        public Suivi(int id, string libelle)
        {
            this.Id = id;
            this.Libelle = libelle;
        }

        /// <summary>
        /// Récupération du libellé pour les ComboBox
        /// </summary>
        /// <returns>Libelle</returns>
        public override string ToString()
        {
            return this.Libelle;
        }
    }
}
