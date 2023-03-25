using Microsoft.VisualStudio.TestTools.UnitTesting;
using MediaTekDocuments.controller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaTekDocuments.controller.Tests
{
    [TestClass()]
    public class FrmMediatekControllerTests
    {
        private readonly DateTime dateCommande = new DateTime(2022, 12, 6);
        private readonly DateTime dateFinAbonnement = new DateTime(2023, 2, 14);
        private readonly DateTime dateParution = new DateTime(2023, 1, 22);
        private readonly DateTime dateParutionFaux = new DateTime(2021, 7, 26);

        private readonly FrmMediatekController controller = new FrmMediatekController();

        [TestMethod()]
        public void ParutionDansAbonnementTest()
        {
            bool resultat = controller.ParutionDansAbonnement(dateCommande, dateFinAbonnement, dateParution);
            Assert.AreEqual(true, resultat);

            bool resultatFaux = controller.ParutionDansAbonnement(dateCommande, dateFinAbonnement, dateParutionFaux);
            Assert.AreEqual(false, resultatFaux);
        }
    }
}