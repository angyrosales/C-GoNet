using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TVShows.Modelos;

namespace TVShows
{
    class ObtieneProgramas
    {
        public List<TVShows.Modelos.Shows> shows { get; set; }

        public ObtieneProgramas(int modo)
        {
            try
            {
                string qConsulta = "";
                DataTable dt = new DataTable();
                TVShows.Resources.Conexion conexion = new TVShows.Resources.Conexion();
                shows = new List<TVShows.Modelos.Shows>();
                if(modo!=0 && modo !=99)
                    qConsulta = @"SELECT NIDENTIFICADOR, ANOMBRE,LFAVORITO FROM dbo.Shows WHERE NIDENTIFICADOR="+ modo;
                else if(modo==0)
                    qConsulta = @"SELECT NIDENTIFICADOR, ANOMBRE,LFAVORITO FROM dbo.Shows";
                else if (modo==99)
                    qConsulta = @"SELECT NIDENTIFICADOR, ANOMBRE,LFAVORITO FROM dbo.Shows WHERE LFAVORITO=1" ;

                dt = conexion.ObtieneProgramas(qConsulta);

                foreach (DataRow registro in dt.Rows)
                {


                    shows.Add(
                   new TVShows.Modelos.Shows
                   {
                       NIDENTIFICADOR = int.Parse(registro["NIDENTIFICADOR"].ToString()),
                       ANOMBRE = registro["ANOMBRE"].ToString(),
                       LFAVORITO = registro["LFAVORITO"].ToString().Equals("False") ? false : true
                   }
                      );

                }

                if (modo == 0 || modo==99)
                {
                    Console.WriteLine("\n Lista de Programas");
                    string texto = "";

                    if (shows.Count != 0)
                    {
                        List<Shows> alfa = shows.OrderBy(shows => shows.ANOMBRE).ToList();
                        foreach (var item in alfa)
                        {
                            if (item.LFAVORITO)
                                texto = "*";
                            else
                            {
                                texto = "";
                            }
                            Console.WriteLine(item.NIDENTIFICADOR + "\t" + item.ANOMBRE + "\t" + texto);
                        }
                        Console.WriteLine("\n  ");
                    }
                }
            }
            catch (Exception ex)
            {
                using (StreamWriter Escribir = File.AppendText(@ConfigurationManager.AppSettings.Get("LOG")))
                {
                    Escribir.WriteLine(DateTime.Now);
                    Escribir.Write("Obtener la lista de programas ");
                    Escribir.WriteLine(ex);
                    Escribir.WriteLine();
                    Escribir.Close();

                }
            }

        }
    }
}
