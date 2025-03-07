using Avalonia.Controls;
using Avalonia.Interactivity;
using MyAvaloApp.Models;
using MyAvaloApp.Services;
using System.Collections.ObjectModel;

namespace MyAvaloApp;

public partial class EmployeView : UserControl
{
    private EmployeService employeService = new EmployeService();
    private Employe selectedEmploye;
    public EmployeView()
    {
        InitializeComponent();
        this.DataContext = new Employe();
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
            Role = RoleTextBox.Text
        };

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
            RoleTextBox.Text = selectedEmploye.Role;
        }
    }

    private void ResetForm()
    {
        FirstNameTextBox.Text = string.Empty;
        LastNameTextBox.Text = string.Empty;
        RoleTextBox.Text = string.Empty;
    }

}