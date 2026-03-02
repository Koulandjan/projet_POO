using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_POO2
{
    public class JsonDonnee
    {
        public Parametres parametres { get; set; }
        public DatasetInfo datasets { get; set; }
        public Evaluation evaluation { get; set; }

        public class Parametres
        {
            public int K { get; set; }
            public string Distance { get; set; }
            public DateTime DateExecution { get; set; }
        }

        public class DatasetInfo
        {
            public int nombreDonneeEntrainement { get; set; }
            public int nombreDonneeTest { get; set; }
        }

        public class Evaluation
        {
            public int[][] matriceConfusion { get; set; }
            public double exactitude { get; set; }
        }
    }
}


