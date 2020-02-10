using System;
using System.IO;
using System.Xml;
using System.Xml.XPath;

namespace FRS_Biblioteca
{
    public class Notas
    {
        /// <summary>
        /// Construtor da classe notas
        /// </summary>
        /// <remarks>É obrigatório informar o nome do arquivo (path+arquivo) para gerar uma instância da classe Notas</remarks>
        public Notas(string _arquivo)
        {
            Arquivo = _arquivo;
        }

        public string Arquivo { get; private set; }
        public string Arquivonome { get; private set; }
        public string NumeroSerieSat { get; private set; }
        public string NCFe { get; private set; }
        public string DataEmissao { get; private set; }
        public string HoraEmissao { get; private set; }
        public string NumeroCaixa { get; private set; }
        public string CNPJ { get; private set; }
        public string Estabelecimento { get; private set; }
        public string CPF_Destino { get; private set; }
        public string ValorBruto { get; private set; }
        public string ValorDesconto { get; private set; }
        public string ValorFinal { get; private set; }
        public string Tributos { get; private set; }
        public string MeiodePagamento { get; private set; }
        public string ValorMeiodePagamento { get; private set; }
        public string ValorTroco { get; private set; }
        public string Data { get; private set; }
        public string TotalProdutos { get; private set; }
        public string XML { get; private set; }

        public static bool CarregaDados(FRS_Biblioteca.Notas nota)
        {
            string expressao = string.Empty;
            XPathDocument documento = new XPathDocument(nota.Arquivo);
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
            expressao = "//nserieSAT";
            nota.NumeroSerieSat = navegador.SelectSingleNode(expressao).Value;
            expressao = "//nCFe";
            nota.NCFe = navegador.SelectSingleNode(expressao).Value;
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
            nota.ValorBruto = navegador.SelectSingleNode(expressao).Value;
            expressao = "//total/ICMSTot/vDesc";
            nota.ValorDesconto = navegador.SelectSingleNode(expressao).Value;
            expressao = "//vCFe";
            nota.ValorFinal = navegador.SelectSingleNode(expressao).Value;
            expressao = "//vCFeLei12741";
            nota.Tributos = navegador.SelectSingleNode(expressao).Value;
            expressao = "/CFe/infCFe/pgto/MP/cMP";
            nota.MeiodePagamento = navegador.SelectSingleNode(expressao).Value;
            expressao = "/CFe/infCFe/pgto/MP/vMP";
            nota.ValorMeiodePagamento = navegador.SelectSingleNode(expressao).Value;
            expressao = "/CFe/infCFe/pgto/vTroco";
            nota.ValorTroco = navegador.SelectSingleNode(expressao).Value;
            expressao = "/CFe/infCFe/@Id";
            nota.Arquivonome = "AD" + navegador.SelectSingleNode(expressao).Value.Substring(3) + ".xml";
            expressao = "/CFe/infCFe/det/@nItem";
            nota.TotalProdutos = navegador.Select(expressao).Count.ToString();
            nota.XML = File.ReadAllText(nota.Arquivo);
            nota.Data = ConverteDataeHora(nota);

            return true;
        }

        private static string ConverteDataeHora(FRS_Biblioteca.Notas nota)
        {
            string temp1 = nota.DataEmissao.Remove(4) + "-" + nota.DataEmissao.Substring(4, 2) + "-" + nota.DataEmissao.Substring(6, 2);
            string temp2 = nota.HoraEmissao.Substring(0, 2) + ":" + nota.HoraEmissao.Substring(2, 2) + ":" + nota.HoraEmissao.Substring(4, 2);
            return temp1 + " " + temp2;
        }

        public static string GeraComandoInclusao(FRS_Biblioteca.Notas nota)
        {
            string temp1 = "INSERT INTO [LojaDB].[dbo].[Notas_Fiscais] ([nCFe],[TotalProdutos],[NumeroSerieSat],[Data],[DataEmissao],[HoraEmissao], [NumeroCaixa], [CNPJ],[Estabelecimento],[CPF_Destino],[ValorBruto],[ValorDesconto],[ValorFinal],[Tributos],[MeiodePagamento],[ValorMeiodePagamento],[ValorTroco],[Arquivo],[XML]) ";
            string temp2 = ("VALUES ('" +
                        nota.NCFe
                + "', '" + nota.TotalProdutos
                + "', '" + nota.NumeroSerieSat
                + "', '" + nota.Data
                + "', '" + nota.DataEmissao
                + "', '" + nota.HoraEmissao
                + "', '" + nota.NumeroCaixa
                + "', '" + nota.CNPJ
                + "', '" + nota.Estabelecimento
                + "', '" + nota.CPF_Destino
                + "', '" + nota.ValorBruto
                + "', '" + nota.ValorDesconto
                + "', '" + nota.ValorFinal
                + "', '" + nota.Tributos
                + "', '" + nota.MeiodePagamento
                + "', '" + nota.ValorMeiodePagamento
                + "', '" + nota.ValorTroco
                + "', '" + nota.Arquivonome
                + "', '" + nota.XML
                + "')");
            string comando = temp1 + temp2;

            return comando;
        }

        public static bool CarregaeGravaDetalhes(FRS_Biblioteca.Notas nota)
        {
            string expressao = string.Empty;
            XPathDocument documento = new XPathDocument(nota.Arquivo);
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
            xml.Load(nota.Arquivo);
            XmlNodeList xnlist = xml.SelectNodes("//prod");
            string cProd, xProd, vProd, vDesc, vItem;
            foreach (XmlNode xn in xnlist)
            {
                cProd = xn.ChildNodes[0].InnerText;
                xProd = xn.ChildNodes[1].InnerText;
                vProd = xn.ChildNodes[7].InnerText;
                vDesc = xn.ChildNodes[9].InnerText;
                vItem = xn.ChildNodes[10].InnerText;
                FRS_Biblioteca.DB.GravaProdutosVendidos(nota.NCFe, nota.Data, xProd, cProd, vProd, vDesc, vItem);
            }

            return true;
        }
    }
}