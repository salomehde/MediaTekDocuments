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
    /// Classe de test unitaire pour la classe métier Suivi
    /// </summary>
    [TestClass()]
    public class SuiviTests
    {
        private const int id = 4;
        private const string libelle = "livrée";
        private static readonly Suivi suivi = new Suivi(id, libelle);

        /// <summary>
        /// Test sur le constructeur de la classe Suivi
        /// </summary>
        [TestMethod()]
        public void SuiviTest()
        {
            Assert.AreEqual(id, suivi.Id, "devrait réussir : id valorisé");
            Assert.AreEqual(libelle, suivi.Libelle, "devrait réussir : libelle valorisé");
        }

        /// <summary>
        /// Test sur la méthode ToString
        /// </summary>
        [TestMethod()]
        public void ToStringTest()
        {
            Assert.AreEqual(libelle, suivi.Libelle, "devrait réussir : libelle valorisé");
        }
    }
}