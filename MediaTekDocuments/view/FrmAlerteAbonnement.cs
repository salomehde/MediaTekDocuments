using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MediaTekDocuments.model;
using MediaTekDocuments.controller;

namespace MediaTekDocuments.view
{
    public partial class FrmAlerteAbonnement : Form
    {
        private readonly FrmMediatekController controller;
        private readonly BindingSource bdgAbonnements = new BindingSource();

        /// <summary>
        /// Constructeur
        /// </summary>
        public FrmAlerteAbonnement(FrmMediatekController controller)
        {
            InitializeComponent();
            List<FinAbonnement> lesAbonnements = controller.GetFinAbonnement();
            RemplirFinAbonnements(lesAbonnements);
        }

        /// <summary>
        /// Remplit le dataGridView avec la liste des abonnements qui vont se terminer
        /// </summary>
        /// <param name="lesAbonnementsAEcheance"></param>
        private void RemplirFinAbonnements(List<FinAbonnement> lesFinAbonnements)
        {
            bdgAbonnements.DataSource = lesFinAbonnements;
            dgvAbonnements.DataSource = bdgAbonnements;
            dgvAbonnements.Columns["idRevue"].Visible = true;
            dgvAbonnements.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dgvAbonnements.Columns[0].HeaderCell.Value = "Date de fin d'abonnement";
            dgvAbonnements.Columns[1].HeaderCell.Value = "idRevue";
            dgvAbonnements.Columns[2].HeaderCell.Value = "Titre";
        }

        /// <summary>
        /// Ferme la fenêtre lorsqu'on appuie sur le boutton ok
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnFenetreOk_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
