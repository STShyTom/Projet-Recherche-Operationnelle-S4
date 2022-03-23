using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetRO
{
    // Createur : Thomas Huguenel
    public class GestionTP1
    {
        private Ville[] villes; //Liste des villes

        // Lecture du fichier top 80 et stockage des valeurs 
        private string[] ListeVilles = System.IO.File.ReadAllLines("top80.txt");

        /// <summary>
        /// Constructeur d'une instance de gestion
        /// </summary>
        public GestionTP1()
        {
            villes = this.rentrerVille(); 
        }

        /// <summary>
        /// Méthode permettant de créer un tableau avec toutes les villes
        /// </summary>
        /// <returns>Le tableau des villes</returns> 
        public Ville[] rentrerVille()
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

        /// <summary>
        /// Méthode qui affiche les villes à l'écran
        /// </summary>
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

        /// <summary>
        /// Méthode calculant la distance entre deux villes
        /// </summary>
        /// <param name="v1"></param> Ville 1
        /// <param name="v2"></param> Ville 2
        /// <returns>La distance</returns>
        public double distanceVilles(Ville v1, Ville v2)
        {
            double distance;
            double long1 = v1.Longitude * (Math.PI / 180.0); // Longitude de la ville 1 convertie en radians
            double long2 = v2.Longitude * (Math.PI / 180.0); // Longitude de la ville 2 convertie en radians
            double lat1 = v1.Latitude * (Math.PI / 180.0); // Latitude de la ville 1 convertie en radians
            double lat2 = v2.Latitude * (Math.PI / 180.0); // Latitude de la ville 2 convertie en radians

            distance = Math.Abs(6371 * Math.Acos((Math.Sin(lat1) * Math.Sin(lat2)) + (Math.Cos(lat1) * Math.Cos(lat2) * Math.Cos(long1 - long2)))); // Calcul de distance

            return distance;
        }

        /// <summary>
        /// Méthode qui affiche la distance entre deux villes
        /// </summary>
        public void afficherDistance()
        {
            Ville[] villes = this.rentrerVille(); // Création d'un tableau de ville qui contient toutes les villes du fichier
            Ville v1, v2;

            Console.Write("Numéro de la ville de départ : ");
            int num1 = int.Parse(Console.ReadLine()); // Lecture du premier identifiant de ville
            Console.Write("Numéro de la ville d'arrivée : ");
            int num2 = int.Parse(Console.ReadLine()); // Lecture du second identifiant de ville

            foreach (Ville ville1 in villes)
            {
                foreach (Ville ville2 in villes)
                {
                    if ((ville1.NumVille == num1) && (ville2.NumVille == num2))
                    {
                        v1 = ville1;
                        v2 = ville2;
                        Console.WriteLine("La distance entre la ville " + num1 + " et la ville " + num2 + " est de " + distanceVilles(v1, v2) + " km.\n");
                    }
                }
            }
        }

        /////////////////////////////////////////////////////////////////////////
        ///////////////////////// TOURNEE CROISSANTE ///////////////////////////
        ////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// Création d'une tournée croissante des villes
        /// </summary>
        /// <returns>La tournée croissante</returns>
        public Tournee<Ville> tourneeCroissante()
        {
            Tournee<Ville> tournee = new Tournee<Ville>(); // Création d'une tournée 
            foreach (Ville v in villes)
            {
                tournee.Add(v); // Ajout de chaque numéro de ville à la liste
            }
            return tournee;
        }

        /// <summary>
        /// Méthode qui affiche la tournée croissante
        /// </summary>
        /// <param name="t"></param> La tournée
        public void afficheTourneeCroissante(Tournee<Ville> t)
        {
            t = tourneeCroissante(); // On récupère la liste des villes pour la tournée croissante
            List<int> listeNumeros = new List<int>();
            foreach (Ville v in t)
            {
                listeNumeros.Add(v.NumVille);
            }
            Console.WriteLine("\nTournée croissante : ");

            string affichage = string.Join(",", listeNumeros);
            Console.Write("[" + affichage + "]\n");
        }

        /// <summary>
        /// Méthode qui calcule la distance totale pour une tournée croissante
        /// </summary>
        /// <param name="t"></param> La tournée
        /// <returns>La distance totale</returns>
        public double coutTourneeCroissante(Tournee<Ville> t)
        {
            t = tourneeCroissante(); // On récupère la liste des villes  pour la tournée croissante
            double distanceTotale = 0;
            for (int i = 1; i < t.Count; i++)
            {
                distanceTotale += distanceVilles(t[i], t[i - 1]);
            }
            distanceTotale += distanceVilles(t[79], t[0]); // Ajout de la distance entre le dernier et le prermier point pour faire un tour complet
            return distanceTotale;
        }

        /////////////////////////////////////////////////////////////////////////
        ///////////////////////// TOURNEE ALEATOIRE ////////////////////////////
        ////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// Méthode qui crée une tournée aléatoire
        /// </summary>
        /// <returns>La tournée</returns>
        public Tournee<Ville> tourneeAleatoire()
        {
            Tournee<Ville> tournee = new Tournee<Ville>(); // Création d'une tournée
            Random r = new Random(); // Création de l'aléatoire
            var listeAleatoire = villes.OrderBy(ville => r.Next()); // Fonction d'aléatoire
            foreach (Ville v in listeAleatoire)
            {
                tournee.Add(v); // Ajout de chaque numéro de ville à la liste
            }
            return tournee;
        }

        /// <summary>
        /// Méthode qui affiche la tournée aléatoire
        /// </summary>
        /// <param name="t"></param> La tournée
        public void afficheTourneeAleatoire(Tournee<Ville> t)
        {
            t = tourneeAleatoire(); // On récupère la liste des villes pour la tournée aléatoire
            List<int> listeNumeros = new List<int>();
            foreach (Ville v in t)
            {
                listeNumeros.Add(v.NumVille);
            }
            Console.WriteLine("\nTournée aléatoire : ");

            string affichage = string.Join(",", listeNumeros);
            Console.Write("[" + affichage + "]\n");
        }

        /// <summary>
        /// Méthode qui calcule la distance totale pour une tournée aléatoire
        /// </summary>
        /// <param name="t"></param> La tournée
        /// <returns>La distance totale</returns>
        public double coutTourneeAleatoire(Tournee<Ville> t)
        {
            t = tourneeAleatoire(); // On récupère la liste des villes  pour la tournée
            double distanceTotale = 0;
            for (int i = 1; i < t.Count; i++)
            {
                distanceTotale += distanceVilles(t[i], t[i - 1]);
            }
            distanceTotale += distanceVilles(t[79], t[0]); // Ajout de la distance entre le dernier et le prermier point pour faire un tour complet
            return distanceTotale;
        }
    }
}
