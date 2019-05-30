using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

namespace LoginWPF
{
    class Autenticacion
    {
        //variable nos sevira para obtener el nombre de usuario logeado
        public static string Nombre;
        public static bool Autenticar(string usuario, string password)
        {
            //consulta a la base de datos
            string sql = @"SELECT nombre
                          FROM Usuario
                          WHERE usuario = @user AND password = @pass";
            //cadena conexion  definida en AppConfig
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["default"].ToString()))
            {
                conn.Open();//abrimos conexion

                SqlCommand cmd = new SqlCommand(sql, conn); //ejecutamos la instruccion
                cmd.Parameters.AddWithValue("@user", usuario); //enviamos los parametros
                cmd.Parameters.AddWithValue("@pass", password);

                //creamos el adaptador

                SqlDataAdapter da = new SqlDataAdapter(cmd);

                DataTable dt = new DataTable();
                da.Fill(dt);
                //valido si encruentra datos
                if (dt.Rows.Count > 0)
                {
                    DataRow row = dt.Rows[0];
                    Nombre = Convert.ToString(row["nombre"]);//guardo el campo nombre de usuario logeado
                    return true;
                }
                else
                    return false;

            }
        }
    }
}
