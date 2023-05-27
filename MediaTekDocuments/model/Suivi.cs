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
        /// <summary>
        /// id de suivi
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// libelle de suivi
        /// </summary>
        public string Libelle { get; set; }
        /// <summary>
        /// Initialisation d'un nouvel objet Suivi
        /// </summary>
        /// <param name="id"></param>
        /// <param name="libelle"></param>
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
