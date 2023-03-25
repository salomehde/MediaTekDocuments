
namespace MediaTekDocuments.view
{
    partial class FrmAlerteAbonnement
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnFenetreOk = new System.Windows.Forms.Button();
            this.dgvAbonnements = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAbonnements)).BeginInit();
            this.SuspendLayout();
            // 
            // btnFenetreOk
            // 
            this.btnFenetreOk.Location = new System.Drawing.Point(615, 368);
            this.btnFenetreOk.Name = "btnFenetreOk";
            this.btnFenetreOk.Size = new System.Drawing.Size(108, 33);
            this.btnFenetreOk.TabIndex = 0;
            this.btnFenetreOk.Text = "OK";
            this.btnFenetreOk.UseVisualStyleBackColor = true;
            this.btnFenetreOk.Click += new System.EventHandler(this.BtnFenetreOk_Click);
            // 
            // dgvAbonnements
            // 
            this.dgvAbonnements.AllowUserToAddRows = false;
            this.dgvAbonnements.AllowUserToDeleteRows = false;
            this.dgvAbonnements.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvAbonnements.Location = new System.Drawing.Point(74, 89);
            this.dgvAbonnements.Name = "dgvAbonnements";
            this.dgvAbonnements.ReadOnly = true;
            this.dgvAbonnements.Size = new System.Drawing.Size(649, 251);
            this.dgvAbonnements.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(71, 38);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(438, 16);
            this.label1.TabIndex = 2;
            this.label1.Text = "Attention ! Les abonnements suivants vont arriver à écheance :";
            // 
            // FrmAlerteAbonnement
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dgvAbonnements);
            this.Controls.Add(this.btnFenetreOk);
            this.Name = "FrmAlerteAbonnement";
            this.Text = "Rappel fin d\'abonnement";
            ((System.ComponentModel.ISupportInitialize)(this.dgvAbonnements)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnFenetreOk;
        private System.Windows.Forms.DataGridView dgvAbonnements;
        private System.Windows.Forms.Label label1;
    }
}