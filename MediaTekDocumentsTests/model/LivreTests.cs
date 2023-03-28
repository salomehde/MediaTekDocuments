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
    /// Classe de test unitaire pour la classe métier Livre
    /// </summary>
    [TestClass()]
    public class LivreTests
    {
        private const string id = "182";
        private const string titre = "untitre";
        private const string image = "uneimage";
        private const string isbn = "1234567";
        private const string auteur = "Kate Atkinson";
        private const string collection = "unecollection";
        private const string idGenre = "047";
        private const string genre = "Policier";
        private const string idPublic = "075";
        private const string lePublic = "Jeunesse";
        private const string idRayon = "007";
        private const string rayon = "Maison";
        private static readonly Livre livre = new Livre(id, titre, image, isbn, auteur, collection, idGenre, genre, idPublic, lePublic, idRayon, rayon);

        /// <summary>
        /// Test sur le constructeur de la classe Livre
        /// </summary>
        [TestMethod()]
        public void LivreTest()
        {
            Assert.AreEqual(id, livre.Id, "devrait réussir : id valorisé");
            Assert.AreEqual(titre, livre.Titre, "devrait réussir : titre valorisé");
            Assert.AreEqual(image, livre.Image, "devrait réussir : image valorisé");
            Assert.AreEqual(isbn, livre.Isbn, "devrait réussir : isbn valorisé");
            Assert.AreEqual(auteur, livre.Auteur, "devrait réussir : auteur valorisé");
            Assert.AreEqual(collection, livre.Collection, "devrait réussir : collection valorisé");
            Assert.AreEqual(idGenre, livre.IdGenre, "devrait réussir : idGenre valorisé");
            Assert.AreEqual(genre, livre.Genre, "devrait réussir : genre valorisé");
            Assert.AreEqual(idPublic, livre.IdPublic, "devrait réussir : idPublic valorisé");
            Assert.AreEqual(lePublic, livre.Public, "devrait réussir : lePublic valorisé");
            Assert.AreEqual(idRayon, livre.IdRayon, "devrait réussir : idRayon valorisé");
            Assert.AreEqual(rayon, livre.Rayon, "devrait réussir : rayon valorisé");
        }
    }
}