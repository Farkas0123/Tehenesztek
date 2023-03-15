using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2022Teheneszetek
{
    class Program
    {

        static bool defined = false;
        static int[] best_best_mo;
        static int[] best_akt_mo;

        //Én írtam
        static int[][] m;
        static int[] teheneszetek;
        static int[] tejuzemek;

        static bool Jobb(int[] egyik, int[] masik)
        {
            return Koltseg(egyik) < Koltseg(masik);
        }

        static int Koltseg(int[] parositas)
        {
            int koltség = 0;

            for (int i = 0; i < parositas.Count() && parositas[i] != -1; i++)
            {
                koltség += teheneszetek[i] * m[i][parositas[i]];
            }
            return koltség;
        }

        /**/
        static void Legjobb(int eleje)
        {
            bool siker = eleje == teheneszetek.Length;
            //bool levél = siker;
            bool reménytelen = defined && !Jobb(best_akt_mo, best_best_mo);

            if (siker)
            {
                if (!defined || Jobb(best_akt_mo, best_best_mo))
                {
                    best_best_mo = best_akt_mo.ToArray();
                }
                defined = true;
                return;
            }

            if (reménytelen)
            {
                return;
            }

            for (int i = 0; i < tejuzemek.Length; i++)
            {
                if (teheneszetek[eleje] <= tejuzemek[i])
                {
                    best_akt_mo[eleje] = i;
                    tejuzemek[i] -= teheneszetek[eleje];

                    Legjobb(eleje + 1);

                    best_akt_mo[eleje] = -1;
                    tejuzemek[i] += teheneszetek[eleje];
                }
            }
        }
        /**/


        static void Main(string[] args)
        {
            // beolvasás
            string[] sortomb = Console.ReadLine().Split(' ');
            int N = int.Parse(sortomb[0]);
            int M = int.Parse(sortomb[1]);

            teheneszetek = Console.ReadLine().Split(' ').Select(int.Parse).ToArray();
            tejuzemek = Console.ReadLine().Split(' ').Select(int.Parse).ToArray();


            m = new int[N][];
            for (int i = 0; i < N; i++)
            {
                m[i] = Console.ReadLine().Split(' ').Select(int.Parse).ToArray();
            }

            #region koltség teszt
            /** /
            int[] nev = new int[3];
            nev[0] = 0;
            nev[1] = 1;
            nev[2] = 0;

            Console.WriteLine(Koltseg(teheneszetek, tejuzemek, nev, m));
            /**/
            #endregion

            // feldolgozás
            best_akt_mo = Enumerable.Repeat(-1,N).ToArray();
            
            Legjobb(0);

            Console.WriteLine(Koltseg(best_best_mo));
            for (int i = 0; i < N; i++)
            {
                Console.Write(best_best_mo[i]+1+" ");
            }

            #region elemek 
            /** /
            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < M; j++)
                {
                    Console.Write(m[i][j]);
                    Console.Write(" ");
                }
                Console.WriteLine();
            }
            /**/
            #endregion


            Console.Error.WriteLine();

            // kiírás
        }
    }
}