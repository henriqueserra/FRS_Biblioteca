using Dapper;
using System;
using System.Data.SqlClient;

namespace FRS_Biblioteca
{
    public class Consultas
    {
        public static string GetTC()

        {
            using (SqlConnection conexaoBD = new SqlConnection(Variaveis.ObtemConnectionString()))
            {
                var repo = conexaoBD.Execute(@"select count(nCFe) from dbo.Notas_Fiscais where cast(data as date) = cast(getdate() as date)");
                if (repo == -1)
                {
                    repo = 0;
                }
                return Convert.ToString(repo);
            }

            //if (FRS_Biblioteca.DB.ExecutaSQLparaString("EXEC dbo.sp_TC") == null)
            //{
            //    return "0";
            //}
            //else
            //{
            //    return FRS_Biblioteca.DB.ExecutaSQLparaString("EXEC dbo.sp_TC");
            //}
        }

        public static string GetTT()
        {
            if (FRS_Biblioteca.DB.ExecutaSQLparaString("EXEC dbo.sp_TT") == null)
            {
                return "0";
            }
            else
            {
                var valor = FRS_Biblioteca.DB.ExecutaSQLparaString("EXEC dbo.sp_TT");
                if (valor == "") { valor = "0"; }
                double valordoluble = Convert.ToDouble(valor);
                return valordoluble.ToString("0.00");
            }
        }

        public static string GetTM()
        {
            if (FRS_Biblioteca.DB.ExecutaSQLparaString("EXEC dbo.sp_TM") == null)
            {
                return "0";
            }
            else
            {
                var valor = FRS_Biblioteca.DB.ExecutaSQLparaString("EXEC dbo.sp_TM");
                double valordoluble = Convert.ToDouble(valor);
                return valordoluble.ToString("0.00");
            }
        }
    }
}