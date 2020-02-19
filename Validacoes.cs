using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FRS_Biblioteca
{
    public class Validacoes
    {
        public static bool Valida_Hora(string valor)
        {
            if (!(valor == ""))
            {
                try
                {
                    DateTime hora = Convert.ToDateTime(valor);
                }
                catch (System.FormatException)
                {
                    MessageBox.Show("O valor digitado está inválido\r\n\r\nO formato correto é HH:MM \r\nExemplo: 09:47", "Valor digitado errado", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return false;
                }
            }
            return true;
        }

        public static bool ValidaStringParaDouble(string valor)
        {
            valor = valor.Replace(",", ".");

            try
            {
                if (!(valor == ""))
                {
                    var teste = Convert.ToDouble(valor);
                }
            }
            catch (System.FormatException)
            {
                MessageBox.Show($"O valor digitado está inválido,\r\nO separador de casa decimais é -> {Variaveis.GetSeparadorNumerico()} \r\nfavor tentar novamente", "Valor digitado errado", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }
            return true;
        }

        public static bool ValidaStringParaInt(string valor)
        {
            valor = valor.Replace(",", ".");
            try
            {
                if (!(valor == ""))
                {
                    var teste = Convert.ToInt32(valor);
                }
            }
            catch (System.FormatException)
            {
                MessageBox.Show($"O valor digitado está inválido,\r\nO separador de casa decimais é -> {Variaveis.GetSeparadorNumerico()} \r\nfavor tentar novamente", "Valor digitado errado", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }
            return true;
        }

        public static string NormalizaDouble(string valor)
        {
            if (Convert.ToDouble("2,5") == (double)5 / 2)
            {
                valor = valor.Replace(".", ",");
            }
            else
            {
                valor = valor.Replace(",", ".");
            }
            return valor;
        }

        public static bool TestaInt(string valor)
        {
            try
            {
                if (!(valor == ""))
                {
                    var teste = Convert.ToInt32(valor);
                    return true;
                }
            }
            catch (System.FormatException)
            {
                MessageBox.Show($"O valor digitado está inválido,\r\nO separador de casa decimais é -> {Variaveis.GetSeparadorNumerico()} \r\nfavor tentar novamente", "Valor digitado errado", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            return false;
        }

        public static bool TestaDouble(string valor)
        {
            try
            {
                if (!(valor == ""))
                {
                    double teste = Convert.ToDouble(valor);
                    return true;
                }
            }
            catch (System.FormatException)
            {
                MessageBox.Show($"O valor digitado está inválido,\r\nO separador de casa decimais é -> {Variaveis.GetSeparadorNumerico()} \r\nfavor tentar novamente", "Valor digitado errado", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            return false;
        }
    }
}