using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ForeignWords.App.Models;
using ForeignWords.App.Services;
using ForeignWords.App.Stores;
using ForeignWords.App.ViewModels;

namespace ForeignWords.App.Commands
{
    internal class EditWordCommand : CommandBase
    {
        private readonly WordsListViewModel _wordsListViewModel;
        private readonly TranslationsBook _book;
        private readonly NavigationStore _navigationStore;
        private readonly NavigationService _showWordsNavigationService;

        public EditWordCommand(WordsListViewModel wordsListViewModel, TranslationsBook book, 
            NavigationStore navigationStore, NavigationService showWordsNavigationService)
        {
            _wordsListViewModel = wordsListViewModel;
            _book = book;
            _navigationStore = navigationStore;
            _showWordsNavigationService = showWordsNavigationService;

            _wordsListViewModel.PropertyChanged += OnViewModelPropertyChanged;
        }

        public override bool CanExecute(object? parameter)
        {
            return _wordsListViewModel.SelectedTranslation is not null 
                   && base.CanExecute(parameter);
        }

        public override void Execute(object? parameter)
        {
            _navigationStore.CurrentViewModel = new ModifyWordViewModel(_book, _showWordsNavigationService, _wordsListViewModel.SelectedTranslation);
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
