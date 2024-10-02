using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace L5
{
    class Journal
    {
        public string title { get; set; }
        public string bankname { get; set; }
        public string accountnumber { get; set; }
        public double price { get; set; }
        public double percentage { get; set; }

        public int numberofOrders { get; set; }

        /// <summary>
        /// constructor with parameters
        /// </summary>
        /// <param name="title">
        /// title of journal</param>
        /// <param name="price">
        /// price of journal</param>
        /// <param name="bankname">
        /// name of the bank</param>
        /// <param name="accountnumber">
        /// account number</param>
        /// <param name="percentage">
        /// how many percents bank will take 
        /// from earned money of journal</param>
        public Journal(string title,
            double price,
            string bankname, 
            string accountnumber,
            double percentage)
        {
            this.title = title;
            this.price = price;
            this.bankname = bankname;
            this.accountnumber = accountnumber;
            this.percentage = percentage;
        }
        /// <summary>
        /// constructor withour parameters
        /// </summary>
        public Journal()
        {

        }

        /// <summary>
        /// Method ToString which 
        /// provides all information about journal
        /// </summary>
        /// <returns>title of journal
        /// price of journal
        /// name of the bank
        /// account number
        /// how many percents bank will take 
        /// from earned money of journal</returns>
        public override string ToString()
        {
            return String.Format("{0,-10}  {1,-9} {2,-10} " +
                "{3,-10}  {4,-13}", title,
                price,
                bankname,
                accountnumber,
                percentage);
        }

        /// <summary>
        /// Method for getting money of 
        /// journal without percents
        /// </summary>
        /// <returns>money of 
        /// journal without percents</returns>
        public double GetTotalMoney()
        {
            return price * numberofOrders - 
              (price * numberofOrders * percentage / 100);
        }
       
    }
}
