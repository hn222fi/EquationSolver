using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EquationSolver
{
    class Program
    {
        static void Main(string[] args)
        {
            do
            {
                // Rensar konsolfönstret
                Console.Clear();

                // Skriver ut en rubrik
                ConsoleGraphics.ShowHeader("Equationsolver 1.0");
                Console.WriteLine("(c) Henrik Nilsson 2014");
                Console.WriteLine();

                // Skapar en ny menu
                Menu EquationSolverMenu = new Menu(new string[]{"Skriv in en ny ekvation","Lös en ekvation","Avsluta"},new bool[]{true,false,true});
                
                // Visar menyn
                EquationSolverMenu.ShowMenu();


            } while (IsContinuing());
        }
        private static bool IsContinuing()
        {

            Console.WriteLine();
            ConsoleGraphics.ViewMessage("Tryck tangent för ny beräkning - Esc avslutar.");

            return (Console.ReadKey().Key != ConsoleKey.Escape);
        }
    }
}
