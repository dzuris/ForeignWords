using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using ForeignWords.App.Models;
using ForeignWords.App.Resources.Texts;
using ForeignWords.App.ViewModels;

namespace ForeignWords.App.Commands
{
    internal class SaveNewWordCommand : CommandBase
    {
        private readonly TranslationsBook _book;
        private readonly ModifyWordViewModel _addNewWordViewModel;

        public SaveNewWordCommand(ModifyWordViewModel addNewWordViewModel, TranslationsBook book)
        {
            _book = book;
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
            var translation = new Translation(
                _addNewWordViewModel.DomesticWord,
                _addNewWordViewModel.ForeignWords
            );

            _book.AddTranslation(translation);

            MessageBox.Show(
                ModifyWordResources.Save_Success_Message_Box_Message, 
                ModifyWordResources.Save_Success_Message_Box_Title, 
                MessageBoxButton.OK, 
                MessageBoxImage.Information
                );

            _addNewWordViewModel.DomesticWord = string.Empty;
            _addNewWordViewModel.ForeignWords = new List<string>();
        }

        private void OnViewModelPropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName is nameof(ModifyWordViewModel.ForeignWords)
                or nameof(ModifyWordViewModel.DomesticWord))
            {
                OnCanExecuteChanged();
            }
        }
    }
}
