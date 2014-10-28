using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EquationSolver
{
    class Term : IAlgebraic
    {
        // Deklarerar privata fält
        private decimal _koefficent;
        private char _varible;

        // Konstruktorer
        /// <summary>
        /// Skapar ett termobjekt
        /// </summary>
        /// <param name="term">En sträng på formen tecken, koefficent och variabel, t.ex. "-4x"</param>
        public Term(string term)
        {
            term.Trim();
            char[] chars = term.Replace('.', ',').ToCharArray();
            string koefficent = "";
            Varible = (char)0;

            foreach (char element in chars)
            {
                if (Char.IsLetter(element))
                    Varible = element;
                else
                    koefficent += element;
            }

            if (koefficent == "-" || koefficent == "+")
                koefficent += "1";

            Koefficent = Decimal.Parse(koefficent);
        }
        /// <summary>
        /// Skapar en konstant term
        /// </summary>
        /// <param name="koefficent">Konstantens värde</param>
        public Term(decimal koefficent)
        {
            Varible = (char)0;
            Koefficent = koefficent;
        }
        // Skapar egenskaper
        /// <summary>
        /// Ger åtkomst till fältet koefficent
        /// </summary>
        public decimal Koefficent
        {
            set { _koefficent = value; }
            get { return _koefficent; }
        }
        /// <summary>
        /// Hanterar termens variabel
        /// </summary>
        public char Varible
        {
            set 
            {
                // Kontrollerar så att variablen är en bokstav
                if (!Char.IsLetter(value) && value != (char)0)
                    throw new ArgumentException("The varible of a term has to be a letter");
                
                // Tilldelar vältet _varible bokstavens värde som en liten bokstav 
                _varible = Char.ToLower(value);
            }
            get { return _varible; }
        }
        /// <summary>
        /// Egenskap som berättar om termen är en konstantterm
        /// </summary>
        public bool IsConstant
        {
            get
            {
                if (Varible == 0)
                    return true;
                else
                    return false;
            }
        }

        // Metoder

        public object Clone()
        {
            return new Term(this.ToString());
        }

        // Overriding objekt metoder
        public override bool Equals(object obj)
        {
            Term term = obj as Term;

            if ((object)term == null)
                throw new ArgumentException("Equals method need to compare two Term objects");

            if (this.Varible == term.Varible && this.Koefficent == term.Koefficent)
                return true;
            else
                return false;
        }
        public override string ToString()
        {
            string temp = "";

            if (Koefficent > 0)
                temp += "+";

            if (Koefficent != 1)
                temp += Koefficent.ToString("G29");

            if (!IsConstant)
                temp += Varible;

            return temp;
        }

        public override int GetHashCode()
        {
            int hash = 486187739;
            hash = hash * 23 + Varible.GetHashCode();
            hash = hash * 23 + Koefficent.GetHashCode();
            return hash;
        }
        // Overriding operators
        public static bool operator ==(Term t1, Term t2)
        {
            return t1.Equals(t2);
        }
        public static bool operator !=(Term t1, Term t2)
        {
            return !(t1 == t2);
        }
        public static Term operator * (Term t1, Term t2)
        {
            if (!t1.IsConstant && !t2.IsConstant)
                throw new ArgumentException("Termer kan ännu endast multipliceras med konstanter");

            Term temp = new Term(t1.ToString());

            temp.Koefficent = t1.Koefficent * t2.Koefficent;

            return temp;
        }
        public static Term operator * (Term t1, decimal dec)
        {
            Term temp = new Term(t1.ToString());

            temp.Koefficent = t1.Koefficent * dec;

            return temp;
        }
        public static Term operator -(Term t1, Term t2)
        {
            if (t1.Varible != t2.Varible)
                throw new ArgumentException("Endast lika termer kan adderas");

            Term temp = new Term(t1.ToString());

            temp.Koefficent = t1.Koefficent - t2.Koefficent;

            return temp;
        }
        public static Term operator +(Term t1, Term t2)
        {
            if (t1.Varible != t2.Varible)
                throw new ArgumentException("Endast lika termer kan adderas");

            Term temp = new Term(t1.ToString());

            temp.Koefficent = t1.Koefficent + t2.Koefficent;

            return temp;
        }

        public static Term operator / (Term t1, Term t2)
        {
            if (!t1.IsConstant && !t2.IsConstant)
                throw new ArgumentException("Termer kan ännu endast divideras med konstanter");

            Term temp = new Term(t1.ToString());

            temp.Koefficent = t1.Koefficent / t2.Koefficent;

            return temp;
        }
        public static Term operator /(Term t1, decimal dec)
        {
            Term temp = new Term(t1.ToString());

            temp.Koefficent = t1.Koefficent / dec;

            return temp;
        }


    }
}
