using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.XPath;

namespace FRS_Biblioteca.Tabelas
{
    [Table("Notas_Fiscais")]
    public class Notas_Fiscais
    {
        [ExplicitKey]
        public string nCFe { get; set; }

        public string NumeroSerieSat { get; set; }
        public DateTime Data { get; set; }
        public string DataEmissao { get; set; }
        public string HoraEmissao { get; set; }
        public string NumeroCaixa { get; set; }
        public string CNPJ { get; set; }
        public string Estabelecimento { get; set; }
        public string CPF_Destino { get; set; }
        public double ValorBruto { get; set; }
        public double ValorDesconto { get; set; }
        public double ValorFinal { get; set; }
        public double Tributos { get; set; }
        public string MeiodePagamento { get; set; }
        public double ValorMeiodePagamento { get; set; }
        public double ValorTroco { get; set; }
        public double TotalProdutos { get; set; }
        public string Arquivo { get; set; }
        public string XML { get; set; }

        public static Notas_Fiscais CarregaDados(string arquivo)
        {
            string expressao = string.Empty;
            XPathDocument documento = new XPathDocument(arquivo);
            var nota = new Notas_Fiscais();
            var navegador = documento.CreateNavigator();
            try
            {
                if (navegador.SelectSingleNode("//CFeCanc").Value.Length > 10)
                {
                    return null;
                }
            }
            catch (NullReferenceException)
            {
            }
            expressao = "//nserieSAT";
            nota.NumeroSerieSat = navegador.SelectSingleNode(expressao).Value;
            expressao = "//nCFe";
            nota.nCFe = navegador.SelectSingleNode(expressao).Value;
            expressao = "//dEmi";
            nota.DataEmissao = navegador.SelectSingleNode(expressao).Value;
            expressao = "//hEmi";
            nota.HoraEmissao = navegador.SelectSingleNode(expressao).Value;
            expressao = "//numeroCaixa";
            nota.NumeroCaixa = navegador.SelectSingleNode(expressao).Value;
            expressao = "//emit/CNPJ";
            nota.CNPJ = navegador.SelectSingleNode(expressao).Value;
            expressao = "//emit/xNome";
            nota.Estabelecimento = navegador.SelectSingleNode(expressao).Value;
            try
            {
                expressao = "//dest/CPF";
                nota.CPF_Destino = navegador.SelectSingleNode(expressao).Value;
            }
            catch (Exception)
            {
                expressao = "//dest/CNPJ";
                nota.CPF_Destino = navegador.SelectSingleNode(expressao).Value;
            }
            expressao = "//total/ICMSTot/vProd";
            nota.ValorBruto = Convert.ToDouble(navegador.SelectSingleNode(expressao).Value);
            expressao = "//total/ICMSTot/vDesc";
            nota.ValorDesconto = Convert.ToDouble(navegador.SelectSingleNode(expressao).Value);
            expressao = "//vCFe";
            nota.ValorFinal = Convert.ToDouble(navegador.SelectSingleNode(expressao).Value);
            expressao = "//vCFeLei12741";
            nota.Tributos = Convert.ToDouble(navegador.SelectSingleNode(expressao).Value);
            expressao = "/CFe/infCFe/pgto/MP/cMP";
            nota.MeiodePagamento = navegador.SelectSingleNode(expressao).Value;
            expressao = "/CFe/infCFe/pgto/MP/vMP";
            nota.ValorMeiodePagamento = Convert.ToDouble(navegador.SelectSingleNode(expressao).Value);
            expressao = "/CFe/infCFe/pgto/vTroco";
            nota.ValorTroco = Convert.ToDouble(navegador.SelectSingleNode(expressao).Value);
            expressao = "/CFe/infCFe/@Id";
            nota.Arquivo = "AD" + navegador.SelectSingleNode(expressao).Value.Substring(3) + ".xml";
            expressao = "/CFe/infCFe/det/@nItem";
            nota.TotalProdutos = Convert.ToInt32(navegador.Select(expressao).Count.ToString());
            nota.XML = File.ReadAllText(arquivo);

            nota.Data = new DateTime(Convert.ToInt32(nota.DataEmissao.Substring(0, 4)), Convert.ToInt32(nota.DataEmissao.Substring(4, 2)), Convert.ToInt32(nota.DataEmissao.Substring(6, 2)), Convert.ToInt32(nota.HoraEmissao.Substring(0, 2)), Convert.ToInt32(nota.HoraEmissao.Substring(2, 2)), Convert.ToInt32(nota.HoraEmissao.Substring(4, 2)));

            return nota;
        }

        public static bool Adiciona_Nota(Notas_Fiscais nota)
        {
            using (SqlConnection conexaoBD = new SqlConnection(FRS_Biblioteca.Variaveis.ObtemConnectionString()))
            {
                try
                {
                    var resutado1 = conexaoBD.Insert<Notas_Fiscais>(nota);

                    if (resutado1 >= 0)
                    {
                        return true;
                    }
                }
                catch (Exception ex)
                {
                    if (ex.Message.Contains("Violation of PRIMARY KEY constraint"))
                    {
                        return false;
                    }
                    throw;
                }
            }
            return false;
        }
    }
}