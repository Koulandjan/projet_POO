using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Projet_POO2
{
    internal class ClassifieurKNN
    {
        public int K;
        public Distance distance;
        public List<Graine> dataset = new List<Graine>();

        public ClassifieurKNN(int k, Distance distance, List<Graine> dataset)
        {
            this.K = k;
            this.distance = distance;
            this.dataset = dataset;
        }



        public string Classifieur(Graine test)
        {
            //Initialiser une liste pour stocker les distance
            List<(double distance, string classe)> DistanceCalcule =
            new List<(double distance, string classe)>(); // liste pour garder les distances calculer


            //calculer la distance a l'aide de la methode "distance".
            for (int i = 0; i < dataset.Count; i++)
            {
                double d = distance.CalculerDistance(dataset[i], test);
                DistanceCalcule.Add((d, dataset[i].Variety));

            }

            //trier la distance calculee a l'aide de la methode "trie".
            Trie(DistanceCalcule);


            //-Choisir le nombre d'element suivant la valeur de k.
            //-initialiser une liste et stocker les elements dans la liste.
            List<(double distance, string classe)> kVoisin =
           new List<(double distance, string classe)>();

            for (int i = 0; i < K; i++)
            {
                kVoisin.Add(DistanceCalcule[i]);
            }



            //Initialiser un dictionnaire pour faire le vote et stocker les k majoritaire.
            Dictionary<string, int> Kmajoritaire = new Dictionary<string, int>();

            foreach (var voisin in kVoisin)
            {
                if (!Kmajoritaire.ContainsKey(voisin.classe))
                    Kmajoritaire[voisin.classe] = 1;
                else
                    Kmajoritaire[voisin.classe]++;
            }



            int maxVotes = 0;
            string classeProbable = "";

            foreach (var item in Kmajoritaire)
            {
                if (item.Value > maxVotes)
                {
                    maxVotes = item.Value;
                    classeProbable = item.Key;
                }
            }


           
            return classeProbable;
        }


        public static void Trie(List<(double distance, string classe)> list) // cette methode permet de trier la liste 
        {
            for (int i = 0; i < list.Count - 1; i++)
            {
                for (int j = 0; j < list.Count - i - 1; j++)
                {
                    if (list[j].distance > list[j + 1].distance)
                    {
                        var temp = list[j];
                        list[j] = list[j + 1];
                        list[j + 1] = temp;
                    }
                }
            }
        }

    }
}
