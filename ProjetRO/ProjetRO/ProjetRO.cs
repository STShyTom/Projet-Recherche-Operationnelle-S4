using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetRO
{
    // Createur : Thomas Huguenel
    public class ProjetRO
    {
        static void Main(string[] args)
        {
            GestionTP1 gtp1 = new GestionTP1(); // Création de la gestion totale
            GestionTP2 gtp2 = new GestionTP2();
            GestionTP3 gtp3 = new GestionTP3();
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
                switch (touche.KeyChar)
                {
                    case '0':
                        Environment.Exit(0);
                        break;
                    case '1':
                        Console.WriteLine("\nTop 80 des plus grandes villes de Côte d'Or");
                        gtp1.afficherVilles(); // Affichage sur la console de toutes les villes du top 80
                        break;
                    case '2':
                        Console.WriteLine("\nCalcul de distance entre 2 villes");
                        gtp1.afficherDistance(); // Affichage de la distance entre 2 villes choisies
                        break;
                    case '3':
                        gtp1.afficheTourneeCroissante(t);
                        Console.WriteLine("\nLa distance totale pour réaliser une tournée croissante est de " + gtp1.coutTourneeCroissante(t) + "kms."); // Affichage de la tournée croissante
                        break;
                    case '4':
                        gtp1.afficheTourneeAleatoire(t);
                        Console.WriteLine("\nLa distance totale pour réaliser une tournée aléatoire est de " + gtp1.coutTourneeAleatoire(t) + "kms."); // Affichage de la tournée aléatoire
                        break;
                    case '5':
                        Console.WriteLine("\n \n Quelle tournée gloutonne souhaitez-vous ?");
                        Console.WriteLine("1 : Effectuer une tournée des villes avec la méthode gloutonne de plus proche voisin");
                        Console.WriteLine("2 : Effectuer une tournée des villes avec la méthode gloutonne de plus proche voisin amélioré");
                        Console.WriteLine("3 : Effectuer une tournée des villes avec la méthode gloutonne d'insertion proche");
                        Console.WriteLine("4 : Effectuer une tournée des villes avec la méthode gloutonne d'insertion loin");
                        Console.WriteLine("0 : Quitter");
                        ConsoleKeyInfo touche2 = Console.ReadKey(); // Lecture de touche
                        switch (touche2.KeyChar)
                        {
                            case '0':
                                Environment.Exit(0);
                                break;
                            case '1':
                                gtp2.afficheTourneePlusProcheVoisin(t);
                                Console.WriteLine("\nLa distance totale pour réaliser une tournée gloutonne de plus proche voisin est de " + gtp2.coutTourneePlusProcheVoisin(t) + "kms."); // Arfichage de la tournée du plus proche voisin
                                break;
                            case '2':
                                gtp2.afficheTourneePlusProcheVoisinAmeliore(t);
                                Console.WriteLine("\nLa distance totale pour réaliser une tournée gloutonne de plus proche voisin améliorée est de " + gtp2.calculCoutTourneePlusProcheVoisinAmeliore(t) + "kms."); // Arfichage de la tournée du plus proche voisin amelioré
                                break;
                            case '3':
                                gtp2.afficheTourneeInsertionProche(t);
                                Console.WriteLine("\nLa distance totale pour réaliser une tournée gloutonne d'insertion proche est de " + gtp2.coutTourneeInsertionProche(t) + "kms."); // Arfichage de la tournée d'insertion proche
                                break;
                            case '4':
                                gtp2.afficheTourneeInsertionLoin(t);
                                Console.WriteLine("\nLa distance totale pour réaliser une tournée gloutonne d'insertion loin est de " + gtp2.coutTourneeInsertionLoin(t) + "kms."); // Arfichage de la tournée d'insertion proche
                                break;
                        }
                        break;
                    case '6':
                        Console.WriteLine("\n \n Quelle recherche locale souhaitez-vous ?");
                        Console.WriteLine("1 : Effectuer une recherche locale avec échange de successeurs en premier d'abord.");
                        Console.WriteLine("2 : Effectuer une recherche locale avec échange de successeurs en premier d'abord.");
                        Console.WriteLine("0 : Quitter");
                        ConsoleKeyInfo touche3 = Console.ReadKey(); // Lecture de touche
                        switch (touche3.KeyChar)
                        {
                            case '0':
                                break;
                            case '1':
                                gtp3.afficheTourneeRechercheLocale(t);
                                Console.WriteLine("\nLa distance totale pour réaliser une tournée avec la recherche locale est de " + gtp3.coutTourneeRechecheLocale(t) + "kms."); // Arfichage de la tournée de la recherche locale
                                break;
                        }
                        break;
                }
                        
            }
        }
    }
}
