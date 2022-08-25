using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ForeignWords.App.ViewModels;

namespace ForeignWords.App.Commands
{
    internal class RemoveLastTranslationCommand : CommandBase
    {
        private readonly ModifyWordViewModel _addNewWordViewModel;

        public RemoveLastTranslationCommand(ModifyWordViewModel addNewWordViewModel)
        {
            _addNewWordViewModel = addNewWordViewModel;

            _addNewWordViewModel.PropertyChanged += OnViewModelPropertyChanged;
        }

        public override bool CanExecute(object? parameter)
        {
            return _addNewWordViewModel.ForeignWords.Count > 0 
                   && base.CanExecute(parameter);
        }

        public override void Execute(object? parameter)
        {
            var result = _addNewWordViewModel.ForeignWords;
            result.RemoveAt(result.Count - 1);

            _addNewWordViewModel.ForeignWords = result;
        }

        private void OnViewModelPropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(ModifyWordViewModel.ForeignWords))
            {
                OnCanExecuteChanged();
            }
        }
    }
}
