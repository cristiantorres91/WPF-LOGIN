using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LoginWPF
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
      
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnEntrar_Click(object sender, RoutedEventArgs e)
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
    }
}
