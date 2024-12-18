﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetRO
{
    //Créateur : Thomas Huguenel
    public class GestionTP3
    {
        private static Ville[] villes; //Liste des villes

        // Lecture du fichier top 80 et stockage des valeurs 
        private string[] ListeVilles = System.IO.File.ReadAllLines("top80.txt");

        /// <summary>
        /// Constructeur d'une instance de gestion
        /// </summary>
        public GestionTP3()
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
        /// Méthode calculant la distance entre deux villes
        /// </summary>
        /// <param name="v1"></param> Ville 1
        /// <param name="v2"></param> Ville 2
        /// <returns>La distance</returns>
        public static double distanceVilles(Ville v1, Ville v2)
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
        /// Renvoie la ville l plus proche d'une ville donnée en paramètre
        /// </summary>
        /// <param name="s"></param> La ville dont on veut trouver le voisin le plus proche
        /// <returns>La ville la plus proche</returns>
        private Ville plusProche(Ville s, List<Ville> pasVisite)
        {
            Ville plusProche = new Ville();
            double distanceProche = 9999999999;

            foreach (Ville v in pasVisite)
            {
                double distance = distanceVilles(s, v); // On calcule la distance ente les villes
                if (distance < distanceProche) // Si la distance calculée est la plus petite trouvée
                {
                    distanceProche = distance; // On stocke la distance
                    plusProche = v; // On stocke la ville
                }
            }
            return plusProche;
        }

        /// <summary>
        /// Méthode qui crée la tournée en effectuant un algorithme glouton de plus proche voisin
        /// </summary>
        /// <param name="s"></param> La première ville
        /// <returns>La tournée complète</returns>
        public Tournee<Ville> tourneePlusProcheVoisin(Ville s)
        {
            Tournee<Ville> tournee = new Tournee<Ville>(); // Création d'une tournée
            List<Ville> pasVisite = new List<Ville>(); // Création liste pour savoir si le sommet est déjà visité
            pasVisite = villes.Cast<Ville>().ToList(); // Conversion du tableau des villes en liste
            Ville suivant = new Ville();

            tournee.Add(s); // On ajoute la première ville à la tournée
            pasVisite.Remove(s); // On retire la ville de la liste pour dire qu'on l'a visitée
            while (pasVisite.Count > 0) // Tant qu'il reste des éléments dans la liste
            {
                suivant = plusProche(s, pasVisite); // La ville la plus proche est prise
                pasVisite.Remove(suivant); // On retire la ville de la liste pour dire qu'on l'a visitée
                tournee.Add(suivant);
                s = suivant;
            }
            return tournee;
        }


        /////////////////////////////////////////////////////////////////////////
        /////////////////////// ECHANGE DE SUCCESSEURS  /////////////////////////
        ////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// Méthode qui calcule la distance totale pour une tournée
        /// </summary>
        /// <param name="t"></param> La tournée
        /// <returns>La distance totale</returns>
        public double coutTournee(Tournee<Ville> t)
        {
            double distanceTotale = 0;
            for (int i = 1; i < t.Count; i++)
            {
                distanceTotale += distanceVilles(t[i], t[i - 1]);
            }
            distanceTotale += distanceVilles(t[79], t[0]); // Ajout de la distance entre le dernier et le prermier point pour faire un tour complet
            return distanceTotale;
        }

        /// <summary>
        /// Fonction de recherche locale 
        /// </summary>
        /// <param name="T_entree"></param> La tournée qu'on veut améliorer
        /// <returns>La tournée améliorée</returns>
        private Tournee<Ville> recherche_locale(Tournee<Ville> T_entree)
        {
            Tournee<Ville> Tcourante = new Tournee<Ville>(); // Création d'une tournée
            Tcourante = T_entree;
            bool fini = false;
            while (fini == false)
            {
                double coutTcourante = coutTournee(Tcourante);
                fini = true;
                Tournee<Ville> Tvoisin = new Tournee<Ville>(); // Création de la tournée de voisin
                Tvoisin = explorationSuccesseursPremierDAbord(Tcourante); // On effectue l'exploration
                if (coutTournee(Tvoisin) < coutTcourante)
                {
                    // COPIE
                    Tcourante = new Tournee<Ville>(Tvoisin);
                    foreach(Ville v in Tvoisin)
                    {
                        Tcourante.Add(v);
                    }
                    fini = false;
                }
            }
            return Tcourante;
        }

        /// <summary>
        /// Méthode qui effectue une exploration des successeurs en premier d'abord
        /// </summary>
        /// <param name="Tcourante"></param> La tournée courante
        private Tournee<Ville> explorationSuccesseursPremierDAbord(Tournee<Ville> Tcour)
        {
            for (int i = 0; i < Tcour.Count; i++)
            {
                if (distanceVilles(Tcour[(i - 1 + 80) % 80], Tcour[i]) + distanceVilles(Tcour[(i + 1) % 80], Tcour[(i + 2) % 80]) > distanceVilles(Tcour[(i - 1 + 80) % 80], Tcour[(i + 1) % 80]) + distanceVilles(Tcour[i], Tcour[(i + 2) % 80]))
                {
                    // Inversion
                    Ville temp = Tcour[i];
                    Tcour[i] = Tcour[(i + 1) % 80];
                    Tcour[(i + 1) % 80] = temp;
                }
            }
            return Tcour;
        }

        /// <summary>
        /// Méthode qui affiche la tournée de recherche locale
        /// </summary>
        /// <param name="t"></param> La tournée
        public void afficheTourneeRechercheLocale(Tournee<Ville> t)
        {
            Ville numero1 = villes[0];
            t = recherche_locale(tourneePlusProcheVoisin(numero1)); // On récupère la liste des villes pour la recherche locale
            List<int> listeNumeros = new List<int>();
            foreach (Ville v in t)
            {
                listeNumeros.Add(v.NumVille);
            }
            Console.WriteLine("\nTournée de recherche locale : ");

            string affichage = string.Join(",", listeNumeros);
            Console.Write("[" + affichage + "]\n");
        }

        /// <summary>
        /// Méthode qui calcule la distance totale pour une recherche locale
        /// </summary>
        /// <param name="t"></param> La tournée
        /// <returns>La distance totale</returns>
        public double coutTourneeRechecheLocale(Tournee<Ville> t)
        {
            Ville numero1 = villes[0];
            t = recherche_locale(tourneePlusProcheVoisin(numero1)); // On récupère la liste des villes pour la tournée
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
