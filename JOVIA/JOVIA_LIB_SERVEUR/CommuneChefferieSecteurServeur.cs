using System;
using System.Collections.Generic;

namespace JOVIA_LIB_SERVEUR
{
    public class CommuneChefferieSecteurServeur
    {
        //decration of attributs
        private int id;
        private int id_VilleTeritoire;
        private string designation;
        private int superficie = 0;
        private List<CommuneChefferieSecteurServeur> lstCommuneChefferieSecteur;

        public List<CommuneChefferieSecteurServeur> LstCommuneChefferieSecteur
        {
            get
            {
                if (lstCommuneChefferieSecteur == null) lstCommuneChefferieSecteur = new List<CommuneChefferieSecteurServeur>();
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
            Factory1.Instance.Save(this);
        }
        public void Supprimer()
        {
            Factory1.Instance.Delete(this);
        }
        public void Modifier()
        {
            Factory1.Instance.Update(this);
        }
    }
}
