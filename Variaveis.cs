using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Threading;

namespace FRS_Biblioteca
{
    public class Variaveis
    {
        public static string ObtemConnectionString()
        {
            var configuracao = ConfigurationManager.AppSettings;
            return configuracao["SQL Server"];
        }

        public static string ObtemOrigem()
        {
            var configuracao = ConfigurationManager.AppSettings;
            return configuracao["Origem"];
        }

        public static string ObtemDestino()
        {
            var configuracao = ConfigurationManager.AppSettings;
            return configuracao["Destino"];
        }

        public static List<string> GetListaUsuarios()
        {
            string comando = "SELECT usuario FROM dbo.Usuarios WHERE Ativo = 1 order by usuario";
            return FRS_Biblioteca.DB.ExecutaSQLparalista(comando);
        }

        public static string GetSeparadorNumerico()
        {
            CultureInfo current = Thread.CurrentThread.CurrentUICulture;

            if (current.NumberFormat.NumberDecimalSeparator == ",")
            {
                return " VÍRGULA ";
            }
            else
            {
                return " PONTO ";
            }
        }
    }
}