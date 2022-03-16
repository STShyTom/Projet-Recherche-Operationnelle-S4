using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetRO
{
    public class ProjetRO
    {
        static void Main(string[] args)
        {
            Gestion g = new Gestion(); // Création de la gestion totale
            Tournee<Ville> t = new Tournee<Ville>(); // Création d'une tournée

            while (true)
            {
                Console.WriteLine("\n Que souhaitez-vous faire ?");
                Console.WriteLine("1 : Afficher les 80 plus grandes villes de Côte d'Or dans l'ordre décroissant");
                Console.WriteLine("2 : Calculer la distance entre 2 villes choisies");
                Console.WriteLine("3 : Effectuer une tournée des villes dans l'ordre");
                Console.WriteLine("4 : Effectuer une tournée des villes aléatoirement");
                Console.WriteLine("5 : Effectuer une tournée des villes avec la méthode gloutonne de plus proche voisin");
                Console.WriteLine("0 : Quitter");

                ConsoleKeyInfo touche = Console.ReadKey(); // Lecture de touche
                if(touche.KeyChar == '0')
                {
                    Environment.Exit(0);
                }
                else if (touche.KeyChar == '1')
                {
                    Console.WriteLine("\nTop 80 des plus grandes villes de Côte d'Or");
                    g.afficherVilles(); // Affichage sur la console de toutes les villes du top 80
                }
                else if (touche.KeyChar == '2')
                {
                    Console.WriteLine("\nCalcul de distance entre 2 villes");
                    g.afficherDistance(); // Affichage de la distance entre 2 villes choisies
                }
                else if (touche.KeyChar == '3')
                {
                    g.afficheTourneeCroissante(t);
                    Console.WriteLine("\nLa distance totale pour réaliser une tournée croissante est de " + g.coutTourneeCroissante(t) + "kms."); // Affichage de la tournée croissante
                }
                else if(touche.KeyChar == '4')
                {
                    g.afficheTourneeAleatoire(t);
                    Console.WriteLine("\nLa distance totale pour réaliser une tournée aléatoire est de " + g.coutTourneeAleatoire(t) + "kms."); // Affichage de la tournée aléatoire
                }
                else if(touche.KeyChar == '5')
                {
                    g.afficheTourneePlusProcheVoisin(t);
                    Console.WriteLine("\nLa distance totale pour réaliser une tournée gloutonne de plus proche voisin est de " + g.coutTourneePlusProcheVoisin(t) + "kms."); // Arfichage de la tournée du plus proche voisin
                }
                else if (touche.KeyChar == '8')
                {
                    g.afficheTourneeRechercheLocale(t);
                    Console.WriteLine("\nLa distance totale pour réaliser une tournée avec la recherche locale est de " + g.coutTourneeRechecheLocale(t) + "kms."); // Arfichage de la tournée de la recherche locale
                }
                else
                {

                }
            }
        }
    }
}
