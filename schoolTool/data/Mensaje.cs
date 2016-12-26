using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace schoolTool.data
{
    public class Mensaje
    {
        public int IdMensaje { get; set; }
        public int IdDocente { get; set; }
        public int IdRepresentante { get; set; }
        public int Via { get; set; }
        public string Texto { get; set; }
        public DateTime fechaHora { get; set; }
        public int Estado { get; set; }
        public string Header { get; set; }
        public string NombreRepresentante { get; set; }
        public string NombreDocente { get; set; }
    }
}