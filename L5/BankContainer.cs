using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace L5
{
    class BankContainer
    {
        Bank[] banks;
        public int n { get; set; }
        const int CMax = 100;

        /// <summary>
        /// constructor without parameters
        /// </summary>
        public BankContainer()
        {
            banks = new Bank[CMax];
        }

        /// <summary>
        /// Getter of bank
        /// </summary>
        /// <param name="i">index of bank</param>
        /// <returns>bank object</returns>
        public Bank Get(int i)
        {
            return banks[i];
        }

        /// <summary>
        /// method for adding bank to bank container
        /// </summary>
        /// <param name="bank"></param>
        public void Add(Bank bank)
        {
            banks[n++] = bank;
        }
    }
}
