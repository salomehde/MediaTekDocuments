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
    /// Classe de test unitaire pour la classe métier Public
    /// </summary>
    [TestClass()]
    public class PublicTests
    {
        private const string id = "0460";
        private const string libelle = "Ados";
        private static readonly Public lePublic = new Public(id, libelle);

        /// <summary>
        /// Teste sur le constructeur de la classe Public
        /// </summary>
        [TestMethod()]
        public void PublicTest()
        {
            Assert.AreEqual(id, lePublic.Id, "devrait réussir : id valorisé");
            Assert.AreEqual(libelle, lePublic.Libelle, "devrait réussir : libelle valorisé");
        }
    }
}