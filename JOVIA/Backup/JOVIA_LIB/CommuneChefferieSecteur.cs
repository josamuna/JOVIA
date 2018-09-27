using System;
using System.Collections.Generic;

namespace JOVIA_LIB
{
    public class CommuneChefferieSecteur
    {
        //decration of attributs
        private int id;
        private int id_VilleTeritoire;
        private string designation;
        private int superficie = 0;
        private List<CommuneChefferieSecteur> lstCommuneChefferieSecteur;

        public List<CommuneChefferieSecteur> LstCommuneChefferieSecteur
        {
            get
            {
                if (lstCommuneChefferieSecteur == null) lstCommuneChefferieSecteur = new List<CommuneChefferieSecteur>();
                return lstCommuneChefferieSecteur;
            }
        }

        public override string ToString()
        {
            return this.Designation.ToUpper();
        }

        //accesseurs and mutators
        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        public string Designation
        {
            get { return designation; }
            set
            {
                if (value != null && value != "")
                {
                    designation = value.ToUpper();
                }
                else throw new Exception("La désignation de la commune ou Cheferie ne peut être vide");     
            }
        }

        public int Superficie
        {
            get { return superficie; }
            set { superficie = value; }
        }

        public int Id_VilleTeritoire
        {
            get { return id_VilleTeritoire; }
            set { id_VilleTeritoire = value; }
        }

        public void Enregistrer()
        {
            Factory.Instance.Save(this);
        }
        public void Supprimer()
        {
            Factory.Instance.Delete(this);
        }
        public void Modifier()
        {
            Factory.Instance.Update(this);
        }
    }
}
