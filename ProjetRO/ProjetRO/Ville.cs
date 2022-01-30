using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetRO
{
    /// <summary>
    /// Classe qui définit une ville 
    /// </summary>
    public class Ville
    {
        private int numVille; // Identifiant de la ville
        private string nom; // Nom de la ville
        private float latitude; // Latitude de la ville
        private float longitude; // Longitude de la ville

        public int NumVille //Propriéte de la liste
        {
            get => numVille;
            set
            {
                numVille = value;
            }
        }

        public string Nom //Propriéte de la liste
        {
            get => nom;
            set
            {
                nom = value;
            }
        }

        public float Latitude //Propriéte de la liste
        {
            get => latitude;
            set
            {
                latitude = value;
            }
        }

        public float Longitude //Propriéte de la liste
        {
            get => longitude;
            set
            {
                longitude = value;
            }
        }
    }
}
