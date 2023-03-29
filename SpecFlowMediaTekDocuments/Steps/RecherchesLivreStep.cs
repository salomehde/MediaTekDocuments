using MediaTekDocuments.view;
using MediaTekDocuments.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TechTalk.SpecFlow;
using NUnit.Framework;
using System.Windows.Forms;

namespace SpecFlowMediaTekDocuments.Steps
{
    /// <summary>
    /// Classe de tests fonctionnels SpecFlow
    /// </summary>
    [Binding]
    public class RecherchesLivreStep
    {
        private readonly FrmMediatek frmMediatek = new FrmMediatek();

        /// <summary>
        /// Saisie du titre du livre dans le textbox
        /// </summary>
        [Given(@"je saisis le titre du livre 'Catastrophes au Brésil' dans txbLivresTitreRecherche")]
        public void GivenJeSaisisLeTitreDuLivreDansTxbLivresTitreRecherche()
        {
            TextBox txbLivresTitreRecherche = (TextBox)frmMediatek.Controls["tabOngletsApplication"].Controls["tabLivres"].Controls["grpLivresRecherche"].Controls["txbLivresTitreRecherche"];
            txbLivresTitreRecherche.Text = "Catastrophes au Brésil";
        }

        /// <summary>
        /// Saisie du numéro de document du livre dans le textbox
        /// </summary>
        [Given(@"je saisis le numéro de document '00017' dans txbLivresNumRecherche")]
        public void GivenJeSaisisLaValeurDansTxbLivresNumRecherche()
        {
            TextBox txbLivresNumRecherche = (TextBox)frmMediatek.Controls["tabOngletsApplication"].Controls["tabLivres"].Controls["grpLivresRecherche"].Controls["txbLivresNumRecherche"];
            frmMediatek.Visible = true;
            txbLivresNumRecherche.Text = "00017";
        }

        /// <summary>
        /// Clic sur le bouton Rechercher
        /// </summary>
        [When(@"je clique sur le bouton 'Rechercher' btnLivresNumRecherche")]
        public void WhenJeCliqueSurBtnLivresNumRecherche()
        {
            Button BtnRecherche = (Button)frmMediatek.Controls["tabOngletsApplication"].Controls["tabLivres"].Controls["grpLivresRecherche"].Controls["btnLivresNumRecherche"];
            frmMediatek.Visible = true;
            BtnRecherche.PerformClick();
        }

        /// <summary>
        /// Compare les lignes attendues avec les lignes obtenues
        /// </summary>
        [Then(@"les informations du livre sont affichées, avec le titre 'Catastrophes au Brésil' et l'auteur 'Philippe Masson'")]
        public void ThenLesInformationsDuLivreDoiventEtreAffichees()
        {
            TextBox TxtTitre = (TextBox)frmMediatek.Controls["tabOngletsApplication"].Controls["tabLivres"].Controls["grpLivresInfos"].Controls["txbLivresTitre"];
            TextBox TxtAuteur = (TextBox)frmMediatek.Controls["tabOngletsApplication"].Controls["tabLivres"].Controls["grpLivresInfos"].Controls["txbLivresAuteur"];

            string titreObtenu = TxtTitre.Text;
            string auteurObtenu = TxtAuteur.Text;

            Assert.AreEqual("Catastrophes au Brésil", titreObtenu);
            Assert.AreEqual("Philippe Masson", auteurObtenu);
        }
    }
}
