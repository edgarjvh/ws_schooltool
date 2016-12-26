using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace schoolTool.data
{
    public class Representante
    {
        public int Id { get; set; }
        public double Cedula { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string Telefono1 { get; set; }
        public string Telefono2 { get; set; }
        public string Direccion { get; set; }
        public int Registrado { get; set; }
        public string Imagen { get; set; }

    }
}