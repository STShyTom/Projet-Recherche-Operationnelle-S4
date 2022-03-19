using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetRO
{
    public class Gestion
    {
        private Ville[] villes; //Liste des villes

        // Lecture du fichier top 80 et stockage des valeurs 
        private string[] ListeVilles = System.IO.File.ReadAllLines("top80.txt");

        /// <summary>
        /// Constructeur d'une instance de gestion
        /// </summary>
        public Gestion()
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

        /////////////////////////////////////////////////////////////////////////
        /////////////////////////////////////////////////////////////////////////
        /////////////////////////////////////////////////////////////////////////
        /////////////////////////TP2////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////////////////////


        /////////////////////////////////////////////////////////////////////////
        ///////////////////// TOURNEE PLUS PROCHE VOISIN ///////////////////////
        ////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// Renvoie la ville l plus proche d'une ville donnée en paramètre
        /// </summary>
        /// <param name="s"></param> La ville dont on veut trouver le voisin le plus proche
        /// <returns>La ville la plus proche</returns>
        private Ville plusProche(Ville s,List<Ville> pasVisite)
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
        private Tournee<Ville> tourneePlusProcheVoisin(Ville s)
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
                        if(list.Count != 0)
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

            Ville v1, v2 , stockVilleTournee1, stockVilleTournee2;
            Ville suivant = new Ville();
            int stockIndex =0;
            List<Ville> loin = new List<Ville>();
            loin = plusLoin(); // On récupère la liste avec les villes les plus éloignées
            v1 = loin[0]; 
            v2 = loin[1]; // v1 et v2 sont les plus loins
            tournee.Add(v1);
            tournee.Add(v2); // Ajout des villes à la tournée
            pasVisite.Remove(v1);
            pasVisite.Remove(v2); // On retire les villes de la liste pour dire qu'on les a visitées

            while(pasVisite.Count > 0) // Tant qu'il reste des villes à visiter
            {
                double distanceProche = 9999999999;
                foreach (Ville v in pasVisite) // Pour chaque ville à visiter
                {
                    for (int i = 0; i < tournee.Count; i++) // Pour chaque ville de la tournée
                    {
                        Ville ville1 = tournee[i]; // On prend une ville de la tournée
                        Ville ville2 = tournee[(i + 1)%tournee.Count]; // On prend celle qui suit

                        double distanceAjoutee = distanceVilles(ville1, v) + distanceVilles(v, ville2)-distanceVilles(ville1,ville2); // On calcule la distance lorsqu'on ajoute la ville entre deux déjà présentes dans la tournée
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
                tournee.Insert((stockIndex+1)%tournee.Count,suivant); // On l'ajoute à la tournée entre les deux villes
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
                tournee.Insert((stockIndex + 1)%tournee.Count, suivant); // On l'ajoute à la tournée entre les deux villes
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

        /////////////////////////////////////////////////////////////////////////
        /////////////////////////////////////////////////////////////////////////
        /////////////////////////////////////////////////////////////////////////
        /////////////////////////TP3////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////////////////////


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
            while(fini == false)
            {
                fini = true;
                Tournee<Ville> Tvoisin = new Tournee<Ville>(); // Création de la tournée de voisin
                Tvoisin = explorationSuccesseursPremierDAbord(Tcourante); // On effectue l'exploration
                if (coutTournee(Tvoisin)< coutTournee(Tcourante))
                {
                    Tcourante = new Tournee<Ville>(Tvoisin); // COPIE
                    fini = false;
                }
            }
            return Tcourante;
        }

        /// <summary>
        /// Méthode qui effectue une exploration des successeurs en premier d'abord
        /// </summary>
        /// <param name="Tcourante"></param> La tournée courante
        private Tournee<Ville> explorationSuccesseursPremierDAbord(Tournee<Ville> Tcourante)
        {
            for (int i = 0; i < Tcourante.Count; i++)
            {
                if (distanceVilles(Tcourante[(i - 1+80)%80], Tcourante[i]) + distanceVilles(Tcourante[(i + 1)% 80], Tcourante[(i + 2)% 80]) > distanceVilles(Tcourante[(i-1+80)% 80], Tcourante[(i+1) % 80]) + distanceVilles(Tcourante[i], Tcourante[(i+2) % 80]))
                {
                    // Inversion
                    Ville temp = Tcourante[i];
                    Tcourante[i] = Tcourante[(i + 1) % 80];
                    Tcourante[(i + 1) % 80] = temp;
                }
            }
            return Tcourante;
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
