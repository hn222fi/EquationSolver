using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EquationSolver
{
    class Menu
    {
        // Konstruktor
        public Menu(string[] options, bool[] chooseble)
        {
            // Kontrollerar så att options och chooseble är lika långa
            if (options.Length != chooseble.Length)
                throw new ArgumentException("Antalet val och om de är valbara eller inte är inte lika.");
            
            // Skapar en Optionslista
            Options = new List<MenuOption>();

            // Skapar ett nytt valobjekt med namnet för varje stäng i options
            foreach (string option in options)
                Options.Add(new MenuOption(option));

            for(int i = 0; i < Options.Count;i++)
            {
                Options[i].Choosable = chooseble[i];
            }
        }

        // Egenskaper
        public List<MenuOption> Options { get; set; }

        // Metoder
        public void ShowMenu()
        {
            Console.WriteLine("Välj ett av följande alternativ");
            Console.WriteLine("-------------------------------");
            for(int i = 0; i < Options.Count;i++)
            {
                if (Options[i].Choosable)
                    Console.WriteLine("{0}:{1}", i+1, Options[i].OptionName);
            }
        }
       
    }
}
