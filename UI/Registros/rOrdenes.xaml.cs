using PedidosSuplidores.BLL;
using PedidosSuplidores.DAL;
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
    /// Interaction logic for rOrdenes.xaml
    /// </summary>
    public partial class rOrdenes : Window
    {
        Ordenes orden = new Ordenes();
        public rOrdenes()
        {
            InitializeComponent();
            SuplidoresComboBox.ItemsSource = SuplidoresBLL.GetList();
            SuplidoresComboBox.SelectedValuePath = "SuplidorId";
            SuplidoresComboBox.DisplayMemberPath = "Nombres";

            ProductosComboBox.ItemsSource = ProductosBLL.GetList();
            ProductosComboBox.SelectedValuePath = "ProductoId";
            ProductosComboBox.DisplayMemberPath = "Descripcion";

            Limpiar();
        }
        

        private void Limpiar()
        {
            this.orden = new Ordenes();
            this.orden.Fecha = DateTime.Now;
            this.DataContext = orden;
            TotalTextBox.Text = "0";

        }
        private void BuscarBoton_Click(object sender, RoutedEventArgs e)
        {
            Ordenes encontrado = OrdenesBLL.Buscar(Convert.ToInt32(IdTextBox.Text));

            if (encontrado != null)
            {
                this.orden = encontrado;
                this.DataContext = null;
                this.DataContext = orden;
            }
            else
            {
                Limpiar();
                MessageBox.Show("El numero de orden no se encuentra en la base de datos", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void EliminarBoton_Click(object sender, RoutedEventArgs e)
        {
            Ordenes existe = OrdenesBLL.Buscar(this.orden.OrdenId);

            if (existe == null)
            {
                MessageBox.Show("No existe la orden en la base de datos", "Fallo",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            else
            {
                OrdenesBLL.Eliminar(this.orden.OrdenId);
                MessageBox.Show("Eliminado", "Exito",
                    MessageBoxButton.OK, MessageBoxImage.Information);
                Limpiar();
            }
        }
        private void GuardarBoton_Click(object sender, RoutedEventArgs e)
        {
            bool paso = false;
            
            if (Validar())
                paso = OrdenesBLL.Guardar(orden);
            
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
            Ordenes esValido = OrdenesBLL.Buscar(orden.OrdenId);

            return (esValido != null);
        }
        private void AgregarBoton_Click(object sender, RoutedEventArgs e)
        {
            Contexto context = new Contexto();
            orden.Monto += Convert.ToDecimal(PrecioTextBox.Text) * Convert.ToDecimal(CantidadTextBox.Text);
            orden.Detalle.Add(new OrdenesDetalle(orden.OrdenId, Convert.ToInt32(ProductosComboBox.SelectedValue), Convert.ToSingle(CantidadTextBox.Text), Convert.ToDecimal(PrecioTextBox.Text)));
            ItbisTextBox.Text = Convert.ToString(orden.Monto * Convert.ToDecimal(0.18));
            SubTotalTextBox.Text = Convert.ToString(orden.Monto - (orden.Monto * Convert.ToDecimal(0.18)));

            this.DataContext = null;
            this.DataContext = orden;
        }

        private void RemoverBoton_Click(object sender, RoutedEventArgs e)
        {
            if (OrdenesDataGrid.Items.Count >= 1 && OrdenesDataGrid.SelectedIndex <= OrdenesDataGrid.Items.Count - 1)
            {
                OrdenesDetalle order = (OrdenesDetalle)OrdenesDataGrid.SelectedValue;
                orden.Monto -= order.Costo * Convert.ToDecimal(order.Cantidad);
                ItbisTextBox.Text = Convert.ToString(orden.Monto * Convert.ToDecimal(0.18));
                SubTotalTextBox.Text = Convert.ToString(orden.Monto - (orden.Monto * Convert.ToDecimal(0.18)));
                orden.Detalle.RemoveAt(OrdenesDataGrid.SelectedIndex);
                this.DataContext = null;
                this.DataContext = orden;
            }
        }

        private void NuevoBoton_Click(object sender, RoutedEventArgs e)
        {
            Limpiar();
        }

        private void ProductosComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Productos producto = ProductosBLL.Buscar(Convert.ToInt32(ProductosComboBox.SelectedValue));
            if (ProductosComboBox.SelectedValue != null)
                PrecioTextBox.Text = Convert.ToString(producto.Costo);
            else
                PrecioTextBox.Text = "";
        }

        private void CantidadTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (CantidadTextBox.Text.Any(char.IsLetter))
                {
                    CantidadTextBox.BorderBrush = new SolidColorBrush(Colors.Red);
                    MessageBox.Show("En la cantidad solo se debe ingresar numeros", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    CantidadTextBox.BorderBrush = new SolidColorBrush(Colors.Black);
                }
            }
            catch
            {
                CantidadTextBox.Foreground = SystemColors.ControlTextBrush;
            }
        }

        private void PrecioTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (PrecioTextBox.Text.Any(char.IsLetter))
                {
                    PrecioTextBox.BorderBrush = new SolidColorBrush(Colors.Red);
                    MessageBox.Show("En el precio solo se debe ingresar numeros", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    PrecioTextBox.BorderBrush = new SolidColorBrush(Colors.Black);
                }
            }
            catch
            {
                PrecioTextBox.Foreground = SystemColors.ControlTextBrush;
            }
        }

        public bool Validar()
        {
            bool esValido = true;
            
            if (FechaDatePicker.SelectedDate < DateTime.Now)
            {
                MessageBox.Show("No puede elegir una fecha menor a la actua", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                esValido = false;
            }
            if (SuplidoresComboBox.SelectedIndex < 0)
            {
                MessageBox.Show("Debe elegir un suplidor", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                esValido = false;
            }
            /*else if (ProductosComboBox.SelectedIndex < 0)
            {
                MessageBox.Show("debe elegir un producto", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                esValido = false;
            }*/
            return esValido;
        }
    }
}
