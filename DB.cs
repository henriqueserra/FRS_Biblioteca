using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace FRS_Biblioteca
    {
    public class DB
        {
        //todo: Deve ser criado um método para checar se o banco de dados é acessível
        /// <summary>
        /// Executa comando SLQ (String)
        /// </summary>
        /// <remarks>Este método executa um comando SQL e checa se ao menos um registro foi afetado.</remarks>
        /// <returns>Campo Returns</returns>
        public static bool ExecutaSql(string comandosql)
            {
            var cs = Variaveis.ObtemConnectionString();
            var conexao = new SqlConnection(cs);
            SqlCommand cmd = new SqlCommand(comandosql, conexao);
            try
                {
                cmd.Connection.Open();
                var reader = cmd.ExecuteReader();
                if (!reader.HasRows)
                    {
                    return false;
                    }
                return true;
                }
            catch (Exception ex)
                {
                FRS_Biblioteca.Log log = new Log("ExecutaSql", "Erro ao tentar executar SQL", ex.Message);
                FRS_Biblioteca.Log.GravaLog(log);
                return false;
                }
            finally
                {
                cmd.Connection.Close();
                cmd.Dispose();
                }
            }

        /// <summary>
        /// Verifica se o número da nota fiscal eletrônica já existe no banco de dados. True = existe no banco, False =  não existe no banco
        /// </summary>
        public static bool TestaNfeDB(FRS_Biblioteca.Notas nota)
            {
            var cs = Variaveis.ObtemConnectionString();
            var conexao = new SqlConnection(cs);
            string comandosql = $"select * from dbo.Notas_Fiscais where nCFe = '{nota.NCFe}'";
            SqlCommand cmd = new SqlCommand(comandosql, conexao);
            try
                {
                cmd.Connection.Open();
                var reader = cmd.ExecuteReader();
                if (!reader.HasRows)
                    {
                    return false;
                    }
                return true;
                }
            catch (Exception ex)
                {
                FRS_Biblioteca.Log log = new Log("TestaNfeDB", $"Erro ao consultar banco de dados para verificar a existência da notafiscal: {nota.NCFe}", ex.Message);
                FRS_Biblioteca.Log.GravaLog(log);

                return false;
                }
            finally
                {
                cmd.Connection.Close();
                cmd.Dispose();
                }
            }

        public static bool GravaProdutosVendidos(string nCFe, string Data, string Vendavel, string CodVendavel, string ValorVendavel, string ValorDesconto, string ValorVendido)
            {
            var cs = Variaveis.ObtemConnectionString();
            var conexao = new SqlConnection(cs);
            string comandosql = $"INSERT INTO [LojaDB].[dbo].[Vendas] "+
                $"([nCFe],[Data],[Vendavel],[CodVendavel],[ValorVendavel],[ValorDesconto],[ValorVendido]) "+
                $"VALUES "+
                $"('{nCFe}', '{Data}', '{Vendavel}', '{CodVendavel}', '{ValorVendavel}', '{ValorDesconto}', '{ValorVendido}')";
            SqlCommand cmd = new SqlCommand(comandosql, conexao);
            try
                {
                cmd.Connection.Open();
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                    {
                    return true;
                    }
                }
            catch (Exception ex)
                {
                FRS_Biblioteca.Log log = new Log("GravaProdutosVendidos", $"Erro ao gravar Vendas: {nCFe}", ex.Message);
                FRS_Biblioteca.Log.GravaLog(log);
                }
            finally
                {
                cmd.Connection.Close();
                cmd.Dispose();
                }

            return false;
            }

        public static List<string> NotasProcessadas()
            {
            var resultado = new List<string>();
            var cs = Variaveis.ObtemConnectionString();
            var conexao = new SqlConnection(cs);
            string comandosql = "select Arquivo from dbo.Notas_Fiscais order by Arquivo";
            SqlCommand cmd = new SqlCommand(comandosql, conexao);
            try
                {
                cmd.Connection.Open();
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                    {
                    resultado.Add(reader[0].ToString());
                    }
                }
            catch (Exception ex)
                {
                FRS_Biblioteca.Log log = new Log("NotasProcessadas", $"Erro ao gerar a lista de notas armazenadas no BD", ex.Message);
                FRS_Biblioteca.Log.GravaLog(log);
                }
            finally
                {
                cmd.Connection.Close();
                cmd.Dispose();
                }
            return resultado;
            }

        public static List<string> VendaveisProcessados()
            {
            var resultado = new List<string>();
            var cs = Variaveis.ObtemConnectionString();
            var conexao = new SqlConnection(cs);
            string comandosql = "select nCFe from dbo.Vendas order by nCFe";
            SqlCommand cmd = new SqlCommand(comandosql, conexao);
            try
                {
                cmd.Connection.Open();
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                    {
                    resultado.Add(reader[0].ToString());
                    }
                }
            catch (Exception ex)
                {
                FRS_Biblioteca.Log log = new Log("VendaveisProcessados", $"Erro ao gerar a lista de notas armazenadas no BD Vendas", ex.Message);
                FRS_Biblioteca.Log.GravaLog(log);
                }
            finally
                {
                cmd.Connection.Close();
                cmd.Dispose();
                }
            return resultado;
            }

        public static List<string> ExecutaStoredProcedure(string storedprocedure)
            {
            var resultado = new List<string>();
            var cs = Variaveis.ObtemConnectionString();
            var conexao = new SqlConnection(cs);
            string comandosql = "EXECUTE dbo."+storedprocedure;
            SqlCommand cmd = new SqlCommand(comandosql, conexao);
            try
                {
                cmd.Connection.Open();
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                    {
                    resultado.Add(reader[0].ToString());
                    }
                }
            catch (Exception ex)
                {
                FRS_Biblioteca.Log log = new Log("ExecutaStoredProcedure", $"Erro ao executar stored procedure {storedprocedure}", ex.Message);
                FRS_Biblioteca.Log.GravaLog(log);
                }
            finally
                {
                cmd.Connection.Close();
                cmd.Dispose();
                }
            return resultado;
            }

        public static List<string> ExecutaSQLparalista(string comandoSQL)
            {
            var resultado = new List<string>();
            var cs = Variaveis.ObtemConnectionString();
            var conexao = new SqlConnection(cs);
            string comandosql = comandoSQL;
            SqlCommand cmd = new SqlCommand(comandosql, conexao);
            try
                {
                cmd.Connection.Open();
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                    {
                    resultado.Add(reader[0].ToString());
                    }
                }
            catch (Exception ex)
                {
                FRS_Biblioteca.Log log = new Log(System.Reflection.MethodBase.GetCurrentMethod().Name, $"Erro ao executar stored procedure {comandoSQL}", ex.Message);
                FRS_Biblioteca.Log.GravaLog(log);
                }
            finally
                {
                cmd.Connection.Close();
                cmd.Dispose();
                }
            return resultado;
            }
        }
    }