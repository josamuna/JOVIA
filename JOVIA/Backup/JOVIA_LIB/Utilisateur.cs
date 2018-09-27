using System;
using System.Collections.Generic;

namespace JOVIA_LIB
{
    public class Utilisateur
    {
        //declaration of attributs
        private int id;
        private int id_personne;
        private int id_categorieUtilisateur;
        private string nomuser;
        private string motpass;
        private bool activation;

        private List<Utilisateur> lstUtilisateur;

        public List<Utilisateur> LstUtilisateur
        {
            get
            {
                if (lstUtilisateur == null) lstUtilisateur = new List<Utilisateur>();
                return lstUtilisateur;
            }
        }

        public override string ToString()
        {
            return this.Nomuser;
        }

        //accesseurs and mutators
        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        public int Id_personne
        {
            get { return id_personne; }
            set { id_personne = value; }
        }

        public int Id_categorieUtilisateur
        {
            get { return id_categorieUtilisateur; }
            set { id_categorieUtilisateur = value; }
        }

        public string Nomuser
        {
            get { return nomuser; }
            set
            {
                if (value != null && value != "")
                {
                    nomuser = value;
                }
                else throw new Exception("Le nom de l'utilisateur ne peut être vide");
            }
        }

        public string Motpass
        {
            get { return motpass; }
            set
            {
                if (value != null && value != "")
                {
                    motpass = value;
                }
                else throw new Exception("Le mot de passe de l'utilisateur ne peut être vide");
            }
        }

        public bool Activation
        {
            get { return activation; }
            set { activation = value; }
        }

        public void Enregistrer()
        {
            Factory.Instance.Save(this);
        }

        public void Modifier()
        {
            Factory.Instance.Update(this);
        }

        public void Supprimer()
        {
            Factory.Instance.Delete(this);
        }
    }
}
