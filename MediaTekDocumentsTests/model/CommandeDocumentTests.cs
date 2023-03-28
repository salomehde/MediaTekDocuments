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
    /// Classe de test unitaire pour la classe métier CommandeDocument
    /// </summary>
    [TestClass()]
    public class CommandeDocumentTests
    {
        private const string id = "64";
        private static readonly DateTime dateCommande = new DateTime(2023, 5, 14);
        private const double montant = 47;
        private const int nbExemplaire = 2;
        private const string idLivreDvd = "20047";
        private const int idSuivi = 2;
        private const string libelleSuivi = "en cours";
        private static readonly CommandeDocument commandedocument = new CommandeDocument(id, dateCommande, montant, nbExemplaire, idLivreDvd, idSuivi, libelleSuivi);

        /// <summary>
        /// Test sur le constructeur de la classe CommandeDocument
        /// </summary>
        [TestMethod()]
        public void CommandeDocumentTest()
        {
            Assert.AreEqual(id, commandedocument.Id, "devrait réussir : id valorisé");
            Assert.AreEqual(dateCommande, commandedocument.DateCommande, "devrait réussir : dateComande valorisé");
            Assert.AreEqual(montant, commandedocument.Montant, "devrait réussir : montant valorisé");
            Assert.AreEqual(idLivreDvd, commandedocument.IdLivreDvd, "devrait réussir : idLivreDvd valorisé");
            Assert.AreEqual(nbExemplaire, commandedocument.NbExemplaire, "devrait réussir : NbExemplaire valorisé");
            Assert.AreEqual(idSuivi, commandedocument.IdSuivi, "devrait réussir : idSuivi valorisé");
            Assert.AreEqual(libelleSuivi, commandedocument.LibelleSuivi, "devrait réussir : libelleSuivi valorisé");
        }
    }
}