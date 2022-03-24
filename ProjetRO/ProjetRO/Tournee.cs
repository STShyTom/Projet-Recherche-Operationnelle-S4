using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetRO
{
    // Createur : Thomas Huguenel
    public class Tournee<Ville> : List<Ville>
    {
        private Tournee<Ville> tvoisin;

        private double cout;
        public double Cout
        {
            get => cout;
            set
            {
                cout = value;
            }
        }

        public Tournee()
        {

        }

        public Tournee(Tournee<Ville> tvoisin)
        {
            this.tvoisin = tvoisin;
        }
    }
}
