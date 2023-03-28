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
    /// Classe de test unitaire pour la classe métier Dvd
    /// </summary>
    [TestClass()]
    public class DvdTests
    {
        private const string id = "98";
        private const string titre = "Titre";
        private const string image = "uneimage";
        private const int duree = 122;
        private const string realisateur = "Hugh Wilson";
        private const string synopsis = "Ceci est un synopsis.";
        private const string idGenre = "018";
        private const string genre = "Essai";
        private const string idPublic = "012";
        private const string lePublic = "Jeunesse";
        private const string idRayon = "81";
        private const string rayon = "Santé";
        private static readonly Dvd dvd = new Dvd(id, titre, image, duree, realisateur, synopsis, idGenre, genre, idPublic, lePublic, idRayon, rayon);

        /// <summary>
        /// Teste sur le constructeur de la classe Dvd
        /// </summary>
        [TestMethod()]
        public void DvdTest()
        {
            Assert.AreEqual(id, dvd.Id, "devrait réussir : id valorisé");
            Assert.AreEqual(titre, dvd.Titre, "devrait réussir : titre valorisé");
            Assert.AreEqual(image, dvd.Image, "devrait réussir : image valorisé");
            Assert.AreEqual(duree, dvd.Duree, "devrait réussir : duree valorisée");
            Assert.AreEqual(realisateur, dvd.Realisateur, "devrait réussir : realisateur valorisée");
            Assert.AreEqual(synopsis, dvd.Synopsis, "devrait réussir : synopsis valorisée");
            Assert.AreEqual(idGenre, dvd.IdGenre, "devrait réussir : idGenre valorisé");
            Assert.AreEqual(genre, dvd.Genre, "devrait réussir : genre valorisé");
            Assert.AreEqual(idPublic, dvd.IdPublic, "devrait réussir : idPublic valorisé");
            Assert.AreEqual(lePublic, dvd.Public, "devrait réussir : lePublic valorisé");
            Assert.AreEqual(idRayon, dvd.IdRayon, "devrait réussir : idRayon valorisé");
            Assert.AreEqual(rayon, dvd.Rayon, "devrait réussir : rayon valorisé");
        }
    }
}