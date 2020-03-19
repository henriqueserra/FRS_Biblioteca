using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FRS_Biblioteca.Tabelas
{
    [Table("Vendaveis")]
    public class Vendaveis
    {
        [ExplicitKey]
        public string Vendavel { get; set; }

        public string Categoria { get; set; }
        public string Subcategoria { get; set; }
        public string Tamanho { get; set; }
        public string Codigo { get; set; }
        public bool Ativo { get; set; }
        public double Saldo { get; set; }
        public bool IsInsumo { get; set; }
        public bool IsSubproduto { get; set; }

        public static Vendaveis Get_Vendavel(string vendavel)
        {
            using (SqlConnection conexaoBD = new SqlConnection(FRS_Biblioteca.Variaveis.ObtemConnectionString()))
            {
                return conexaoBD.Get<Vendaveis>(vendavel);
            }
        }

        public static bool Set_Saldo_Pro_Venda(string vendavel)
        {
            using (SqlConnection conexaoBD = new SqlConnection(FRS_Biblioteca.Variaveis.ObtemConnectionString()))
            {
                var repo = conexaoBD.Get<Vendaveis>(vendavel);
                repo.Saldo = repo.Saldo - 1;
                var resultado = conexaoBD.Update<Vendaveis>(repo);
                return resultado;
            }
        }
    }
}