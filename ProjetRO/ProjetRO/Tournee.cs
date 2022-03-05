using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetRO
{
    public class Tournee
    {
        private Gestion g = new Gestion();
        private Ville[] villes = g.rentrerVille(); //Liste des villes

        public Tournee(Ville[] tourneeVilles)
        {
            this.villes = tourneeVilles;
        }

        public void TourneeCroissante()
        {
            foreach(Ville v in villes)
            {

            }
        }
    }
}
