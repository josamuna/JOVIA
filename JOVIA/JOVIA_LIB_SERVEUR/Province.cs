using System;
using System.Collections.Generic;

namespace JOVIA_LIB_SERVEUR
{
    public class Province
    {
        private int id;
        private string designation;
        private long superficie;
        private List<Province> lstProvince;

        public List<Province> LstProvince
        {
            get 
            {
                if (lstProvince == null) lstProvince = new List<Province>();
                return lstProvince; 
            }
        }

        public override string ToString()
        {
            return this.Designation.ToUpper();
        }

        //accesseurs et mutateurs
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
                else throw new Exception("La désignation ne peut être vide");
            }
        }

        public long Superficie
        {
            get { return superficie; }
            set { superficie = value; }
        }

        //mehodes
        public void Enregistrer()
        {
            //Factory.Instance.savePronvince(this);
            Factory1.Instance.Save(this);
        }
        public void Supprimmer()
        {
            Factory1.Instance.Delete(this);
        }
        public void Modifier()
        {
            Factory1.Instance.Update(this);
        }
    }
}
