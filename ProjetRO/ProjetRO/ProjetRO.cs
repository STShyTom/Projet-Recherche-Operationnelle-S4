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
                    Console.WriteLine("\nLa distance totale pour réaliser une tournée croissante est de " + g.coutTourneeCroissante(t) + "kms.");
                }
                else if(touche.KeyChar == '4')
                {
                    g.afficheTourneeAleatoire(t);
                    Console.WriteLine("\nLa distance totale pour réaliser une tournée aléatoire est de " + g.coutTourneeAleatoire(t) + "kms.");
                }
                else
                {

                }
            }
        }
    }
}
