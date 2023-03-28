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
    /// Classe de test unitaire pour la classe métier Document
    /// </summary>
    [TestClass()]
    public class DocumentTests
    {
        private const string id = "64";
        private const string titre = "untitre";
        private const string image = "uneimage";
        private const string idGenre = "4";
        private const string genre = "Roman";
        private const string idPublic = "5";
        private const string lePublic = "Jeunesse";
        private const string idRayon = "8";
        private const string rayon = "Santé";
        private static readonly Document document = new Document(id, titre, image, idGenre, genre, idPublic, lePublic, idRayon, rayon);

        /// <summary>
        /// Test sur le constructeur de la classe Document
        /// </summary>
        [TestMethod()]
        public void DocumentTest()
        {
            Assert.AreEqual(id, document.Id, "devrait réussir : id valorisé");
            Assert.AreEqual(titre, document.Titre, "devrait réussir : titre valorisé");
            Assert.AreEqual(image, document.Image, "devrait réussir : image valorisé");
            Assert.AreEqual(idGenre, document.IdGenre, "devrait réussir : idGenre valorisé");
            Assert.AreEqual(genre, document.Genre, "devrait réussir : genre valorisé");
            Assert.AreEqual(idPublic, document.IdPublic, "devrait réussir : idPublic valorisé");
            Assert.AreEqual(lePublic, document.Public, "devrait réussir : lePublic valorisé");
            Assert.AreEqual(idRayon, document.IdRayon, "devrait réussir : idRayon valorisé");
            Assert.AreEqual(rayon, document.Rayon, "devrait réussir : rayon valorisé");
        }
    }
}