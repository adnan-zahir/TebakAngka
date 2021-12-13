using System;
using System.Collections.Generic;

namespace TebakAngka
{
    class Program
    {
        static void Main(string[] args)
        {
            TebakAngka game = new();
            game.Start();

            // Terminate Program
            Console.WriteLine("Press any key to exit ...");
            Console.ReadKey(true);
        }
    }

    class TebakAngka
    {
        Random random = new();
        private string angkaKunci = "";
        private int percobaan;
        private int?[] tebakan = new int?[4];
        private char tebak;
        private string angkaKosong = "_";
        private string angkaValid = "0123456789";
        private string currentTebakan = "    ";
        private int tebakanBenar, posisiBenar, posisiSalah = 0;
        public void Start()
        {
            InitValues();
            MakeAngkaKunci();
            
            while (true)
            {
                // Check if the player wins
                if (CheckWin()) break;
                
                // Reset tebakan
                ResetTebakan();
                
                while (tebakan.Contains(null))
                {
                    // Display UI to Console
                    DisplayUI();
                    // Read input
                    tebak = Console.ReadKey(true).KeyChar;
                    // Check input
                    // if a valid number
                    if (!angkaValid.Contains(tebak)) continue;
                    AssignTebakan(tebak);
                }

                CheckTebakan();

                percobaan++;
            }

            // Do this if the player wins
            DisplayWin();
            AskPlayAgain();
        }
        private void InitValues()
        {
            for (int i = 0; i < tebakan.Length; i++) tebakan[i] = null;
            angkaKunci = "";
            percobaan = 0;
            currentTebakan = "    ";
            tebakanBenar = posisiBenar = posisiSalah = 0;
        }
        private void DisplayUI()
        {
            Console.Clear();
            Console.WriteLine("----------TEBAK ANGKA----------");
            Console.WriteLine($"Percobaan : {percobaan}");
            // For debug only, uncomment the line below to see the key
            // Console.WriteLine($"Kunci     : {angkaKunci}");
            Console.WriteLine();
            Console.Write("Tebak : ");
            Console.Write(tebakan[0] == null ? angkaKosong : tebakan[0].ToString());
            Console.Write(tebakan[1] == null ? angkaKosong : tebakan[1].ToString());
            Console.Write(tebakan[2] == null ? angkaKosong : tebakan[2].ToString());
            Console.Write(tebakan[3] == null ? angkaKosong : tebakan[3].ToString());
            Console.WriteLine();
            Console.WriteLine();
            Console.Write(currentTebakan + "  : ");
            Console.WriteLine($"Angka Benar  : {tebakanBenar}   |");
            Console.WriteLine($"        Posisi Benar : {posisiBenar}   |   Posisi Salah : {posisiSalah}");
        }
        private void MakeAngkaKunci()
        {
            // init angka kunci
            while (angkaKunci.Length < 4)
            {
                int toAssign = random.Next(10);
                if (angkaKunci.Contains(Convert.ToString(toAssign))) continue;
                angkaKunci += toAssign;
            }
        }
        private void ResetTebakan()
        {
            tebakan[0] = null;
            tebakan[1] = null;
            tebakan[2] = null;
            tebakan[3] = null;
        }
        private void CheckTebakan()
        {
            tebakanBenar = posisiBenar = posisiSalah = 0;
            foreach (var current in currentTebakan.Select((angka, i) => (angka, i)))
            {
                if (angkaKunci.Contains(current.angka))
                {
                    tebakanBenar++;
                    if (angkaKunci[current.i] == current.angka) posisiBenar++;
                    posisiSalah = tebakanBenar - posisiBenar;
                }
            }
        }
        private void UpdateCurrent(int i)
        {
            if (i == 3)
            {
                currentTebakan = "";
                foreach (var angka in tebakan) currentTebakan += angka;
            }
        }
        private void AssignTebakan(int tebak)
        {
            foreach (var item in tebakan.Select((angka, i) => (angka, i)))
            {
                if (item.angka == null)
                {
                    tebakan[item.i] = tebak - '0';
                    UpdateCurrent(item.i);
                    break;
                }
            }
        }
        private bool CheckWin()
        {
            if (angkaKunci == currentTebakan) return true;
            return false;
        }
        private void DisplayWin()
        {
            Console.Clear();
            Console.WriteLine("----------TEBAK ANGKA----------");
            Console.WriteLine();
            Console.WriteLine("            YOU WIN            ");
            Console.WriteLine();
            Console.WriteLine($"The answer is      : {angkaKunci}");
            Console.WriteLine($"Number of attempts : {percobaan}");
            Console.WriteLine();
            Console.WriteLine("-------------------------------");
            Console.WriteLine();
        }
        private void AskPlayAgain()
        {
            Console.WriteLine("Play again? [Y/n]");
            char answer = Console.ReadKey(true).KeyChar;
            if (answer.ToString().ToUpper() == "N")
            {
                return;
            }
            else
            {
                Start();
            }
        }
    }
}