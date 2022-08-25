using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using ForeignWords.App.ViewModels;

namespace ForeignWords.App.Commands
{
    internal class UpdateWordCommand : CommandBase
    {
        private ModifyWordViewModel _addNewWordViewModel;

        public UpdateWordCommand(ModifyWordViewModel addNewWordViewModel)
        {
            _addNewWordViewModel = addNewWordViewModel;

            _addNewWordViewModel.PropertyChanged += OnViewModelPropertyChanged;
        }

        public override bool CanExecute(object? parameter)
        {
            return !string.IsNullOrEmpty(_addNewWordViewModel.DomesticWord) 
                   && _addNewWordViewModel.ForeignWords.Count > 0
                   && base.CanExecute(parameter);
        }

        public override void Execute(object? parameter)
        {
            _addNewWordViewModel.Translation.DomesticWord = _addNewWordViewModel.DomesticWord;
            _addNewWordViewModel.Translation.ForeignWords = _addNewWordViewModel.ForeignWords;

            MessageBox.Show("Word successfully updated", "Success",
                MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void OnViewModelPropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName is nameof(ModifyWordViewModel.DomesticWord)
                or nameof(ModifyWordViewModel.ForeignWords))
            {
                OnCanExecuteChanged();
            }
        }
    }
}
