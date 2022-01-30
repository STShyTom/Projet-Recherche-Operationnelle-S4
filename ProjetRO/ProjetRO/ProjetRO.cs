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
            Tournee t = new Tournee();
            Ville[] villes = t.rentrerVille();

            Console.WriteLine("Liste des villes :");
            foreach (Ville ville in villes)
            {
                // Use a tab to indent each line of the file.
                Console.WriteLine("\t" + ville);
            }

            Console.WriteLine("Appuyez sur n'importe quelle touche pour quitter.");
            System.Console.ReadKey();
        }
    }
}
