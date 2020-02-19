using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FRS_Biblioteca
{
    public class VerificaProcessos
    {
        public static int GaranteApenasUmaInstancia(string nome)
        {
            //Se esta aplicação for ser executada mais de uma vez o resultado será false.
            //    Neste caso, deve ser utilizado o comando  System.Environment.Exit(0);
            Process[] localAll = Process.GetProcesses();
            var processos = from p in localAll
                            where p.ProcessName == nome
                            orderby p.ProcessName
                            select p;
            return processos.Count();
        }
    }
}