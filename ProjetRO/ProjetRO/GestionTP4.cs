using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace ProjetRO
{
    //Créateur : Thomas Huguenel
    public class GestionTP4
    {
        private static Ville[] villes; //Liste des villes

        // Lecture du fichier top 80 et stockage des valeurs 
        private string[] ListeVilles = System.IO.File.ReadAllLines("top80.txt");

        /// <summary>
        /// Constructeur d'une instance de gestion
        /// </summary>
        public GestionTP4()
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

        //////////////////////////////////////////////////////////////////////////
        ////////////////////////////ALGO GENETIQUE////////////////////////////////
        //////////////////////////////////////////////////////////////////////////

        
        /// <summary>
        /// Méthode qui récupère les 200 meilleurs parents d'une génération
        /// </summary>
        /// <param name="listeTournees"></param> La liste de base
        /// <returns>La liste des meilleurs parents</returns>
        private List<Tournee<Ville>> choisirMeilleursParents(List<Tournee<Ville>> listeTournees)
        {
            foreach(Tournee<Ville> tournee in listeTournees) // Pour chaque tournée de la liste
            {
                tournee.Cout = coutTournee(tournee); // Récupération du cout
            }
            List<Tournee<Ville>> top200 = listeTournees.OrderByDescending(o => o.Cout).Take(200).ToList(); // On récupère les 200 meilleures tournées
            return top200;
        }

        /// <summary>
        /// Méthode qui récupère 200 parents aléatoirement
        /// </summary>
        /// <param name="listeTournees"></param> La liste de base
        /// <returns>Liste de parents</returns>
        private List<Tournee<Ville>> choisirParentsAleatoire(List<Tournee<Ville>> listeTournees)
        {
            Random r = new Random();
            List<Tournee<Ville>> listeParents = listeTournees.OrderBy(x => r.Next()).Take(200).ToList();
            return listeParents;
        }

        /// <summary>
        /// Méthode qui récupère les 20 meilleurs enfants d'une génération
        /// </summary>
        /// <param name="listeTournees"></param> La liste de base
        /// <returns>La liste des meilleurs enfants</returns>
        private List<Tournee<Ville>> choisirMeilleursEnfants(List<Tournee<Ville>> listeTournees)
        {
            foreach (Tournee<Ville> tournee in listeTournees) // Pour chaque tournée de la liste
            {
                tournee.Cout = coutTournee(tournee); // Récupération du cout
            }
            List<Tournee<Ville>> top20 = listeTournees.OrderByDescending(o => o.Cout).Take(20).ToList(); // On récupère les 20 meilleures tournées
            return top20;
        }

        /// <summary>
        /// Méthode qui récupère 20 enfants aléatoirement
        /// </summary>
        /// <param name="listeTournees"></param> La liste de base
        /// <returns>Liste de enfants</returns>
        private List<Tournee<Ville>> choisirEnfantsAleatoire(List<Tournee<Ville>> listeTournees)
        {
            Random r = new Random();
            List<Tournee<Ville>> listeEnfants = listeTournees.OrderBy(x => r.Next()).Take(20).ToList();
            return listeEnfants;
        }

        /// <summary>
        /// Méthode qui renvoie deux parents 
        /// </summary>
        /// <param name="listeParents"></param>
        /// <returns>Les deux villes</returns>
        private List<Tournee<Ville>> choix2Parents(List<Tournee<Ville>> listeParents)
        {
            Random rnd = new Random();
            Random rnd2 = new Random();
            int n1 = rnd.Next(listeParents.Count); // Tirage aléatoire d'un nombre
            int n2 = 0;
            do
            {
                n2 = rnd2.Next(listeParents.Count); // Tirage aléatoire d'un second nombre
            } while (n1 == n2);
            Tournee<Ville> tournee1 = listeParents[n1]; // Première tournée
            Tournee<Ville> tournee2 = listeParents[n1]; // Seconde tournée
            List<Tournee<Ville>> list = new List<Tournee<Ville>>();
            list.Add(tournee1);
            list.Add(tournee2);
            listeParents.Remove(tournee1);
            listeParents.Remove(tournee2);
            return list;
        }

        /// <summary>
        /// Algorithme de croisement entre deux tournées
        /// </summary>
        /// <param name="list"></param> Deux parents
        /// <param name="pointCroisement"></param> Point de croisement
        /// <returns>Deux enfants</returns>
        private List<Tournee<Ville>> croisement(List<Tournee<Ville>> list,int pointCroisement)
        {
            Tournee<Ville> tournee1 = list[0]; // Première tournée
            Tournee<Ville> tournee2 = list[1]; // Première tournée
            //COPIE
            Tournee<Ville> tournee1dupliquee = new Tournee<Ville>(tournee1);
            foreach(Ville ville1 in tournee1)
            {
                tournee1dupliquee.Add(ville1);
            }
            Tournee<Ville> tournee2dupliquee = new Tournee<Ville>(tournee2);
            foreach (Ville ville2 in tournee2)
            {
                tournee2dupliquee.Add(ville2);
            }

            Tournee<Ville> E1 = new Tournee<Ville>();
            Tournee<Ville> E2 = new Tournee<Ville>();

            // Premier remplissage de E
            for(int i = 0; i < pointCroisement; i++)
            {
                E1.Add(tournee1[i]);
            }
            for (int i = 0; i < pointCroisement; i++)
            {
                E2.Add(tournee2[i]);
            }

            // Suppression des villes déjà présentes dans E pour les tournées
            foreach(Ville v1 in E1) 
            {
                tournee2.Remove(v1);
            }
            foreach (Ville v2 in E2)
            {
                tournee1.Remove(v2);
            }

            // Sélections des villes
            Tournee<Ville> temp1 = new Tournee<Ville>();
            Tournee<Ville> temp2 = new Tournee<Ville>();
            for (int i = (tournee1.Count) - 1; i >= (tournee1.Count) - pointCroisement; i--)
            {
                temp1.Add(tournee1[i]);
            }
            for (int i = (tournee2.Count) - 1; i >= (tournee2.Count) - pointCroisement; i--)
            {
                temp2.Add(tournee2[i]);
            }
            // Inversion
            temp1.Reverse();
            temp2.Reverse();
            // Ajout des villes aux E
            foreach (Ville vtemp2 in temp2)
            {
                E1.Add(vtemp2);
            }
            foreach (Ville vtemp1 in temp1)
            {
                E2.Add(vtemp1);
            }

            // Suppression des villes déjà présentes dans E pour les tournées
            foreach (Ville v1 in E1)
            {
                tournee1dupliquee.Remove(v1);
            }
            foreach (Ville v2 in E2)
            {
                tournee2dupliquee.Remove(v2);
            }
            // On complète E
            foreach(Ville city1 in tournee1dupliquee)
            {
                E1.Add(city1);
            }
            foreach (Ville city2 in tournee2dupliquee)
            {
                E1.Add(city2);
            }

            List<Tournee<Ville>> liste = new List<Tournee<Ville>>();
            list.Add(E1);
            list.Add(E2);
            return liste;
        }

        
        private void algorithmeGenetique()
        {
            int X = 800; // Nombre de solutions de départ
            int generation = 1; // Première génération
            int NBMAX = 50; // Nombre maximal de générations
            int pointCroisement = 10; // Définition du point de croisement pour le croisement
            List<Tournee<Ville>> listeTournees = new List<Tournee<Ville>>(); // Liste des tournées
            for(int i = 0; i<X ; i++)
            {
                listeTournees.Add(tourneeAleatoire()); // Ajout des tournees à la liste
            }
            // Duplication liste tournées pour plus tard
            List<Tournee<Ville>> listeTourneesDupliquee = new List<Tournee<Ville>>(listeTournees);
            foreach (Tournee<Ville> t in listeTournees)
            {
                listeTourneesDupliquee.Add(t);
            }

            List<Tournee<Ville>> listeParents = new List<Tournee<Ville>>(); // Liste des parents choisis
            while (generation < NBMAX)
            {
                //Choix de N parents
                listeParents = choisirMeilleursParents(listeTournees); // Récupération des 200 meilleurs parents
                foreach(Tournee<Ville> parent in listeParents)
                {
                    listeTournees.Remove(parent); // On retire les parents de la liste de base
                }
                foreach(Tournee<Ville> aleatoire in choisirParentsAleatoire(listeTournees)) // Récupération de 200 parents aléatoires
                {
                    listeParents.Add(aleatoire); // On ajoute les parents aléatoires à la liste de parents
                }
                // Duplication liste parents pour plus tard
                List<Tournee<Ville>> listeParentDupliquee = new List<Tournee<Ville>>(listeParents);
                foreach (Tournee<Ville> t in listeParents)
                {
                    listeParentDupliquee.Add(t);
                }

                // Croisement
                List<Tournee<Ville>> paireParents = new List<Tournee<Ville>>();
                List<Tournee<Ville>> paireEnfants = new List<Tournee<Ville>>();
                List<Tournee<Ville>> listeEnfants = new List<Tournee<Ville>>(); // Liste des enfants
                while (listeParents.Count > 0)
                {
                    paireParents = choix2Parents(listeParents); // Choix de 2 parents
                    paireEnfants = croisement(paireParents, pointCroisement); // Croisement
                    listeEnfants.Add(paireEnfants[0]);
                    listeEnfants.Add(paireEnfants[1]); // Ajout des enfants à la liste d'enfants
                }
                // Duplication liste enfants pour plus tard
                List<Tournee<Ville>> listeEnfantDupliquee = new List<Tournee<Ville>>(listeEnfants);
                foreach (Tournee<Ville> t in listeEnfants)
                {
                    listeEnfantDupliquee.Add(t);
                }

                // Mutation
                List<Tournee<Ville>> selectionMutation = new List<Tournee<Ville>>();
                selectionMutation = choisirMeilleursEnfants(listeTournees); // Récupération des 20 meilleurs enfants
                foreach (Tournee<Ville> enfant in selectionMutation)
                {
                    listeTournees.Remove(enfant); // On retire les enfants de la liste de base
                }
                foreach (Tournee<Ville> aleatoire in choisirParentsAleatoire(listeTournees)) // Récupération de 200 parents aléatoires
                {
                    listeParents.Add(aleatoire); // On ajoute les parents aléatoires à la liste de parents
                }
            }
        }
    }
}
