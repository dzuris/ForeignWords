using System;
using System.Windows;
using System.Windows.Input;
using ForeignWords.App.ViewModels;

namespace ForeignWords.App.Views;

/// <summary>
/// Interaction logic for HomeView.xaml
/// </summary>
public partial class HomeView
{
    public HomeView()
    {
        InitializeComponent();
    }

    private void HomeView_OnLoaded(object sender, RoutedEventArgs e)
    {
        var window = Window.GetWindow(this);

        if (window is null) return;
        
        window.KeyDown += HandleKeyPress;
    }

    private void HandleKeyPress(object sender, KeyEventArgs e)
    {
        var vm = (HomeViewModel)DataContext;
        switch (e.Key)
        {
            case Key.R:
                vm.RandomWordCommand.Execute(null);
                break;
            case Key.T:
                vm.TranslationCommand.Execute(null);
                break;
            case Key.D:
                vm.DidNotKnowCommand.Execute(null);
                break;
            case Key.K:
                vm.KnowCommand.Execute(null);
                break;
            case Key.N:
                vm.DoNotKnowCommand.Execute(null);
                break;
        }
    }
}
