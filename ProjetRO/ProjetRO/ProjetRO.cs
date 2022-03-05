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

            Console.WriteLine("Top 80 des plus grandes villes de Côte d'Or");
            g.afficherVilles(); // Affichage sur la console de toutes les villes du top 80

            Console.WriteLine("Calcul de distance entre 2 villes");
            g.afficherDistance(); // Affichage de la distance entre 2 villes choisies




            Console.WriteLine("\n Appuyez sur n'importe quelle touche pour quitter.");
            System.Console.ReadKey();
        }
    }
}
