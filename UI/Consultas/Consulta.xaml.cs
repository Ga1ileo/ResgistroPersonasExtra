using RegistroPersonasExtra.BLL;
using RegistroPersonasExtra.Entidades;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace RegistroPersonasExtra.UI.Consultas
{
    /// <summary>
    /// Interaction logic for Consulta.xaml
    /// </summary>
    public partial class Consulta : Window
    {
        public Consulta()
        {
            InitializeComponent();
        }

        private void ConsultarButton_Click(object sender, RoutedEventArgs e)
        {
            var listado = new List<Personas>();

            if (CriteriotextBox.Text.Trim().Length > 0)
            {
                switch (FiltroComboBox.SelectedIndex)
                {
                    case 0://todo
                        listado = PersonasBll.GetList(p => true);
                        break;
                    case 1://ID
                        int id = Convert.ToInt32(CriteriotextBox.Text);
                        listado = PersonasBll.GetList(p => p.PersonaId == id);
                        break;
                    case 2://Nombre
                        listado = PersonasBll.GetList(p => p.Nombre.Contains(CriteriotextBox.Text));
                        break;



                }

            }
            else
            {
                listado = PersonasBll.GetList(p => true);
            }

            ConsultaDataGrid.ItemsSource = null;
            ConsultaDataGrid.ItemsSource = listado;

        }
    }
}
