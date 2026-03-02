using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Projet_POO2
{
    internal class Graine
    {
        public string Variety { get; set; }
        public double Area { get; set; }
        public double Perimeter { get; set; }
        public double Compactness { get; set; }
        public double Kernel_Length { get; set; }
        public double Kernel_Width { get; set; }
        public double Asymmetry_Coefficient { get; set; }
        public double Groove_Length { get; set; }

        public Graine(string variety, double area, double perimeter, double compactness, double kernel_Lengt, double kernel_width, double asymmetry_coefficient, double groove_length)
        {
            this.Variety = variety;
            this.Area = area;
            this.Perimeter = perimeter;
            this.Compactness = compactness;
            this.Kernel_Length = kernel_Lengt;
            this.Kernel_Width = kernel_width;
            this.Asymmetry_Coefficient = asymmetry_coefficient;
            this.Groove_Length = groove_length;

        }

        public string variety
        {
            get
            {
                return this.Variety;
            }
            set
            {
                this.Variety = value;
            }
        }


        public static List<Graine> ChargerDonnees(string chemin)
        {
            List<Graine> liste = new List<Graine>();
            string[] lignes = File.ReadAllLines(chemin);

            for (int i = 1; i < lignes.Length; i++)
            {
                string[] colonnes = lignes[i].Split(';');

                Graine g = new Graine(
                    colonnes[0],
                    double.Parse(colonnes[1]),
                    double.Parse(colonnes[2]),
                    double.Parse(colonnes[3]),
                    double.Parse(colonnes[4]),
                    double.Parse(colonnes[5]),
                    double.Parse(colonnes[6]),
                    double.Parse(colonnes[7])
                );

                liste.Add(g);
            }

            return liste;
        }

        public static Graine SaisirGraine()
        {
            Console.Write("Area: ");
            double a =double.Parse(Console.ReadLine());
            Console.Write("Perymeter: ");
            double b =double.Parse(Console.ReadLine());
            Console.Write("Kompactness: ");
            double c =double.Parse(Console.ReadLine());
            Console.Write("Kernel Lenght: ");
            double d =double.Parse(Console.ReadLine());
            Console.Write("Kernel Weight: ");
            double e =double.Parse(Console.ReadLine());
            Console.Write("Asymetrie Coefficient: ");
            double f =double.Parse(Console.ReadLine());
            Console.Write("Groove Lenght: ");
            double g =double.Parse(Console.ReadLine());

            return new Graine("??", a, b, c, d, e, f, g);
        }




    }
}
