using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EquationSolver
{
    class Menu
    {
        // Fält
        private List<MenuOption> _choosebleOptions;

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

            // Initerar fältet ChoosebleOptions
            _choosebleOptions = new List<MenuOption>(); 
        }

        // Egenskaper
        public List<MenuOption> Options { get; set; }
        public List<MenuOption> ChoosebleOptions
        {
            get 
            {
                _choosebleOptions.Clear();

                foreach (MenuOption option in Options)
                {
                    if (option.Choosable)
                        _choosebleOptions.Add(option);
                }

                return _choosebleOptions;
            }
        }
        public MenuOption ChoosenOption { get; private set; }
        // Metoder
        /// <summary>
        /// Visar alla menualternativ som är valbara
        /// </summary>
        public void ShowMenu()
        {
            Console.WriteLine("Välj ett av följande alternativ");
            Console.WriteLine("-------------------------------");

            int i = 1;

            foreach (MenuOption option in ChoosebleOptions)
            {
                    Console.WriteLine("{0}:{1}", i++, option.OptionName);
            }
        }
        /// <summary>
        /// Reads a choice from the consolewindow and stores the choice in property ChoosenOption 
        /// </summary>
        public void MakeChoice()
        {
            // Deklarerar lokala variabler
            string readChoice = "";
            int choice = 0;

            while (true)
            {
                try
                {
                    // Läser in från consolfönstret
                    readChoice = Console.ReadLine();

                    // Försöker omvandla inläst sträng till ett heltal
                    choice = int.Parse(readChoice);

                    // Kontrollerar så att heltalet ligger i rätt intervall
                    if (choice < 1 || choice > Options.Count(x => x.Choosable == true))
                        throw new IndexOutOfRangeException();

                    // Lagrar det valda alternativet i egenskapen ChoosenOption
                    ChoosenOption = ChoosebleOptions[choice - 1];

                    // Avslutar whileloopen
                    Console.WriteLine();
                    break;
                }
                catch
                {
                    ConsoleGraphics.ViewMessage(String.Format("Välj ett tal mellan 1 och {0}", Options.Count(x => x.Choosable == true)), ConsoleColor.Red);
                }
            }

        }
       
    }
}
