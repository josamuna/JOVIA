using System;
//using System.Collections.Generic;
using JOVIA_LIB;

namespace JOVIA_LIB
{
    public class ParametreProvince
    {
        private int id;
        private int id_province;
        private string designation;

        //private List<ParametreProvince> lstParamProvince;

        //public List<ParametreProvince> LstParamProvince
        //{
        //    get
        //    {
        //        if (lstParamProvince == null) lstParamProvince = new List<ParametreProvince>();
        //        return lstParamProvince;
        //    }
        //}

        //public override string ToString()
        //{
        //    return this.Designation.ToUpper() + " => " + this.Adresse_ip;
        //}

        //accesseurs et mutateurs
        public int Id
        {
            get { return id; }
            set { id = value; }

        }

        public int Id_province
        {
            get { return id_province; }
            set { id_province = value; }
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
        public override string ToString()
        {
            return this.Designation.ToUpper();
        }
        public void Enregistrer()
        {
            Factory.Instance.Save(this);
        }
        public void Supprimmer()
        {
            Factory.Instance.Delete(this);
        }
        public void Modifier()
        {
            Factory.Instance.Update(this);
        }
    }
}
