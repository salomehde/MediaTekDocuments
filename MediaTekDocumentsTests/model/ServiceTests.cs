using Microsoft.VisualStudio.TestTools.UnitTesting;
using MediaTekDocuments.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaTekDocuments.model.Tests
{
    /// <summary>
    /// Classe de test unitaire pour la classe métier Service
    /// </summary>
    [TestClass()]
    public class ServiceTests
    {
        private const int idService = 2;
        private const string libelle = "Prets";
        private static readonly Service service = new Service(idService, libelle);

        /// <summary>
        /// Test sur le constructeur de la classe Service
        /// </summary>
        [TestMethod()]
        public void ServiceTest()
        {
            Assert.AreEqual(idService, service.IdService, "devrait réussir : idService valorisé");
            Assert.AreEqual(libelle, service.Libelle, "devrait réussir : libelle valorisé");
        }
    }
}