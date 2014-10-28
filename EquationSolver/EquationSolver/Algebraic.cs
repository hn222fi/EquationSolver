using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EquationSolver
{
    abstract class Algebraic: ICloneable
    {

        public abstract bool IsConstant { get;}

        public abstract object Clone();
       

    }
}
