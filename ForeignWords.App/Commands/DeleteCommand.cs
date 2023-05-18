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
    internal class DeleteCommand : CommandBase
    {
        private readonly WordsListViewModel _wordsListViewModel;
        private readonly TranslationsBook _book;

        public DeleteCommand(WordsListViewModel wordsListViewModel, TranslationsBook book)
        {
            _wordsListViewModel = wordsListViewModel;
            _book = book;

            _wordsListViewModel.PropertyChanged += OnViewModelPropertyChanged;
        }

        public override bool CanExecute(object? parameter)
        {
            return _wordsListViewModel.SelectedTranslation is not null && base.CanExecute(parameter);
        }

        public override void Execute(object? parameter)
        {
            _book.DeleteTranslation(_wordsListViewModel.SelectedTranslation!);

            MessageBox.Show(
                WordsListResources.Delete_Success_Message_Box_Message, 
                WordsListResources.Delete_Success_Message_Box_Title,
                MessageBoxButton.OK, 
                MessageBoxImage.Information
                );

            _wordsListViewModel.SelectedTranslation = null;
            _wordsListViewModel.FilteredText = "";
        }

        private void OnViewModelPropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName is nameof(WordsListViewModel.SelectedTranslation))
            {
                OnCanExecuteChanged();
            }
        }
    }
}
