using System.ComponentModel;
using ForeignWords.App.Enums;
using ForeignWords.App.ViewModels;

namespace ForeignWords.App.Commands;

internal class DoNotKnowCommand : CommandBase
{
    private readonly HomeViewModel _homeViewModel;

    public DoNotKnowCommand(HomeViewModel homeViewModel)
    {
        _homeViewModel = homeViewModel;

        _homeViewModel.PropertyChanged += OnViewModelPropertyChanged;
    }

    public override bool CanExecute(object? parameter)
    {
        return _homeViewModel.Status is Status.RandomWordStatus or Status.TranslationStatus 
               && base.CanExecute(parameter);
    }

    public override void Execute(object? parameter)
    {
        if (_homeViewModel.Translation.Score > 0)
        {
            _homeViewModel.Translation.Score -= 1;
        }

        _homeViewModel.Status = Status.ResponseDoNotKnowStatus;
        _homeViewModel.UpdateWordsCount();
    }

    private void OnViewModelPropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName is nameof(HomeViewModel.Status))
        {
            OnCanExecuteChanged();
        }
    }
}
