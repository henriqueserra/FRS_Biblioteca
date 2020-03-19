using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.XPath;

namespace FRS_Biblioteca.Tabelas
{
    [Table("Vendas")]
    public class Vendas
    {
        [Key]
        public int Id { get; set; }

        public string nCFe { get; set; }
        public DateTime Data { get; set; }
        public string Vendavel { get; set; }
        public string CodVendavel { get; set; }
        public double ValorVendavel { get; set; }
        public double ValorDesconto { get; set; }
        public double ValorVendido { get; set; }
        public bool Processado { get; set; }

        public static bool Insert_Venda(Vendas venda)
        {
            using (SqlConnection conexaoBD = new SqlConnection(FRS_Biblioteca.Variaveis.ObtemConnectionString()))
            {
                var resutado1 = conexaoBD.Insert<Vendas>(venda);
                FRS_Biblioteca.Notificacao.Envianotificação($"TD={FRS_Biblioteca.Consultas.GetTT()} TC={FRS_Biblioteca.Consultas.GetTC()} TM={FRS_Biblioteca.Consultas.GetTM()} - {venda.Vendavel} - {DateTime.Now.ToString("hh:mm")}");
                var vendavel = FRS_Biblioteca.Tabelas.Vendaveis.Get_Vendavel(venda.Vendavel);

                if (resutado1 > 0)
                {
                    return true;
                }
            }
            return false;
        }

        public static void Update_Venda(Vendas venda)
        {
            using (SqlConnection conexaoBD = new SqlConnection(FRS_Biblioteca.Variaveis.ObtemConnectionString()))
            {
                var resutado1 = conexaoBD.Update<Vendas>(venda);
            }
        }

        public static bool Get_Detalhes_Grava_Vendas_Atualiza_Saldo(FRS_Biblioteca.Tabelas.Notas_Fiscais nota, string arquivo)
        {
            string expressao = string.Empty;
            XPathDocument documento = new XPathDocument(arquivo);
            var navegador = documento.CreateNavigator();
            try
            {
                if (navegador.SelectSingleNode("//CFeCanc").Value.Length > 10)
                {
                    return false;
                }
            }
            catch (NullReferenceException)
            {
            }

            XmlDocument xml = new XmlDocument();
            xml.Load(arquivo);
            XmlNodeList xnlist = xml.SelectNodes("//prod");
            FRS_Biblioteca.Tabelas.Vendas vendas;
            foreach (XmlNode xn in xnlist)
            {
                vendas = new FRS_Biblioteca.Tabelas.Vendas();
                vendas.nCFe = nota.nCFe;
                vendas.Data = Convert.ToDateTime(nota.Data);
                vendas.Vendavel = xn.ChildNodes[1].InnerText;
                vendas.CodVendavel = xn.ChildNodes[0].InnerText;
                vendas.ValorVendavel = Convert.ToDouble(xn.ChildNodes[7].InnerText);
                vendas.ValorDesconto = Convert.ToDouble(xn.ChildNodes[9].InnerText);
                vendas.ValorVendido = Convert.ToDouble(xn.ChildNodes[10].InnerText);

                FRS_Biblioteca.Tabelas.Vendas.Insert_Venda(vendas);
                if (FRS_Biblioteca.Tabelas.Vendaveis.Set_Saldo_Pro_Venda(vendas.Vendavel))
                {
                    vendas.Processado = true;
                    FRS_Biblioteca.Tabelas.Vendas.Update_Venda(vendas);
                };
            }

            return true;
        }
    }
}