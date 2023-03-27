using System;
using System.Windows.Forms;
using MediaTekDocuments.model;
using MediaTekDocuments.controller;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.IO;

namespace MediaTekDocuments.view

{
    /// <summary>
    /// Classe d'affichage
    /// </summary>
    public partial class FrmMediatek : Form
    {
        #region Commun
        private readonly FrmMediatekController controller;
        private readonly BindingSource bdgGenres = new BindingSource();
        private readonly BindingSource bdgPublics = new BindingSource();
        private readonly BindingSource bdgRayons = new BindingSource();
        private readonly BindingSource bdgCommandesLivre = new BindingSource();
        private readonly BindingSource bdgCommandesDvd = new BindingSource();
        private readonly BindingSource bdgCommandesRevues = new BindingSource();
        private readonly BindingSource bdgSuivis = new BindingSource();

        private List<CommandeDocument> Lescommandesdocument = new List<CommandeDocument>();
        private List<Abonnement> Lescommandesabonnement = new List<Abonnement>();

        /// <summary>
        /// Constructeur : création du contrôleur lié à ce formulaire
        /// </summary>
        internal FrmMediatek()
        {
            InitializeComponent();
            this.controller = new FrmMediatekController();
        }

        /// <summary>
        /// Ouverture de la fênetre d'alerte de fin d'abonnements
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrmMediatek_Shown(object sender, EventArgs e)
        {
            if (this.Text == "Gestion des documents de la médiathèque: prets")
            {
                AccesPrets();
            }
            else
            {
                AccesAdmin();
            }
        }

        public void AccesPrets()
        {
            tabOngletsApplication.TabPages.Remove(tabCommandesLivres);
            tabOngletsApplication.TabPages.Remove(tabCommandesDVD);
            tabOngletsApplication.TabPages.Remove(tabCommandesRevues);
        }

        public void AccesAdmin()
        {
            FrmAlerteAbonnement FrmAlerteAbonnement = new FrmAlerteAbonnement(controller);
            FrmAlerteAbonnement.StartPosition = FormStartPosition.CenterParent;
            FrmAlerteAbonnement.ShowDialog();
        }

        /// <summary>
        /// Rempli un des 3 combo (genre, public, rayon)
        /// </summary>
        /// <param name="lesCategories">liste des objets de type Genre ou Public ou Rayon</param>
        /// <param name="bdg">bindingsource contenant les informations</param>
        /// <param name="cbx">combobox à remplir</param>
        public void RemplirComboCategorie(List<Categorie> lesCategories, BindingSource bdg, ComboBox cbx)
        {
            bdg.DataSource = lesCategories;
            cbx.DataSource = bdg;
            if (cbx.Items.Count > 0)
            {
                cbx.SelectedIndex = -1;
            }
        }

        /// <summary>
        /// Rempli le combo suivi
        /// </summary>
        /// <param name="lesSuivis">liste des objets de type Suivi</param>
        /// <param name="bdg">bindingsource contenant les informations</param>
        /// <param name="cbx">combobox à remplir</param>
        public void RemplirComboSuivi(List<Suivi> lesSuivis, BindingSource bdg, ComboBox cbx)
        {
            bdg.DataSource = lesSuivis;
            cbx.DataSource = bdg;
            if (cbx.Items.Count > 0)
            {
                cbx.SelectedIndex = -1;
            }
        }

        #endregion

        #region Onglet Livres
        private readonly BindingSource bdgLivresListe = new BindingSource();
        private List<Livre> lesLivres = new List<Livre>();

        /// <summary>
        /// Ouverture de l'onglet Livres : 
        /// appel des méthodes pour remplir le datagrid des livres et des combos (genre, rayon, public)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TabLivres_Enter(object sender, EventArgs e)
        {
            lesLivres = controller.GetAllLivres();
            RemplirComboCategorie(controller.GetAllGenres(), bdgGenres, cbxLivresGenres);
            RemplirComboCategorie(controller.GetAllPublics(), bdgPublics, cbxLivresPublics);
            RemplirComboCategorie(controller.GetAllRayons(), bdgRayons, cbxLivresRayons);
            RemplirLivresListeComplete();
        }

        /// <summary>
        /// Remplit le dategrid avec la liste reçue en paramètre
        /// </summary>
        /// <param name="livres">liste de livres</param>
        private void RemplirLivresListe(List<Livre> livres)
        {
            bdgLivresListe.DataSource = livres;
            dgvLivresListe.DataSource = bdgLivresListe;
            dgvLivresListe.Columns["isbn"].Visible = false;
            dgvLivresListe.Columns["idRayon"].Visible = false;
            dgvLivresListe.Columns["idGenre"].Visible = false;
            dgvLivresListe.Columns["idPublic"].Visible = false;
            dgvLivresListe.Columns["image"].Visible = false;
            dgvLivresListe.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dgvLivresListe.Columns["id"].DisplayIndex = 0;
            dgvLivresListe.Columns["titre"].DisplayIndex = 1;
        }

        /// <summary>
        /// Recherche et affichage du livre dont on a saisi le numéro.
        /// Si non trouvé, affichage d'un MessageBox.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnLivresNumRecherche_Click(object sender, EventArgs e)
        {
            if (!txbLivresNumRecherche.Text.Equals(""))
            {
                txbLivresTitreRecherche.Text = "";
                cbxLivresGenres.SelectedIndex = -1;
                cbxLivresRayons.SelectedIndex = -1;
                cbxLivresPublics.SelectedIndex = -1;
                Livre livre = lesLivres.Find(x => x.Id.Equals(txbLivresNumRecherche.Text));
                if (livre != null)
                {
                    List<Livre> livres = new List<Livre>() { livre };
                    RemplirLivresListe(livres);
                }
                else
                {
                    MessageBox.Show("numéro introuvable");
                    RemplirLivresListeComplete();
                }
            }
            else
            {
                RemplirLivresListeComplete();
            }
        }

        /// <summary>
        /// Recherche et affichage des livres dont le titre matche acec la saisie.
        /// Cette procédure est exécutée à chaque ajout ou suppression de caractère
        /// dans le textBox de saisie.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TxbLivresTitreRecherche_TextChanged(object sender, EventArgs e)
        {
            if (!txbLivresTitreRecherche.Text.Equals(""))
            {
                cbxLivresGenres.SelectedIndex = -1;
                cbxLivresRayons.SelectedIndex = -1;
                cbxLivresPublics.SelectedIndex = -1;
                txbLivresNumRecherche.Text = "";
                List<Livre> lesLivresParTitre;
                lesLivresParTitre = lesLivres.FindAll(x => x.Titre.ToLower().Contains(txbLivresTitreRecherche.Text.ToLower()));
                RemplirLivresListe(lesLivresParTitre);
            }
            else
            {
                // si la zone de saisie est vide et aucun élément combo sélectionné, réaffichage de la liste complète
                if (cbxLivresGenres.SelectedIndex < 0 && cbxLivresPublics.SelectedIndex < 0 && cbxLivresRayons.SelectedIndex < 0
                    && txbLivresNumRecherche.Text.Equals(""))
                {
                    RemplirLivresListeComplete();
                }
            }
        }

        /// <summary>
        /// Affichage des informations du livre sélectionné
        /// </summary>
        /// <param name="livre">le livre</param>
        private void AfficheLivresInfos(Livre livre)
        {
            txbLivresAuteur.Text = livre.Auteur;
            txbLivresCollection.Text = livre.Collection;
            txbLivresImage.Text = livre.Image;
            txbLivresIsbn.Text = livre.Isbn;
            txbLivresNumero.Text = livre.Id;
            txbLivresGenre.Text = livre.Genre;
            txbLivresPublic.Text = livre.Public;
            txbLivresRayon.Text = livre.Rayon;
            txbLivresTitre.Text = livre.Titre;
            string image = livre.Image;
            try
            {
                pcbLivresImage.Image = Image.FromFile(image);
            }
            catch
            {
                pcbLivresImage.Image = null;
            }
        }

        /// <summary>
        /// Vide les zones d'affichage des informations du livre
        /// </summary>
        private void VideLivresInfos()
        {
            txbLivresAuteur.Text = "";
            txbLivresCollection.Text = "";
            txbLivresImage.Text = "";
            txbLivresIsbn.Text = "";
            txbLivresNumero.Text = "";
            txbLivresGenre.Text = "";
            txbLivresPublic.Text = "";
            txbLivresRayon.Text = "";
            txbLivresTitre.Text = "";
            pcbLivresImage.Image = null;
        }

