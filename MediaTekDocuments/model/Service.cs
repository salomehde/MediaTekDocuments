using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaTekDocuments.model
{
    public class Service
    {
        public int IdService { get; }
        public string Libelle { get; }

        public Service(int idService, string libelle)
        {
            this.IdService = idService;
            this.Libelle = libelle;
        }
    }
}
