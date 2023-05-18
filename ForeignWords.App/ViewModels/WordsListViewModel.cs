using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using ForeignWords.App.Commands;
using ForeignWords.App.Models;
using ForeignWords.App.Services;
using ForeignWords.App.Stores;

namespace ForeignWords.App.ViewModels
{
    public class WordsListViewModel : ViewModelBase
    {
        private readonly TranslationsBook _book;
        private Translation? _selectedTranslation;
        private string _filteredText = string.Empty;
        private IEnumerable<Translation> _translations;

        public IEnumerable<Translation> Translations
        {
            get => _translations.OrderBy(trans => trans.DomesticWord);
            set
            {
                _translations = value;
                OnPropertyChanged(nameof(Translations));
            }
        }

        public string FilteredText
        {
            get => _filteredText;
            set
            {
                _filteredText = value;
                OnPropertyChanged(nameof(FilteredText));

                Translations = string.IsNullOrEmpty(_filteredText) ? _book.GetAllTranslations() : _book.GetFilteredTranslations(_filteredText);
            }
        }

        public ICommand CancelCommand { get; }
        public ICommand DeleteCommand { get; }
        public ICommand EditCommand { get; }
        public ICommand ClearFilterCommand { get; }

        public Translation? SelectedTranslation
        {
            get => _selectedTranslation;
            set
            {
                _selectedTranslation = value;
                OnPropertyChanged(nameof(SelectedTranslation));
            }
        }

        public WordsListViewModel(TranslationsBook book, NavigationService homeNavigationService,
            NavigationStore editWordNavigationStore, NavigationService wordsListNavigationService)
        {
            _book = book;
            _translations = _book.GetAllTranslations();

            EditCommand = new EditWordCommand(this, book, 
                editWordNavigationStore, wordsListNavigationService);
            DeleteCommand = new DeleteCommand(this, book);

            CancelCommand = new NavigateCommand(homeNavigationService);
            ClearFilterCommand = new ClearFilterCommand(this);
        }
    }
}
