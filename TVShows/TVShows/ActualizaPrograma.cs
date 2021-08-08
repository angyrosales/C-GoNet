using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TVShows.Resources;

namespace TVShows
{
    class ActualizaPrograma
    {

        public ActualizaPrograma(int clave, int valor)
        {
            try
            {

               

                    Resources.Conexion cActualiza = new Conexion();
                  string  qActualiza = "UPDATE dbo.Shows SET LFAVORITO ="+valor+"  WHERE NIDENTIFICADOR=" + clave;
            

                    cActualiza.ActualizaPrograma(qActualiza);

                
                
            }
            catch (Exception E)
            {
                using (StreamWriter Escribir = File.AppendText(@ConfigurationManager.AppSettings.Get("LOG")))
                {
                    Escribir.WriteLine(DateTime.Now);
                    Escribir.Write("Actualiza Programa");
                    Escribir.WriteLine(E);
                    Escribir.WriteLine();
                    Escribir.Close();

                }


            }


        }
    }
}
