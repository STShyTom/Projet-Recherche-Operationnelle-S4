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
        private double latitude; // Latitude de la ville
        private double longitude; // Longitude de la ville

        public int NumVille // Propriétés de l'identifiant
        {
            get => numVille;
            set
            {
                numVille = value;
            }
        }

        public string Nom //Propriétés du nom
        {
            get => nom;
            set
            {
                nom = value;
            }
        }

        public double Latitude //Propriétés de la latitude
        {
            get => latitude;
            set
            {
                latitude = value;
            }
        }

        public double Longitude //Propriétés de la longitude
        {
            get => longitude;
            set
            {
                longitude = value;
            }
        }
    }
}
