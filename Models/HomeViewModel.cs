using Avalonia.Controls;
using ReactiveUI;
using System.Reactive;

namespace MyAvaloApp.Models
{
    public class HomeViewModel : ReactiveObject
    {
        private UserControl _currentView;
        public UserControl CurrentView
        {
            get => _currentView;
            set => this.RaiseAndSetIfChanged(ref _currentView, value);
        }

        private int _selectedTab;
        public int SelectedTab
        {
            get => _selectedTab;
            set
            {
                this.RaiseAndSetIfChanged(ref _selectedTab, value);
                UpdateView();
            }
        }

        public HomeViewModel()
        {
            UpdateView(); // Afficher la vue par défaut
        }

        private void UpdateView()
        {
            switch (SelectedTab)
            {
                case 0:
                    CurrentView = new EmployeView();
                    break;
                case 1:
                    // Ajouter la vue pour les auteurs si nécessaire
                    CurrentView = new UserControl();
                    break;
                case 2:
                    // Ajouter la vue pour les films si nécessaire
                    CurrentView = new UserControl();
                    break;
                default:
                    CurrentView = new UserControl();
                    break;
            }
        }
    }
}
