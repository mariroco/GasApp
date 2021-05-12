using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace GasApp.Models
{
    [Table("Gasolinera")]
    public class GasModel
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string Marca { get; set; }
        public string Sucursal { get; set; }
        public string Foto { get; set; }
        public double Verde { get; set; }
        public double Roja { get; set; }
        public double Diesel { get; set; }
        public double Latitud { get; set; }
        public double Longitud { get; set; }
    }
}
