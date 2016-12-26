using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace schoolTool.data
{
    public class Alumno
    {
        public int Id { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public Representante Representante { get; set; }
        public Docente Docente { get; set; }

        public Alumno()
        {
            Representante = new Representante();
            Docente = new Docente();
        }
    }
}