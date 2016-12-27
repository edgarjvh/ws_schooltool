using System;
using System.Data;
using System.Collections.Generic;
using System.Web;
using System.Web.Services;
using MySql.Data.MySqlClient;
using schoolTool.data;
using System.Web.Script.Serialization;
using System.Globalization;
using System.Text;
using System.Net;
using System.IO;

namespace schoolTool
{
    /// <summary>
    /// Descripción breve de schooltool
    /// </summary>
    [WebService(Namespace = "http://schooltool.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Para permitir que se llame a este servicio web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente. 
    [System.Web.Script.Services.ScriptService]
    public class schooltool : WebService
    {
        private static string cadena = "server=154.42.65.212;uid=userschooltool;pwd=schooltool;database=schooltooldb";
        
        [WebMethod()]
        public string loginRepresentante(double Cedula, string Clave)
        {
            try
            {
                Representante Representante = new Representante();
                MySqlCommand cmd = default(MySqlCommand);
                MySqlDataAdapter da = default(MySqlDataAdapter);
                DataSet ds = default(DataSet);
                string query = null;

                using (MySqlConnection conn = new MySqlConnection(cadena))
                {
                    conn.Open();

                    query = "select * from representantes";

                    cmd = new MySqlCommand(query, conn);
                    da = new MySqlDataAdapter(cmd);
                    ds = new DataSet();
                    da.Fill(ds);

                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        for (int i = 0; i <= ds.Tables[0].Rows.Count - 1; i++)
                        {
                            DataRow row = ds.Tables[0].Rows[i];
                            var _ced = (double)row["cedula"];

                            if (_ced == Cedula)
                            {
                                if (row["clave"].ToString() == Clave)
                                {
                                    Representante.Id = (int)row["idRepresentante"];
                                    Representante.Cedula = (double)row["cedula"];
                                    Representante.Nombres = row["nombres"].ToString();
                                    Representante.Apellidos = row["apellidos"].ToString();
                                    Representante.Telefono1 = row["telefono1"].ToString();
                                    Representante.Telefono2 = row["telefono2"].ToString();
                                    Representante.Direccion = row["direccion"].ToString();

                                    if (row["imagen"] is DBNull)
                                    {
                                        Representante.Imagen = "";
                                    }
                                    else
                                    {
                                        byte[] imageBytes = (byte[])row["imagen"];
                                        Representante.Imagen = Convert.ToBase64String(imageBytes);
                                    }

                                    string query2 = "select * from registrosGcm where idRepresentante = @1";
                                    MySqlCommand cmd2 = new MySqlCommand(query2, conn);
                                    cmd2.Parameters.AddWithValue("@1", Representante.Id);
                                    MySqlDataAdapter da2 = new MySqlDataAdapter(cmd2);
                                    DataSet ds2 = new DataSet();
                                    da2.Fill(ds2);

                                    if (ds2.Tables[0].Rows.Count > 0)
                                    {
                                        return new JavaScriptSerializer().Serialize(new
                                        {
                                            Result = "REGISTERED",
                                            Representante = Representante
                                        });
                                    }
                                    else
                                    {
                                        return new JavaScriptSerializer().Serialize(new
                                        {
                                            Result = "OK",
                                            Representante = Representante
                                        });
                                    }
                                }
                                else
                                {
                                    return new JavaScriptSerializer().Serialize(new
                                    {
                                        Result = "NO ROWS",
                                        Message = "Contraseña incorrecta"
                                    });
                                }
                            }
                        }

                        return new JavaScriptSerializer().Serialize(new
                        {
                            Result = "NO ROWS",
                            Message = "Representante no registrado"
                        });
                    }
                    else
                    {
                        return new JavaScriptSerializer().Serialize(new
                        {
                            Result = "NO ROWS",
                            Message = "Representante no registrado"
                        });
                    }
                }

                
            }
            catch (Exception ex)
            {
                return new JavaScriptSerializer().Serialize(new
                {
                    Result = "ERROR",
                    Message = ex.Message
                });
            }
        }

        [WebMethod()]
        public string loginDocente(double Cedula, string Clave)
        {
            try
            {
                Docente Docente = new Docente();
                MySqlCommand cmd = default(MySqlCommand);
                MySqlDataAdapter da = default(MySqlDataAdapter);
                DataSet ds = default(DataSet);
                string query = null;

                using (MySqlConnection conn = new MySqlConnection(cadena))
                {
                    conn.Open();

                    query = "select * from docentes";

                    cmd = new MySqlCommand(query, conn);
                    da = new MySqlDataAdapter(cmd);
                    ds = new DataSet();
                    da.Fill(ds);

                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        for (int i = 0; i <= ds.Tables[0].Rows.Count - 1; i++)
                        {
                            DataRow row = ds.Tables[0].Rows[i];
                            var _ced = (double)row["cedula"];

                            if (_ced == Cedula)
                            {
                                if (row["clave"].ToString() == Clave)
                                {
                                    Docente.Id = (int)row["idDocente"];
                                    Docente.Cedula = (double)row["cedula"];
                                    Docente.Nombres = row["nombres"].ToString();
                                    Docente.Apellidos = row["apellidos"].ToString();
                                    Docente.Telefono1 = row["telefono1"].ToString();
                                    Docente.Telefono2 = row["telefono2"].ToString();
                                    Docente.Direccion = row["direccion"].ToString();

                                    if (row["imagen"] is DBNull)
                                    {
                                        Docente.Imagen = "";
                                    }
                                    else
                                    {
                                        byte[] imageBytes = (byte[])row["imagen"];
                                        Docente.Imagen = Convert.ToBase64String(imageBytes);
                                    }

                                    string query2 = "select * from registrosGcm where idDocente = @1";
                                    MySqlCommand cmd2 = new MySqlCommand(query2, conn);
                                    cmd2.Parameters.AddWithValue("@1", Docente.Id);
                                    MySqlDataAdapter da2 = new MySqlDataAdapter(cmd2);
                                    DataSet ds2 = new DataSet();
                                    da2.Fill(ds2);

                                    if (ds2.Tables[0].Rows.Count > 0)
                                    {
                                        return new JavaScriptSerializer().Serialize(new
                                        {
                                            Result = "REGISTERED",
                                            Docente = Docente
                                        });
                                    }
                                    else
                                    {
                                        return new JavaScriptSerializer().Serialize(new
                                        {
                                            Result = "OK",
                                            Docente = Docente
                                        });
                                    }
                                }
                                else
                                {
                                    return new JavaScriptSerializer().Serialize(new
                                    {
                                        Result = "NO ROWS",
                                        Message = "Contraseña incorrecta"
                                    });
                                }
                            }
                        }

                        return new JavaScriptSerializer().Serialize(new
                        {
                            Result = "NO ROWS",
                            Message = "Representante no registrado"
                        });
                    }
                    else
                    {
                        return new JavaScriptSerializer().Serialize(new
                        {
                            Result = "NO ROWS",
                            Message = "Representante no registrado"
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                return new JavaScriptSerializer().Serialize(new
                {
                    Result = "ERROR",
                    Message = ex.Message
                });
            }
        }

        [WebMethod()]
        public string getCalendarioRepresentante(int idRepresentante, int tipoCalendario)
        {
            //tipo calendario
            //0 = esta semana
            //1 = este mes
            //2 = este año

            try
            {
                Calendario Evento = new Calendario();
                List<Calendario> Calendario = new List<Calendario>();
                MySqlCommand cmd = default(MySqlCommand);
                MySqlDataAdapter da = default(MySqlDataAdapter);
                DataSet ds = default(DataSet);
                string query = "";
                DateTime hoy = DateTime.Today;
                DateTime fechaInicio = new DateTime();
                DateTime fechaFin = new DateTime(); 
                string iAnio = "";
                string iMes = "";
                string iDia = "";
                string fAnio = "";
                string fMes = "";
                string fDia = "";
                
                string param1 = "";
                string param2 = "";

                using (MySqlConnection conn = new MySqlConnection(cadena))
                { 
                    conn.Open();

                    query = "select ac.idActividadCurso, ac.fechaHora as 'fecha', DATE_FORMAT(ac.fechaHora,'%d-%m-%Y %r') as 'fechaHora', ac.lugar, a.titulo, a.descripcion, c.idCurso, c.grado, c.seccion from actividadesCursos as ac " + "left join actividades as a on ac.idActividad = a.idActividad " + "left join cursos as c on ac.idCurso = c.idCurso " + "where ";

                    switch (tipoCalendario)
                    {
                        case 0:
                            fechaInicio = hoy.AddDays(-Convert.ToInt32(hoy.DayOfWeek) + 1);
                            fechaFin = hoy.AddDays(-Convert.ToInt32(hoy.DayOfWeek) + 7);

                            iAnio = fechaInicio.Year.ToString();
                            iMes = fechaInicio.Month.ToString("00");
                            iDia = fechaInicio.Day.ToString("00");
                            fAnio = fechaFin.Year.ToString();
                            fMes = fechaFin.Month.ToString("00");
                            fDia = fechaFin.Day.ToString("00");

                            param1 = iAnio + "-" + iMes + "-" + iDia + " 00:00:00";
                            param2 = fAnio + "-" + fMes + "-" + fDia + " 00:00:00";

                            query += "ac.fechaHora between @1 and @2 order by ac.fechaHora ASC;";
                            break;
                        case 1:
                            param1 = hoy.Month.ToString("00");
                            param2 = hoy.Year.ToString("0000");

                            query += "month(ac.fechaHora) = @1 and year(ac.fechaHora) = @2 order by ac.fechaHora ASC;";
                            break;
                        case 2:
                            query += "ac.fechaHora between (select inicio from periodos where activo = 1 limit 1) and (select final from periodos where activo = 1 limit 1) order by ac.fechaHora ASC;";
                            break;
                    }

                    query += "select DISTINCT(c.idCurso) from representantes as r left join alumnos as a on r.idRepresentante = a.idRepresentante " + "left join cursosalumnos as c on a.idAlumno = c.idAlumno " + "where r.idRepresentante = @3;";

                    cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@1", param1);
                    cmd.Parameters.AddWithValue("@3", idRepresentante);

                    if (query.Contains("@2"))
                    {
                        cmd.Parameters.AddWithValue("@2", param2);
                    }

                    da = new MySqlDataAdapter(cmd);
                    ds = new DataSet();
                    da.Fill(ds);
                }

                string tempHeader = "";

                if (ds.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i <= ds.Tables[0].Rows.Count - 1; i++)
                    {
                        Evento = new Calendario();
                        DataRow row = ds.Tables[0].Rows[i];
                        int representado = 0;
                        string header = "";
                        string nMes = "";
                        TextInfo textInfo;

                        // VALIDAR REPRESENTADO =================
                        if (ds.Tables[1].Rows.Count > 0)
                        {
                            for (int x = 0; x <= ds.Tables[1].Rows.Count - 1; x++)
                            {
                                var a = ds.Tables[1].Rows[x][0];
                                var b = row["idCurso"];

                                if ((int)ds.Tables[1].Rows[x][0] == (int)row["idCurso"])
                                {
                                    representado = 1;
                                    break; // TODO: might not be correct. Was : Exit For
                                }
                            }
                        }
                        // =======================================

                        if (i == 0)
                        {
                            switch (tipoCalendario)
                            {
                                case 0:
                                    if (iMes != fMes)
                                    {
                                        if (iAnio != fAnio)
                                        {
                                            header = "Del " + iDia + "-" + iMes + "-" + iAnio + " Al " + fDia + "-" + fMes + "-" + fAnio;
                                        }
                                        else
                                        {
                                            header = " Del " + iDia + "-" + iMes + " Al " + fDia + "-" + fMes + " de " + iAnio;
                                        }
                                    }
                                    else
                                    {
                                        nMes = fechaInicio.ToString("MMMM", CultureInfo.CreateSpecificCulture("es"));
                                        textInfo = new CultureInfo("es", false).TextInfo;
                                        nMes = textInfo.ToTitleCase(nMes); 

                                        header = "Del " + iDia + " Al " + fDia + " De " + nMes + " de " + iAnio;
                                    }
                                    break;
                                case 1:
                                    nMes = hoy.ToString("MMMM", CultureInfo.CreateSpecificCulture("es"));
                                    textInfo = new CultureInfo("es", false).TextInfo;
                                    header = textInfo.ToTitleCase(nMes);
                                    break;
                                case 2:
                                    nMes = ((DateTime)row["fecha"]).ToString("MMMM", CultureInfo.CreateSpecificCulture("es"));
                                    textInfo = new CultureInfo("es", false).TextInfo;
                                    header = textInfo.ToTitleCase(nMes);
                                    tempHeader = header;
                                    break;
                            }
                        }
                        else
                        {
                            if (tipoCalendario == 2)
                            {
                                nMes = ((DateTime)row["fecha"]).ToString("MMMM", CultureInfo.CreateSpecificCulture("es"));
                                textInfo = new CultureInfo("es", false).TextInfo;
                                nMes = textInfo.ToTitleCase(nMes);

                                if (tempHeader != nMes)
                                {
                                    header = nMes;
                                    tempHeader = nMes;
                                }
                            }
                        }

                        Evento.Id = (int)row["idActividadCurso"];
                        Evento.Titulo = row["titulo"].ToString();
                        Evento.Descripcion = row["descripcion"].ToString();
                        Evento.Fecha = row["fechaHora"].ToString();
                        Evento.Lugar = row["lugar"].ToString();
                        Evento.Representado = representado;
                        Evento.Header = header;
                        Evento.Antiguedad = ((DateTime)row["fecha"]).Date > DateTime.Now.Date ? 2 : ((DateTime)row["fecha"]).Date == DateTime.Now.Date ? 1 : 0;
                        Calendario.Add(Evento);
                    }

                    return new JavaScriptSerializer().Serialize(new
                    {
                        Result = "OK",
                        Calendario = Calendario,
                        TotalRecordCount = Calendario.Count
                    });
                }
                else
                {
                    return new JavaScriptSerializer().Serialize(new
                    {
                        Result = "NO ROWS",
                        Message = "No hay eventos que mostrar"
                    });
                }
            }
            catch (Exception ex)
            {
                return new JavaScriptSerializer().Serialize(new
                {
                    Result = "ERROR",
                    Message = ex.Message
                });
            }
        }

        [WebMethod()]
        public string getCalendarioDocente(int idDocente, int tipoCalendario)
        {
            //tipo calendario
            //0 = esta semana
            //1 = este mes
            //2 = este año

            try
            {
                Calendario Evento = new Calendario();
                List<Calendario> Calendario = new List<Calendario>();
                MySqlCommand cmd = default(MySqlCommand);
                MySqlDataAdapter da = default(MySqlDataAdapter);
                DataSet ds = default(DataSet);
                string query = "";
                DateTime hoy = DateTime.Today;
                DateTime fechaInicio = new DateTime();
                DateTime fechaFin = new DateTime();
                string iAnio = "";
                string iMes = "";
                string iDia = "";
                string fAnio = "";
                string fMes = "";
                string fDia = "";

                string param1 = "";
                string param2 = "";

                using (MySqlConnection conn = new MySqlConnection(cadena))
                {
                    conn.Open();

                    query = "select ac.idActividadCurso, ac.fechaHora as 'fecha', DATE_FORMAT(ac.fechaHora,'%d-%m-%Y %r') as 'fechaHora', " + 
                        "ac.lugar, a.titulo, a.descripcion, c.idCurso, c.grado, c.seccion from actividadesCursos as ac " + 
                        "left join actividades as a on ac.idActividad = a.idActividad " +
                        "left join cursos as c on ac.idCurso = c.idCurso " +
                        "where ";

                    switch (tipoCalendario)
                    {
                        case 0:
                            fechaInicio = hoy.AddDays(-Convert.ToInt32(hoy.DayOfWeek) + 1);
                            fechaFin = hoy.AddDays(-Convert.ToInt32(hoy.DayOfWeek) + 7);

                            iAnio = fechaInicio.Year.ToString();
                            iMes = fechaInicio.Month.ToString("00");
                            iDia = fechaInicio.Day.ToString("00");
                            fAnio = fechaFin.Year.ToString();
                            fMes = fechaFin.Month.ToString("00");
                            fDia = fechaFin.Day.ToString("00");

                            param1 = iAnio + "-" + iMes + "-" + iDia + " 00:00:00";
                            param2 = fAnio + "-" + fMes + "-" + fDia + " 00:00:00";

                            query += "ac.fechaHora between @1 and @2 order by ac.fechaHora ASC;";
                            break;
                        case 1:
                            param1 = hoy.Month.ToString("00");
                            param2 = hoy.Year.ToString("0000");

                            query += "month(ac.fechaHora) = @1 and year(ac.fechaHora) = @2 order by ac.fechaHora ASC;";
                            break;
                        case 2:
                            query += "ac.fechaHora between (select inicio from periodos where activo = 1 limit 1) and (select final from periodos where activo = 1 limit 1) order by ac.fechaHora ASC;";
                            break;
                    }

                    query += "select DISTINCT(c.idCurso) from docentes as d " +
                        "left join cursosalumnos as c on d.idDocente = c.idDocente " +
                        "where c.idDocente = @1;";

                    cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@1", param1);
                    cmd.Parameters.AddWithValue("@3", idDocente);

                    if (query.Contains("@2"))
                    {
                        cmd.Parameters.AddWithValue("@2", param2);
                    }

                    da = new MySqlDataAdapter(cmd);
                    ds = new DataSet();
                    da.Fill(ds);
                }

                string tempHeader = "";

                if (ds.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i <= ds.Tables[0].Rows.Count - 1; i++)
                    {
                        Evento = new Calendario();
                        DataRow row = ds.Tables[0].Rows[i];
                        int representado = 0;
                        string header = "";
                        string nMes = "";
                        TextInfo textInfo;

                        // VALIDAR REPRESENTADO =================
                        if (ds.Tables[1].Rows.Count > 0)
                        {
                            for (int x = 0; x <= ds.Tables[1].Rows.Count - 1; x++)
                            {
                                var a = ds.Tables[1].Rows[x][0];
                                var b = row["idCurso"];

                                if ((int)ds.Tables[1].Rows[x][0] == (int)row["idCurso"])
                                {
                                    representado = 1;
                                    break; // TODO: might not be correct. Was : Exit For
                                }
                            }
                        }
                        // =======================================

                        if (i == 0)
                        {
                            switch (tipoCalendario)
                            {
                                case 0:
                                    if (iMes != fMes)
                                    {
                                        if (iAnio != fAnio)
                                        {
                                            header = "Del " + iDia + "-" + iMes + "-" + iAnio + " Al " + fDia + "-" + fMes + "-" + fAnio;
                                        }
                                        else
                                        {
                                            header = " Del " + iDia + "-" + iMes + " Al " + fDia + "-" + fMes + " de " + iAnio;
                                        }
                                    }
                                    else
                                    {
                                        nMes = fechaInicio.ToString("MMMM", CultureInfo.CreateSpecificCulture("es"));
                                        textInfo = new CultureInfo("es", false).TextInfo;
                                        nMes = textInfo.ToTitleCase(nMes);

                                        header = "Del " + iDia + " Al " + fDia + " De " + nMes + " de " + iAnio;
                                    }
                                    break;
                                case 1:
                                    nMes = hoy.ToString("MMMM", CultureInfo.CreateSpecificCulture("es"));
                                    textInfo = new CultureInfo("es", false).TextInfo;
                                    header = textInfo.ToTitleCase(nMes);
                                    break;
                                case 2:
                                    nMes = ((DateTime)row["fecha"]).ToString("MMMM", CultureInfo.CreateSpecificCulture("es"));
                                    textInfo = new CultureInfo("es", false).TextInfo;
                                    header = textInfo.ToTitleCase(nMes);
                                    tempHeader = header;
                                    break;
                            }
                        }
                        else
                        {
                            if (tipoCalendario == 2)
                            {
                                nMes = ((DateTime)row["fecha"]).ToString("MMMM", CultureInfo.CreateSpecificCulture("es"));
                                textInfo = new CultureInfo("es", false).TextInfo;
                                nMes = textInfo.ToTitleCase(nMes);

                                if (tempHeader != nMes)
                                {
                                    header = nMes;
                                    tempHeader = nMes;
                                }
                            }
                        }

                        Evento.Id = (int)row["idActividadCurso"];
                        Evento.Titulo = row["titulo"].ToString();
                        Evento.Descripcion = row["descripcion"].ToString();
                        Evento.Fecha = row["fechaHora"].ToString();
                        Evento.Lugar = row["lugar"].ToString();
                        Evento.Representado = representado;
                        Evento.Header = header;
                        Evento.Antiguedad = ((DateTime)row["fecha"]).Date > DateTime.Now.Date ? 2 : ((DateTime)row["fecha"]).Date == DateTime.Now.Date ? 1 : 0;
                        Calendario.Add(Evento);
                    }

                    return new JavaScriptSerializer().Serialize(new
                    {
                        Result = "OK",
                        Calendario = Calendario,
                        TotalRecordCount = Calendario.Count
                    });
                }
                else
                {
                    return new JavaScriptSerializer().Serialize(new
                    {
                        Result = "NO ROWS",
                        Message = "No hay eventos que mostrar"
                    });
                }
            }
            catch (Exception ex)
            {
                return new JavaScriptSerializer().Serialize(new
                {
                    Result = "ERROR",
                    Message = ex.Message
                });
            }
        }

        [WebMethod()]
        public string sendMensaje(int via, int idDocente, int idRepresentante, string mensaje, DateTime fechaHora)
        {
            /*via
             * 0 = doc a rep
             * 1 = rep a doc
             */
            try
            {
                MySqlCommand cmd = default(MySqlCommand);                
                string query = "";

                using (MySqlConnection conn = new MySqlConnection(cadena))
                {
                    conn.Open();

                    query = "insert into mensajes (via,idDocente,idRepresentante,mensaje,fechaHora) values (@1,@2,@3,@4,@5)";
                    cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@1", via);
                    cmd.Parameters.AddWithValue("@2", idDocente);
                    cmd.Parameters.AddWithValue("@3", idRepresentante);
                    cmd.Parameters.AddWithValue("@4", mensaje);
                    cmd.Parameters.AddWithValue("@5", fechaHora);
                    cmd.ExecuteNonQuery();

                    // enviar via gcm

                    return new JavaScriptSerializer().Serialize(new
                    {
                        Result = "OK"
                    });
                }
            }
            catch (Exception ex)
            {
                return new JavaScriptSerializer().Serialize(new
                {
                    Result = "ERROR",
                    Message = ex.Message
                });
            }            
        }

        [WebMethod()]
        public string getMensajesRep(int idRepresentante, int idDocente, int estado, int limite)
        {
            /* estado **
             * 0 = todos
             * 1 = sin leer
             * 2 = leidos
             * 
             * docente **
             * 0 = todos
             * <> 0 = por docente
             */

            try
            {
                List<Mensaje> Mensajes = new List<Mensaje>();
                Mensaje mensaje = new Mensaje();
                MySqlCommand cmd = default(MySqlCommand);
                MySqlDataAdapter da = default(MySqlDataAdapter);
                DataSet ds = default(DataSet);
                string query = "";

                using (MySqlConnection conn = new MySqlConnection(cadena))
                {
                    conn.Open();

                    query = "select m.*, DATE_FORMAT(m.fechaHora,'%d-%m-%Y %r') as 'fecha' from mensajes as m where m.idRepresentante = @1";

                    if (idDocente > 0)
                    {
                        query += " and m.idDocente = @2";
                    }

                    if (estado > 0)
                    {
                        query += " and m.estado = @3";
                    }

                    query += " order by m.fechaHora ASC;";

                    cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@1", idRepresentante);

                    if (query.Contains("@2"))
                    {
                        cmd.Parameters.AddWithValue("@2", idDocente);
                    }

                    if (query.Contains("@3"))
                    {
                        cmd.Parameters.AddWithValue("@3", estado);
                    }

                    ds = new DataSet();
                    da = new MySqlDataAdapter(cmd);
                    da.Fill(ds);        
                }

                string tempHeader = "";
                string header = "";

                if (ds.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0;i< ds.Tables[0].Rows.Count; i++)
                    {
                        mensaje = new Mensaje();
                        DataRow row = ds.Tables[0].Rows[i];
                        DateTime hoy = DateTime.Today;
                        DateTime fecha = (DateTime)row["fechaHora"];

                        mensaje.IdMensaje = (int)row["idMensaje"];
                        mensaje.Via = (int)row["via"];
                        mensaje.IdDocente = (int)row["idDocente"];
                        mensaje.IdRepresentante = (int)row["idRepresentante"];
                        mensaje.Texto = row["mensaje"].ToString();
                        mensaje.fechaHora = fecha;
                        mensaje.Estado = (int)row["estado"];

                        if (hoy.Date == fecha.Date)
                        {
                            tempHeader = "Hoy";
                        }
                        else if (hoy.Date.AddDays(-1) == fecha.Date)
                        {
                            tempHeader = "Ayer";
                        }
                        else
                        {
                            tempHeader = fecha.Day.ToString("00") + "-" +
                                         fecha.Month.ToString("00") + "-" +
                                         fecha.Year.ToString("0000");
                        }

                        if (i == 0)
                        {                            
                            mensaje.Header = tempHeader;
                            header = tempHeader;
                        }
                        else
                        {
                            mensaje.Header = header == tempHeader ? "" : tempHeader;
                            header = tempHeader;
                        }

                        Mensajes.Add(mensaje);
                    }

                    return new JavaScriptSerializer().Serialize(new
                    {
                        Result = "OK",
                        Mensajes = Mensajes,
                        TotalRecordCount = Mensajes.Count
                    });
                }
                else
                {
                    return new JavaScriptSerializer().Serialize(new
                    {
                        Result = "NO ROWS",
                        Message = "No hay mensajes para mostrar"
                    });
                }                
            }
            catch (Exception ex)
            {
                return new JavaScriptSerializer().Serialize(new
                {
                    Result = "ERROR",
                    Message = ex.Message
                });
            }
        }

        [WebMethod()]
        public string getAlumnosDocentes(int idRepresentante)
        {
            /*idRepresentante
             * 0 = cargar todos los docentes
             * > 0 = cargar por representante
             */

            try
            {
                List<Alumno> Alumnos = new List<Alumno>();                
                MySqlCommand cmd = default(MySqlCommand);
                MySqlDataAdapter da = default(MySqlDataAdapter);
                DataSet ds = default(DataSet);
                string query = "";

                using (MySqlConnection conn = new MySqlConnection(cadena))
                {
                    conn.Open();
                    
                    query = "select d.idDocente, d.apellidos as apellidosDoc, d.nombres as nombresDoc, d.imagen, a.idAlumno, " + 
                        "a.nombres as nombresAl, a.apellidos as apellidosAl, " +
                        "(select ifnull(g.idGcm,'') from registrosgcm as g where g.idDocente = d.idDocente) as idGcm " +
                        "from alumnos as a left join representantes as r on a.idRepresentante = r.idRepresentante " +
                        "left join cursosalumnos as ca on a.idAlumno = ca.idAlumno " +
                        "left join docentes as d on ca.idDocente = d.idDocente " +
                        "where r.idRepresentante = @1 group by a.idAlumno order by a.apellidos asc;";

                    cmd = new MySqlCommand(query, conn);

                    if (query.Contains("@1"))
                    {
                        cmd.Parameters.AddWithValue("@1", idRepresentante);
                    }

                    da = new MySqlDataAdapter(cmd);
                    ds = new DataSet();
                    da.Fill(ds);
                }

                if (ds.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        Alumno alumno = new Alumno();                        
                        DataRow row = ds.Tables[0].Rows[i];

                        alumno.Id = (int)row["idAlumno"];
                        alumno.Nombres = row["nombresAl"].ToString();
                        alumno.Apellidos = row["apellidosAl"].ToString();
                        alumno.Docente.Id = (int)row["idDocente"];
                        alumno.Docente.Nombres = row["nombresDoc"].ToString();
                        alumno.Docente.Apellidos = row["apellidosDoc"].ToString();
                        alumno.Docente.Registrado = row["idGcm"].ToString().Equals("") ? 0 : 1;                        
                        
                        if (row["imagen"] is DBNull)
                        {
                            alumno.Docente.Imagen = "";
                        }
                        else
                        {
                            byte[] imagenByte = (byte[])row["imagen"];
                            alumno.Docente.Imagen = Convert.ToBase64String(imagenByte);
                        }

                        Alumnos.Add(alumno);
                    }

                    return new JavaScriptSerializer().Serialize(new
                    {
                        Result = "OK",
                        Alumnos = Alumnos,
                        TotalRecordCount = Alumnos.Count
                    });
                }
                else
                {
                    return new JavaScriptSerializer().Serialize(new
                    {
                        Result = "NO ROWS",
                        Message = "No hay alumnos para mostrar"
                    });
                }
            }
            catch(Exception ex)
            {
                return new JavaScriptSerializer().Serialize(new
                {
                    Result = "ERROR",
                    Message = ex.Message
                });
            }
        }
        [WebMethod()]
        public string getAlumnosRepresentantes(int idDocente)
        {
            /*idRepresentante
             * 0 = cargar todos los docentes
             * > 0 = cargar por representante
             */

            try
            {
                List<Alumno> Alumnos = new List<Alumno>();
                MySqlCommand cmd = default(MySqlCommand);
                MySqlDataAdapter da = default(MySqlDataAdapter);
                DataSet ds = default(DataSet);
                string query = "";

                using (MySqlConnection conn = new MySqlConnection(cadena))
                {
                    conn.Open();
                    
                    query = "select r.idRepresentante, r.apellidos as apellidosRep, r.nombres as nombresRep, r.imagen, a.idAlumno, " + 
                        "a.nombres as nombresAl, a.apellidos as apellidosAl, " + 
                        "(select ifnull(g.idGcm,'') from registrosgcm as g where g.idRepresentante = r.idRepresentante) as idGcm " +
                        "from alumnos as a left join representantes as r on a.idRepresentante = r.idRepresentante " +
                        "left join cursosalumnos as ca on a.idAlumno = ca.idAlumno " +
                        "left join docentes as d on ca.idDocente = d.idDocente " +
                        "where d.idDocente = @1 group by a.idAlumno order by a.apellidos asc;";
                                      
                    cmd = new MySqlCommand(query, conn);                    

                    if (query.Contains("@1"))
                    {
                        cmd.Parameters.AddWithValue("@1", idDocente);
                    }

                    da = new MySqlDataAdapter(cmd);
                    ds = new DataSet();
                    da.Fill(ds);
                }

                if (ds.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        Alumno alumno = new Alumno();
                        DataRow row = ds.Tables[0].Rows[i];

                        alumno.Id = (int)row["idAlumno"];
                        alumno.Nombres = row["nombresAl"].ToString();
                        alumno.Apellidos = row["apellidosAl"].ToString();
                        alumno.Representante.Id = (int)row["idRepresentante"];                        
                        alumno.Representante.Nombres = row["nombresRep"].ToString();
                        alumno.Representante.Apellidos = row["apellidosRep"].ToString();
                        alumno.Representante.Registrado = row["idGcm"].ToString().Equals("") ? 0 : 1;                        
                        
                        if (row["imagen"] is DBNull)
                        {
                            alumno.Representante.Imagen = "";
                        }else
                        {
                            byte[] imagenByte = (byte[])row["imagen"];
                            alumno.Representante.Imagen = Convert.ToBase64String(imagenByte);
                        }

                        Alumnos.Add(alumno);
                    }

                    return new JavaScriptSerializer().Serialize(new
                    {
                        Result = "OK",
                        Alumnos = Alumnos,
                        TotalRecordCount = Alumnos.Count
                    });
                }
                else
                {
                    return new JavaScriptSerializer().Serialize(new
                    {
                        Result = "NO ROWS",
                        Message = "No hay alumnos para mostrar"
                    });
                }
            }
            catch (Exception ex)
            {
                return new JavaScriptSerializer().Serialize(new
                {
                    Result = "ERROR",
                    Message = ex.Message
                });
            }
        }

