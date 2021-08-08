using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TVShows.Modelos;

namespace TVShows
{
    class Program
    {
        static void Main(string[] args)
        {

            ObtieneProgramas programas = new ObtieneProgramas(0);
            ActualizaPrograma actualiza;

            string entrada = "";
            int identificador = 0;
            entrada = Console.ReadLine();
            Console.WriteLine("\n  ");
            while (entrada.Trim() != "")
            {
               
                bool EsEntero = Int32.TryParse(entrada, out identificador);

                programas.shows.Clear();
                
                if (entrada.Trim() != "" && !EsEntero && entrada.Trim() == "list")
                    programas = new ObtieneProgramas(0);
                else if (entrada.Trim() != "" && !EsEntero && entrada.Trim() == "favorites")
                    programas = new ObtieneProgramas(99);
                else if (entrada.Trim() != "" && EsEntero)
                {
                 
                    programas = new ObtieneProgramas(int.Parse(entrada));
                    if (programas.shows[0].LFAVORITO)
                        actualiza = new ActualizaPrograma(int.Parse(entrada), 0);
                    else
                        actualiza = new ActualizaPrograma(int.Parse(entrada), 1);
                }

                else if (!EsEntero)
                    Console.WriteLine("\n El dato ingresado no es un entero");
                Console.WriteLine("\n  ");
                entrada = Console.ReadLine();
            }
        }

    }
}

