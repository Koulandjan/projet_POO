using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_POO2
{
    internal class DistanceManhatan : Distance
    {
        public override double CalculerDistance(Graine a, Graine b)
        {
            double[] tab1 =
          {
             a.Area,
             a.Perimeter,
             a.Compactness,
             a.Kernel_Length,
             a.Kernel_Width,
             a.Asymmetry_Coefficient,
             a.Groove_Length
           };

            double[] tab2 =
         {
             b.Area,
             b.Perimeter,
             b.Compactness,
             b.Kernel_Length,
             b.Kernel_Width,
             b.Asymmetry_Coefficient,
             b.Groove_Length
          };

            double somme = 0;

            for (int i = 0; i < tab1.Length; i++)
            {
                somme += Math.Abs(tab1[i] - tab2[i]);
            }

            return somme;
        }
    }
}
