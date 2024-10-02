using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace L5
{
    class Bank
    {
        public string name { get; set; }
        const int CMax = 100;
        Journal[] journals;
        public int n { get; set; }

        /// <summary>
        /// constuctor without parameters
        /// </summary>
        public Bank()
        {
            journals = new Journal[CMax];
            name = "";
        }

        /// <summary>
        /// getter for journal
        /// </summary>
        /// <param name="i">index of nedeed journal</param>
        /// <returns>journal</returns>
        public Journal Get(int i)
        {
            return journals[i];
        }

        /// <summary>
        /// method for adding journal to class
        /// </summary>
        /// <param name="journal">class object of journal</param>
        public void Add(Journal journal)
        {
            journals[n] = journal;
            n++;
        }

        /// <summary>
        /// method for getting how much money bank 
        /// will take from earned money of journal
        /// </summary>
        /// <param name="i">index of journal</param>
        /// <returns>how much money bank 
        /// will take from earned money of journal</returns>
        public double GetReturnedMoney(int i)
        {
            return journals[i].price * journals[i].numberofOrders * 
                journals[i].percentage / 100;
        }

    }
}
