using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtividadeLemafJoseRenato.Batch
{
    static class Program
    {
        static void Main(string[] args)
        {
            string text = File.ReadAllText("Entrada.txt");
            System.Console.WriteLine("Contents of WriteText.txt = {0}", text);
            string[] lines = File.ReadAllLines(@"Entrada.txt");
            System.Console.WriteLine("Contents of WriteLines2.txt = ");
        }
    }
}
