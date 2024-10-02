using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Messaging;

namespace L5
{
    internal class Program
    {
        const string Cfn = "L5_10.txt";
        const string Cfr = "Results.txt";
        static void Main(string[] args)
        {
            using (StreamWriter writer = new StreamWriter(Cfr, false))
            {

            }
            JournalContainer journals = new JournalContainer();
            Read(Cfn, ref journals);
            TotalNumberOfOrders(journals);
            Print(journals, Cfr, "Information about journals");
            PrintOrders(journals, Cfr);

            int minInd = IndexOfUnpopularJournal(journals);
            using (StreamWriter writer = new StreamWriter(Cfr, true))
            {
                writer.WriteLine("Information about the most unpopular journal:");
                writer.WriteLine("-----------------------------" +
                    "--------------------------------\n" +
                                "|   Name   |  Price  |Bank Name " +
                                "|  Account  |  Percentage  |\n" +
                                "---------------------------------" +
                                "----------------------------");
                writer.WriteLine("|" + 
                    journals.Get(minInd).ToString() + "|");
                writer.WriteLine("------------------------------------------" +
                    "-------------------\n");
            }

            JournalContainer richestjournals = new JournalContainer();
            double maxMoney = RichestJournals(journals, ref richestjournals);
            Print(richestjournals, Cfr, String.Format("Information about " +
                " the richests journals (money earned {0:f2})", maxMoney));

            BankContainer banks = new BankContainer();
            Banks(journals, ref banks);
            PrintBanks(banks, Cfr);

        }

        /// <summary>
        /// Method for reading data from text file
        /// </summary>
        /// <param name="fd">name of input file</param>
        /// <param name="A">container object of journals</param>
        static void Read(string fd, ref JournalContainer A)
        {
            string line, title, accountnumber, bankname;
            double price, percentage;
            Journal journal1;
            using (StreamReader reader = new StreamReader(fd))
            {
                line = reader.ReadLine();
                string[] parts = line.Split(' ');
                A.numberJournals = int.Parse(parts[0]);
                A.m = int.Parse(parts[1]);

                for (int i = 0; i < A.numberJournals; i++)
                {
                    line = reader.ReadLine();
                    parts = line.Split(';');

                    title = parts[0].Trim();
                    price = double.Parse(parts[1]);
                    bankname = parts[2].Trim();
                    accountnumber = parts[3].Trim();
                    percentage = double.Parse(parts[4]);
                    journal1 = new Journal(title,
                        price,
                        bankname,
                        accountnumber,
                        percentage);
                    A.Append(journal1);
                }

                for (int i = 0; i < A.numberJournals; i++)
                {
                    line = reader.ReadLine();
                    parts = line.Split(' ');
                    for (int j = 0; j < A.m; j++)
                    {
                        A.SetWWW(i, j, Convert.ToInt32(parts[j]));
                    }
                }

            }
        }

        /// <summary>
        /// Method for printing printing information about journals
        /// </summary>
        /// <param name="A">container object of journals</param>
        /// <param name="result">name of result text file</param>
        /// <param name="header">header</param>
        static void Print(JournalContainer A, string result, string header)
        {
            using (StreamWriter writer = new StreamWriter(result, true))
            {
                writer.WriteLine(header);
                writer.WriteLine("--------------------------------" +
                    "-----------------------------\n" +
                                 "|   Name   |  Price  |Bank Name |" +
                                 "  Account  |  Percentage  |\n" +
                                 "------------------------------------" +
                                 "-------------------------");
                for (int i = 0; i < A.numberJournals; i++)
                {
                    writer.WriteLine("|" + A.Get(i).ToString() + "|");
                }
                writer.WriteLine("---------------------" +
                    "----------------------------------------\n");
            }
        }

        /// <summary>
        /// Method for filling the variable 
        /// total number of orders of journal from matrix
        /// </summary>
        /// <param name="A">container objects of journals</param>
        static void TotalNumberOfOrders(JournalContainer A)
        {
            for (int i = 0; i < A.numberJournals; i++)
            {
                int sum = 0;
                for (int j = 0; j < A.m; j++)
                {
                    sum += A.GetWWW(i, j);
                }
                A.Get(i).numberofOrders = sum;
            }
        }

        /// <summary>
        /// Method for printing matrix of orders and 
        /// total number of orders for each journal
        /// </summary>
        /// <param name="A">container object of journals</param>
        /// <param name="result">result file</param>
        static void PrintOrders(JournalContainer A, string result)
        {
            using (StreamWriter writer = new StreamWriter(result, true))
            {
                for (int i = 0; i < A.numberJournals; i++)
                {
                    writer.Write("{0}|      ", i + 1);
                    for (int j = 0; j < A.m; j++)
                    {
                        writer.Write(A.GetWWW(i, j) + " ");
                    }
                    writer.WriteLine("      |Total number of orders of {0}" +
                        " journal {1}", A.Get(i).title,
                        A.Get(i).numberofOrders);
                }
                writer.WriteLine();
            }
        }

