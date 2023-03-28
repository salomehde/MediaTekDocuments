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
    /// Classe de test unitaire pour la classe métier Exemplaire
    /// </summary>
    [TestClass()]
    public class ExemplaireTests
    {
        private const int numero = 32;
        private static readonly DateTime dateAchat = new DateTime(2023, 11, 15);
        private const string photo = "unephoto";
        private const string idEtat = "0002";
        private const string idDocument = "00065";
        private static readonly Exemplaire exemplaire = new Exemplaire(numero, dateAchat, photo, idEtat, idDocument);

        /// <summary>
        /// Test sur le constructeur de la classe Exemplaire
        /// </summary>
        [TestMethod()]
        public void ExemplaireTest()
        {
            Assert.AreEqual(numero, exemplaire.Numero, "devrait réussir : numero valorisé");
            Assert.AreEqual(dateAchat, exemplaire.DateAchat, "devrait réussir : dateAchat valorisé");
            Assert.AreEqual(photo, exemplaire.Photo, "devrait réussir : photo valorisé");
            Assert.AreEqual(idEtat, exemplaire.IdEtat, "devrait réussir : idEtat valorisé");
            Assert.AreEqual(idDocument, exemplaire.Id, "devrait réussir : idDocument valorisé");
        }
    }
}