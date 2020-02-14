using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FRS_Biblioteca
{
    public class MonitorV2
    {
        public static void ChecaMonitorV2()
        {
            Process[] localAll = Process.GetProcesses();
            foreach (var item in localAll)
            {
                if (!item.ProcessName.Contains("MonitorV2"))
                {
                    FRS_Biblioteca.Notificacao.Envianotificação("Monitor não está em execução !!!");
                }
            }
        }
    }
}