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
    /// Classe de test unitaire pour la classe métier Categorie
    /// </summary>
    [TestClass()]
    public class CategorieTests
    {
        private const string id = "58";
        private const string libelle = "Humour";
        private static readonly Categorie categorie = new Categorie(id, libelle);

        /// <summary>
        /// Test sur le constructeur de la classe Categorie
        /// </summary>
        [TestMethod()]
        public void CategorieTest()
        {
            Assert.AreEqual(id, categorie.Id, "devrait réussir : id valorisé");
            Assert.AreEqual(libelle, categorie.Libelle, "devrait réussir : libelle valorisé");
        }

        /// <summary>
        /// Test sur la méthode ToString
        /// </summary>
        [TestMethod()]
        public void ToStringTest()
        {
            Assert.AreEqual(libelle, categorie.Libelle, "devrait réussir : libelle valorisé");
        }
    }
}