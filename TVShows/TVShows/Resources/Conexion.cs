using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TVShows.Resources
{
    class Conexion
    {
        SqlConnection conexion;
        string cadenaConexion;
        public Conexion()
        {
            this.cadenaConexion = ConfigurationManager.ConnectionStrings["CadenaConexion"].ConnectionString;
            this.conexion = new SqlConnection(this.cadenaConexion);
        }

        public DataTable ObtieneProgramas(string consulta)
        {
            DataTable resultados = new DataTable();

            //Abrir conexion
            this.conexion.Open();

            //Ejecutar consulta de seleccion
            SqlCommand comando = new SqlCommand(consulta, this.conexion);

            //Guardar resultados de la consulta en un adaptador
            SqlDataAdapter adaptador = new SqlDataAdapter(comando);

            //Llenar DataTable
            adaptador.Fill(resultados);
            conexion.Close();
            return resultados;
        }
        public void ActualizaPrograma(string consulta)
        {

            //Abrir conexion
            this.conexion.Open();
            //Ejecutar consulta de seleccion
            SqlCommand comando = new SqlCommand(consulta, this.conexion);
            SqlTransaction trans = this.conexion.BeginTransaction();
            comando.Transaction = trans;
            comando.CommandText = consulta;
            comando.ExecuteNonQuery();
            trans.Commit();
            conexion.Close();
        }
    }

}
