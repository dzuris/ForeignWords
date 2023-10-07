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

        if (vm is null) return;

        switch (e.Key)
        {
            case Key.R:
                if (vm.RandomWordCommand.CanExecute(null))
                    vm.RandomWordCommand.Execute(null);
                break;
            case Key.T:
                if (vm.TranslationCommand.CanExecute(null))
                    vm.TranslationCommand.Execute(null);
                break;
            case Key.D:
                if (vm.DidNotKnowCommand.CanExecute(null))
                    vm.DidNotKnowCommand.Execute(null);
                break;
            case Key.N:
                if (vm.DoNotKnowCommand.CanExecute(null))
                    vm.DoNotKnowCommand.Execute(null);
                break;
            case Key.K:
                if (vm.KnowCommand.CanExecute(null))
                    vm.KnowCommand.Execute(null);
                break;
        }
    }
}
