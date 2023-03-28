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
    /// Classe de test unitaire pour la classe métier Abonnement
    /// </summary>
    [TestClass()]
    public class AbonnementTests
    {
        private const string id = "32";
        private static readonly DateTime dateCommande = new DateTime(2023, 1, 16);
        private const double montant = 122;
        private static readonly DateTime dateFinAbonnement = new DateTime(2023, 12, 24);
        private const string idRevue = "10056";
        private static readonly Abonnement abonnement = new Abonnement(id, dateCommande, montant, dateFinAbonnement, idRevue);

        [TestMethod()]
        /// <summary>
        /// Teste sur le constructeur de la classe Abonnement
        /// </summary>
        public void AbonnementTest()
        {
            Assert.AreEqual(id, abonnement.Id, "devrait réussir : id valorisé");
            Assert.AreEqual(dateCommande, abonnement.DateCommande, "devrait réussir : dateComande valorisé");
            Assert.AreEqual(montant, abonnement.Montant, "devrait réussir : montant valorisé");
            Assert.AreEqual(dateFinAbonnement, abonnement.DateFinAbonnement, "devrait réussir : dateFinAbonnement valorisé");
            Assert.AreEqual(idRevue, abonnement.IdRevue, "devrait réussir : idRevue valorisé");
        }
    }
}