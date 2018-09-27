using System;

using System.Collections.Generic;
namespace JOVIA_LIB
{
    public class CategorieUtilisateur
    {
        //general declaration
        private int id;
        private string groupe;
        private String designation;
        private List<CategorieUtilisateur> lstCategorieUtilisateur;

        public List<CategorieUtilisateur> LstCategorieUtilisateur
        {
            get
            {
                if (lstCategorieUtilisateur == null) lstCategorieUtilisateur = new List<CategorieUtilisateur>();
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
