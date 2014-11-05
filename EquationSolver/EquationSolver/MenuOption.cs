using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EquationSolver
{
    class MenuOption
    {
        // Konstruktor
        public MenuOption(string name)
        {
            OptionName = name;
        }

        // Egenskaper
        public string OptionName { get; set; }

        public bool Choosable { get; set; }
    }
}
