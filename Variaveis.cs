using System.Configuration;

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
        }
    }