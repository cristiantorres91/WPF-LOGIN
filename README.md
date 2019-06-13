# WPF-LOGIN
Crear Login en WPF C#

Articulo completo: http://cristiantorresalfaro.blogspot.com/2019/05/crear-login-wpf-c.html


En este articulo veremos como crear un <b>Login </b>en <b>WPF</b> usando <b>C# </b>y que este me muestre el nombre del usuario que a iniciado sesión. que es una de las consultas que me hacia un lector de mi blog&nbsp; así que manos a la obra.<br />
<br />
El ejemplo lo he realizado usando SQL SERVER 2014 EXPRESS y VS 2017<br />
<br />
La base de datos que ocupare en esta ocasión se llama Usuarios y solo cuenta con una tabla Usuario<br />

<br />
Creamos nuestro  proyecto WPF en Visual Studio

<div class="separator" style="clear: both; text-align: center;">
</div>
<br />
<br />
<br />
Creamos el diseño de nuestro formulario
<br />
<pre class="brush: xml">    
&lt;Window x:Class="LoginWPF.MainWindow"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        xmlns:d="http://schemas.microsoft.com/expression/blend/2008"
        xmlns:mc="http://schemas.openxmlformats.org/markup-compatibility/2006"
        xmlns:local="clr-namespace:LoginWPF"
        mc:Ignorable="d"
        Title="LOGIN " Height="400" Width="400" FontSize="14" Background="Black" WindowStartupLocation="CenterScreen"&gt;
    &lt;Border Background="WhiteSmoke" CornerRadius="20" Margin="20"&gt;
        &lt;StackPanel Margin="20"&gt;
            &lt;Label Foreground="Black" FontSize="25" HorizontalAlignment="Center" &gt;
                &lt;Bold&gt; Login&lt;/Bold&gt;
            &lt;/Label&gt;
            &lt;Separator Background="Black"&gt;&lt;/Separator&gt;
            &lt;Label Foreground="Blue" &gt;
                &lt;Bold&gt;Usuario&lt;/Bold&gt;
            &lt;/Label&gt;
            &lt;TextBox Name="txtUsuario" Foreground="Black" FontSize="18"&gt;&lt;/TextBox&gt;
            &lt;Label Foreground="Blue"&gt;
                &lt;Bold&gt; Contraseña&lt;/Bold&gt;
            &lt;/Label&gt;
            &lt;PasswordBox Name="txtPasword" Foreground="Black" FontSize="18"&gt;&lt;/PasswordBox&gt;
            &lt;Button Name="btnEntrar" Margin="60 10" Background="#545d6a" Foreground="Black" Height="50" Click="btnEntrar_Click"&gt; Entrar&lt;/Button&gt;
        &lt;/StackPanel&gt;
    &lt;/Border&gt;
&lt;/Window&gt;

</pre>
<div class="separator" style="clear: both; text-align: center;">
<a href="https://drive.google.com/uc?id=1SKgYYreqJ2iJNvIObASTp3qQ-d2tXbIn" imageanchor="1" style="margin-left: 1em; margin-right: 1em;"><img border="0" data-original-height="410" data-original-width="409" height="400" src="https://drive.google.com/uc?id=1SKgYYreqJ2iJNvIObASTp3qQ-d2tXbIn" width="398" /></a></div>
<br />
Ahora que ya tenemos nuestro diseño, agregaremos nuestra cadena de conexión a nuestro archivo <b>App.config</b>(como siempre lo hemos hecho en mis anteriores artículos)
<br />
<pre class="brush: xml">  &lt; connectionStrings&gt; 
    &lt; add name="default"
       connectionString="Data Source=.\SQLEXPRESS;AttachDbFilename=|DataDirectory|\Usuarios.mdf;Integrated Security=True;User Instance=True"
        providerName="System.Data.SqlClient" /&gt;
  &lt; /connectionStrings&gt;   
</pre>
<br />
Lo siguiente que aremos es agregar una clase a nuestro proyecto en donde manejaremos el código de nuestro login para tenerlo de una manera mas ordenado(ademas de ser una de las mejores practicas de POO)
<br />
<center>
</center>
<br />
https://drive.google.com/file/d/1nfLVBJ_PuUmmeL7HM1W3z6Ge7liOfExU/preview" 

<br />
<br />
El código que tendrá nuestra clase sera el siguiente
<br />
<pre class="brush: csharp"> 
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
                if (dt.Rows.Count &gt; 0)
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
</pre>
<br />
Lo primero que hacemos es declarar una variable llamada<b> nombre</b> que nos servirá para obtener el nombre del usuario que inicio sesión.
Luego tenemos un método <b>Autenticar</b> de tipo bool que recibe 2 parámetros usuario y password que serán los datos que consultaremos en la bd para verificar si son correctos o no.
El método nos retornara verdadero si los datos ingresados son correctos, y si son correctos guarda en la variable <b>Nombre </b>el nombre del usuario que inicio sesión.<br />
<br />
<b>&nbsp;Importante:&nbsp;</b>dentro de esta clase que creamos agregar la referencia <b>System.Configuration
</b><br />
<br />
Ahora dentro del evento click del botón entrar tendremos el siguiente código
<br />
<pre class="brush: csharp">        private void btnEntrar_Click(object sender, RoutedEventArgs e)
        {
            //validamos datos ingresados llamando nuestro metodo Autenticar de la clase Autenticacion
            if(Autenticacion.Autenticar(txtUsuario.Text,txtPasword.Password))
            {
                //si los datos son correctos muestro nombre de usuario
                MessageBox.Show("Bienvenido:" + Autenticacion.Nombre);
            }
            else
            {
                //si los datos no son correctos
                MessageBox.Show("Ingrese Datos Correctos");
            }

        } 
</pre>
<br />
Como pueden ver lo único que hacemos es invocar el método <b>Autenticar</b> de la clase que creamos <b>Autenticacion </b>y le enviamos como argumentos los datos de las cajas de textbox, si los datos son correctos mostramos un mensaje con el nombre del usuario y si son incorrectos le mostramos un mensaje para que ingrese los datos correctos.
<br />
<center>
</center>
<br />
https://drive.google.com/file/d/1IjpykGVQ3SQKItdsF54K94PxP6niVT5A/preview" 


<br />
<br />
<b>Bueno eso seria todo espero le sea útil, en los próximos&nbsp;artículos&nbsp;estaré&nbsp;poniendo ejemplos de java para android y un poco de php saludos desde El Salvador...</b><br />
