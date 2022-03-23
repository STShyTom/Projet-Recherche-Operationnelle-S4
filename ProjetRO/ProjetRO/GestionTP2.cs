using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetRO
{
    // Createur : Thomas Huguenel
    public class GestionTP2
    {
        private static Ville[] villes; //Liste des villes

        // Lecture du fichier top 80 et stockage des valeurs 
        private string[] ListeVilles = System.IO.File.ReadAllLines("top80.txt");

        /// <summary>
        /// Constructeur d'une instance de gestion
        /// </summary>
        public GestionTP2()
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

        /////////////////////////////////////////////////////////////////////////
        ///////////////////// TOURNEE PLUS PROCHE VOISIN ///////////////////////
        ////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// Renvoie la ville l plus proche d'une ville donnée en paramètre
        /// </summary>
        /// <param name="s"></param> La ville dont on veut trouver le voisin le plus proche
        /// <returns>La ville la plus proche</returns>
        private static Ville plusProche(Ville s, List<Ville> pasVisite)
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
        public static Tournee<Ville> tourneePlusProcheVoisin(Ville s)
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

        /// <summary>
        /// Méthode qui affiche la tournée gloutonne de plus proche voisin
        /// </summary>
        /// <param name="t"></param> La tournée
        public void afficheTourneePlusProcheVoisin(Tournee<Ville> t)
        {
            Ville numero1 = villes[0];
            t = tourneePlusProcheVoisin(numero1); // On récupère la liste des villes pour la tournée de plus proche voisin
            List<int> listeNumeros = new List<int>();
            foreach (Ville v in t)
            {
                listeNumeros.Add(v.NumVille);
            }
            Console.WriteLine("\nTournée gloutonne de plus proche voisin : ");

            string affichage = string.Join(",", listeNumeros);
            Console.Write("[" + affichage + "]\n");
        }

        /// <summary>
        /// Méthode qui calcule la distance totale pour une tournée de plus proche voisin
        /// </summary>
        /// <param name="t"></param> La tournée
        /// <returns>La distance totale</returns>
        public double coutTourneePlusProcheVoisin(Tournee<Ville> t)
        {
            Ville numero1 = villes[0];
            t = tourneePlusProcheVoisin(numero1); // On récupère la liste des villes pour la tournée
            double distanceTotale = 0;
            for (int i = 1; i < t.Count; i++)
            {
                distanceTotale += distanceVilles(t[i], t[i - 1]);
            }
            distanceTotale += distanceVilles(t[79], t[0]); // Ajout de la distance entre le dernier et le prermier point pour faire un tour complet
            return distanceTotale;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /////////////////////////////////////////////PLUS PROCHE VOISIN AMELIORE////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// Méthode qui calcule la meilleure distance totale pour une tournée de plus proche voisin améliorée
        /// </summary>
        /// <param name="t"></param> La tournée
        /// <returns>La meilleure distance totale</returns>
        public double calculCoutTourneePlusProcheVoisinAmeliore(Tournee<Ville> t)
        {
            double distance = 9999999999999;
            Ville stockV = new Ville();

            foreach (Ville v in villes)
            {
                double distanceTotale = 0;
                t = tourneePlusProcheVoisin(v); // On récupère la liste des villes pour la tournée
                for (int i = 1; i < t.Count; i++)
                {
                    distanceTotale += distanceVilles(t[i], t[i - 1]);
                }
                distanceTotale += distanceVilles(t[79], t[0]); // Ajout de la distance entre le dernier et le prermier point pour faire un tour complet

                if (distanceTotale < distance) // Si le cout total est inférieur à ceux trouvés précedemment
                {
                    distance = distanceTotale; // On récupère ce cout
                    stockV = v;
                }
            }
            return distance;
        }

        /// <summary>
        /// Méthode qui permet de déterminer la première ville pour une tournée de plus proche voisin améliorée
        /// </summary>
        /// <param name="t"></param> La tournée
        /// <returns>La première ville</returns>
        public Ville premiereVilleTourneePlusProcheVoisinAmeliore(Tournee<Ville> t)
        {
            double distance = 9999999999999;
            Ville stockV = new Ville();

            foreach (Ville v in villes)
            {
                double distanceTotale = 0;
                t = tourneePlusProcheVoisin(v); // On récupère la liste des villes pour la tournée
                for (int i = 1; i < t.Count; i++)
                {
                    distanceTotale += distanceVilles(t[i], t[i - 1]);
                }
                distanceTotale += distanceVilles(t[79], t[0]); // Ajout de la distance entre le dernier et le prermier point pour faire un tour complet

                if (distanceTotale < distance) // Si le cout total est inférieur à ceux trouvés précedemment
                {
                    distance = distanceTotale; // On récupère ce cout
                    stockV = v;
                }
            }
            return stockV;
        }

        /// <summary>
        /// Méthode qui affiche la tournée gloutonne de plus proche voisin améliorée
        /// </summary>
        /// <param name="t"></param> La tournée
        public void afficheTourneePlusProcheVoisinAmeliore(Tournee<Ville> t)
        {
            t = tourneePlusProcheVoisin(premiereVilleTourneePlusProcheVoisinAmeliore(t)); // On récupère la liste des villes pour la tournée de plus proche voisin améliorée
            List<int> listeNumeros = new List<int>();
            foreach (Ville v in t)
            {
                listeNumeros.Add(v.NumVille);
            }
            Console.WriteLine("\nTournée gloutonne de plus proche voisin : ");

            string affichage = string.Join(",", listeNumeros);
            Console.Write("[" + affichage + "]\n");
        }

        /////////////////////////////////////////////////////////////////////////
        ///////////////////// TOURNEE INSERTION PROCHE //////////////////////////
        ////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// Renvoie les villes les plus éloignées entre elles
        /// </summary>
        /// <returns>L'ensemble des deux villes</returns>
        private List<Ville> plusLoin()
        {
            double distanceLoin = 0;
            List<Ville> list = new List<Ville>();

            foreach (Ville v1 in villes)
            {
                foreach (Ville v2 in villes)
                {
                    double distance = distanceVilles(v1, v2); // On calcule la distance ente les villes
                    if (distance > distanceLoin) // Si la distance calculée est la plus grande trouvée
                    {
                        distanceLoin = distance; // On stocke la distance
                        if (list.Count != 0)
                        {
                            list.RemoveAt(1);
                            list.RemoveAt(0); // On supprime les villes stokées
                        }
                        list.Add(v1);
                        list.Add(v2); // On stocke les nouvelles villes
                    }
                }
            }
            return list;
        }

        /// <summary>
        /// Méthode qui crée la tournée en effectuant un algorithme glouton d'insertion proche
        /// </summary>
        /// <returns>La tournée complète</returns>
        private Tournee<Ville> tourneeInsertionProche()
        {
            Tournee<Ville> tournee = new Tournee<Ville>(); // Création d'une tournée
            List<Ville> pasVisite = new List<Ville>(); // Création liste pour savoir si le sommet est déjà visité
            pasVisite = villes.Cast<Ville>().ToList(); // Conversion du tableau des villes en liste

            Ville v1, v2, stockVilleTournee1, stockVilleTournee2;
            Ville suivant = new Ville();
            int stockIndex = 0;
            List<Ville> loin = new List<Ville>();
            loin = plusLoin(); // On récupère la liste avec les villes les plus éloignées
            v1 = loin[0];
            v2 = loin[1]; // v1 et v2 sont les plus loins
            tournee.Add(v1);
            tournee.Add(v2); // Ajout des villes à la tournée
            pasVisite.Remove(v1);
            pasVisite.Remove(v2); // On retire les villes de la liste pour dire qu'on les a visitées

            while (pasVisite.Count > 0) // Tant qu'il reste des villes à visiter
            {
                double distanceProche = 9999999999;
                foreach (Ville v in pasVisite) // Pour chaque ville à visiter
                {
                    for (int i = 0; i < tournee.Count; i++) // Pour chaque ville de la tournée
                    {
                        Ville ville1 = tournee[i]; // On prend une ville de la tournée
                        Ville ville2 = tournee[(i + 1) % tournee.Count]; // On prend celle qui suit

                        double distanceAjoutee = distanceVilles(ville1, v) + distanceVilles(v, ville2) - distanceVilles(ville1, ville2); // On calcule la distance lorsqu'on ajoute la ville entre deux déjà présentes dans la tournée
                        if (distanceAjoutee < distanceProche) // Si la distance calculée est la plus petite trouvée
                        {
                            distanceProche = distanceAjoutee; // On stocke la distance
                            suivant = v; // On stocke la ville
                            stockVilleTournee1 = ville1;
                            stockVilleTournee2 = ville1; // On stocke les villes choisies de la tournée
                            stockIndex = i;
                        }
                    }
                }
                pasVisite.Remove(suivant); // On retire la ville de la liste pour dire qu'on l'a visitée
                tournee.Insert((stockIndex + 1) % tournee.Count, suivant); // On l'ajoute à la tournée entre les deux villes
            }
            return tournee;
        }

        /// <summary>
        /// Méthode qui affiche la tournée gloutonne d'insertion proche
        /// </summary>
        /// <param name="t"></param> La tournée
        public void afficheTourneeInsertionProche(Tournee<Ville> t)
        {
            t = tourneeInsertionProche(); // On récupère la liste des villes pour la tournée de l'insertion proche
            List<int> listeNumeros = new List<int>();
            foreach (Ville v in t)
            {
                listeNumeros.Add(v.NumVille);
            }
            List<Ville> list = plusLoin();
            Console.WriteLine("\nLes villes les plus éloignées sont " + list[0].NumVille + "-" + list[0].Nom + " et " + list[1].NumVille + "-" + list[1].Nom);
            Console.WriteLine("Tournée gloutonne d'insertion proche : ");

            string affichage = string.Join(",", listeNumeros);
            Console.Write("[" + affichage + "]\n");
        }

        /// <summary>
        /// Méthode qui calcule la distance totale pour une tournée d'insertion proche
        /// </summary>
        /// <param name="t"></param> La tournée
        /// <returns>La distance totale</returns>
        public double coutTourneeInsertionProche(Tournee<Ville> t)
        {
            t = tourneeInsertionProche(); // On récupère la liste des villes pour la tournée
            double distanceTotale = 0;
            for (int i = 1; i < t.Count; i++)
            {
                distanceTotale += distanceVilles(t[i], t[i - 1]);
            }
            distanceTotale += distanceVilles(t[79], t[0]); // Ajout de la distance entre le dernier et le prermier point pour faire un tour complet
            return distanceTotale;
        }

        /////////////////////////////////////////////////////////////////////////
        ///////////////////// TOURNEE INSERTION LOIN //////////////////////////
        ////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// Méthode qui crée la tournée en effectuant un algorithme glouton d'insertion loin
        /// </summary>
        /// <returns>La tournée complète</returns>
        private Tournee<Ville> tourneeInsertionLoin()
        {
            Tournee<Ville> tournee = new Tournee<Ville>(); // Création d'une tournée
            List<Ville> pasVisite = new List<Ville>(); // Création liste pour savoir si le sommet est déjà visité
            pasVisite = villes.Cast<Ville>().ToList(); // Conversion du tableau des villes en liste

            Ville v1, v2, stockVilleTournee1, stockVilleTournee2;
            Ville stockVilleTournee1Inter = new Ville();
            Ville stockVilleTournee2Inter = new Ville();
            Ville suivant = new Ville();
            Ville suivantInter = new Ville();
            int stockIndexInter = 0;
            int stockIndex = 0;
            List<Ville> loin = new List<Ville>();
            loin = plusLoin(); // On récupère la liste avec les villes les plus éloignées
            v1 = loin[0];
            v2 = loin[1]; // v1 et v2 sont les plus loins
            tournee.Add(v1);
            tournee.Add(v2); // Ajout des villes à la tournée
            pasVisite.Remove(v1);
            pasVisite.Remove(v2); // On retire les villes de la liste pour dire qu'on les a visitées

            while (pasVisite.Count > 0) // Tant qu'il reste des villes à visiter
            {
                double distanceLoin = 0;
                foreach (Ville v in pasVisite) // Pour chaque ville à visiter
                {
                    double dMin = 9999999999;
                    for (int i = 0; i < tournee.Count; i++) // Pour chaque ville de la tournée
                    {
                        Ville ville1 = tournee[i]; // On prend une ville de la tournée
                        Ville ville2 = tournee[(i + 1) % tournee.Count]; // On prend celle qui suit

                        double distanceAjoutee = Math.Abs(distanceVilles(ville1, v) + distanceVilles(v, ville2) - distanceVilles(ville1, ville2)); // On calcule la distance lorsqu'on ajoute la ville entre deux déjà présentes dans la tournée
                        if (distanceAjoutee < dMin) // Si la distance calculée est la plus petite trouvée
                        {
                            dMin = distanceAjoutee; // On stocke la distance
                            suivantInter = v; // On stocke la ville
                            stockVilleTournee1Inter = ville1;
                            stockVilleTournee2Inter = ville1; // On stocke les villes choisies de la tournée
                            stockIndexInter = i; // On stocke l'index
                        }
                    }
                    if (distanceLoin < dMin) // Si la distance minimale de la tournée est plus grande qu'une autre déjà trouvée
                    {
                        distanceLoin = dMin; // On stocke la distance
                        suivant = suivantInter; // On stocke la ville
                        stockVilleTournee1 = stockVilleTournee1Inter;
                        stockVilleTournee2 = stockVilleTournee2Inter; // On stocke les villes choisies de la tournée
                        stockIndex = stockIndexInter; // On stocke l'index
                    }
                }
                pasVisite.Remove(suivant); // On retire la ville de la liste pour dire qu'on l'a visitée
                tournee.Insert((stockIndex + 1) % tournee.Count, suivant); // On l'ajoute à la tournée entre les deux villes
            }
            return tournee;
        }

        /// <summary>
        /// Méthode qui affiche la tournée gloutonne d'insertion proche
        /// </summary>
        /// <param name="t"></param> La tournée
        public void afficheTourneeInsertionLoin(Tournee<Ville> t)
        {
            t = tourneeInsertionLoin(); // On récupère la liste des villes pour la tournée de l'insertion loin
            List<int> listeNumeros = new List<int>();
            foreach (Ville v in t)
            {
                listeNumeros.Add(v.NumVille);
            }
            List<Ville> list = plusLoin();
            Console.WriteLine("\nLes villes les plus éloignées sont " + list[0].NumVille + "-" + list[0].Nom + " et " + list[1].NumVille + "-" + list[1].Nom);
            Console.WriteLine("Tournée gloutonne d'insertion loin : ");

            string affichage = string.Join(",", listeNumeros);
            Console.Write("[" + affichage + "]\n");
        }

        /// <summary>
        /// Méthode qui calcule la distance totale pour une tournée d'insertion loin
        /// </summary>
        /// <param name="t"></param> La tournée
        /// <returns>La distance totale</returns>
        public double coutTourneeInsertionLoin(Tournee<Ville> t)
        {
            t = tourneeInsertionLoin(); // On récupère la liste des villes pour la tournée
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
