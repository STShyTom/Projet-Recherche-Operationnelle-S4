using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetRO
{
    public class Tournee<Ville> : List<Ville>
    {
        private Tournee<Ville> tvoisin;

        public Tournee()
        {

        }

        public Tournee(Tournee<Ville> tvoisin)
        {
            this.tvoisin = tvoisin;
        }
    }
}
