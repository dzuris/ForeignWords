using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ForeignWords.App.Models;
using ForeignWords.App.ViewModels;

namespace ForeignWords.App.Commands;

internal class AddTranslationCommand : CommandBase
{
    private readonly ModifyWordViewModel _addNewWordViewModel;

    public AddTranslationCommand(ModifyWordViewModel addNewWordViewModel)
    {
        _addNewWordViewModel = addNewWordViewModel;

        _addNewWordViewModel.PropertyChanged += OnViewModelPropertyChanged;
    }

    public override bool CanExecute(object? parameter)
    {
        return !string.IsNullOrEmpty(_addNewWordViewModel.TranslatedWord) 
               && !_addNewWordViewModel.ForeignWords.Contains(_addNewWordViewModel.TranslatedWord, StringComparer.OrdinalIgnoreCase)
               && base.CanExecute(parameter);
    }

    public override void Execute(object? parameter)
    {
        var result = _addNewWordViewModel.ForeignWords;
        result.Add(_addNewWordViewModel.TranslatedWord);

        _addNewWordViewModel.ForeignWords = result;
        _addNewWordViewModel.TranslatedWord = string.Empty;
    }

    private void OnViewModelPropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName is nameof(ModifyWordViewModel.TranslatedWord))
        {
            OnCanExecuteChanged();
        }
    }
}