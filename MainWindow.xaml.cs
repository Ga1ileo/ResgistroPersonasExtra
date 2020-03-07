using RegistroPersonasExtra.BLL;
using RegistroPersonasExtra.Entidades;
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

namespace RegistroPersonasExtra
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

        private void Limpiar()
        {
            PersonaIdtextBox.Text = string.Empty;
            NombreTextBox.Text = string.Empty;
        }

        private Personas LlenaClase()
        {
            Personas persona = new Personas();
            if (string.IsNullOrWhiteSpace(PersonaIdtextBox.Text))
            {
                PersonaIdtextBox.Text = "0";
            }
            else persona.PersonaId = Convert.ToInt32(PersonaIdtextBox.Text);
            persona.Nombre = NombreTextBox.Text;

            return persona;
        }

        private void LlenaCampos(Personas persona)
        {
            PersonaIdtextBox.Text = Convert.ToString(persona.PersonaId);
            NombreTextBox.Text = persona.Nombre;
        }

        private bool Validar()
        {
            bool paso = true;

            if(NombreTextBox.Text == string.Empty)
            {
                MessageBox.Show(NombreTextBox.Text, "ESTE CAMPO NO PUEDE ESTAR VACIO");
                NombreTextBox.Focus();
                paso = false;
            }

            return paso;
        }

        private void NuevoButton_Click(object sender, RoutedEventArgs e)
        {
            Limpiar();
        }

        private void BuscarButton_Click(object sender, RoutedEventArgs e)
        {
            int id;
            Personas persona = new Personas();
            int.TryParse(PersonaIdtextBox.Text, out id);

            Limpiar();

            persona = PersonasBll.Buscar(id);

            if(persona != null)
            {
                LlenaCampos(persona);
            }
            else
            {
                MessageBox.Show("NO SE ENCUENTRA LA PERSONA");
            }
        }

        private bool Existe()
        {
            Personas persona = PersonasBll.Buscar(Convert.ToInt32(PersonaIdtextBox.Text));

            return (persona != null);
        }

        private void GuardarButton_Click(object sender, RoutedEventArgs e)
        {
            Personas persona;
            bool paso = false;

            if (!Validar())
                return;

            persona = LlenaClase();

            if (string.IsNullOrWhiteSpace(PersonaIdtextBox.Text) || PersonaIdtextBox.Text == "0")
                paso = PersonasBll.Guardar(persona);
            else
            {
                if (!Existe())
                {
                    MessageBox.Show("No Se puede Modificar porque no existe", "Fallo", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                paso = PersonasBll.Modificar(persona);
            }

            if (paso)
            {
                Limpiar();
                MessageBox.Show("Guardado", " :) ", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("No Guardo", " :( ", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void EliminarButton_Click(object sender, RoutedEventArgs e)
        {
            int id;
            id = Convert.ToInt32(PersonaIdtextBox.Text);

            Limpiar();

            if (PersonasBll.Eliminar(id))
                MessageBox.Show("Eliminado", " :) ", MessageBoxButton.OK, MessageBoxImage.Information);
            else
                MessageBox.Show(PersonaIdtextBox.Text, "Esa Persona todavia no ha nacido");
        }
    }
}
