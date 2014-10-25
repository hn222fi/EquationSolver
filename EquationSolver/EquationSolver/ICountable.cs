using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EquationSolver
{
    interface ICountable
    {
        ICountable Add
        { }
        ICountable Subtract
        { }
        ICountable Multiply
        { }
        ICountable Divide
        { }
    }
}
