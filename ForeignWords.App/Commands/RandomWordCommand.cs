using System;
using System.ComponentModel;
using ForeignWords.App.Enums;
using ForeignWords.App.Models;
using ForeignWords.App.ViewModels;

namespace ForeignWords.App.Commands
{
    internal class RandomWordCommand : CommandBase
    {
        private readonly HomeViewModel _homeViewModel;
        private readonly TranslationsBook _book;

        public RandomWordCommand(HomeViewModel homeViewModel, TranslationsBook book)
        {
            _homeViewModel = homeViewModel;
            _book = book;

            _homeViewModel.PropertyChanged += OnViewModelPropertyChanged;
        }

        public override bool CanExecute(object? parameter)
        {
            return _homeViewModel.Status is Status.None
                            or Status.ResponseKnowStatus
                            or Status.ResponseDoNotKnowStatus
                            or Status.ResponseDidNotKnowStatus
                            && _homeViewModel.WordsCount > 0
                            && base.CanExecute(parameter);
        }

        public override void Execute(object? parameter)
        {
            if (!CanExecute(parameter)) return;

            _homeViewModel.Translation = _homeViewModel.AllNewPassedSelection switch
            {
                0 => _book.GetRandomTranslation(),
                1 => _book.GetRandomNewTranslation(),
                2 => _book.GetRandomPassedTranslation(),
                _ => throw new ArgumentOutOfRangeException()
            };

            switch (_homeViewModel.DomesticForeignSelection)
            {
                case 0:
                    _homeViewModel.DomesticWord = _homeViewModel.Translation.DomesticWord;
                    _homeViewModel.ForeignWord = _homeViewModel.Translation.ForeignWords;
                    break;
                case 1:
                    _homeViewModel.DomesticWord = GetRandomForeignWord(_homeViewModel.Translation);
                    _homeViewModel.ForeignWord = _book.GetDomesticTranslations(_homeViewModel.DomesticWord);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            _homeViewModel.Status = Status.RandomWordStatus;
            _homeViewModel.Score += 1;
        }

        private void OnViewModelPropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName is nameof(HomeViewModel.Status) or nameof(HomeViewModel.Score))
            {
                OnCanExecuteChanged();
            }
        }

        private string GetRandomForeignWord(Translation translation)
        {
            var rn = new Random();

            return translation.ForeignWords[rn.Next(translation.ForeignWords.Count)];
        }
    }
}
