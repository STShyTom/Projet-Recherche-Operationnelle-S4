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

        // Lecture du fichier top 80 et stockage des valeurs 
        private string[] ListeVilles = System.IO.File.ReadAllLines("top80.txt");

        private Ville[] rentrerVille()
        {
            List<Ville> Villes = new List<Ville>();

            foreach (string Ligne in ListeVilles)
            {
                string[] Valeur = Ligne.Split(' '); // On sépare toutes les données de la ligne
                int num = int.Parse(Valeur[0]); // Identifiant de la ville
                string n = Valeur[1]; // Nom
                float lat = float.Parse(Valeur[2].Replace('.', ',')); // Latitude
                float lon = float.Parse(Valeur[3].Replace('.', ',')); // Longitude
                Ville v = new Ville { NumVille = num, Nom = n, Latitude = lat, Longitude = lon }; // Création de la ville grâce à ses informations
                Villes.Add(v); // Ajout de la ville à la liste
            }
            villes = Villes.ToArray();
            return villes;
        }

        public void afficherVilles()
        {
            Ville[] villes = this.rentrerVille(); // Création d'un tableau de ville qui contient toutes les villes du fichier

            Console.WriteLine("Liste des villes :");
            foreach (Ville ville in villes)
            {
                // Utilisation d'un tableau pour indenter la liste de villes
                Console.WriteLine("\t" + ville.NumVille + " " + ville.Nom + " " + ville.Latitude + " " + ville.Longitude);
            }
        }

        public double DistanceVilles(Ville v1, Ville v2)
        {
            double distance;
            double long1 = v1.Longitude; // Longitude de la ville 1
            double long2 = v2.Longitude; // Longitude de la ville 2
            double lat1 = v1.Latitude; // Latitude de la ville 1
            double lat2 = v2.Latitude; // Latitude de la ville 2

            distance = Math.Abs(6371 * Math.Acos((Math.Sin(lat1) * Math.Sin(lat2)) + (Math.Cos(lat1) * Math.Cos(lat2) * Math.Cos(long1 - long2)))); // Calcul de distance

            return distance;
        }

        public void AfficherDistance()
        {
            Ville[] villes = this.rentrerVille(); // Création d'un tableau de ville qui contient toutes les villes du fichier
            Ville v1, v2;

            int num1 = int.Parse(Console.ReadLine()); // Lecture du premier identifiant de ville
            int num2 = int.Parse(Console.ReadLine()); // Lecture du second identifiant de ville

            foreach(Ville ville1 in villes)
            {
                foreach(Ville ville2 in villes)
                {
                    if ((ville1.NumVille == num1) && (ville2.NumVille == num2))
                    {
                        v1 = ville1;
                        v2 = ville2;
                        Console.WriteLine("La distance entre la ville " + num1 + " et la ville " + num2 + " est de " + DistanceVilles(v1, v2) + " km.");
                    }
                }
                
            }
        }
    }
}
