using PedidosSuplidores.BLL;
using PedidosSuplidores.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PedidosSuplidores.UI.Registros
{
    /// <summary>
    /// Interaction logic for rProductos.xaml
    /// </summary>
    public partial class rProductos : Window
    {
        Productos producto;
        public rProductos()
        {
            InitializeComponent();
            

            Limpiar();
        }

        private void Limpiar()
        {
            this.producto = new Productos();
            this.DataContext = producto;
            ProductosComboBox.ItemsSource = ProductosBLL.GetList();
            ProductosComboBox.SelectedValuePath = "ProductoId";
            ProductosComboBox.DisplayMemberPath = "Descripcion";


        }
        private void BuscarButton_Click(object sender, RoutedEventArgs e)
        {
            Productos encontrado = ProductosBLL.Buscar(Convert.ToInt32(ProductosComboBox.SelectedValue));

            if (encontrado != null)
            {
                this.producto = encontrado;
                this.DataContext = null;
                this.DataContext = producto;
            }
            else
            {
                Limpiar();
                MessageBox.Show("El producto no existe", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void NuevoButton_Click(object sender, RoutedEventArgs e)
        {
            Limpiar();
        }

        private void EliminarButton_Click(object sender, RoutedEventArgs e)
        {
            Productos existe = ProductosBLL.Buscar(producto.ProductoId);

            if (existe == null)
            {
                MessageBox.Show("No existe el producto en la base de datos", "Fallo",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            else
            {
                ProductosBLL.Eliminar(producto.ProductoId);
                MessageBox.Show("Eliminado", "Exito",
                    MessageBoxButton.OK, MessageBoxImage.Information);
                Limpiar();
            }
        }
        private void GuardarButton_Click(object sender, RoutedEventArgs e)
        {
            var paso = ProductosBLL.Guardar(producto);
            if (paso)
            {
                Limpiar();
                MessageBox.Show("Guardado!", "Exito",
                    MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
                MessageBox.Show("Fallo al guardar", "Fallo",
                    MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private bool Existe()
        {
            Productos esValido = ProductosBLL.Buscar(producto.ProductoId);

            return (esValido != null);
        }

        public void DescripcionTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            try 
            { 
            if (DescripcionTextBox.Text.Any(char.IsNumber))
            {
                    DescripcionTextBox.BorderBrush = new SolidColorBrush(Colors.Red);
                    MessageBox.Show("En la descripcion solo se debe ingresar letras", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            else
            {
                    DescripcionTextBox.BorderBrush = new SolidColorBrush(Colors.Black);
            }
            }
            catch
            {
                DescripcionTextBox.Foreground = SystemColors.ControlTextBrush;
            }
        }

        private void CostoTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (CostoTextBox.Text.Any(char.IsLetter))
                {
                    CostoTextBox.BorderBrush = new SolidColorBrush(Colors.Red);
                    MessageBox.Show("En el costo solo se debe ingresar numeros", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    CostoTextBox.BorderBrush = new SolidColorBrush(Colors.Black);
                }
            }
            catch
            {
                CostoTextBox.Foreground = SystemColors.ControlTextBrush;
            }
        }
    }
}
