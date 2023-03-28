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
    /// Classe de test unitaire pour la classe métier Rayon
    /// </summary>
    [TestClass()]
    public class RayonTests
    {
        private const string id = "00055";
        private const string libelle = "Voyages";
        private static readonly Rayon rayon = new Rayon(id, libelle);

        /// <summary>
        /// Test sur le constructeur de la classe Rayon
        /// </summary>
        [TestMethod()]
        public void RayonTest()
        {
            Assert.AreEqual(id, rayon.Id, "devrait réussir : id valorisé");
            Assert.AreEqual(libelle, rayon.Libelle, "devrait réussir : libelle valorisé");
        }
    }
}