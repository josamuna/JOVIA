using System;
using System.Collections.Generic;

namespace JOVIA_LIB
{
    public class VilleTeritoire
    {
        private int id;
        private string designation;
        private int superficie = 0;
        private List<VilleTeritoire> lstVilleTeritoire;

        public List<VilleTeritoire> LstVilleTeritoire
        {
            get
            {
                if (lstVilleTeritoire == null) lstVilleTeritoire = new List<VilleTeritoire>();
                return lstVilleTeritoire;
            }
        }

        public override string ToString()
        {
            return this.Designation.ToUpper();
        }

        //accesseur et mutateurs
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

        public int Superficie
        {
            get { return superficie; }
            set { superficie = value; }
        }

        //mehodes
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
