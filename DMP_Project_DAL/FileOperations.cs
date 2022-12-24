using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMP_Project_DAL
{
    public class FileOperations
    {
        public static void foutLoggen(Exception fout,string waarInProgramma)
        {
            using (StreamWriter writer = new StreamWriter("foutenbestand.txt", true))
            {
                // true wil zeggen : vanachter toevoegen (append op true)
                writer.WriteLine(DateTime.Now.ToString("HH:mm:ss"));
                writer.WriteLine(waarInProgramma);
                writer.WriteLine(fout.GetType().Name);
                writer.WriteLine(fout.Message);
                writer.WriteLine(fout.StackTrace);
                writer.WriteLine(new string('-', 80));
                writer.WriteLine();
            }
        }
    }
}
