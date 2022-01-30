using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetRO
{
    public class Tournee
    {
        private Ville[] villes; //Liste des villes

        // Lecture du fichier top 80
        private string[] ListeVilles = System.IO.File.ReadAllLines("top80.txt");

        public Ville[] rentrerVille()
        {
            List<Ville> Villes = new List<Ville>();         
                
            foreach (string Ligne in ListeVilles)
            {
                string[] Valeur = Ligne.Split(' ');
                int num = int.Parse(Valeur[0]);
                string n = Valeur[1];
                float lat = float.Parse(Valeur[2]);
                float lon = float.Parse(Valeur[3]);
                Ville v = new Ville { NumVille = num, Nom = n, Latitude = lat, Longitude = lon };
                Villes.Add(v);
            }
            villes = Villes.ToArray();
            return villes;
        }
    }
}
