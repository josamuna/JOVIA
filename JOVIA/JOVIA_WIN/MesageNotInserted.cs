using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using JOVIA_LIB;

namespace JOVIA_WIN
{
    public partial class MesageNotInserted : UserControl
    {
        ErreurEnvoie errEnvoie = new ErreurEnvoie();

        public MesageNotInserted()
        {
            InitializeComponent();
        }

        private void MesageNotInserted_Load(object sender, EventArgs e)
        {
            try
            {
                dgvEnvoie.DataSource = Factory.Instance.getErreurEnvoies();
            }
            catch (Exception)
            {
                MessageBox.Show("erreur lors de l'affichage", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }            
        }

        private void dgvEnvoie_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                if (dgvEnvoie.SelectedRows.Count > 0)
                {
                    errEnvoie = (ErreurEnvoie)dgvEnvoie.SelectedRows[0].DataBoundItem;
                    //SetBindings();
                    txtCode.Text = Convert.ToString(errEnvoie.Id);
                    txtExpediteur.Text = errEnvoie.Expediteur;
                    txtMessage.Text = errEnvoie.Message;
                    txtDate.Text = Convert.ToString(errEnvoie.Date_envoie);
                    txtErreur.Text = errEnvoie.Erreur;
                }
            }
            catch (Exception) { MessageBox.Show("Erreur dans la zone d'affichage","Erreur d'affichage"); }
        }

        private void btnclose_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void cmdRefrech_Click(object sender, EventArgs e)
        {
            try
            {
                dgvEnvoie.DataSource = Factory.Instance.getErreurEnvoies();
            }
            catch (Exception)
            {
                MessageBox.Show("Erreur lors de l'actualisation", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
    }
}
