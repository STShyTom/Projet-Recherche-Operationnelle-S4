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
            Tournee t = new Tournee(); // Création d'une tournée

            Console.WriteLine("Top 80 des plus grandes villes de Côte d'Or");
            t.afficherVilles(); // Affichage sur la console de toutes les villes du top 80

            Console.WriteLine("Calcul de distance entre 2 villes");
            t.AfficherDistance();

            Console.WriteLine("\n Appuyez sur n'importe quelle touche pour quitter.");
            System.Console.ReadKey();
        }
    }
}
