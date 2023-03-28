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
    /// Classe de test unitaire pour la classe métier Commande
    /// </summary>
    [TestClass()]
    public class CommandeTests
    {
        private const string id = "64";
        private static readonly DateTime dateCommande = new DateTime(2023, 5, 14);
        private const double montant = 47;
        private static readonly Commande commande = new Commande(id, dateCommande, montant);

        /// <summary>
        /// Test sur le constructeur de la classe Commande
        /// </summary>
        [TestMethod()]
        public void CommandeTest()
        {
            Assert.AreEqual(id, commande.Id, "devrait réussir : id valorisé");
            Assert.AreEqual(dateCommande, commande.DateCommande, "devrait réussir : dateComande valorisé");
            Assert.AreEqual(montant, commande.Montant, "devrait réussir : montant valorisé");
        }
    }
}