using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace Projet_POO2
{
    internal class EvaluationSystem
    {
        public int nbKamaCorrect = 0, nbKamaRosa = 0, nbKamaCanadian = 0;
        public int nbRosaKama = 0, nbRosaCorrect = 0, nbRosaCanadian = 0;
        public int nbCanadianKama = 0, nbCanadianRosa = 0, nbCanadianCorrect = 0;

        public void ConstruireMatrice(ClassifieurKNN knn, List<Graine> testSet)
        {
            foreach (var graineTest in testSet)
            {
                string prediction = knn.Classifieur(graineTest);
                string vraiLabel = graineTest.Variety;

                if (vraiLabel == "Kama")
                {
                    if (prediction == "Kama") nbKamaCorrect++;
                    else if (prediction == "Rosa") nbKamaRosa++;
                    else nbKamaCanadian++;
                }
                else if (vraiLabel == "Rosa")
                {
                    if (prediction == "Kama") nbRosaKama++;
                    else if (prediction == "Rosa") nbRosaCorrect++;
                    else nbRosaCanadian++;
                }
                else
                {
                    if (prediction == "Kama") nbCanadianKama++;
                    else if (prediction == "Rosa") nbCanadianRosa++;
                    else nbCanadianCorrect++;
                }
            }
        }

        public double CalculerExactitude()
        {
            int correct = nbKamaCorrect + nbRosaCorrect + nbCanadianCorrect;

            int total =
                nbKamaCorrect + nbKamaRosa + nbKamaCanadian +
                nbRosaKama + nbRosaCorrect + nbRosaCanadian +
                nbCanadianKama + nbCanadianRosa + nbCanadianCorrect;

            return ((double)correct / total) * 100;
        }


        public int[][] Matrice()
        {
            return new int[][]
            {
        new int[] { nbKamaCorrect, nbKamaRosa, nbKamaCanadian },
        new int[] { nbRosaKama, nbRosaCorrect, nbRosaCanadian },
        new int[] { nbCanadianKama, nbCanadianRosa, nbCanadianCorrect }
            };
        }

        



    }
}
