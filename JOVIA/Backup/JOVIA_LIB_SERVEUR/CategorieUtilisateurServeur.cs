using System;
using System.Collections.Generic;

namespace JOVIA_LIB_SERVEUR
{
    public class CategorieUtilisateurServeur
    {
        //general declaration
        private int id;
        private string groupe;
        private String designation;

        private List<CategorieUtilisateurServeur> lstCategorieUtilisateur;

        public List<CategorieUtilisateurServeur> LstCategorieUtilisateur
        {
            get
            {
                if (lstCategorieUtilisateur == null) lstCategorieUtilisateur = new List<CategorieUtilisateurServeur>();
                return lstCategorieUtilisateur;
            }
        }

        public override string ToString()
        {
            return this.Designation.ToUpper();
        }

        //mutateurs and accesseurs
        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        public string Groupe
        {
            get { return groupe; }
            set
            {
                groupe = value;
            }
        }

        public String Designation
        {
            get { return designation; }
            set
            {
                if (value != null && value != "")
                {
                    designation = value.ToUpper();
                }
                else throw new Exception("La désignation de la catégorie ne peut être vide");
            }
        }

        public void Enregistrer()
        {
            Factory1.Instance.Save(this);
        }

        public void Modifier()
        {
            Factory1.Instance.Update(this);
        }

        public void Supprimer()
        {
            Factory1.Instance.Delete(this);
        }
    }
}
