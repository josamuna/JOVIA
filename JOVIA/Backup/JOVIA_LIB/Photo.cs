using System;

namespace JOVIA_LIB
{
    public class Photo
    {
        private int id;
        private int id_personne;
        private string photo;

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
        public string PhotoPersonne
        {
            get { return photo; }
            set { photo = value; }
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
