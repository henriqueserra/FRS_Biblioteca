﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FRS_Biblioteca
    {
    public class Insumos
        {
        public string Insumo_Nome { get; set; }
        public string Unidade { get; set; }

        public double Estoque
            {
            get;
            set;
            }

        public string Estoque_string { get; set; }
        }
    }