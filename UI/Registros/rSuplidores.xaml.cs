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
    /// Interaction logic for rSuplidores.xaml
    /// </summary>
    public partial class rSuplidores : Window
    {
        Suplidores suplidor = new Suplidores();
        public rSuplidores()
        {
            InitializeComponent();
            

            Limpiar();
        }

        private void Limpiar()
        {
            this.suplidor = new Suplidores();
            this.DataContext = suplidor;
            SuplidoresComboBox.ItemsSource = SuplidoresBLL.GetList();
            SuplidoresComboBox.SelectedValuePath = "SuplidorId";
            SuplidoresComboBox.DisplayMemberPath = "Nombres";


        }
        private void BuscarButton_Click(object sender, RoutedEventArgs e)
        {
            Suplidores encontrado = SuplidoresBLL.Buscar(Convert.ToInt32(SuplidoresComboBox.SelectedValue));

            if (encontrado != null)
            {
                this.suplidor = encontrado;
                this.DataContext = null;
                this.DataContext = suplidor;
            }
            else
            {
                Limpiar();
                MessageBox.Show("El suplidor no existe", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void EliminarButton_Click(object sender, RoutedEventArgs e)
        {
            Suplidores existe = SuplidoresBLL.Buscar(suplidor.SuplidorId);

            if (existe == null)
            {
                MessageBox.Show("No existe el suplidor en la base de datos", "Fallo",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            else
            {
                SuplidoresBLL.Eliminar(suplidor.SuplidorId);
                MessageBox.Show("Eliminado", "Exito",
                    MessageBoxButton.OK, MessageBoxImage.Information);
                Limpiar();
            }
        }
        private void GuardarButton_Click(object sender, RoutedEventArgs e)
        {
            bool paso = false;

            if (suplidor.SuplidorId == 0)
            {
                paso = SuplidoresBLL.Guardar(suplidor);
            }
            else
            {
                if (Existe())
                {
                    paso = SuplidoresBLL.Guardar(suplidor);
                }
                else
                {
                    MessageBox.Show("No existe en la base de datos", "Error");
                }
            }

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
            Suplidores esValido = SuplidoresBLL.Buscar(suplidor.SuplidorId);

            return (esValido != null);
        }

        private void NuevoButton_Click(object sender, RoutedEventArgs e)
        {
            Limpiar();
        }

        private void NombreTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
                try
                {
                    if (NombreTextBox.Text.Any(char.IsNumber))
                    {
                        NombreTextBox.BorderBrush = new SolidColorBrush(Colors.Red);
                        MessageBox.Show("Solo debe ingresar letras", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    else
                    {
                        NombreTextBox.BorderBrush = new SolidColorBrush(Colors.Black);
                    }
                }
                catch
                {
                    NombreTextBox.Foreground = SystemColors.ControlTextBrush;
                }
            
        }
    }
}
