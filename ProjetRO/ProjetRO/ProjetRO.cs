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
                Console.WriteLine("5 : Effectuer une tournée des villes avec une méthode gloutonne");
                Console.WriteLine("6 : Effectuer une tournée des villes avec la méthode gloutonne de plus proche voisin en recherche locale");
                Console.WriteLine("0 : Quitter");

                ConsoleKeyInfo touche = Console.ReadKey(); // Lecture de touche
                if (touche.KeyChar == '0')
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
                else if (touche.KeyChar == '4')
                {
                    g.afficheTourneeAleatoire(t);
                    Console.WriteLine("\nLa distance totale pour réaliser une tournée aléatoire est de " + g.coutTourneeAleatoire(t) + "kms."); // Affichage de la tournée aléatoire
                }
                else if (touche.KeyChar == '5')
                {
                    Console.WriteLine("\n \n Quelle tournée gloutonne souhaitez-vous ?");
                    Console.WriteLine("1 : Effectuer une tournée des villes avec la méthode gloutonne de plus proche voisin");
                    Console.WriteLine("2 : Effectuer une tournée des villes avec la méthode gloutonne de plus proche voisin amélioré");
                    Console.WriteLine("3 : Effectuer une tournée des villes avec la méthode gloutonne d'insertion proche");
                    Console.WriteLine("4 : Effectuer une tournée des villes avec la méthode gloutonne d'insertion loin");
                    Console.WriteLine("0 : Quitter");
                    ConsoleKeyInfo touche2 = Console.ReadKey(); // Lecture de touche

                    if (touche2.KeyChar == '0')
                    {
                        Environment.Exit(0);
                    }
                    else if (touche2.KeyChar == '1')
                    {
                        g.afficheTourneePlusProcheVoisin(t);
                        Console.WriteLine("\nLa distance totale pour réaliser une tournée gloutonne de plus proche voisin est de " + g.coutTourneePlusProcheVoisin(t) + "kms."); // Arfichage de la tournée du plus proche voisin
                    }
                    else if (touche2.KeyChar == '2')
                    {
                        g.afficheTourneePlusProcheVoisinAmeliore(t);
                        Console.WriteLine("\nLa distance totale pour réaliser une tournée gloutonne de plus proche voisin améliorée est de " + g.calculCoutTourneePlusProcheVoisinAmeliore(t) + "kms."); // Arfichage de la tournée du plus proche voisin amelioré

                    }
                    else if (touche2.KeyChar == '3')
                    {
                        g.afficheTourneeInsertionProche(t);
                        Console.WriteLine("\nLa distance totale pour réaliser une tournée gloutonne d'insertion proche est de " + g.coutTourneeInsertionProche(t) + "kms."); // Arfichage de la tournée d'insertion proche
                    }
                    else if (touche2.KeyChar == '4')
                    {
                        g.afficheTourneeInsertionLoin(t);
                        Console.WriteLine("\nLa distance totale pour réaliser une tournée gloutonne d'insertion loin est de " + g.coutTourneeInsertionLoin(t) + "kms."); // Arfichage de la tournée d'insertion proche
                    }
                }
                else if (touche.KeyChar == '6')
                {

                }
                else if (touche.KeyChar == '7')
                {

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
