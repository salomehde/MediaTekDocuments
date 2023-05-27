using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MediaTekDocuments.controller;
using MediaTekDocuments.model;

namespace MediaTekDocuments.view
{
    /// <summary>
    /// Fenêtre d'authentification
    /// </summary>
    public partial class FrmAuthentification : Form
    {
        private readonly FrmAuthentificationController controller;

        /// <summary>
        /// Constructeur
        /// </summary>
        public FrmAuthentification()
        {
            InitializeComponent();
            this.controller = new FrmAuthentificationController();
        }

        /// <summary>
        /// Bouton qui permet de se connecter à l'application après avoir saisi le login ainsi que le password
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnConnexion_Click(object sender, EventArgs e)
        {
            string utilisateur = txbLogin.Text;
            string password = txbPassword.Text;
            Utilisateur user = controller.GetUtilisateur(utilisateur);
            if (!txbLogin.Text.Equals("") && !txbPassword.Text.Equals(""))
            {
                if (!controller.GetAuthentification(utilisateur, password))
                {
                    MessageBox.Show("Authentification incorrecte", "Erreur Connexion");
                    txbLogin.Text = "";
                    txbPassword.Text = "";
                    txbLogin.Focus();
                }
                else if(user.IdService.Equals(3))
                {
                    MessageBox.Show("Cette application n'est pas disponible pour les employés du service Culture", "Accès non autorisé");
                    this.Close();
                }
                else if(user.IdService.Equals(2))
                {
                    MessageBox.Show("Authentification réussie", "Connexion");
                    FrmMediatek frmMediatek = new FrmMediatek();
                    frmMediatek.Text = "Gestion des documents de la médiathèque: Service Prets";
                    frmMediatek.ShowDialog();
                    this.Close();

                }
                else if(user.IdService.Equals(1))
                {
                    MessageBox.Show("Authentification réussie", "Connexion");
                    FrmMediatek frmMediatek = new FrmMediatek();
                    frmMediatek.Text = "Gestion des documents de la médiathèque: Service Administratif";
                    frmMediatek.ShowDialog();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Authentification réussie", "Connexion");
                    FrmMediatek frmMediatek = new FrmMediatek();
                    frmMediatek.ShowDialog();
                    this.Close();
                }
            }
            else
            {
                MessageBox.Show("Un ou plusieurs champs sont vides", "Erreur Connexion");
            }
        }
    }
}
