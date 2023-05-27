using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaTekDocuments.model
{
    /// <summary>
    /// Classe métier Service
    /// </summary>
    public class Service
    {
        /// <summary>
        /// id du service
        /// </summary>
        public int IdService { get; }
        /// <summary>
        /// libelle du service
        /// </summary>
        public string Libelle { get; }
        /// <summary>
        /// Initialisation d'un nouvel objet Service
        /// </summary>
        /// <param name="idService"></param>
        /// <param name="libelle"></param>
        public Service(int idService, string libelle)
        {
            this.IdService = idService;
            this.Libelle = libelle;
        }
    }
}
