using System;
using System.Collections.Generic;

namespace JOVIA_LIB_SERVEUR
{
    public class VilleTeritoireServeur
    {
        private int id;
        private string designation;
        private int id_pronvince;
        private int superficie = 0;
        private List<VilleTeritoireServeur> lstVilleTeritoire;

        public List<VilleTeritoireServeur> LstVilleTeritoire
        {
            get
            {
                if (lstVilleTeritoire == null) lstVilleTeritoire = new List<VilleTeritoireServeur>();
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

        public int Id_pronvince
        {
            get { return id_pronvince; }
            set { id_pronvince = value; }
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
