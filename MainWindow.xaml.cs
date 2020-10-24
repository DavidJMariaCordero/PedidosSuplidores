using PedidosSuplidores.UI.Registros;
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

namespace PedidosSuplidores
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void rOrdenes_Click(object sender, RoutedEventArgs e)
        {
            rOrdenes orden = new rOrdenes();
            orden.Show();
        }
        private void rSuplidores_Click(object sender, RoutedEventArgs e)
        {
            rSuplidores suplidor = new rSuplidores();
            suplidor.Show();
        }

        private void rProductos_Click(object sender, RoutedEventArgs e)
        {
            rProductos productos = new rProductos();
            productos.Show();
        }
        private void cSuplidores_Click(object sender, RoutedEventArgs e)
        {
            /*rSuplidores suplidor = new rSuplidores();
            suplidor.Show();*/
        }
        private void cOrdenes_Click(object sender, RoutedEventArgs e)
        {
            /*rSuplidores suplidor = new rSuplidores();
            suplidor.Show();*/
        }
        private void cProductos_Click(object sender, RoutedEventArgs e)
        {
            /*rSuplidores suplidor = new rSuplidores();
            suplidor.Show();*/
        }
    }
}
