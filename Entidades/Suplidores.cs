﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PedidosSuplidores.Entidades
{
    public class Suplidores
    {
        [Key]
        public int SuplidorId { get; set; }
        public string Nombres { get; set; }
    }
}
