using System.ComponentModel;
using ForeignWords.App.Enums;
using ForeignWords.App.ViewModels;

namespace ForeignWords.App.Commands;

internal class KnowCommand : CommandBase
{
    private readonly HomeViewModel _homeViewModel;
    private const int MaxScore = 8;

    public KnowCommand(HomeViewModel homeViewModel)
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
        if (_homeViewModel.Translation.Score < MaxScore)
        {
            _homeViewModel.Translation.Score += 1;
        }

        _homeViewModel.Status = Status.ResponseKnowStatus;
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
