using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using JOVIA_LIB;

namespace JOVIA_WIN
{
    public partial class ExecuteQuery : UserControl
    {
        public ExecuteQuery()
        {
            InitializeComponent();
        }

        private void btnCloseQuery_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void btnLoadFile_Click(object sender, EventArgs e)
        {
            try
            {
                dlgOpenFile.ShowDialog();
                string openFile = dlgOpenFile.FileName;
                if (File.Exists(openFile))
                {
                    TextReader read = new StreamReader(openFile);
                    TxtQuery.Text = read.ReadToEnd();
                    read.Close();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Echec du chargement du fichier choisi", "Chargement du fichier", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void btnSaveFile_Click(object sender, EventArgs e)
        {
            try
            {
                dlgSaveFile.ShowDialog();
                string file = dlgSaveFile.FileName;
                if (!File.Exists(file))
                {
                    TextWriter write = new StreamWriter(file);
                    write.Write(TxtQuery.Text.Trim());
                    write.Close();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Echec de l'enregistrement du fichier", "Enregistrement", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void btnCreateFile_Click(object sender, EventArgs e)
        {
            try
            {
                Factory.Instance.ExecuteOneQyery(TxtQuery.Text);
                MessageBox.Show("Commande exécutée avec succès", "Exécution d'une requête", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception)
            {
                MessageBox.Show("La requête ne s'est pas correctement exécutée, vérifiez qu'elle est correcte", "Exécution d'une requête", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
    }
}
