using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;

namespace FRS_Biblioteca
{
    public class Arquivos
    {
        public static List<String> CarregaListaArquivos()
        {
            string origem = FRS_Biblioteca.Variaveis.ObtemOrigem();
            return Directory.EnumerateFiles(origem, "*.xml", SearchOption.TopDirectoryOnly).ToList();
        }

        public static void MoveArquivo(string arquivo)
        {
            var configuracao = ConfigurationManager.AppSettings;
            var pathorigem = configuracao["Origem"] + arquivo;
            var pathdestino = configuracao["Destino"] + arquivo;
            try
            {
                File.Move(pathorigem, pathdestino);
            }
            catch (System.IO.IOException)
            {
                File.Delete(pathdestino);
                File.Move(pathorigem, pathdestino);
            }
            catch (Exception ex)
            {
                FRS_Biblioteca.Log erro = new Log("MoveArquivo", $"Erro tentando mover o arquivo de {pathdestino} para {pathorigem}", ex.ToString());
                Log.GravaLog(erro);
            }
        }

        public static bool ArquivoOk(string arquivo)
        {
            bool teste = false;
            int contagem = 1;
            while (contagem <= 5000 && teste == false)
            {
                try
                {
                    string[] balde1 = File.ReadAllLines(arquivo);
                    teste = true;
                }
                catch (IOException)
                {
                    contagem++;
                    teste = false;
                }
            }
            if (teste)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}