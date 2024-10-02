using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace L5
{
    class JournalContainer
    {
        const int CMaxS = 1000;
        const int CMaxD = 30;
        
        Journal[] journals;
        const int Max = 100;
        private int n;
        public int numberJournals { get; set; }
        private int[,] WWW;
        public int m { get; set; }

        /// <summary>
        /// constructor withour parameters
        /// </summary>
        public JournalContainer()
        {
            journals = new Journal[Max];
            n = 0;
            m = 0;
            WWW = new int[CMaxS, CMaxD];
        }

        /// <summary>
        /// Method for setting variable to matrix element
        /// </summary>
        /// <param name="i">number of row</param>
        /// <param name="j">number of column</param>
        /// <param name="r">number of orders</param>
        public void SetWWW(int i, int j, int r) 
        { 
            WWW[i, j] = r; 
        }

        /// <summary>
        /// Method for getting element
        /// from the matrix
        /// </summary>
        /// <param name="i">number of row</param>
        /// <param name="j">number of column</param>
        /// <returns>element from the matrix</returns>
        public int GetWWW(int i, int j) 
        { 
            return WWW[i, j];
        }


        /// <summary>
        /// Method for adding journal to 
        /// container object of journals
        /// </summary>
        /// <param name="journal">journal</param>
        public void Append(Journal journal)
        {
            journals[n++] = journal;
            
        }

        /// <summary>
        /// method for getting journal from container
        /// </summary>
        /// <param name="i">index of journal</param>
        /// <returns>journal</returns>
        public Journal Get(int i)
        {
            return journals[i];
        }
    }
}
