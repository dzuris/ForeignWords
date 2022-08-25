using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ForeignWords.App.ViewModels;

namespace ForeignWords.App.Commands
{
    internal class ClearFilterCommand : CommandBase
    {
        private readonly WordsListViewModel _wordsListViewModel;

        public ClearFilterCommand(WordsListViewModel wordsListViewModel)
        {
            _wordsListViewModel = wordsListViewModel;

            _wordsListViewModel.PropertyChanged += OnViewModelPropertyChanged;
        }

        public override bool CanExecute(object? parameter)
        {
            return !string.IsNullOrEmpty(_wordsListViewModel.FilteredText) 
                   && base.CanExecute(parameter);
        }

        public override void Execute(object? parameter)
        {
            _wordsListViewModel.FilteredText = string.Empty;
        }

        private void OnViewModelPropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName is nameof(WordsListViewModel.FilteredText))
            {
                OnCanExecuteChanged();
            }
        }
    }
}