        [WebMethod()]
        public string SaveGcmId(int tipo, int id, string regID, string apiServidor, string dispositivo, string versionAndroid, int versionApp)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(cadena))
                {
                    conn.Open();

                    string query = "insert into registrosGcm (" + (tipo == 0 ? "idDocente," : "idRepresentante,") + 
                        "idGcm,apiServidor,fechaRegistro,dispositivo,versionAndroid,versionApp) values (@1,@2,@3,@4,@5,@6,@7);";

                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@1", id);
                    cmd.Parameters.AddWithValue("@2", regID);
                    cmd.Parameters.AddWithValue("@3", apiServidor);
                    cmd.Parameters.AddWithValue("@4", DateTime.Now);
                    cmd.Parameters.AddWithValue("@5", dispositivo);
                    cmd.Parameters.AddWithValue("@6", versionAndroid);
                    cmd.Parameters.AddWithValue("@7", versionApp);
                    cmd.ExecuteNonQuery();

                    if (tipo == 0)
                    {
                        notificarRegistroDoc(id, 1);
                    }else
                    {
                        notificarRegistroRep(id, 1);
                    }

                    return new JavaScriptSerializer().Serialize(new
                    {
                        Result = "OK"
                    });
                }
            }
            catch (Exception ex)
            {
                return new JavaScriptSerializer().Serialize(new
                {
                    Result = "ERROR",
                    Message = ex.Message
                });
            }
        }
        
        [WebMethod]
        public string enviarMensaje(int tempId, int via, int idRepresentante, int idDocente, string texto, string fechaHora)
        {
            try
            {
                Mensaje mensaje = new Mensaje();
                MySqlCommand cmd;
                MySqlDataAdapter da;
                DataSet ds;
                int idMensaje = 0;

                using (MySqlConnection conn = new MySqlConnection(cadena))
                {
                    conn.Open();

                    string query1 = "select * from registrosgcm where ";

                    string queryDoc = "select nombres as nombresDoc, apellidos as apellidosDoc from docentes where idDocente = @2";
                    string queryRep = "select nombres as nombresRep, apellidos as apellidosRep from representantes where idRepresentante = @2";

                    query1 += via.ToString() == "1" ? "idDocente = @1;" + queryRep : "idRepresentante = @1;"+queryDoc;
                    
                    cmd = new MySqlCommand(query1, conn);
                    cmd.Parameters.AddWithValue("@1", via == 1 ? idDocente : idRepresentante);
                    cmd.Parameters.AddWithValue("@2", via == 1 ? idRepresentante : idDocente);
                    da = new MySqlDataAdapter(cmd);
                    ds = new DataSet();
                    da.Fill(ds);

                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        DataRow row1 = ds.Tables[0].Rows[0];
                        DataRow row2 = ds.Tables[1].Rows[0];

                        string idGcm = row1["idGcm"].ToString();
                        string apiServidor = row1["apiServidor"].ToString();
                        
                        string GCM_URL = "https://gcm-http.googleapis.com/gcm/send";

                        string query2 = "insert into mensajes (via,idDocente,idRepresentante,mensaje,fechaHora,estado) " +
                        "values (@1,@2,@3,@4,@5,@6);select last_insert_id() as 'id';";

                        MySqlCommand cmd2 = new MySqlCommand(query2, conn);
                        cmd2.Parameters.AddWithValue("@1", via);
                        cmd2.Parameters.AddWithValue("@2", idDocente);
                        cmd2.Parameters.AddWithValue("@3", idRepresentante);
                        cmd2.Parameters.AddWithValue("@4", texto);
                        cmd2.Parameters.AddWithValue("@5", fechaHora);
                        cmd2.Parameters.AddWithValue("@6", 0);
                        MySqlDataAdapter da2 = new MySqlDataAdapter(cmd2);
                        DataSet ds2 = new DataSet();
                        da2.Fill(ds2);

                        idMensaje = int.Parse(ds2.Tables[0].Rows[0][0].ToString());

                        mensaje.IdMensaje = idMensaje;
                        mensaje.IdRepresentante = idRepresentante;
                        mensaje.IdDocente = idDocente;
                        mensaje.Texto = texto;
                        mensaje.Via = via;
                        mensaje.fechaHora = DateTime.Parse(fechaHora);
                        mensaje.Estado = 1;

                        if (via == 0)
                        {
                            string[] arrayNombresDoc = row2["nombresDoc"].ToString().Split(' ');
                            string[] arrayApellidosDoc = row2["apellidosDoc"].ToString().Split(' ');
                            mensaje.NombreDocente = "Doc. " + arrayApellidosDoc[0] + ", " + arrayNombresDoc[0];
                        }else
                        {
                            string[] arrayNombresRep = row2["nombresRep"].ToString().Split(' ');
                            string[] arrayApellidosRep = row2["apellidosRep"].ToString().Split(' ');
                            mensaje.NombreRepresentante = "Rep. " + arrayApellidosRep[0] + ", " + arrayNombresRep[0];
                        }
                        
                        string msj = new JavaScriptSerializer().Serialize(new
                        {
                            Result = "INCOMING",
                            Mensaje = mensaje
                        });

                        Dictionary<string, string> data = new Dictionary<string, string>();
                        data.Add("data.mensaje", HttpUtility.UrlEncode(msj));
                                                
                        StringBuilder sb = new StringBuilder();
                        sb.AppendFormat("registration_id={0}", idGcm);

                        foreach (string item in data.Keys)
                        {
                            if (item.Contains("data."))
                            {
                                sb.AppendFormat("&{0}={1}", item, data[item]);
                            }
                        }

                        string msg = sb.ToString();
                        HttpWebRequest req = (HttpWebRequest)WebRequest.Create(GCM_URL);
                        req.Method = "POST";
                        req.ContentLength = msg.Length;
                        req.ContentType = "application/x-www-form-urlencoded";
                        req.Headers.Add("Authorization:key=" + apiServidor);

                        using (StreamWriter oWriter = new StreamWriter(req.GetRequestStream()))
                        {
                            oWriter.Write(msg);
                        }

                        using (HttpWebResponse resp = (HttpWebResponse)req.GetResponse())
                        {
                            using (StreamReader sr = new StreamReader(resp.GetResponseStream()))
                            {
                                string respData = sr.ReadToEnd();

                                if (resp.StatusCode == HttpStatusCode.OK)
                                {
                                    // OK = 200
                                    /*if (respData.StartsWith("id="))
                                    {
                                        flag = true;
                                    }*/
                                }
                                else if (resp.StatusCode == HttpStatusCode.InternalServerError)
                                {
                                    // 500
                                    Console.WriteLine("Error interno del servidor, prueba más tarde.");
                                }
                                else if (resp.StatusCode == HttpStatusCode.ServiceUnavailable)
                                {
                                    // 503
                                    Console.WriteLine("Servidor no disponible temporalmente, prueba más tarde.");
                                }
                                else if (resp.StatusCode == HttpStatusCode.Unauthorized)
                                {
                                    // 401
                                    Console.WriteLine("La API Key utilizada no es válida.");
                                }
                                else
                                {
                                    Console.WriteLine("Error: " + resp.StatusCode);
                                }
                            }
                        }

                        return new JavaScriptSerializer().Serialize(new
                        {
                            Result = "OK",
                            TempId = tempId,
                            IdDocente = idDocente,
                            IdRepresentante = idRepresentante,
                            IdMensaje = idMensaje,
                            Estado = 0
                        });
                    }
                    else
                    {
                        return new JavaScriptSerializer().Serialize(new
                        {
                            Result = "ERROR",
                            Message = "No registrado"
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                return new JavaScriptSerializer().Serialize(new
                {
                    Result = "ERROR",
                    Message = ex.Message
                });
            }
        }

        [WebMethod]
        public string confirmarMensaje(int idMensaje, int estado)
        {
            try
            {
                Mensaje mensaje = new Mensaje();
                MySqlCommand cmd;
                MySqlDataAdapter da;
                DataSet ds;                

                using (MySqlConnection conn = new MySqlConnection(cadena))
                {
                    conn.Open();

                    string query1 = "select * from mensajes where idMensaje = @1";                                  

                    cmd = new MySqlCommand(query1, conn);
                    cmd.Parameters.AddWithValue("@1", idMensaje);
                    da = new MySqlDataAdapter(cmd);
                    ds = new DataSet();
                    da.Fill(ds);

                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        int via = int.Parse(ds.Tables[0].Rows[0]["via"].ToString());
                        int idDocente = int.Parse(ds.Tables[0].Rows[0]["idDocente"].ToString());
                        int idRepresentante = int.Parse(ds.Tables[0].Rows[0]["idRepresentante"].ToString());

                        string query2 = "";

                        MySqlCommand cmd2;

                        if (via == 0)
                        {
                            query2 += "select * from registrosGcm as r where idDocente = @1";
                            cmd2 = new MySqlCommand(query2, conn);
                            cmd2.Parameters.AddWithValue("@1", idDocente);
                        }
                        else
                        {
                            query2 += "select * from registrosGcm as r where idRepresentante = @1";
                            cmd2 = new MySqlCommand(query2, conn);
                            cmd2.Parameters.AddWithValue("@1", idRepresentante);
                        }
                                                
                        MySqlDataAdapter da2 = new MySqlDataAdapter(cmd2);
                        DataSet ds2 = new DataSet();
                        da2.Fill(ds2);

                        if (ds2.Tables[0].Rows.Count > 0)
                        {
                            DataRow row = ds2.Tables[0].Rows[0];
                            string idGcm = row["idGcm"].ToString();
                            string apiServidor = row["apiServidor"].ToString();
                            string GCM_URL = "https://gcm-http.googleapis.com/gcm/send";

                            string msj = new JavaScriptSerializer().Serialize(new
                            {
                                Result = "UPDATE",
                                IdMensaje = idMensaje,
                                Estado = estado,
                                Via = via,
                                IdDocente = idDocente,
                                IdRepresentante = idRepresentante
                            });

                            Dictionary<string, string> data = new Dictionary<string, string>();
                            data.Add("data.mensaje", HttpUtility.UrlEncode(msj));

                            StringBuilder sb = new StringBuilder();
                            sb.AppendFormat("registration_id={0}", idGcm);

                            foreach (string item in data.Keys)
                            {
                                if (item.Contains("data."))
                                {
                                    sb.AppendFormat("&{0}={1}", item, data[item]);
                                }
                            }

                            string msg = sb.ToString();
                            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(GCM_URL);
                            req.Method = "POST";
                            req.ContentLength = msg.Length;
                            req.ContentType = "application/x-www-form-urlencoded";
                            req.Headers.Add("Authorization:key=" + apiServidor);

                            using (StreamWriter oWriter = new StreamWriter(req.GetRequestStream()))
                            {
                                oWriter.Write(msg);
                            }

                            using (HttpWebResponse resp = (HttpWebResponse)req.GetResponse())
                            {
                                using (StreamReader sr = new StreamReader(resp.GetResponseStream()))
                                {
                                    string respData = sr.ReadToEnd();

                                    if (resp.StatusCode == HttpStatusCode.OK)
                                    {
                                        // OK = 200
                                        /*if (respData.StartsWith("id="))
                                        {
                                            flag = true;
                                        }*/
                                    }
                                    else if (resp.StatusCode == HttpStatusCode.InternalServerError)
                                    {
                                        // 500
                                        Console.WriteLine("Error interno del servidor, prueba más tarde.");
                                    }
                                    else if (resp.StatusCode == HttpStatusCode.ServiceUnavailable)
                                    {
                                        // 503
                                        Console.WriteLine("Servidor no disponible temporalmente, prueba más tarde.");
                                    }
                                    else if (resp.StatusCode == HttpStatusCode.Unauthorized)
                                    {
                                        // 401
                                        Console.WriteLine("La API Key utilizada no es válida.");
                                    }
                                    else
                                    {
                                        Console.WriteLine("Error: " + resp.StatusCode);
                                    }
                                }
                            }

                            string query3 = "update mensajes set estado = @1 where idMensaje = @2";
                            MySqlCommand cmd3 = new MySqlCommand(query3, conn);
                            cmd3.Parameters.AddWithValue("@1", estado);
                            cmd3.Parameters.AddWithValue("@2", idMensaje);
                            cmd3.ExecuteNonQuery();

                            return new JavaScriptSerializer().Serialize(new
                            {
                                Result = "CONFIRMADO",
                                Via = via,
                                IdDocente = idDocente,
                                IdRepresentante = idRepresentante,
                                IdMensaje = idMensaje,
                                Estado = estado
                            });

                        }
                        else
                        {
                            return new JavaScriptSerializer().Serialize(new
                            {
                                Result = "ERROR",
                                Message = "No registrado"
                            });
                        }
                    }
                    else
                    {
                        return new JavaScriptSerializer().Serialize(new
                        {
                            Result = "ERROR",
                            Message = "Mensaje no existe"
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                return new JavaScriptSerializer().Serialize(new
                {
                    Result = "ERROR",
                    Message = ex.Message
                });
            }
        }

        [WebMethod]
        public string eliminarGcmRep(int idRepresentante, int origen)
        {
            /*origen
             * 0 = cierre de sesion manual
             * 1 = por inicio en otro dispositivo
             */

            try
            {
                using (MySqlConnection conn = new MySqlConnection(cadena))
                {
                    conn.Open();

                    if (origen == 0) // cierre de sesion de usuario
                    {
                        notificarRegistroRep(idRepresentante, 0);

                        string query = "delete from registrosgcm where idRepresentante = @1";
                        MySqlCommand cmd = new MySqlCommand(query, conn);
                        cmd.Parameters.AddWithValue("@1", idRepresentante);
                        cmd.ExecuteNonQuery();

                        return new JavaScriptSerializer().Serialize(new
                        {
                            Result = "OK"
                        });
                    }
                    else // cerrar sesion en otros dispositivos
                    {
                        string query = "select * from registrosGcm where idRepresentante = @1";
                        MySqlCommand cmd = new MySqlCommand(query, conn);
                        cmd.Parameters.AddWithValue("@1", idRepresentante);
                        MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                        DataSet ds = new DataSet();
                        da.Fill(ds);

                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                            {
                                cerrarSesionesRep(
                                    ds.Tables[0].Rows[i]["idGcm"].ToString(),
                                    ds.Tables[0].Rows[i]["apiServidor"].ToString(),
                                    idRepresentante);
                            }                            
                        }

                        return new JavaScriptSerializer().Serialize(new
                        {
                            Result = "OK"
                        });
                    }                    
                }
            }
            catch (Exception ex)
            {
                return new JavaScriptSerializer().Serialize(new
                {
                    Result = "ERROR",
                    Message = ex.Message
                });
            }
        }

        private void cerrarSesionesRep(string idGcm, string apiServidor, int idRepresentante)
        {
            string GCM_URL = "https://gcm-http.googleapis.com/gcm/send";
            
            string msj = new JavaScriptSerializer().Serialize(new
            {
                Result = "UNREGISTERED",
                IdRepresentante = idRepresentante
            });

            Dictionary<string, string> data = new Dictionary<string, string>();
            data.Add("data.mensaje", HttpUtility.UrlEncode(msj));

            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("registration_id={0}", idGcm);

            foreach (string item in data.Keys)
            {
                if (item.Contains("data."))
                {
                    sb.AppendFormat("&{0}={1}", item, data[item]);
                }
            }

            string msg = sb.ToString();
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(GCM_URL);
            req.Method = "POST";
            req.ContentLength = msg.Length;
            req.ContentType = "application/x-www-form-urlencoded";
            req.Headers.Add("Authorization:key=" + apiServidor);

            using (StreamWriter oWriter = new StreamWriter(req.GetRequestStream()))
            {
                oWriter.Write(msg);
            }

            using (HttpWebResponse resp = (HttpWebResponse)req.GetResponse())
            {
                using (StreamReader sr = new StreamReader(resp.GetResponseStream()))
                {
                    string respData = sr.ReadToEnd();

                    if (resp.StatusCode == HttpStatusCode.OK)
                    {
                        // OK = 200
                        /*if (respData.StartsWith("id="))
                        {
                            flag = true;
                        }*/
                    }
                    else if (resp.StatusCode == HttpStatusCode.InternalServerError)
                    {
                        // 500
                        Console.WriteLine("Error interno del servidor, prueba más tarde.");
                    }
                    else if (resp.StatusCode == HttpStatusCode.ServiceUnavailable)
                    {
                        // 503
                        Console.WriteLine("Servidor no disponible temporalmente, prueba más tarde.");
                    }
                    else if (resp.StatusCode == HttpStatusCode.Unauthorized)
                    {
                        // 401
                        Console.WriteLine("La API Key utilizada no es válida.");
                    }
                    else
                    {
                        Console.WriteLine("Error: " + resp.StatusCode);
                    }
                }
            }

            using (MySqlConnection conn = new MySqlConnection(cadena))
            {
                conn.Open();

                string query = "delete from registrosGcm where idRepresentante = @1";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@1", idRepresentante);
                cmd.ExecuteNonQuery();
            }
        }

        private void cerrarSesionesDoc(string idGcm, string apiServidor, int idDocente)
        {
            string GCM_URL = "https://gcm-http.googleapis.com/gcm/send";

            string msj = new JavaScriptSerializer().Serialize(new
            {
                Result = "UNREGISTERED",
                IdDocente = idDocente
            });

            Dictionary<string, string> data = new Dictionary<string, string>();
            data.Add("data.mensaje", HttpUtility.UrlEncode(msj));

            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("registration_id={0}", idGcm);

            foreach (string item in data.Keys)
            {
                if (item.Contains("data."))
                {
                    sb.AppendFormat("&{0}={1}", item, data[item]);
                }
            }

            string msg = sb.ToString();
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(GCM_URL);
            req.Method = "POST";
            req.ContentLength = msg.Length;
            req.ContentType = "application/x-www-form-urlencoded";
            req.Headers.Add("Authorization:key=" + apiServidor);

            using (StreamWriter oWriter = new StreamWriter(req.GetRequestStream()))
            {
                oWriter.Write(msg);
            }

            using (HttpWebResponse resp = (HttpWebResponse)req.GetResponse())
            {
                using (StreamReader sr = new StreamReader(resp.GetResponseStream()))
                {
                    string respData = sr.ReadToEnd();

                    if (resp.StatusCode == HttpStatusCode.OK)
                    {
                        // OK = 200
                        /*if (respData.StartsWith("id="))
                        {
                            flag = true;
                        }*/
                    }
                    else if (resp.StatusCode == HttpStatusCode.InternalServerError)
                    {
                        // 500
                        Console.WriteLine("Error interno del servidor, prueba más tarde.");
                    }
                    else if (resp.StatusCode == HttpStatusCode.ServiceUnavailable)
                    {
                        // 503
                        Console.WriteLine("Servidor no disponible temporalmente, prueba más tarde.");
                    }
                    else if (resp.StatusCode == HttpStatusCode.Unauthorized)
                    {
                        // 401
                        Console.WriteLine("La API Key utilizada no es válida.");
                    }
                    else
                    {
                        Console.WriteLine("Error: " + resp.StatusCode);
                    }
                }
            }

            using (MySqlConnection conn = new MySqlConnection(cadena))
            {
                conn.Open();

                string query = "delete from registrosGcm where idDocente = @1";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@1", idDocente);
                cmd.ExecuteNonQuery();
            }
        }

        [WebMethod]
        public string eliminarGcmDoc(int idDocente, int origen)
        {
            /*origen
             * 0 = cierre de sesion manual
             * 1 = por inicio en otro dispositivo
             */

            try
            {
                using (MySqlConnection conn = new MySqlConnection(cadena))
                {
                    conn.Open();

                    if (origen == 0) // cierre de sesion de usuario
                    {
                        notificarRegistroDoc(idDocente, 0);

                        string query = "delete from registrosgcm where idDocente = @1";
                        MySqlCommand cmd = new MySqlCommand(query, conn);
                        cmd.Parameters.AddWithValue("@1", idDocente);
                        cmd.ExecuteNonQuery();

                        return new JavaScriptSerializer().Serialize(new
                        {
                            Result = "OK"
                        });
                    }
                    else // cerrar sesion en otros dispositivos
                    {
                        string query = "select * from registrosGcm where idDocente = @1";
                        MySqlCommand cmd = new MySqlCommand(query, conn);
                        cmd.Parameters.AddWithValue("@1", idDocente);
                        MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                        DataSet ds = new DataSet();
                        da.Fill(ds);

                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                            {
                                cerrarSesionesDoc(
                                    ds.Tables[0].Rows[i]["idGcm"].ToString(),
                                    ds.Tables[0].Rows[i]["apiServidor"].ToString(),
                                    idDocente);
                            }
                        }

                        return new JavaScriptSerializer().Serialize(new
                        {
                            Result = "OK"
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                return new JavaScriptSerializer().Serialize(new
                {
                    Result = "ERROR",
                    Message = ex.Message
                });
            }
        }

        [WebMethod]
        public string notificarRegistroRep(int idRepresentante, int registrado)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(cadena))
                {
                    conn.Open();

                    string query = "select d.*, IFNULL(g.idGcm,'') as idGcm, IFNULL(g.apiServidor,'') as apiServidor " +
                        "from docentes as d left join cursosalumnos as ca on d.idDocente = ca.idDocente " +
                        "left join registrosgcm as g on d.idDocente = g.idDocente " +
                        "left join alumnos as a on ca.idAlumno = a.idAlumno " +
                        "left join representantes as r on a.idRepresentante = r.idRepresentante " +
                        "where r.idRepresentante = @1 group by d.idDocente;";

                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@1", idRepresentante);
                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    da.Fill(ds);

                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            DataRow row = ds.Tables[0].Rows[i];

                            string idGcm = row["idGcm"].ToString();
                            string apiServidor = row["apiServidor"].ToString();

                            if (!idGcm.Equals("") && !apiServidor.Equals(""))
                            {
                                string GCM_URL = "https://gcm-http.googleapis.com/gcm/send";
                                
                                string msj = new JavaScriptSerializer().Serialize(new
                                {
                                    Result = registrado == 1 ? "REPREGISTERED" : "REPUNREGISTERED",
                                    IdRepresentante = idRepresentante
                                });

                                Dictionary<string, string> data = new Dictionary<string, string>();
                                data.Add("data.mensaje", HttpUtility.UrlEncode(msj));

                                StringBuilder sb = new StringBuilder();
                                sb.AppendFormat("registration_id={0}", idGcm);

                                foreach (string item in data.Keys)
                                {
                                    if (item.Contains("data."))
                                    {
                                        sb.AppendFormat("&{0}={1}", item, data[item]);
                                    }
                                }

                                string msg = sb.ToString();
                                HttpWebRequest req = (HttpWebRequest)WebRequest.Create(GCM_URL);
                                req.Method = "POST";
                                req.ContentLength = msg.Length;
                                req.ContentType = "application/x-www-form-urlencoded";
                                req.Headers.Add("Authorization:key=" + apiServidor);

                                using (StreamWriter oWriter = new StreamWriter(req.GetRequestStream()))
                                {
                                    oWriter.Write(msg);
                                }

                                using (HttpWebResponse resp = (HttpWebResponse)req.GetResponse())
                                {
                                    using (StreamReader sr = new StreamReader(resp.GetResponseStream()))
                                    {
                                        string respData = sr.ReadToEnd();

                                        if (resp.StatusCode == HttpStatusCode.OK)
                                        {
                                            // OK = 200
                                            /*if (respData.StartsWith("id="))
                                            {
                                                flag = true;
                                            }*/
                                        }
                                        else if (resp.StatusCode == HttpStatusCode.InternalServerError)
                                        {
                                            // 500
                                            Console.WriteLine("Error interno del servidor, prueba más tarde.");
                                        }
                                        else if (resp.StatusCode == HttpStatusCode.ServiceUnavailable)
                                        {
                                            // 503
                                            Console.WriteLine("Servidor no disponible temporalmente, prueba más tarde.");
                                        }
                                        else if (resp.StatusCode == HttpStatusCode.Unauthorized)
                                        {
                                            // 401
                                            Console.WriteLine("La API Key utilizada no es válida.");
                                        }
                                        else
                                        {
                                            Console.WriteLine("Error: " + resp.StatusCode);
                                        }
                                    }
                                }
                            }                                                   
                        }

                        return new JavaScriptSerializer().Serialize(new
                        {
                            Result = "OK"
                        });
                    }
                    else
                    {
                        return new JavaScriptSerializer().Serialize(new
                        {
                            Result = "NO ROWS"
                        });
                    }                   
                } 
            }
            catch (Exception ex)
            {
                return new JavaScriptSerializer().Serialize(new
                {
                    Result = "ERROR",
                    Message = ex.Message
                });
            }
        }

        [WebMethod]
        public string notificarRegistroDoc(int idDocente, int registrado)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(cadena))
                {
                    conn.Open();

                    string query = "select r.*, IFNULL(g.idGcm,'') as idGcm, IFNULL(g.apiServidor,'') as apiServidor " +
                                    "from representantes as r left join alumnos as a on r.idRepresentante = a.idRepresentante " +
                                    "left join registrosgcm as g on r.idRepresentante = g.idRepresentante " +
                                    "left join cursosalumnos as ca on a.idAlumno = ca.idAlumno " +
                                    "left join docentes as d on ca.idDocente = d.idDocente " +
                                    "where d.idDocente = @1 group by r.idRepresentante;";

                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@1", idDocente);
                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    da.Fill(ds);

                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            DataRow row = ds.Tables[0].Rows[i];

                            string idGcm = row["idGcm"].ToString();
                            string apiServidor = row["apiServidor"].ToString();

                            if (!idGcm.Equals("") && !apiServidor.Equals(""))
                            {
                                string GCM_URL = "https://gcm-http.googleapis.com/gcm/send";

                                string msj = new JavaScriptSerializer().Serialize(new
                                {
                                    Result = registrado == 1 ? "DOCREGISTERED" : "DOCUNREGISTERED",
                                    IdDocente = idDocente
                                });

                                Dictionary<string, string> data = new Dictionary<string, string>();
                                data.Add("data.mensaje", HttpUtility.UrlEncode(msj));

                                StringBuilder sb = new StringBuilder();
                                sb.AppendFormat("registration_id={0}", idGcm);

                                foreach (string item in data.Keys)
                                {
                                    if (item.Contains("data."))
                                    {
                                        sb.AppendFormat("&{0}={1}", item, data[item]);
                                    }
                                }

                                string msg = sb.ToString();
                                HttpWebRequest req = (HttpWebRequest)WebRequest.Create(GCM_URL);
                                req.Method = "POST";
                                req.ContentLength = msg.Length;
                                req.ContentType = "application/x-www-form-urlencoded";
                                req.Headers.Add("Authorization:key=" + apiServidor);

                                using (StreamWriter oWriter = new StreamWriter(req.GetRequestStream()))
                                {
                                    oWriter.Write(msg);
                                }

                                using (HttpWebResponse resp = (HttpWebResponse)req.GetResponse())
                                {
                                    using (StreamReader sr = new StreamReader(resp.GetResponseStream()))
                                    {
                                        string respData = sr.ReadToEnd();

                                        if (resp.StatusCode == HttpStatusCode.OK)
                                        {
                                            // OK = 200
                                            /*if (respData.StartsWith("id="))
                                            {
                                                flag = true;
                                            }*/
                                        }
                                        else if (resp.StatusCode == HttpStatusCode.InternalServerError)
                                        {
                                            // 500
                                            Console.WriteLine("Error interno del servidor, prueba más tarde.");
                                        }
                                        else if (resp.StatusCode == HttpStatusCode.ServiceUnavailable)
                                        {
                                            // 503
                                            Console.WriteLine("Servidor no disponible temporalmente, prueba más tarde.");
                                        }
                                        else if (resp.StatusCode == HttpStatusCode.Unauthorized)
                                        {
                                            // 401
                                            Console.WriteLine("La API Key utilizada no es válida.");
                                        }
                                        else
                                        {
                                            Console.WriteLine("Error: " + resp.StatusCode);
                                        }
                                    }
                                }
                            }
                        }

                        return new JavaScriptSerializer().Serialize(new
                        {
                            Result = "OK"
                        });
                    }
                    else
                    {
                        return new JavaScriptSerializer().Serialize(new
                        {
                            Result = "NO ROWS"
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                return new JavaScriptSerializer().Serialize(new
                {
                    Result = "ERROR",
                    Message = ex.Message
                });
            }
        }

        [WebMethod]
        public string getRepresentados (int idRepresentante)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(cadena))
                {
                    conn.Open();

                    string query = "select a.*, c.*, d.apellidos as apellidosDoc, d.nombres as nombresDoc from alumnos as a " +
                                    "left join cursosalumnos as ca on a.idAlumno = ca.idAlumno " +
                                    "left join docentes as d on ca.idDocente = d.idDocente " +
                                    "left join cursos as c on ca.idCurso = c.idCurso " +
                                    "where a.idRepresentante = @1 order by c.grado, c.seccion asc; " +
                                    "select * from institucion";

                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@1", idRepresentante);
                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    da.Fill(ds);

                    List<Representado> representados = new List<Representado>();
                    Institucion institucion = new Institucion();

                    bool hayRepresentados = false;
                    bool hayInstitucion = false;

                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        for (int i = 0;i< ds.Tables[0].Rows.Count; i++)
                        {
                            Representado representado = new Representado();
                            DataRow row = ds.Tables[0].Rows[i];

                            representado.IdAlumno = (int)row["idAlumno"];
                            representado.Apellidos = row["apellidos"].ToString();
                            representado.Nombres = row["nombres"].ToString();
                            representado.Grado = (int)row["grado"];
                            representado.Seccion = row["seccion"].ToString();
                            representado.Docente = row["apellidosDoc"].ToString() + ", " + row["nombresDoc"].ToString();
                            representados.Add(representado);
                        }

                        hayRepresentados = true;                                                   
                    }                    

                    if (ds.Tables[1].Rows.Count > 0)
                    {
                        
                        institucion.Nombre = ds.Tables[1].Rows[0]["nombre"].ToString();
                        institucion.Direccion = ds.Tables[1].Rows[0]["direccion"].ToString();
                        institucion.Telefono1 = ds.Tables[1].Rows[0]["telefono1"].ToString();
                        institucion.Telefono2 = ds.Tables[1].Rows[0]["telefono2"].ToString();
                        institucion.Fundacion = int.Parse(ds.Tables[1].Rows[0]["fundacion"].ToString());
                        institucion.Director = ds.Tables[1].Rows[0]["director"].ToString();
                        institucion.Etapas = ds.Tables[1].Rows[0]["etapas"].ToString();

                        hayInstitucion = true;
                        
                    }

                    return new JavaScriptSerializer().Serialize(new
                    {
                        Result = "OK",
                        Representados = hayRepresentados ? representados : (object)"",
                        Institucion = hayInstitucion ? institucion : (object)""
                    });
                }
            }
            catch (Exception ex)
            {
                return new JavaScriptSerializer().Serialize(new
                {
                    Result = "ERROR",
                    Message = ex.Message
                });
            }
        }

        [WebMethod]
        public string getCursos(int idDocente)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(cadena))
                {
                    conn.Open();

                    string query = "select c.* from cursos as c " +
                                    "left join cursosalumnos as ca on c.idCurso = ca.idCurso " +
                                    "where ca.idDocente = @1 group by c.idCurso order by c.grado, c.seccion asc; " +
                                    "select * from institucion;";

                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@1", idDocente);
                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    da.Fill(ds);

                    List<Curso> cursos = new List<Curso>();
                    Institucion institucion = new Institucion();

                    bool hayCursos = false;
                    bool hayInstitucion = false;

                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            Curso curso = new Curso();
                            DataRow row = ds.Tables[0].Rows[i];

                            curso.Id = (int)row["idCurso"];
                            curso.Grado = (int)row["grado"];
                            curso.Seccion = row["seccion"].ToString();                            
                            cursos.Add(curso);
                        }

                        hayCursos = true;
                    }

                    if (ds.Tables[1].Rows.Count > 0)
                    {

                        institucion.Nombre = ds.Tables[1].Rows[0]["nombre"].ToString();
                        institucion.Direccion = ds.Tables[1].Rows[0]["direccion"].ToString();
                        institucion.Telefono1 = ds.Tables[1].Rows[0]["telefono1"].ToString();
                        institucion.Telefono2 = ds.Tables[1].Rows[0]["telefono2"].ToString();
                        institucion.Fundacion = int.Parse(ds.Tables[1].Rows[0]["fundacion"].ToString());
                        institucion.Director = ds.Tables[1].Rows[0]["director"].ToString();
                        institucion.Etapas = ds.Tables[1].Rows[0]["etapas"].ToString();

                        hayInstitucion = true;

                    }

                    return new JavaScriptSerializer().Serialize(new
                    {
                        Result = "OK",
                        Cursos = hayCursos ? cursos : (object)"",
                        Institucion = hayInstitucion ? institucion : (object)""
                    });
                }
            }
            catch (Exception ex)
            {
                return new JavaScriptSerializer().Serialize(new
                {
                    Result = "ERROR",
                    Message = ex.Message
                });
            }
        }

        [WebMethod]
        public string saveImagenPerfil(int IdRepresentante, int IdDocente, string imageString)
        {
            try
            {                
                using (MySqlConnection conn = new MySqlConnection(cadena))
                {
                    conn.Open();

                    string query = "";
                    int id = 0;

                    if (IdRepresentante > 0)
                    {
                        query = "update representantes set imagen = @1 where idRepresentante = @2";
                        id = IdRepresentante;
                    }
                    else
                    {
                        query = "update docentes set imagen = @1 where idDocente = @2";
                        id = IdDocente;
                    }

                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    string resultado = "";
                                        
                    MySqlParameter param1 = new MySqlParameter("@1", MySqlDbType.LongBlob);
                    if (imageString.Equals(""))
                    {
                        param1.Value = DBNull.Value;
                        cmd.Parameters.Add(param1);
                        resultado = "DELETED";
                    }
                    else
                    {
                        byte[] imageBytes = Convert.FromBase64String(imageString);
                        param1.Value = imageBytes;
                        cmd.Parameters.Add(param1);
                        resultado = "INSERTED";
                    }
                    
                    MySqlParameter param2 = new MySqlParameter("@2", MySqlDbType.Int32);
                    param2.Value = id;
                    cmd.Parameters.Add(param2);

                    cmd.ExecuteNonQuery();

                    return new JavaScriptSerializer().Serialize(new
                    {
                        Result = resultado
                    });
                }  
            }
            catch (Exception ex)
            {
                return new JavaScriptSerializer().Serialize(new
                {
                    Result = "ERROR",
                    Message = ex.Message
                });
            }
        }

        private string getImagenPerfil(int IdRepresentante, int IdDocente)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(cadena))
                {
                    conn.Open();

                    string query = "";
                    int id = 0;

                    if (IdRepresentante > 0)
                    {
                        query = "select imagen from representantes where idRepresentante = @1";
                        id = IdRepresentante;
                    }else
                    {
                        query = "select imagen from docentes where idDocente = @1";
                        id = IdDocente;
                    }

                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@1", id);
                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    da.Fill(ds);

                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        byte[] imageBytes = (byte[])ds.Tables[0].Rows[0][0];

                        string imageString = Convert.ToBase64String(imageBytes);

                        return imageString;
                    }
                    else{
                        return  "NO ROWS";
                    }
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        [WebMethod]
        public string cambiarClave(int idRepresentante, int idDocente, string claveActual, string claveNueva)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(cadena))
                {
                    conn.Open();
                                        
                    string query1 = "", query2 = "";
                    MySqlCommand cmd;
                    MySqlDataAdapter da;
                    DataSet ds;

                    if (idRepresentante > 0)
                    {
                        query1 = "select * from representantes where idRepresentante = @1";
                        cmd = new MySqlCommand(query1, conn);
                        cmd.Parameters.AddWithValue("@1", idRepresentante);
                        da = new MySqlDataAdapter(cmd);
                        ds = new DataSet();
                        da.Fill(ds);

                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            DataRow row = ds.Tables[0].Rows[0];

                            if (row["clave"].ToString().Equals(claveActual))
                            {
                                query2 = "update representantes set clave = @1 where idRepresentante = @2";

                                cmd = new MySqlCommand(query2, conn);
                                cmd.Parameters.AddWithValue("@1", claveNueva);
                                cmd.Parameters.AddWithValue("@2", idRepresentante);
                                cmd.ExecuteNonQuery();

                                return new JavaScriptSerializer().Serialize(new
                                {
                                    Result = "OK"
                                });
                            }
                            else
                            {
                                return new JavaScriptSerializer().Serialize(new
                                {
                                    Result = "NO PASS"
                                });
                            }
                        }else
                        {
                            return new JavaScriptSerializer().Serialize(new
                            {
                                Result = "NO ROWS"
                            });
                        }
                    }
                    else
                    {
                        query1 = "select * from docentes where idDocente = @1";
                        cmd = new MySqlCommand(query1, conn);
                        cmd.Parameters.AddWithValue("@1", idDocente);
                        da = new MySqlDataAdapter(cmd);
                        ds = new DataSet();
                        da.Fill(ds);

                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            DataRow row = ds.Tables[0].Rows[0];

                            if (row["clave"].ToString().Equals(claveActual))
                            {
                                query2 = "update docentes set clave = @1 where idDocente = @2";

                                cmd = new MySqlCommand(query2, conn);
                                cmd.Parameters.AddWithValue("@1", claveNueva);
                                cmd.Parameters.AddWithValue("@2", idDocente);
                                cmd.ExecuteNonQuery();

                                return new JavaScriptSerializer().Serialize(new
                                {
                                    Result = "OK"
                                });
                            }
                            else
                            {
                                return new JavaScriptSerializer().Serialize(new
                                {
                                    Result = "NO PASS"
                                });
                            }
                        }
                        else
                        {
                            return new JavaScriptSerializer().Serialize(new
                            {
                                Result = "NO ROWS"
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return new JavaScriptSerializer().Serialize(new
                {
                    Result = "ERROR",
                    Message = ex.Message
                });
            }
        }
    }
}
