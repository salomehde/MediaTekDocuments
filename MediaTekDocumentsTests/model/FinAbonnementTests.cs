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
    /// Classe de test unitaire pour la classe métier FinAbonnement
    /// </summary>
    [TestClass()]
    public class FinAbonnementTests
    {
        private static readonly DateTime dateFinAbonnement = new DateTime(2024, 02, 12);
        private const string idRevue = "10063";
        private const string RevueTitre = "unTitre";
        private static readonly FinAbonnement finabonnement = new FinAbonnement(dateFinAbonnement, idRevue, RevueTitre);

        /// <summary>
        /// Teste sur le constructeur de la classe FinAbonnement
        /// </summary>
        [TestMethod()]
        public void FinAbonnementTest()
        {
            Assert.AreEqual(dateFinAbonnement, finabonnement.DateFinAbonnement, "devrait réussir : dateFinAbonnement valorisé");
            Assert.AreEqual(idRevue, finabonnement.IdRevue, "devrait réussir : idRevue valorisé");
            Assert.AreEqual(RevueTitre, finabonnement.RevueTitre, "devrait réussir : RevueTitre valorisé");
        }
    }
}