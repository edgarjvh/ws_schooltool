using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace schoolTool.data
{
    public class Calendario
    {
        public Calendario()
        {
            Representado = 0;
            Antiguedad = 0;
            Cursos = new List<Curso>();
        }

        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        public string Fecha { get; set; }
        public string Lugar { get; set; }
        public int Representado { get; set; }
        public string Header { get; set; }
        public int Antiguedad { get; set; }
        public List<Curso> Cursos { get; set; }
    }
}