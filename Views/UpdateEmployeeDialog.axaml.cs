using Avalonia.Controls;
using Avalonia.Interactivity;
using MyAvaloApp.Models;

namespace MyAvaloApp
{
    public partial class UpdateEmployeeDialog : Window
    {
        // La propriété qui contiendra l'employé mis à jour
        public Employe UpdatedEmploye { get; private set; }

        public UpdateEmployeeDialog(Employe employe)
        {
            InitializeComponent();

            // On initialise le dialog avec les valeurs existantes
            UpdatedEmploye = employe;
            FirstNameTextBox.Text = employe.FirstName;
            LastNameTextBox.Text = employe.LastName;
            RoleTextBox.Text = employe.Role;
        }

        private void Ok_Click(object sender, RoutedEventArgs e)
        {
            // Mise à jour de l'objet avec les nouvelles valeurs
            UpdatedEmploye.FirstName = FirstNameTextBox.Text;
            UpdatedEmploye.LastName = LastNameTextBox.Text;
            UpdatedEmploye.Role = RoleTextBox.Text;

            // Fermer le dialog et retourner true pour confirmer
            Close(true);
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            // Fermer le dialog sans appliquer de modifications
            Close(false);
        }
    }
}
