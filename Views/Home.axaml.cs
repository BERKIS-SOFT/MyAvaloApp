using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using MyAvaloApp.Models;

namespace MyAvaloApp;

public partial class Home : UserControl
{
    public Home()
    {
        InitializeComponent();
        DataContext = new HomeViewModel();
    }
}