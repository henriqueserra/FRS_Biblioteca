using System;
using System.Windows.Forms;

namespace FRS_Biblioteca
{
    public class Log
    {
        public Log(string _metodo, string _mensagem, string _exessao)
        {
            this.Metodo = _metodo;
            this.Mensagem = _mensagem;
            this.Exessao = _exessao;
            this.DataHora = DateTime.Now;
        }

        public DateTime DataHora { get; private set; }
        public string Metodo { get; private set; }
        public string Mensagem { get; private set; }
        public string Exessao { get; private set; }

        public static void GravaLog(FRS_Biblioteca.Log log)
        {
            //string comandosql = $"INSERT INTO LojaDB.dbo.Log " +
            //    $"([Data], [Metodo], [Mensagem], [Exessao]) " +
            //    $"VALUES ('{log.DataHora}', '{log.Metodo}', '{log.Mensagem }', '{log.Exessao.Replace("'", "#")}')";
            //if (FRS_Biblioteca.DB.ExecutaSql(comandosql))
            //{
            //    MessageBox.Show("Erro gerado", "Erro !", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
            //else if (true)
            //{
            //    MessageBox.Show($"Log de erro não pode ser gravado \n {log.Metodo} ", "Erro !", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
        }
    }
}