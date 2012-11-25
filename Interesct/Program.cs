using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interesct
{
    class Program
    {
        static void Main(string[] args)
        {

            Console.WriteLine(" ");
            string[] moviesInDB = System.IO.File.ReadAllLines(@"C:\Users\Kal-El\Desktop\movies2.txt");
            string[] moviesFromList = System.IO.File.ReadAllLines(@"C:\Users\Kal-El\Desktop\moviesDB.txt");

            List<string> differences = moviesInDB.Where(x => moviesFromList.All(x1 => x1.Trim() != x.Trim()))
            .Union(moviesFromList.Where(x => moviesInDB.All(x1 => x1.Trim() != x.Trim()))).ToList();
            StringBuilder sb = new StringBuilder();

            sb.Append("Differences " + Environment.NewLine + "--------------------" + Environment.NewLine);


            foreach (string s in differences)
            {
                sb.Append(s + Environment.NewLine);
            }

            using (StreamWriter outfile = new StreamWriter(@"C:\Users\Kal-El\Desktop\diff.txt"))
            {
                outfile.Write(sb.ToString());
            }
        }
    }
}