        /// <summary>
        /// Filtre sur le genre
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CbxLivresGenres_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbxLivresGenres.SelectedIndex >= 0)
            {
                txbLivresTitreRecherche.Text = "";
                txbLivresNumRecherche.Text = "";
                Genre genre = (Genre)cbxLivresGenres.SelectedItem;
                List<Livre> livres = lesLivres.FindAll(x => x.Genre.Equals(genre.Libelle));
                RemplirLivresListe(livres);
                cbxLivresRayons.SelectedIndex = -1;
                cbxLivresPublics.SelectedIndex = -1;
            }
        }

        /// <summary>
        /// Filtre sur la catégorie de public
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CbxLivresPublics_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbxLivresPublics.SelectedIndex >= 0)
            {
                txbLivresTitreRecherche.Text = "";
                txbLivresNumRecherche.Text = "";
                Public lePublic = (Public)cbxLivresPublics.SelectedItem;
                List<Livre> livres = lesLivres.FindAll(x => x.Public.Equals(lePublic.Libelle));
                RemplirLivresListe(livres);
                cbxLivresRayons.SelectedIndex = -1;
                cbxLivresGenres.SelectedIndex = -1;
            }
        }

        /// <summary>
        /// Filtre sur le rayon
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CbxLivresRayons_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbxLivresRayons.SelectedIndex >= 0)
            {
                txbLivresTitreRecherche.Text = "";
                txbLivresNumRecherche.Text = "";
                Rayon rayon = (Rayon)cbxLivresRayons.SelectedItem;
                List<Livre> livres = lesLivres.FindAll(x => x.Rayon.Equals(rayon.Libelle));
                RemplirLivresListe(livres);
                cbxLivresGenres.SelectedIndex = -1;
                cbxLivresPublics.SelectedIndex = -1;
            }
        }

        /// <summary>
        /// Sur la sélection d'une ligne ou cellule dans le grid
        /// affichage des informations du livre
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DgvLivresListe_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvLivresListe.CurrentCell != null)
            {
                try
                {
                    Livre livre = (Livre)bdgLivresListe.List[bdgLivresListe.Position];
                    AfficheLivresInfos(livre);
                }
                catch
                {
                    VideLivresZones();
                }
            }
            else
            {
                VideLivresInfos();
            }
        }

        /// <summary>
        /// Sur le clic du bouton d'annulation, affichage de la liste complète des livres
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnLivresAnnulPublics_Click(object sender, EventArgs e)
        {
            RemplirLivresListeComplete();
        }

        /// <summary>
        /// Sur le clic du bouton d'annulation, affichage de la liste complète des livres
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnLivresAnnulRayons_Click(object sender, EventArgs e)
        {
            RemplirLivresListeComplete();
        }

        /// <summary>
        /// Sur le clic du bouton d'annulation, affichage de la liste complète des livres
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnLivresAnnulGenres_Click(object sender, EventArgs e)
        {
            RemplirLivresListeComplete();
        }

        /// <summary>
        /// Affichage de la liste complète des livres
        /// et annulation de toutes les recherches et filtres
        /// </summary>
        private void RemplirLivresListeComplete()
        {
            RemplirLivresListe(lesLivres);
            VideLivresZones();
        }

        /// <summary>
        /// vide les zones de recherche et de filtre
        /// </summary>
        private void VideLivresZones()
        {
            cbxLivresGenres.SelectedIndex = -1;
            cbxLivresRayons.SelectedIndex = -1;
            cbxLivresPublics.SelectedIndex = -1;
            txbLivresNumRecherche.Text = "";
            txbLivresTitreRecherche.Text = "";
        }

        /// <summary>
        /// Tri sur les colonnes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DgvLivresListe_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            VideLivresZones();
            string titreColonne = dgvLivresListe.Columns[e.ColumnIndex].HeaderText;
            List<Livre> sortedList = new List<Livre>();
            switch (titreColonne)
            {
                case "Id":
                    sortedList = lesLivres.OrderBy(o => o.Id).ToList();
                    break;
                case "Titre":
                    sortedList = lesLivres.OrderBy(o => o.Titre).ToList();
                    break;
                case "Collection":
                    sortedList = lesLivres.OrderBy(o => o.Collection).ToList();
                    break;
                case "Auteur":
                    sortedList = lesLivres.OrderBy(o => o.Auteur).ToList();
                    break;
                case "Genre":
                    sortedList = lesLivres.OrderBy(o => o.Genre).ToList();
                    break;
                case "Public":
                    sortedList = lesLivres.OrderBy(o => o.Public).ToList();
                    break;
                case "Rayon":
                    sortedList = lesLivres.OrderBy(o => o.Rayon).ToList();
                    break;
            }
            RemplirLivresListe(sortedList);
        }
        #endregion

        #region Onglet Dvd
        private readonly BindingSource bdgDvdListe = new BindingSource();
        private List<Dvd> lesDvd = new List<Dvd>();

        /// <summary>
        /// Ouverture de l'onglet Dvds : 
        /// appel des méthodes pour remplir le datagrid des dvd et des combos (genre, rayon, public)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tabDvd_Enter(object sender, EventArgs e)
        {
            lesDvd = controller.GetAllDvd();
            RemplirComboCategorie(controller.GetAllGenres(), bdgGenres, cbxDvdGenres);
            RemplirComboCategorie(controller.GetAllPublics(), bdgPublics, cbxDvdPublics);
            RemplirComboCategorie(controller.GetAllRayons(), bdgRayons, cbxDvdRayons);
            RemplirDvdListeComplete();
        }

        /// <summary>
        /// Remplit le dategrid avec la liste reçue en paramètre
        /// </summary>
        /// <param name="Dvds">liste de dvd</param>
        private void RemplirDvdListe(List<Dvd> Dvds)
        {
            bdgDvdListe.DataSource = Dvds;
            dgvDvdListe.DataSource = bdgDvdListe;
            dgvDvdListe.Columns["idRayon"].Visible = false;
            dgvDvdListe.Columns["idGenre"].Visible = false;
            dgvDvdListe.Columns["idPublic"].Visible = false;
            dgvDvdListe.Columns["image"].Visible = false;
            dgvDvdListe.Columns["synopsis"].Visible = false;
            dgvDvdListe.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dgvDvdListe.Columns["id"].DisplayIndex = 0;
            dgvDvdListe.Columns["titre"].DisplayIndex = 1;
        }

        /// <summary>
        /// Recherche et affichage du Dvd dont on a saisi le numéro.
        /// Si non trouvé, affichage d'un MessageBox.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDvdNumRecherche_Click(object sender, EventArgs e)
        {
            if (!txbDvdNumRecherche.Text.Equals(""))
            {
                txbDvdTitreRecherche.Text = "";
                cbxDvdGenres.SelectedIndex = -1;
                cbxDvdRayons.SelectedIndex = -1;
                cbxDvdPublics.SelectedIndex = -1;
                Dvd dvd = lesDvd.Find(x => x.Id.Equals(txbDvdNumRecherche.Text));
                if (dvd != null)
                {
                    List<Dvd> Dvd = new List<Dvd>() { dvd };
                    RemplirDvdListe(Dvd);
                }
                else
                {
                    MessageBox.Show("numéro introuvable");
                    RemplirDvdListeComplete();
                }
            }
            else
            {
                RemplirDvdListeComplete();
            }
        }

        /// <summary>
        /// Recherche et affichage des Dvd dont le titre matche acec la saisie.
        /// Cette procédure est exécutée à chaque ajout ou suppression de caractère
        /// dans le textBox de saisie.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txbDvdTitreRecherche_TextChanged(object sender, EventArgs e)
        {
            if (!txbDvdTitreRecherche.Text.Equals(""))
            {
                cbxDvdGenres.SelectedIndex = -1;
                cbxDvdRayons.SelectedIndex = -1;
                cbxDvdPublics.SelectedIndex = -1;
                txbDvdNumRecherche.Text = "";
                List<Dvd> lesDvdParTitre;
                lesDvdParTitre = lesDvd.FindAll(x => x.Titre.ToLower().Contains(txbDvdTitreRecherche.Text.ToLower()));
                RemplirDvdListe(lesDvdParTitre);
            }
            else
            {
                // si la zone de saisie est vide et aucun élément combo sélectionné, réaffichage de la liste complète
                if (cbxDvdGenres.SelectedIndex < 0 && cbxDvdPublics.SelectedIndex < 0 && cbxDvdRayons.SelectedIndex < 0
                    && txbDvdNumRecherche.Text.Equals(""))
                {
                    RemplirDvdListeComplete();
                }
            }
        }

        /// <summary>
        /// Affichage des informations du dvd sélectionné
        /// </summary>
        /// <param name="dvd">le dvd</param>
        private void AfficheDvdInfos(Dvd dvd)
        {
            txbDvdRealisateur.Text = dvd.Realisateur;
            txbDvdSynopsis.Text = dvd.Synopsis;
            txbDvdImage.Text = dvd.Image;
            txbDvdDuree.Text = dvd.Duree.ToString();
            txbDvdNumero.Text = dvd.Id;
            txbDvdGenre.Text = dvd.Genre;
            txbDvdPublic.Text = dvd.Public;
            txbDvdRayon.Text = dvd.Rayon;
            txbDvdTitre.Text = dvd.Titre;
            string image = dvd.Image;
            try
            {
                pcbDvdImage.Image = Image.FromFile(image);
            }
            catch
            {
                pcbDvdImage.Image = null;
            }
        }

        /// <summary>
        /// Vide les zones d'affichage des informations du dvd
        /// </summary>
        private void VideDvdInfos()
        {
            txbDvdRealisateur.Text = "";
            txbDvdSynopsis.Text = "";
            txbDvdImage.Text = "";
            txbDvdDuree.Text = "";
            txbDvdNumero.Text = "";
            txbDvdGenre.Text = "";
            txbDvdPublic.Text = "";
            txbDvdRayon.Text = "";
            txbDvdTitre.Text = "";
            pcbDvdImage.Image = null;
        }

        /// <summary>
        /// Filtre sur le genre
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbxDvdGenres_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbxDvdGenres.SelectedIndex >= 0)
            {
                txbDvdTitreRecherche.Text = "";
                txbDvdNumRecherche.Text = "";
                Genre genre = (Genre)cbxDvdGenres.SelectedItem;
                List<Dvd> Dvd = lesDvd.FindAll(x => x.Genre.Equals(genre.Libelle));
                RemplirDvdListe(Dvd);
                cbxDvdRayons.SelectedIndex = -1;
                cbxDvdPublics.SelectedIndex = -1;
            }
        }

        /// <summary>
        /// Filtre sur la catégorie de public
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbxDvdPublics_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbxDvdPublics.SelectedIndex >= 0)
            {
                txbDvdTitreRecherche.Text = "";
                txbDvdNumRecherche.Text = "";
                Public lePublic = (Public)cbxDvdPublics.SelectedItem;
                List<Dvd> Dvd = lesDvd.FindAll(x => x.Public.Equals(lePublic.Libelle));
                RemplirDvdListe(Dvd);
                cbxDvdRayons.SelectedIndex = -1;
                cbxDvdGenres.SelectedIndex = -1;
            }
        }

        /// <summary>
        /// Filtre sur le rayon
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbxDvdRayons_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbxDvdRayons.SelectedIndex >= 0)
            {
                txbDvdTitreRecherche.Text = "";
                txbDvdNumRecherche.Text = "";
                Rayon rayon = (Rayon)cbxDvdRayons.SelectedItem;
                List<Dvd> Dvd = lesDvd.FindAll(x => x.Rayon.Equals(rayon.Libelle));
                RemplirDvdListe(Dvd);
                cbxDvdGenres.SelectedIndex = -1;
                cbxDvdPublics.SelectedIndex = -1;
            }
        }

        /// <summary>
        /// Sur la sélection d'une ligne ou cellule dans le grid
        /// affichage des informations du dvd
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvDvdListe_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvDvdListe.CurrentCell != null)
            {
                try
                {
                    Dvd dvd = (Dvd)bdgDvdListe.List[bdgDvdListe.Position];
                    AfficheDvdInfos(dvd);
                }
                catch
                {
                    VideDvdZones();
                }
            }
            else
            {
                VideDvdInfos();
            }
        }

        /// <summary>
        /// Sur le clic du bouton d'annulation, affichage de la liste complète des Dvd
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDvdAnnulPublics_Click(object sender, EventArgs e)
        {
            RemplirDvdListeComplete();
        }

        /// <summary>
        /// Sur le clic du bouton d'annulation, affichage de la liste complète des Dvd
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDvdAnnulRayons_Click(object sender, EventArgs e)
        {
            RemplirDvdListeComplete();
        }

        /// <summary>
        /// Sur le clic du bouton d'annulation, affichage de la liste complète des Dvd
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDvdAnnulGenres_Click(object sender, EventArgs e)
        {
            RemplirDvdListeComplete();
        }

        /// <summary>
        /// Affichage de la liste complète des Dvd
        /// et annulation de toutes les recherches et filtres
        /// </summary>
        private void RemplirDvdListeComplete()
        {
            RemplirDvdListe(lesDvd);
            VideDvdZones();
        }

        /// <summary>
        /// vide les zones de recherche et de filtre
        /// </summary>
        private void VideDvdZones()
        {
            cbxDvdGenres.SelectedIndex = -1;
            cbxDvdRayons.SelectedIndex = -1;
            cbxDvdPublics.SelectedIndex = -1;
            txbDvdNumRecherche.Text = "";
            txbDvdTitreRecherche.Text = "";
        }

        /// <summary>
        /// Tri sur les colonnes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvDvdListe_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            VideDvdZones();
            string titreColonne = dgvDvdListe.Columns[e.ColumnIndex].HeaderText;
            List<Dvd> sortedList = new List<Dvd>();
            switch (titreColonne)
            {
                case "Id":
                    sortedList = lesDvd.OrderBy(o => o.Id).ToList();
                    break;
                case "Titre":
                    sortedList = lesDvd.OrderBy(o => o.Titre).ToList();
                    break;
                case "Duree":
                    sortedList = lesDvd.OrderBy(o => o.Duree).ToList();
                    break;
                case "Realisateur":
                    sortedList = lesDvd.OrderBy(o => o.Realisateur).ToList();
                    break;
                case "Genre":
                    sortedList = lesDvd.OrderBy(o => o.Genre).ToList();
                    break;
                case "Public":
                    sortedList = lesDvd.OrderBy(o => o.Public).ToList();
                    break;
                case "Rayon":
                    sortedList = lesDvd.OrderBy(o => o.Rayon).ToList();
                    break;
            }
            RemplirDvdListe(sortedList);
        }
        #endregion

        #region Onglet Revues
        private readonly BindingSource bdgRevuesListe = new BindingSource();
        private List<Revue> lesRevues = new List<Revue>();

        /// <summary>
        /// Ouverture de l'onglet Revues : 
        /// appel des méthodes pour remplir le datagrid des revues et des combos (genre, rayon, public)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tabRevues_Enter(object sender, EventArgs e)
        {
            lesRevues = controller.GetAllRevues();
            RemplirComboCategorie(controller.GetAllGenres(), bdgGenres, cbxRevuesGenres);
            RemplirComboCategorie(controller.GetAllPublics(), bdgPublics, cbxRevuesPublics);
            RemplirComboCategorie(controller.GetAllRayons(), bdgRayons, cbxRevuesRayons);
            RemplirRevuesListeComplete();
        }

        /// <summary>
        /// Remplit le dategrid avec la liste reçue en paramètre
        /// </summary>
        /// <param name="revues"></param>
        private void RemplirRevuesListe(List<Revue> revues)
        {
            bdgRevuesListe.DataSource = revues;
            dgvRevuesListe.DataSource = bdgRevuesListe;
            dgvRevuesListe.Columns["idRayon"].Visible = false;
            dgvRevuesListe.Columns["idGenre"].Visible = false;
            dgvRevuesListe.Columns["idPublic"].Visible = false;
            dgvRevuesListe.Columns["image"].Visible = false;
            dgvRevuesListe.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dgvRevuesListe.Columns["id"].DisplayIndex = 0;
            dgvRevuesListe.Columns["titre"].DisplayIndex = 1;
        }

        /// <summary>
        /// Recherche et affichage de la revue dont on a saisi le numéro.
        /// Si non trouvé, affichage d'un MessageBox.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRevuesNumRecherche_Click(object sender, EventArgs e)
        {
            if (!txbRevuesNumRecherche.Text.Equals(""))
            {
                txbRevuesTitreRecherche.Text = "";
                cbxRevuesGenres.SelectedIndex = -1;
                cbxRevuesRayons.SelectedIndex = -1;
                cbxRevuesPublics.SelectedIndex = -1;
                Revue revue = lesRevues.Find(x => x.Id.Equals(txbRevuesNumRecherche.Text));
                if (revue != null)
                {
                    List<Revue> revues = new List<Revue>() { revue };
                    RemplirRevuesListe(revues);
                }
                else
                {
                    MessageBox.Show("numéro introuvable");
                    RemplirRevuesListeComplete();
                }
            }
            else
            {
                RemplirRevuesListeComplete();
            }
        }

        /// <summary>
        /// Recherche et affichage des revues dont le titre matche acec la saisie.
        /// Cette procédure est exécutée à chaque ajout ou suppression de caractère
        /// dans le textBox de saisie.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txbRevuesTitreRecherche_TextChanged(object sender, EventArgs e)
        {
            if (!txbRevuesTitreRecherche.Text.Equals(""))
            {
                cbxRevuesGenres.SelectedIndex = -1;
                cbxRevuesRayons.SelectedIndex = -1;
                cbxRevuesPublics.SelectedIndex = -1;
                txbRevuesNumRecherche.Text = "";
                List<Revue> lesRevuesParTitre;
                lesRevuesParTitre = lesRevues.FindAll(x => x.Titre.ToLower().Contains(txbRevuesTitreRecherche.Text.ToLower()));
                RemplirRevuesListe(lesRevuesParTitre);
            }
            else
            {
                // si la zone de saisie est vide et aucun élément combo sélectionné, réaffichage de la liste complète
                if (cbxRevuesGenres.SelectedIndex < 0 && cbxRevuesPublics.SelectedIndex < 0 && cbxRevuesRayons.SelectedIndex < 0
                    && txbRevuesNumRecherche.Text.Equals(""))
                {
                    RemplirRevuesListeComplete();
                }
            }
        }

        /// <summary>
        /// Affichage des informations de la revue sélectionné
        /// </summary>
        /// <param name="revue">la revue</param>
        private void AfficheRevuesInfos(Revue revue)
        {
            txbRevuesPeriodicite.Text = revue.Periodicite;
            txbRevuesImage.Text = revue.Image;
            txbRevuesDateMiseADispo.Text = revue.DelaiMiseADispo.ToString();
            txbRevuesNumero.Text = revue.Id;
            txbRevuesGenre.Text = revue.Genre;
            txbRevuesPublic.Text = revue.Public;
            txbRevuesRayon.Text = revue.Rayon;
            txbRevuesTitre.Text = revue.Titre;
            string image = revue.Image;
            try
            {
                pcbRevuesImage.Image = Image.FromFile(image);
            }
            catch
            {
                pcbRevuesImage.Image = null;
            }
        }

        /// <summary>
        /// Vide les zones d'affichage des informations de la reuve
        /// </summary>
        private void VideRevuesInfos()
        {
            txbRevuesPeriodicite.Text = "";
            txbRevuesImage.Text = "";
            txbRevuesDateMiseADispo.Text = "";
            txbRevuesNumero.Text = "";
            txbRevuesGenre.Text = "";
            txbRevuesPublic.Text = "";
            txbRevuesRayon.Text = "";
            txbRevuesTitre.Text = "";
            pcbRevuesImage.Image = null;
        }

        /// <summary>
        /// Filtre sur le genre
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbxRevuesGenres_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbxRevuesGenres.SelectedIndex >= 0)
            {
                txbRevuesTitreRecherche.Text = "";
                txbRevuesNumRecherche.Text = "";
                Genre genre = (Genre)cbxRevuesGenres.SelectedItem;
                List<Revue> revues = lesRevues.FindAll(x => x.Genre.Equals(genre.Libelle));
                RemplirRevuesListe(revues);
                cbxRevuesRayons.SelectedIndex = -1;
                cbxRevuesPublics.SelectedIndex = -1;
            }
        }

        /// <summary>
        /// Filtre sur la catégorie de public
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbxRevuesPublics_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbxRevuesPublics.SelectedIndex >= 0)
            {
                txbRevuesTitreRecherche.Text = "";
                txbRevuesNumRecherche.Text = "";
                Public lePublic = (Public)cbxRevuesPublics.SelectedItem;
                List<Revue> revues = lesRevues.FindAll(x => x.Public.Equals(lePublic.Libelle));
                RemplirRevuesListe(revues);
                cbxRevuesRayons.SelectedIndex = -1;
                cbxRevuesGenres.SelectedIndex = -1;
            }
        }

        /// <summary>
        /// Filtre sur le rayon
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbxRevuesRayons_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbxRevuesRayons.SelectedIndex >= 0)
            {
                txbRevuesTitreRecherche.Text = "";
                txbRevuesNumRecherche.Text = "";
                Rayon rayon = (Rayon)cbxRevuesRayons.SelectedItem;
                List<Revue> revues = lesRevues.FindAll(x => x.Rayon.Equals(rayon.Libelle));
                RemplirRevuesListe(revues);
                cbxRevuesGenres.SelectedIndex = -1;
                cbxRevuesPublics.SelectedIndex = -1;
            }
        }

        /// <summary>
        /// Sur la sélection d'une ligne ou cellule dans le grid
        /// affichage des informations de la revue
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvRevuesListe_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvRevuesListe.CurrentCell != null)
            {
                try
                {
                    Revue revue = (Revue)bdgRevuesListe.List[bdgRevuesListe.Position];
                    AfficheRevuesInfos(revue);
                }
                catch
                {
                    VideRevuesZones();
                }
            }
            else
            {
                VideRevuesInfos();
            }
        }

        /// <summary>
        /// Sur le clic du bouton d'annulation, affichage de la liste complète des revues
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRevuesAnnulPublics_Click(object sender, EventArgs e)
        {
            RemplirRevuesListeComplete();
        }

        /// <summary>
        /// Sur le clic du bouton d'annulation, affichage de la liste complète des revues
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRevuesAnnulRayons_Click(object sender, EventArgs e)
        {
            RemplirRevuesListeComplete();
        }

        /// <summary>
        /// Sur le clic du bouton d'annulation, affichage de la liste complète des revues
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRevuesAnnulGenres_Click(object sender, EventArgs e)
        {
            RemplirRevuesListeComplete();
        }

        /// <summary>
        /// Affichage de la liste complète des revues
        /// et annulation de toutes les recherches et filtres
        /// </summary>
        private void RemplirRevuesListeComplete()
        {
            RemplirRevuesListe(lesRevues);
            VideRevuesZones();
        }

        /// <summary>
        /// vide les zones de recherche et de filtre
        /// </summary>
        private void VideRevuesZones()
        {
            cbxRevuesGenres.SelectedIndex = -1;
            cbxRevuesRayons.SelectedIndex = -1;
            cbxRevuesPublics.SelectedIndex = -1;
            txbRevuesNumRecherche.Text = "";
            txbRevuesTitreRecherche.Text = "";
        }

        /// <summary>
        /// Tri sur les colonnes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvRevuesListe_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            VideRevuesZones();
            string titreColonne = dgvRevuesListe.Columns[e.ColumnIndex].HeaderText;
            List<Revue> sortedList = new List<Revue>();
            switch (titreColonne)
            {
                case "Id":
                    sortedList = lesRevues.OrderBy(o => o.Id).ToList();
                    break;
                case "Titre":
                    sortedList = lesRevues.OrderBy(o => o.Titre).ToList();
                    break;
                case "Periodicite":
                    sortedList = lesRevues.OrderBy(o => o.Periodicite).ToList();
                    break;
                case "DelaiMiseADispo":
                    sortedList = lesRevues.OrderBy(o => o.DelaiMiseADispo).ToList();
                    break;
                case "Genre":
                    sortedList = lesRevues.OrderBy(o => o.Genre).ToList();
                    break;
                case "Public":
                    sortedList = lesRevues.OrderBy(o => o.Public).ToList();
                    break;
                case "Rayon":
                    sortedList = lesRevues.OrderBy(o => o.Rayon).ToList();
                    break;
            }
            RemplirRevuesListe(sortedList);
        }
        #endregion

        #region Onglet Paarutions
        private readonly BindingSource bdgExemplairesListe = new BindingSource();
        private List<Exemplaire> lesExemplaires = new List<Exemplaire>();
        const string ETATNEUF = "00001";

        /// <summary>
        /// Ouverture de l'onglet : récupère le revues et vide tous les champs.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tabReceptionRevue_Enter(object sender, EventArgs e)
        {
            lesRevues = controller.GetAllRevues();
            txbReceptionRevueNumero.Text = "";
        }

        /// <summary>
        /// Remplit le dategrid des exemplaires avec la liste reçue en paramètre
        /// </summary>
        /// <param name="exemplaires">liste d'exemplaires</param>
        private void RemplirReceptionExemplairesListe(List<Exemplaire> exemplaires)
        {
            if (exemplaires != null)
            {
                bdgExemplairesListe.DataSource = exemplaires;
                dgvReceptionExemplairesListe.DataSource = bdgExemplairesListe;
                dgvReceptionExemplairesListe.Columns["idEtat"].Visible = false;
                dgvReceptionExemplairesListe.Columns["id"].Visible = false;
                dgvReceptionExemplairesListe.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                dgvReceptionExemplairesListe.Columns["numero"].DisplayIndex = 0;
                dgvReceptionExemplairesListe.Columns["dateAchat"].DisplayIndex = 1;
            }
            else
            {
                bdgExemplairesListe.DataSource = null;
            }
        }

        /// <summary>
        /// Recherche d'un numéro de revue et affiche ses informations
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnReceptionRechercher_Click(object sender, EventArgs e)
        {
            if (!txbReceptionRevueNumero.Text.Equals(""))
            {
                Revue revue = lesRevues.Find(x => x.Id.Equals(txbReceptionRevueNumero.Text));
                if (revue != null)
                {
                    AfficheReceptionRevueInfos(revue);
                }
                else
                {
                    MessageBox.Show("numéro introuvable");
                }
            }
        }

        /// <summary>
        /// Si le numéro de revue est modifié, la zone de l'exemplaire est vidée et inactive
        /// les informations de la revue son aussi effacées
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txbReceptionRevueNumero_TextChanged(object sender, EventArgs e)
        {
            txbReceptionRevuePeriodicite.Text = "";
            txbReceptionRevueImage.Text = "";
            txbReceptionRevueDelaiMiseADispo.Text = "";
            txbReceptionRevueGenre.Text = "";
            txbReceptionRevuePublic.Text = "";
            txbReceptionRevueRayon.Text = "";
            txbReceptionRevueTitre.Text = "";
            pcbReceptionRevueImage.Image = null;
            RemplirReceptionExemplairesListe(null);
            AccesReceptionExemplaireGroupBox(false);
        }

        /// <summary>
        /// Affichage des informations de la revue sélectionnée et les exemplaires
        /// </summary>
        /// <param name="revue">la revue</param>
        private void AfficheReceptionRevueInfos(Revue revue)
        {
            // informations sur la revue
            txbReceptionRevuePeriodicite.Text = revue.Periodicite;
            txbReceptionRevueImage.Text = revue.Image;
            txbReceptionRevueDelaiMiseADispo.Text = revue.DelaiMiseADispo.ToString();
            txbReceptionRevueNumero.Text = revue.Id;
            txbReceptionRevueGenre.Text = revue.Genre;
            txbReceptionRevuePublic.Text = revue.Public;
            txbReceptionRevueRayon.Text = revue.Rayon;
            txbReceptionRevueTitre.Text = revue.Titre;
            string image = revue.Image;
            try
            {
                pcbReceptionRevueImage.Image = Image.FromFile(image);
            }
            catch
            {
                pcbReceptionRevueImage.Image = null;
            }
            // affiche la liste des exemplaires de la revue
            AfficheReceptionExemplairesRevue();
        }

        /// <summary>
        /// Récupère et affiche les exemplaires d'une revue
        /// </summary>
        private void AfficheReceptionExemplairesRevue()
        {
            string idDocuement = txbReceptionRevueNumero.Text;
            lesExemplaires = controller.GetExemplairesRevue(idDocuement);
            RemplirReceptionExemplairesListe(lesExemplaires);
            AccesReceptionExemplaireGroupBox(true);
        }

        /// <summary>
        /// Permet ou interdit l'accès à la gestion de la réception d'un exemplaire
        /// et vide les objets graphiques
        /// </summary>
        /// <param name="acces">true ou false</param>
        private void AccesReceptionExemplaireGroupBox(bool acces)
        {
            grpReceptionExemplaire.Enabled = acces;
            txbReceptionExemplaireImage.Text = "";
            txbReceptionExemplaireNumero.Text = "";
            pcbReceptionExemplaireImage.Image = null;
            dtpReceptionExemplaireDate.Value = DateTime.Now;
        }

        /// <summary>
        /// Recherche image sur disque (pour l'exemplaire à insérer)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnReceptionExemplaireImage_Click(object sender, EventArgs e)
        {
            string filePath = "";
            OpenFileDialog openFileDialog = new OpenFileDialog()
            {
                // positionnement à la racine du disque où se trouve le dossier actuel
                InitialDirectory = Path.GetPathRoot(Environment.CurrentDirectory),
                Filter = "Files|*.jpg;*.bmp;*.jpeg;*.png;*.gif"
            };
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                filePath = openFileDialog.FileName;
            }
            txbReceptionExemplaireImage.Text = filePath;
            try
            {
                pcbReceptionExemplaireImage.Image = Image.FromFile(filePath);
            }
            catch
            {
                pcbReceptionExemplaireImage.Image = null;
            }
        }

        /// <summary>
        /// Enregistrement du nouvel exemplaire
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnReceptionExemplaireValider_Click(object sender, EventArgs e)
        {
            if (!txbReceptionExemplaireNumero.Text.Equals(""))
            {
                try
                {
                    int numero = int.Parse(txbReceptionExemplaireNumero.Text);
                    DateTime dateAchat = dtpReceptionExemplaireDate.Value;
                    string photo = txbReceptionExemplaireImage.Text;
                    string idEtat = ETATNEUF;
                    string idDocument = txbReceptionRevueNumero.Text;
                    Exemplaire exemplaire = new Exemplaire(numero, dateAchat, photo, idEtat, idDocument);
                    if (controller.CreerExemplaire(exemplaire))
                    {
                        AfficheReceptionExemplairesRevue();
                    }
                    else
                    {
                        MessageBox.Show("numéro de publication déjà existant", "Erreur");
                    }
                }
                catch
                {
                    MessageBox.Show("le numéro de parution doit être numérique", "Information");
                    txbReceptionExemplaireNumero.Text = "";
                    txbReceptionExemplaireNumero.Focus();
                }
            }
            else
            {
                MessageBox.Show("numéro de parution obligatoire", "Information");
            }
        }

        /// <summary>
        /// Tri sur une colonne
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvExemplairesListe_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            string titreColonne = dgvReceptionExemplairesListe.Columns[e.ColumnIndex].HeaderText;
            List<Exemplaire> sortedList = new List<Exemplaire>();
            switch (titreColonne)
            {
                case "Numero":
                    sortedList = lesExemplaires.OrderBy(o => o.Numero).Reverse().ToList();
                    break;
                case "DateAchat":
                    sortedList = lesExemplaires.OrderBy(o => o.DateAchat).Reverse().ToList();
                    break;
                case "Photo":
                    sortedList = lesExemplaires.OrderBy(o => o.Photo).ToList();
                    break;
            }
            RemplirReceptionExemplairesListe(sortedList);
        }

        /// <summary>
        /// affichage de l'image de l'exemplaire suite à la sélection d'un exemplaire dans la liste
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvReceptionExemplairesListe_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvReceptionExemplairesListe.CurrentCell != null)
            {
                Exemplaire exemplaire = (Exemplaire)bdgExemplairesListe.List[bdgExemplairesListe.Position];
                string image = exemplaire.Photo;
                try
                {
                    pcbReceptionExemplaireRevueImage.Image = Image.FromFile(image);
                }
                catch
                {
                    pcbReceptionExemplaireRevueImage.Image = null;
                }
            }
            else
            {
                pcbReceptionExemplaireRevueImage.Image = null;
            }
        }
        #endregion

        #region Onglet Commandes Livres

        /// <summary>
        /// Ouverture de l'onglet Commandes de Livres
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TabCommandesLivres_Enter(object sender, EventArgs e)
        {
            lesLivres = controller.GetAllLivres();
            RemplirComboSuivi(controller.GetAllSuivis(), bdgSuivis, cbxCmdLivresSuivi);
            VideToutCmdLivres();
        }

        /// <summary>
        /// Recherche et affichage du livre dont le numéro est saisi.
        /// Si non trouvé, affichage d'un MessageBox.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnCmdLivresNumRecherche_Click(object sender, EventArgs e)
        {
            if (!txbCmdLivresNumRecherche.Text.Equals(""))
            {
                Livre livre = lesLivres.Find(x => x.Id.Equals(txbCmdLivresNumRecherche.Text));
                if (livre != null)
                {
                    AfficherCmdLivresInfos(livre);
                }
                else
                {
                    MessageBox.Show("numéro introuvable");
                    txbCmdLivresNumRecherche.Text = "";
                }
            }
        }

        /// <summary>
        /// Methode d'affichage des informations du livre sélectionné dans l'onglet des commandes de livre
        /// </summary>
        /// <param name="livre">le livre</param>
        private void AfficherCmdLivresInfos(Livre livre)
        {
            txbCmdLivresAuteur.Text = livre.Auteur;
            txbCmdLivresCollection.Text = livre.Collection;
            txbCmdLivresImage.Text = livre.Image;
            txbCmdLivresIsbn.Text = livre.Isbn;
            txbCmdLivresNumRecherche.Text = livre.Id;
            txbCmdLivresGenre.Text = livre.Genre;
            txbCmdLivresPublic.Text = livre.Public;
            txbCmdLivresRayon.Text = livre.Rayon;
            txbCmdLivresTitre.Text = livre.Titre;
            string image = livre.Image;
            try
            {
                pcbCmdLivresImage.Image = Image.FromFile(image);
            }
            catch
            {
                pcbCmdLivresImage.Image = null;
            }
            string idDocument = txbCmdLivresNumRecherche.Text;
            Lescommandesdocument = controller.GetCommandesDocument(idDocument);
            RemplirCommandesLivresListe(Lescommandesdocument);
            AccesNouvelleCommandeLivre(true);
        }

        /// <summary>
        /// Remplit le dataGridView avec la liste des commandes du livre reçu en paramètre
        /// </summary>
        /// <param name="lescommandesdocument">liste des commandes</param>
        private void RemplirCommandesLivresListe(List<CommandeDocument> lescommandesdocument)
        {
            if (lescommandesdocument != null)
            {
                bdgCommandesLivre.DataSource = lescommandesdocument;
                dgvCmdLivresListe.DataSource = bdgCommandesLivre;
                dgvCmdLivresListe.Columns["id"].Visible = true;
                dgvCmdLivresListe.Columns["idLivreDvd"].Visible = false;
                dgvCmdLivresListe.Columns["idSuivi"].Visible = false;
                dgvCmdLivresListe.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                dgvCmdLivresListe.Columns["id"].DisplayIndex = 0;
                dgvCmdLivresListe.Columns["dateCommande"].DisplayIndex = 1;
                dgvCmdLivresListe.Columns["montant"].DisplayIndex = 2;
                dgvCmdLivresListe.Columns[5].HeaderCell.Value = "Date de la commande";
                dgvCmdLivresListe.Columns[0].HeaderCell.Value = "Exemplaires";
                dgvCmdLivresListe.Columns[3].HeaderCell.Value = "Etape de suivi";

            }
            else
            {
                bdgCommandesLivre.DataSource = null;
            }
        }

        /// <summary>
        /// Permet ou interdit l'accès à la gestion de la commande d'un livre
        /// et vide les zones de saisie d'une nouvelle commande
        /// </summary>
        /// <param name="acces">true ou false</param>
        private void AccesNouvelleCommandeLivre(bool acces)
        {
            grpLivresNouvCommandes.Enabled = acces;
            grpLivresModifCommandes.Enabled = acces;
            txbCmdLivreId.Text = "";
            dtpCmdLivreDate.Value = DateTime.Now;
            txbNouvCmdLivresMontant.Text = "";
            nudCmdLivresNbExemplaires.Value = 0;
        }

        /// <summary>
        /// Tri sur les colonnes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DgvCmdLivresListe_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            string titreColonne = dgvCmdLivresListe.Columns[e.ColumnIndex].HeaderText;
            List<CommandeDocument> sortedList = new List<CommandeDocument>();
            switch (titreColonne)
            {
                case "Id":
                    sortedList = Lescommandesdocument.OrderBy(o => o.Id).ToList();
                    break;
                case "Date de la commande":
                    sortedList = Lescommandesdocument.OrderBy(o => o.DateCommande).Reverse().ToList();
                    break;
                case "Montant":
                    sortedList = Lescommandesdocument.OrderBy(o => o.Montant).ToList();
                    break;
                case "Exemplaires":
                    sortedList = Lescommandesdocument.OrderBy(o => o.NbExemplaire).ToList();
                    break;
                case "Etape de suivi":
                    sortedList = Lescommandesdocument.OrderBy(o => o.LibelleSuivi).ToList();
                    break;
            }
            RemplirCommandesLivresListe(sortedList);
        }

        /// <summary>
        /// Récupère les informations de commande d'un livre
        /// et initialise les éléments correspondants
        /// </summary>
        /// <param name="commandeDocument"></param>
        private void AfficherCommandeLivre(CommandeDocument commandeDocument)
        {
            txbCmdLivresCmdId.Text = commandeDocument.Id;
            cbxCmdLivresSuivi.Text = commandeDocument.LibelleSuivi;
            txbCmdLivresNbExemplaires.Text = commandeDocument.NbExemplaire.ToString();
            txbCmdLivresDateCommande.Text = commandeDocument.DateCommande.ToString();
            txbCmdLivresMontant.Text = commandeDocument.Montant.ToString();
        }

        /// <summary>
        /// Affiche le détail de la commande livre en fonction de sa position datagrid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DgvCmdLivresListe_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvCmdLivresListe.CurrentCell != null)
            {
                try
                {
                    CommandeDocument commandeDocument = (CommandeDocument)bdgCommandesLivre.List[bdgCommandesLivre.Position];
                    AfficherCommandeLivre(commandeDocument);
                }
                catch
                {
                    VideToutCmdLivres();
                }
            }
            else
            {
                VideToutCmdLivres();
            }
        }

        /// <summary>
        /// Vide les zones d'affichage des informations du livre
        /// </summary>
        private void VideCmdLivresInfos()
        {
            txbCmdLivresAuteur.Text = "";
            txbCmdLivresCollection.Text = "";
            txbCmdLivresImage.Text = "";
            txbCmdLivresIsbn.Text = "";
            txbCmdLivresNumRecherche.Text = "";
            txbCmdLivresGenre.Text = "";
            txbCmdLivresPublic.Text = "";
            txbCmdLivresRayon.Text = "";
            txbCmdLivresTitre.Text = "";
            pcbCmdLivresImage.Image = null;
        }

        private void VideToutCmdLivres()
        {
            VideCmdLivresInfos();
            AccesNouvelleCommandeLivre(false);
        }

        /// <summary>
        /// Retourne l'id de suivi en fonction du libelle
        /// </summary>
        /// <param name="LibelleSuivi"></param>
        /// <returns></returns>
        private int GetIdSuivi(string LibelleSuivi)
        {
            switch (LibelleSuivi)
            {
                case "relancé":
                    return 4;
                case "livrée":
                    return 2;
                case "réglée":
                    return 3;
                default:
                    return 1;
            }
        }

        /// <summary>
        /// Enregistrement d'une nouvelle commande d'un livre
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnNouvelleCmdLivreValider_Click(object sender, EventArgs e)
        {
            if (txbNouvCmdLivresMontant.Text != null && nudCmdLivresNbExemplaires.Value != 0 && !txbCmdLivreId.Text.Equals(""))
            {
                var dialogResult = MessageBox.Show("Souhaitez-vous confirmer l'enregistrement de la commande ?", "Commande Livre", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dialogResult == DialogResult.Yes)
                {
                    try
                    {
                        string id = txbCmdLivreId.Text;
                        DateTime dateCommande = dtpCmdLivreDate.Value;
                        double montant = Double.Parse(txbNouvCmdLivresMontant.Text);
                        int nbExemplaire = (int)nudCmdLivresNbExemplaires.Value;
                        string idLivreDvd = txbCmdLivresNumRecherche.Text;
                        int idSuivi = 1;
                        string libelleSuivi = "en cours";
                        CommandeDocument commandeLivre = new CommandeDocument(id, dateCommande, montant, nbExemplaire, idLivreDvd, idSuivi, libelleSuivi);
                        if (controller.CreerCommandeDocument(commandeLivre))
                        {
                            MessageBox.Show("Commande effectuée");
                            Lescommandesdocument = controller.GetCommandesDocument(idLivreDvd);
                            RemplirCommandesLivresListe(Lescommandesdocument);
                        }
                        else
                        {
                            MessageBox.Show("Erreur lors de la création de la commande", "Commande Livre Erreur");
                        }
                    }
                    catch
                    {
                        MessageBox.Show("Vérifier que tous les champs sont correctement renseignés", "Information");
                    }
                }

            }
            else
            {
                MessageBox.Show("Tous les champs sont obligatoires", "Commande Livre");
            }
        }

        /// <summary>
        /// Suppression d'une commande d'un livre
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void btnCmdLivreSupprimer_Click(object sender, EventArgs e)
        {
            if (txbCmdLivresCmdId.Text != null)
            {
                if (MessageBox.Show("Souhaitez-vous confirmer la supression de la commande ?", "Commande Livre", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    string libelleSuivis = cbxCmdLivresSuivi.SelectedItem.ToString();
                    if (libelleSuivis.Equals("livrée"))
                    {
                        MessageBox.Show("La commande ne peut pas être supprimée lorsqu'elle a déja été livrée");
                    }
                    else
                    {
                        try
                        {
                            CommandeDocument commandeLivre = (CommandeDocument)bdgCommandesLivre.List[bdgCommandesLivre.Position];
                            controller.SupprimerCommandeDocument(commandeLivre);
                            MessageBox.Show("Suppression effectuée");
                            Lescommandesdocument = controller.GetCommandesDocument(txbCmdLivresNumRecherche.Text);
                            RemplirCommandesLivresListe(Lescommandesdocument);
                        }
                        catch
                        {
                            MessageBox.Show("Erreur lors de la suppression de la commande", "Suppression Commande Erreur");
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Sélectionnez une ligne", "Commande Livre");
            }
        }
        
        /// <summary>
        /// Modification de l'étape de suivi d'une commande de livre
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnCmdLivreSuiviModifier_Click(object sender, EventArgs e)
        {
            if (!txbCmdLivresCmdId.Equals(""))
            {
                string libelleSuivi = cbxCmdLivresSuivi.SelectedItem.ToString();
                string oldLibelleSuivi = dgvCmdLivresListe.CurrentRow.Cells["libelleSuivi"].Value.ToString();
                string id = txbCmdLivresCmdId.Text;
                DateTime dateCommande = DateTime.Parse(txbCmdLivresDateCommande.Text);
                double montant = double.Parse(txbCmdLivresMontant.Text);
                int nbExemplaire = int.Parse(txbCmdLivresNbExemplaires.Text);
                int idSuivi = GetIdSuivi(cbxCmdLivresSuivi.SelectedItem.ToString());
                string idLivreDvd = txbCmdLivresNumRecherche.Text;
                CommandeDocument commandeDocument = new CommandeDocument(id, dateCommande, montant, nbExemplaire, idLivreDvd, idSuivi, libelleSuivi);

                if (MessageBox.Show("Souhaitez-vous confirmer la modification du suivi de la commande ?", "Commande Livre", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    if ((oldLibelleSuivi.Equals("livrée") || oldLibelleSuivi.Equals("réglée")) && (libelleSuivi.Equals("en cours") || libelleSuivi.Equals("relancée")))
                    {
                        MessageBox.Show("La commande ne peut pas revenir à une étape précédente");
                    }
                    else if (!oldLibelleSuivi.Equals("livrée") && libelleSuivi.Equals("réglée"))
                    {
                        MessageBox.Show("La commande ne peut pas être réglée si elle n'est pas livrée");
                    }
                    else
                    {
                        controller.ModifierCommandeDocument(commandeDocument);
                        MessageBox.Show("Modification de la commande effectuée");
                        Lescommandesdocument = controller.GetCommandesDocument(txbCmdLivresNumRecherche.Text);
                        RemplirCommandesLivresListe(Lescommandesdocument);
                    }
                }
            }
            else
            {
                MessageBox.Show("Sélectionnez une commande", "Commande Livre");
            }
        }

        #endregion

        #region Onglet Commandes DVD

        /// <summary>
        /// Ouverture de l'onglet Commandes de DVD
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TabCommandesDVD_Enter(object sender, EventArgs e)
        {
            lesDvd = controller.GetAllDvd();
            RemplirComboSuivi(controller.GetAllSuivis(), bdgSuivis, cbxCmdDvdSuivi);
            dgvCmdDvdListe.DataSource = null;
            VideToutCmdDvd();
        }

        /// <summary>
        /// Recherche et affichage du dvd dont le numéro est saisi.
        /// Si non trouvé, affichage d'un MessageBox.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnCmdDvdNumRecherche_Click(object sender, EventArgs e)
        {
            if (!txbCmdDvdNumero.Text.Equals(""))
            {
                Dvd dvd = lesDvd.Find(x => x.Id.Equals(txbCmdDvdNumero.Text));
                if (dvd != null)
                {
                    AfficherCmdDvdInfos(dvd);
                }
                else
                {
                    MessageBox.Show("numéro introuvable");
                    txbCmdDvdNumero.Text = "";
                }
            }
        }

        /// <summary>
        /// Methode d'affichage des informations du dvd sélectionné dans l'onglet des commandes de dvd
        /// </summary>
        /// <param name="livre">le livre</param>
        private void AfficherCmdDvdInfos(Dvd dvd)
        {
            txbCmdDvdNumero.Text = dvd.Id;
            txbCmdDvdTitre.Text = dvd.Titre;
            txbCmdDvdRealisateur.Text = dvd.Realisateur;
            txbCmdDvdSynopsis.Text = dvd.Synopsis;
            txbCmdDvdGenre.Text = dvd.Genre;
            txbCmdDvdPublic.Text = dvd.Public;
            txbCmdDvdRayon.Text = dvd.Rayon;
            txbCmdDvdImage.Text = dvd.Image;
            txbCmdDvdDuree.Text = dvd.Duree.ToString();
            string image = dvd.Image;
            try
            {
                pcbCmdDvdImage.Image = Image.FromFile(image);
            }
            catch
            {
                pcbCmdDvdImage.Image = null;
            }
            string idDocument = txbCmdDvdNumero.Text;
            Lescommandesdocument = controller.GetCommandesDocument(idDocument);
            RemplirCommandesDvdListe(Lescommandesdocument);
            AccesNouvelleCommandeDvd(true);
        }

        /// <summary>
        /// Remplit le dataGridView avec la liste des commandes du dvd reçu en paramètre
        /// </summary>
        /// <param name="lescommandesdocument">liste des commandes</param>
        private void RemplirCommandesDvdListe(List<CommandeDocument> lescommandesdocument)
        {
            if (lescommandesdocument != null)
            {
                bdgCommandesDvd.DataSource = lescommandesdocument;
                dgvCmdDvdListe.DataSource = bdgCommandesDvd;
                dgvCmdDvdListe.Columns["id"].Visible = true;
                dgvCmdDvdListe.Columns["idLivreDvd"].Visible = false;
                dgvCmdDvdListe.Columns["idSuivi"].Visible = false;
                dgvCmdDvdListe.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                dgvCmdDvdListe.Columns["id"].DisplayIndex = 0;
                dgvCmdDvdListe.Columns["dateCommande"].DisplayIndex = 1;
                dgvCmdDvdListe.Columns["montant"].DisplayIndex = 2;
                dgvCmdDvdListe.Columns[5].HeaderCell.Value = "Date de la commande";
                dgvCmdDvdListe.Columns[0].HeaderCell.Value = "Exemplaires";
                dgvCmdDvdListe.Columns[3].HeaderCell.Value = "Etape de suivi";

            }
            else
            {
                bdgCommandesDvd.DataSource = null;
            }
        }

        /// <summary>
        /// Permet ou interdit l'accès à la gestion de la commande d'un dvd
        /// et vide les zones de saisie d'une nouvelle commande
        /// </summary>
        /// <param name="acces">true ou false</param>
        private void AccesNouvelleCommandeDvd(bool acces)
        {
            grpDvdNouvCommandes.Enabled = acces;
            grpDvdModifCommandes.Enabled = acces;
            txbCmdDvdId.Text = "";
            dtpCmdDvdDate.Value = DateTime.Now;
            txbNouvCmdDvdMontant.Text = "";
            nudCmdDvdNbExemplaires.Value = 0;
        }

        /// <summary>
        /// Tri sur les colonnes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DgvCmdDvdListe_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            string titreColonne = dgvCmdDvdListe.Columns[e.ColumnIndex].HeaderText;
            List<CommandeDocument> sortedList = new List<CommandeDocument>();
            switch (titreColonne)
            {
                case "Id":
                    sortedList = Lescommandesdocument.OrderBy(o => o.Id).ToList();
                    break;
                case "Date de la commande":
                    sortedList = Lescommandesdocument.OrderBy(o => o.DateCommande).Reverse().ToList();
                    break;
                case "Montant":
                    sortedList = Lescommandesdocument.OrderBy(o => o.Montant).ToList();
                    break;
                case "Exemplaires":
                    sortedList = Lescommandesdocument.OrderBy(o => o.NbExemplaire).ToList();
                    break;
                case "Etape de suivi":
                    sortedList = Lescommandesdocument.OrderBy(o => o.LibelleSuivi).ToList();
                    break;
            }
            RemplirCommandesDvdListe(sortedList);
        }

        /// <summary>
        /// Récupère les informations de commande d'un dvd
        /// et initialise les éléments correspondants
        /// </summary>
        /// <param name="commandeDocument"></param>
        private void AfficherCommandeDvd(CommandeDocument commandeDocument)
        {
            txbCmdDvdNumId.Text = commandeDocument.Id;
            cbxCmdDvdSuivi.Text = commandeDocument.LibelleSuivi;
            txbCmdDvdNbExemplaires.Text = commandeDocument.NbExemplaire.ToString();
            txbCmdDvdDateCommande.Text = commandeDocument.DateCommande.ToString();
            txbCmdDvdMontant.Text = commandeDocument.Montant.ToString();
        }

        /// <summary>
        /// Affiche le détail de la commande dvd en fonction de sa position datagrid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DgvCmdDvdListe_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvCmdDvdListe.CurrentCell != null)
            {
                try
                {
                    CommandeDocument commandeDocument = (CommandeDocument)bdgCommandesDvd.List[bdgCommandesDvd.Position];
                    AfficherCommandeDvd(commandeDocument);
                }
                catch
                {
                    VideToutCmdDvd();
                }
            }
            else
            {
                VideToutCmdDvd();
            }
        }

        /// <summary>
        /// Vide les zones d'affichage des informations du dvd
        /// </summary>
        private void VideCmdDvdInfos()
        {
            txbCmdDvdNumero.Text = "";
            txbCmdDvdTitre.Text = "";
            txbCmdDvdRealisateur.Text = "";
            txbCmdDvdSynopsis.Text = "";
            txbCmdDvdGenre.Text = "";
            txbCmdDvdPublic.Text = "";
            txbCmdDvdRayon.Text = "";
            txbCmdDvdImage.Text = "";
            txbCmdDvdDuree.Text = "";
            pcbCmdDvdImage.Image = null;

        }

        private void VideToutCmdDvd()
        {
            VideCmdDvdInfos();
            AccesNouvelleCommandeDvd(false);
        }

        /// <summary>
        /// Enregistrement d'une nouvelle commande d'un dvd
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnNouvelleCmdDvdValider_Click(object sender, EventArgs e)
        {
            if (txbNouvCmdDvdMontant.Text != null && nudCmdDvdNbExemplaires.Value != 0 && !txbCmdDvdId.Text.Equals(""))
            {
                var dialogResult = MessageBox.Show("Souhaitez-vous confirmer l'enregistrement de la commande ?", "Commande DVD", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dialogResult == DialogResult.Yes)
                {
                    try
                    {
                        string id = txbCmdDvdId.Text;
                        DateTime dateCommande = dtpCmdDvdDate.Value;
                        double montant = Double.Parse(txbNouvCmdDvdMontant.Text);
                        int nbExemplaire = (int)nudCmdDvdNbExemplaires.Value;
                        string idLivreDvd = txbCmdDvdNumero.Text;
                        int idSuivi = 1;
                        string libelleSuivi = "en cours";
                        CommandeDocument commandeDvd = new CommandeDocument(id, dateCommande, montant, nbExemplaire, idLivreDvd, idSuivi, libelleSuivi);
                        if (controller.CreerCommandeDocument(commandeDvd))
                        {
                            MessageBox.Show("Commande effectuée");
                            Lescommandesdocument = controller.GetCommandesDocument(idLivreDvd);
                            RemplirCommandesDvdListe(Lescommandesdocument);
                        }
                        else
                        {
                            MessageBox.Show("Erreur lors de la création de la commande", "Commande DVD Erreur");
                        }
                    }
                    catch
                    {
                        MessageBox.Show("Vérifier que tous les champs sont correctement renseignés", "Information");
                    }
                }

            }
            else
            {
                MessageBox.Show("Tous les champs sont obligatoires", "Commande DVD");
            }
        }

        /// <summary>
        /// Suppression d'une commande d'un dvd
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void BtnCmdDvdSupprimer_Click(object sender, EventArgs e)
        {
            if (txbCmdDvdNumId != null)
            {
                if (MessageBox.Show("Souhaitez-vous confirmer la supression de la commande ?", "Commande DVD", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    string libelleSuivis = cbxCmdDvdSuivi.SelectedItem.ToString();
                    if (libelleSuivis.Equals("livrée"))
                    {
                        MessageBox.Show("La commande ne peut pas être supprimée lorsqu'elle a déja été livrée");
                    }
                    else
                    {
                        try
                        {
                            CommandeDocument commandeDvd = (CommandeDocument)bdgCommandesDvd.Current;
                            controller.SupprimerCommandeDocument(commandeDvd);
                            MessageBox.Show("Suppression effectuée");
                            Lescommandesdocument = controller.GetCommandesDocument(txbCmdDvdNumero.Text);
                            RemplirCommandesLivresListe(Lescommandesdocument);
                        }
                        catch
                        {
                            MessageBox.Show("Erreur lors de la suppression de la commande", "Suppression Commande Erreur");
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Sélectionnez une ligne", "Commande DVD");
            }
        }

        /// <summary>
        /// Modification de l'étape de suivi d'une commande de dvd
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnCmdDvdSuiviModifier_Click(object sender, EventArgs e)
        {
            if (!txbCmdDvdNumId.Equals(""))
            {
                string libelleSuivi = cbxCmdDvdSuivi.SelectedItem.ToString();
                string oldLibelleSuivi = dgvCmdDvdListe.CurrentRow.Cells["libelleSuivi"].Value.ToString();
                string id = txbCmdDvdNumId.Text;
                DateTime dateCommande = DateTime.Parse(txbCmdDvdDateCommande.Text);
                double montant = double.Parse(txbCmdDvdMontant.Text);
                int nbExemplaire = int.Parse(txbCmdDvdNbExemplaires.Text);
                int idSuivi = GetIdSuivi(libelleSuivi);
                string idLivreDvd = txbCmdDvdNumero.Text;
                CommandeDocument commandeDocument = new CommandeDocument(id, dateCommande, montant, nbExemplaire, idLivreDvd, idSuivi, libelleSuivi);

                if (MessageBox.Show("Souhaitez-vous confirmer la modification du suivi de la commande ?", "Commande DVD", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    if ((oldLibelleSuivi.Equals("livrée") || oldLibelleSuivi.Equals("réglée")) && (libelleSuivi.Equals("en cours") || libelleSuivi.Equals("relancée")))
                    {
                        MessageBox.Show("La commande ne peut pas revenir à une étape précédente");
                    }
                    else if (!oldLibelleSuivi.Equals("livrée") && libelleSuivi.Equals("réglée"))
                    {
                        MessageBox.Show("La commande ne peut pas être réglée si elle n'est pas livrée");
                    }
                    else
                    {
                        controller.ModifierCommandeDocument(commandeDocument);
                        MessageBox.Show("Modification de la commande effectuée");
                        Lescommandesdocument = controller.GetCommandesDocument(txbCmdDvdNumero.Text);
                        RemplirCommandesDvdListe(Lescommandesdocument);
                    }
                }
            }
            else
            {
                MessageBox.Show("Sélectionnez une commande", "Commande DVD");
            }
        }

        #endregion

        #region Onglet Commandes Revues

        /// <summary>
        /// Ouverture de l'onglet Commandes de Revues
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TabCommandesRevues_Enter(object sender, EventArgs e)
        {
            lesRevues = controller.GetAllRevues();
            dgvCmdRevuesListe.DataSource = null;
            VideToutCmdRevues();
        }

        /// <summary>
        /// Recherche et affichage de la revue dont le numéro est saisi.
        /// Si non trouvé, affichage d'un MessageBox.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnCmdRevuesNumRecherche_Click(object sender, EventArgs e)
        {
            if (!txbCmdRevuesNumRecherche.Text.Equals(""))
            {
                Revue revue = lesRevues.Find(x => x.Id.Equals(txbCmdRevuesNumRecherche.Text));
                if (revue != null)
                {
                    AfficherCmdRevuesInfos(revue);
                }
                else
                {
                    MessageBox.Show("numéro introuvable");
                    txbCmdRevuesNumRecherche.Text = "";
                }
            }
        }

        /// <summary>
        /// Methode d'affichage des informations de la revue sélectionnée dans l'onglet des commandes de revue
        /// </summary>
        /// <param name="livre">le livre</param>
        private void AfficherCmdRevuesInfos(Revue revue)
        {
            txbCmdRevuesPeriodicite.Text = revue.Periodicite;
            txbCmdRevuesImage.Text = revue.Image;
            txbCmdRevuesDelaiDispo.Text = revue.DelaiMiseADispo.ToString();
            txbCmdRevuesNumRecherche.Text = revue.Id;
            txbCmdRevuesGenre.Text = revue.Genre;
            txbCmdRevuesPublic.Text = revue.Public;
            txbCmdRevuesRayon.Text = revue.Rayon;
            txbCmdRevuesTitre.Text = revue.Titre;
            string image = revue.Image;
            try
            {
                pcbCmdRevuesImage.Image = Image.FromFile(image);
            }
            catch
            {
                pcbCmdRevuesImage.Image = null;
            }
            string idDocument = txbCmdRevuesNumRecherche.Text;
            Lescommandesabonnement = controller.GetAbonnement(idDocument);
            RemplirCommandesRevuesListe(Lescommandesabonnement);
            AccesNouvelleCommandeRevue(true);
        }

        /// <summary>
        /// Remplit le dataGridView avec la liste des commandes de la revue reçue en paramètre
        /// </summary>
        /// <param name="lescommandesdocument">liste des commandes</param>
        private void RemplirCommandesRevuesListe(List<Abonnement> lescommandesabonnement)
        {
            if (lescommandesabonnement != null)
            {
                bdgCommandesRevues.DataSource = lescommandesabonnement;
                dgvCmdRevuesListe.DataSource = bdgCommandesRevues;
                dgvCmdRevuesListe.Columns["id"].Visible = true;
                dgvCmdRevuesListe.Columns["idRevue"].Visible = false;
                dgvCmdRevuesListe.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                dgvCmdRevuesListe.Columns["id"].DisplayIndex = 0;
                dgvCmdRevuesListe.Columns["dateCommande"].DisplayIndex = 1;
                dgvCmdRevuesListe.Columns["montant"].DisplayIndex = 2;
                dgvCmdRevuesListe.Columns[3].HeaderCell.Value = "Date de la commande";
                dgvCmdRevuesListe.Columns[0].HeaderCell.Value = "Fin d'abonnement";

            }
            else
            {
                bdgCommandesRevues.DataSource = null;
            }
        }

        /// <summary>
        /// Permet ou interdit l'accès à la gestion de la commande d'une revue
        /// et vide les zones de saisie d'une nouvelle commande
        /// </summary>
        /// <param name="acces">true ou false</param>
        private void AccesNouvelleCommandeRevue(bool acces)
        {
            grpRevuesNouvCommandes.Enabled = acces;
            grpRevuesModifCommandes.Enabled = acces;
            txbCmdRevuesId.Text = "";
            dtpCmdRevuesDate.Value = DateTime.Now;
            txbNouvCmdRevuesMontant.Text = "";
            dtpCmdRevuesFinAbonnement.Value = DateTime.Now;
        }

        /// <summary>
        /// Tri sur les colonnes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DgvCmdRevuesListe_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            string titreColonne = dgvCmdRevuesListe.Columns[e.ColumnIndex].HeaderText;
            List<Abonnement> sortedList = new List<Abonnement>();
            switch (titreColonne)
            {
                case "Id":
                    sortedList = Lescommandesabonnement.OrderBy(o => o.Id).ToList();
                    break;
                case "Date de la commande":
                    sortedList = Lescommandesabonnement.OrderBy(o => o.DateCommande).Reverse().ToList();
                    break;
                case "Montant":
                    sortedList = Lescommandesabonnement.OrderBy(o => o.Montant).ToList();
                    break;
                case "Fin d'abonnement":
                    sortedList = Lescommandesabonnement.OrderBy(o => o.DateFinAbonnement).ToList();
                    break;
            }
            RemplirCommandesRevuesListe(sortedList);
        }

        /// <summary>
        /// Récupère les informations de commande d'une revue
        /// et initialise les éléments correspondants
        /// </summary>
        /// <param name="commandeDocument"></param>
        private void AfficherCommandeRevue(Abonnement abonnement)
        {
            txbCmdRevuesMontant.Text = abonnement.Montant.ToString();
            txbCmdRevuesCmdId.Text = abonnement.Id;
            txbCmdRevuesFinAbo.Text = abonnement.DateFinAbonnement.ToString();
            txbCmdRevuesDate.Text = abonnement.DateCommande.ToString();
        }

        /// <summary>
        /// Affiche le détail de la commande revue en fonction de sa position datagrid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DgvCmdRevuesListe_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvCmdRevuesListe.CurrentCell != null)
            {
                try
                {
                    Abonnement abonnement = (Abonnement)bdgCommandesRevues.List[bdgCommandesRevues.Position];
                    AfficherCommandeRevue(abonnement);
                }
                catch
                {
                    VideToutCmdRevues();
                }
            }
            else
            {
                VideToutCmdRevues();
            }
        }

        /// <summary>
        /// Vide les zones d'affichage des informations du livre
        /// </summary>
        private void VideCmdRevuesInfos()
        {
            txbCmdRevuesPeriodicite.Text = "";
            txbCmdRevuesImage.Text = "";
            txbCmdRevuesDelaiDispo.Text = "";
            txbCmdRevuesNumRecherche.Text = "";
            txbCmdRevuesGenre.Text = "";
            txbCmdRevuesPublic.Text = "";
            txbCmdRevuesRayon.Text = "";
            txbCmdRevuesTitre.Text = "";
            pcbCmdRevuesImage.Image = null;
        }

        private void VideToutCmdRevues()
        {
            VideCmdRevuesInfos();
            AccesNouvelleCommandeRevue(false);
        }

        /// <summary>
        /// Enregistrement d'une nouvelle commande d'une revue
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnNouvelleCmdRevuesValider_Click(object sender, EventArgs e)
        {
            if (txbNouvCmdRevuesMontant.Text != null && !txbCmdRevuesId.Text.Equals(""))
            {
                var dialogResult = MessageBox.Show("Souhaitez-vous confirmer l'enregistrement de la commande ?", "Commande Revue", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dialogResult == DialogResult.Yes)
                {
                    try
                    {
                        string id = txbCmdRevuesId.Text;
                        DateTime dateCommande = dtpCmdRevuesDate.Value;
                        double montant = Double.Parse(txbNouvCmdRevuesMontant.Text);
                        DateTime dateFinAbonnement = dtpCmdRevuesFinAbonnement.Value;
                        string idRevue = txbCmdRevuesNumRecherche.Text;
                        Abonnement commandeRevue = new Abonnement(id, dateCommande, montant, dateFinAbonnement, idRevue);
                        if (controller.CreerCommandeRevue(commandeRevue))
                        {
                            MessageBox.Show("Commande effectuée");
                            Lescommandesabonnement = controller.GetAbonnement(idRevue);
                            RemplirCommandesRevuesListe(Lescommandesabonnement);
                        }
                        else
                        {
                            MessageBox.Show("Erreur lors de la création de la commande", "Commande Revue Erreur");
                        }
                    }
                    catch
                    {
                        MessageBox.Show("Vérifier que tous les champs sont correctement renseignés", "Information");
                    }
                }

            }
            else
            {
                MessageBox.Show("Tous les champs sont obligatoires", "Commande Revue");
            }
        }

        /// <summary>
        /// Suppression d'une commande d'une revue
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void BtnCmdRevueSupprimer_Click(object sender, EventArgs e)
        {
            if (txbCmdRevuesCmdId != null)
            {
                if (MessageBox.Show("Souhaitez-vous confirmer la supression de la commande ?", "Commande Revue", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    string idRevue = txbCmdRevuesNumRecherche.Text;
                    Abonnement commandeRevue = (Abonnement)bdgCommandesRevues.Current;
                    if (controller.ExemplaireAbonnement(commandeRevue))
                    {
                        try
                        {
                            controller.SupprimerCommandeRevue(commandeRevue);
                            MessageBox.Show("Suppression effectuée");
                            Lescommandesabonnement = controller.GetAbonnement(idRevue);
                            RemplirCommandesRevuesListe(Lescommandesabonnement);
                        }
                        catch
                        {
                            MessageBox.Show("Erreur lors de la suppression de la commande");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Cette commande ne peut pas être supprimée.", "Erreur suppression");
                    } 
                }
            }
            else
            {
                MessageBox.Show("Sélectionnez une ligne", "Commande Revue");
            }
        }

        #endregion

    }
}
