using Avalonia.Controls;
using Avalonia.Interactivity;
using MyAvaloApp.Models;
using MyAvaloApp.Services;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace MyAvaloApp;

public partial class EmployeView : UserControl
{
    private EmployeService employeService = new EmployeService();
    private Employe selectedEmploye;
    public EmployeView()
    {
        InitializeComponent();
        var employe = new Employe();
        employe.Role = "USER";  // Assurez-vous que la valeur par défaut de Role est explicitement assignée.
        DataContext = employe;
        RefreshEmployeList();
    }
    private void RefreshEmployeList()
    {
        EmployeListBox.ItemsSource = employeService.GetEmployeList();
    }

    private void OnAddClick(object sender, RoutedEventArgs e)
    {
        var employe = new Employe
        {
            FirstName = FirstNameTextBox.Text,
            LastName = LastNameTextBox.Text,
            Role = RoleTextBox.SelectedItem?.ToString() ?? string.Empty
        };
        employe.ValidateFirstName();
        employe.ValidateLastName();

        if (employe.HasErrors)
        {
            return; // Arrêter l'exécution si le formulaire est invalide
        }
        employeService.Add(employe);
        ResetForm();
        RefreshEmployeList();
    }

    private async void OnUpdateCLick(object sender, RoutedEventArgs e)
    {
        // Récupération de l'employé lié au bouton cliqué
        if (sender is Button button && button.DataContext is Employe employe)
        {
            // Création et affichage du dialog en lui passant l'employé à modifier
            var window = this.VisualRoot as Window;
            var dialog = new UpdateEmployeeDialog(employe);
            // 'this' représente la fenêtre principale pour le parent du dialog
            if (window != null)
            {
                var result = await dialog.ShowDialog<bool>(window);

                if (result)
                {
                    // L'objet 'employe' est déjà mis à jour dans le dialog,
                    // il suffit d'appeler le service de mise à jour et de rafraîchir la liste
                    employeService.Update(employe);
                    RefreshEmployeList();
                }
            }
        }
    }

    private void OnDeleteClick(object sender, RoutedEventArgs e)
    {
        if (sender is Button button && button.DataContext is Employe employe)
        {
            employeService.Delete(employe.Id);
            RefreshEmployeList();
        }
    }
    private void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        selectedEmploye = (Employe)EmployeListBox.SelectedItem;
        if (selectedEmploye != null)
        {
            FirstNameTextBox.Text = selectedEmploye.FirstName;
            LastNameTextBox.Text = selectedEmploye.LastName;
            RoleTextBox.SelectedItem = selectedEmploye.Role;
        }
    }

    private void ResetForm()
    {
        FirstNameTextBox.Text = string.Empty;
        LastNameTextBox.Text = string.Empty;
        RoleTextBox.SelectedItem = "USER";
    }

}