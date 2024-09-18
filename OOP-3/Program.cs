using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;

namespace OOP1
{
    internal class Program
    {
        static void Main(string[] args)
        {

            #region Opgave 4
            Console.WriteLine("Opgave 4");
            Console.WriteLine("Skriv det, du vil have krypteret");
            string alfabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZÆØÅ1234567890 ";
            string alfabetshift = "DEFGHIJKLMNOPQRSTUVWXYZÆØÅ1234567890ABC ";

            string hilsen = Console.ReadLine().ToUpper();
            var hilsenarray = hilsen.ToCharArray();

            StringBuilder result = new StringBuilder();
            for (var i = 0; i < hilsenarray.Length; i++)
            {
                result.Append(alfabetshift[Array.IndexOf(alfabet.ToCharArray(), hilsenarray[i])]);
            }

            Console.WriteLine(result);
            #endregion


            #region Opgave 3
            Console.WriteLine(" ");
            Console.WriteLine("Opgave 3");
            bool game = true;

            while (game)
            {
                int gamenumber = 1;

                // initial parameters
                bool alive = true;
                int roundnumber = 0;

                bool waiting = false;
                int playerposx = 0;
                int playerposy = 0;

                int length = 5;
                int height = 5;
                float[,] minefield = new float[5, 5];

                Random n = new Random();

                for (int x = 0; x < length; x++)
                {
                    for (int y = 0; y < height; y++)
                    {
                        minefield[x, y] = float.Round(n.NextSingle(), 3);
                        //Console.WriteLine("X " + x + " Y " + y + " " + minefield[x, y]); <-- idk what this does?? didn't write this
                    }
                }



                bool gameover = false;

                while (alive)
                {
                    //drawing borders
                    List<string> lines = new List<string>();
                    string topline = "———————";
                    Console.WriteLine($"Round {roundnumber}");
                    lines.Add(topline);

                    for (var x = 0; x < length; x++)
                    {

                        string line = "|";
                        for (var y = 0; y < height; y++)
                        {

                            // drawing symbols

                            char marker = 'o';

                            if (minefield[x, y] == minefield[playerposy, playerposx])
                            {
                                marker = 'A';
                            }


                            if (minefield[x, y] <= 0.1f && minefield[x, y] != minefield[0, 0] && minefield[x, y] != minefield[4, 4])
                            {
                                marker = '*';
                            }

                            if (minefield[playerposy, playerposx] <= 0.1f)
                            {
                                marker = '☠';
                                alive = false;
                                gameover = true;
                            }

                            line += marker;


                        }
                        line += "|";


                        lines.Add(line);

                    }
                    lines.Add(topline);
                    waiting = true;

                    // actually drawing
                    foreach (string line in lines)
                    {
                        Console.WriteLine(line);
                    }

                    if (minefield[playerposx, playerposy] == minefield[4, 4])
                        alive = false;

                    // checking for death
                    if (!alive)
                    {

                        if (gameover)
                            Console.WriteLine("You died.");
                        else
                            Console.WriteLine("You won!");
                        string restartinput = "somethingrandom";

                        bool restarted = false;
                        while (!restarted)
                        {
                            if (restartinput == "r")
                            {
                                playerposx = 0;
                                playerposy = 0;
                                roundnumber = 0;
                                restarted = true;
                                gamenumber += 1;
                            }
                            else if (restartinput == "EndGame")
                            {
                                restarted = true;
                                game = false;
                            }
                            else
                            {
                                Console.WriteLine("Write 'r' to restart");
                                Console.WriteLine("Write 'EndGame' if you want to leave");
                                restartinput = Console.ReadLine();
                            }

                        }


                        waiting = false;
                    }

                    string input = "somethingrandom";

                    // getting player input
                    while (waiting)
                    {
                        Console.WriteLine("Write up, down, left, right?");

                        if (input == "up" && playerposy != 0)
                        {
                            playerposy -= 1;
                            waiting = false;
                        }
                        else if (input == "down" && playerposy != 4)
                        {
                            playerposy += 1;
                            waiting = false;
                        }
                        else if (input == "left" && playerposx != 0)
                        {
                            playerposx -= 1;
                            waiting = false;
                        }
                        else if (input == "right" && playerposx != 4)
                        {
                            playerposx += 1;
                            waiting = false;
                        }
                        else
                        {
                            input = Console.ReadLine();
                        }


                    }
                    roundnumber += 1;
                }
            }
            #endregion
            Console.WriteLine(" ");
            Console.WriteLine("Opgave 2");
            #region Opgave 2
            Console.WriteLine("Skriv for hver dag, hvor meget nedbør der faldt.");
            int[] vejr = new int[7];

            bool manglertal = true;
            while (manglertal)
            {
                for (int i = 0; i < vejr.Length; i++)
                {
                    Console.WriteLine($"Dag {i + 1}");
                    bool dag = Int32.TryParse(Console.ReadLine(), out int tal);
                    while (!dag)
                    {
                        Console.WriteLine("Prøv igen");
                        dag = Int32.TryParse(Console.ReadLine(), out tal);
                    }
                    vejr[i] = tal;
                }
                manglertal = false;
            }
            for (int i = 0; i < vejr.Length; i++)
            {
                Console.WriteLine($"dag {i + 1}: {vejr[i]}");
            }
            Console.WriteLine("Gennemsnittet er: " + vejr.Average());
            Console.WriteLine("Der faldte mindst regn: " + vejr.Min() + ", på dag " + Array.IndexOf(vejr, vejr.Min()));
            Console.WriteLine("Der faldte mest regn: " + vejr.Max() + ", på dag " + Array.IndexOf(vejr, vejr.Max()));
            #endregion

            Console.WriteLine(" ");
            Console.WriteLine("Opgave 1");
            #region Opgave 1
            Console.WriteLine("Hvor mange terninger vil du slå?");
            bool antalterninger = Int32.TryParse(Console.ReadLine(), out int antal);
            while (!antalterninger)
            {
                Console.WriteLine("Prøv igen");
                antalterninger = Int32.TryParse(Console.ReadLine(), out antal);
            }

            int[] terningresultat = new int[10000];
            int[] mangeterninger = new int[antal];

            int terningmin = 1;
            int terningmax = 7;

            int[] frekvens = new int[mangeterninger.Length * terningmax - (mangeterninger.Length - 1)];

            Random r = new Random();
            int RollDice()
            {
                return r.Next(terningmin, terningmax);
            }

            Console.WriteLine("Begynd");

            for (int i = 0; i < terningresultat.Length; i++)
            {
                for (int j = 0; j < antal; j++)
                {
                    mangeterninger[j] = RollDice();
                }
                int resultat = (mangeterninger.Sum());
                terningresultat[i] = resultat;
                frekvens[resultat]++;
            }
            for (int i = terningmin * antal; i < frekvens.Length; i++)
            {
                Console.WriteLine($"{i}: {frekvens[i]}");
            }
            Console.WriteLine("Gennemsnittet er: " + terningresultat.Average());
            Console.WriteLine("Summen er: " + terningresultat.Sum());

            #endregion
        }
    }
}