        /// <summary>
        /// Method for getting the index 
        /// of the most unpopular journal
        /// </summary>
        /// <param name="A">container object of journals</param>
        /// <returns>index of the most unpopular journal</returns>
        static int IndexOfUnpopularJournal(JournalContainer A)
        {
            int min = A.Get(0).numberofOrders;
            int minInd = 0;
            for (int i = 1; i < A.numberJournals; i++)
            {
                if (A.Get(i).numberofOrders <= min)
                {
                    min = A.Get(i).numberofOrders;
                    minInd = i;
                }
            }
            return minInd;
        }

        /// <summary>
        /// Method for filling container object of journals
        /// only with the most richest journals
        /// </summary>
        /// <param name="A">container object of journals</param>
        /// <param name="result">container object 
        /// of the richest journals</param>
        /// <returns>money of the most richest journal</returns>
        static double RichestJournals(JournalContainer A,
            ref JournalContainer result)
        {
            double sum = 0;
            for (int i = 0; i < A.numberJournals; i++)
            {
                double calculation =  A.Get(i).price * A.Get(i).numberofOrders -
                    (A.Get(i).price * A.Get(i).numberofOrders *
                    A.Get(i).percentage / 100);
                if (calculation > sum)
                {
                    result = new JournalContainer();
                    Journal temp = new Journal(A.Get(i).title,
                        A.Get(i).price,
                        A.Get(i).bankname,
                        A.Get(i).accountnumber,
                        A.Get(i).percentage);
                    result.numberJournals++;
                    temp.numberofOrders = A.Get(i).numberofOrders;
                    result.Append(temp);
                    sum = calculation;
                }
                else
                {
                    if (calculation == sum)
                    {
                        Journal temp = new Journal(A.Get(i).title, 
                            A.Get(i).price,
                            A.Get(i).bankname,
                            A.Get(i).accountnumber,
                            A.Get(i).percentage);
                        result.numberJournals++;
                        temp.numberofOrders = A.Get(i).numberofOrders;
                        result.Append(temp);
                    }
                }


            }
            return sum;
        }

        /// <summary>
        /// Method for getting all banks names
        /// </summary>
        /// <param name="journals">container object of journals</param>
        /// <param name="banks">container object of banks</param>
        /// <param name="counter">counter of result array</param>
        /// <returns>string array with banks names</returns>
        static string[] GetBankNames(JournalContainer journals,
            BankContainer banks,
            ref int counter)
        {
            string[] names = new string[100];
            for (int i = 0; i < journals.numberJournals; i++)
            {
                bool contains = false;
                for (int j = 0; j < banks.n; j++)
                {
                    if (banks.Get(j).Get(0) != null)
                    {
                        if (banks.Get(j).Get(0).bankname == 
                            journals.Get(i).bankname)
                        {
                            contains = true;
                        }
                    }
                }

                if (!contains)
                {
                    Bank temp = new Bank();
                    temp.name = journals.Get(i).bankname;
                    Journal temp2 = new Journal(journals.Get(i).title, 
                        journals.Get(i).price,
                        journals.Get(i).bankname,
                        journals.Get(i).accountnumber,
                        journals.Get(i).percentage);
                    temp.Add(temp2);
                    banks.Add(temp);
                    names[counter++] = journals.Get(i).bankname;

                }
            }
            return names;
        }

        /// <summary>
        /// Method for filling container object of banks
        /// </summary>
        /// <param name="journals">container object of journals</param>
        /// <param name="banks">container object of banks</param>
        static void Banks(JournalContainer journals, ref BankContainer banks)
        {
            int counter = 0;
            string[] names = GetBankNames(journals, banks, ref counter);
            banks = new BankContainer();
            Bank bank = new Bank();
            for (int i = 0; i < counter; i++)
            {
                bank = new Bank();
                bank.name = names[i];
                for (int j = 0; j < journals.numberJournals; j++)
                {

                    if (journals.Get(j).bankname == bank.name)
                    {
                        Journal temp2 = new Journal(journals.Get(j).title,
                            journals.Get(j).price, 
                            journals.Get(j).bankname,
                            journals.Get(j).accountnumber, 
                            journals.Get(j).percentage);
                        temp2.numberofOrders = journals.Get(j).numberofOrders;
                        bank.Add(temp2);

                    }
                }

                banks.Add(bank);
            }
        }

        /// <summary>
        /// Method for printing all information about banks
        /// </summary>
        /// <param name="banks"></param>
        /// <param name="result"></param>
        static void PrintBanks(BankContainer banks, string result)
        {
            using (StreamWriter writer = new StreamWriter(result, true))
            {
                for (int i = 0; i < banks.n; i++)
                {
                    writer.WriteLine("Name of the bank {0}", banks.Get(i).name);
                    writer.WriteLine("----------------------------------" +
                        "------------------\n" +
                                     "|   Name   |  Price  |  Account  |" +
                                     "  Returned Money |\n" +
                                     "----------------------------------" +
                                     "------------------");
                    for (int j = 0; j < banks.Get(i).n; j++)
                    {

                        writer.WriteLine("|{0,-10}  {1,-9}  {2,-10} " +
                            "{3,-16}|", banks.Get(i).Get(j).title,
                            banks.Get(i).Get(j).price,
                            banks.Get(i).Get(j).accountnumber,
                            banks.Get(i).GetReturnedMoney(j));

                    }

                    writer.WriteLine("-----------------------" +
                        "-----------------------------\n");
                }
            }
        }
    }
}

