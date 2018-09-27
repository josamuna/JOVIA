using System;

namespace JOVIA_LIB_SERVEUR
{
    public class CarteServeur
    {
        private int id;
        private int id_personne;
        private System.DateTime datelivraison = DateTime.Today;

        //acceseurs and mutators
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

        public System.DateTime Datelivraison
        {
            get { return datelivraison; }
            set { datelivraison = value; }
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
