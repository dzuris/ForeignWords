using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            switch (_homeViewModel.AllNewPassedStatus)
            {
                case AllNewPassedStatus.All when _book.GetTranslationsCount() == 0:
                case AllNewPassedStatus.New when _book.GetNewTranslationsCount() == 0:
                case AllNewPassedStatus.Passed when _book.GetPassedTranslationsCount() == 0:
                    return false;
                case AllNewPassedStatus.None:
                default:
                    return _homeViewModel.Status is Status.None 
                               or Status.ResponseKnowStatus 
                               or Status.ResponseDoNotKnowStatus 
                               or Status.ResponseDidNotKnowStatus
                           && base.CanExecute(parameter);
            }
        }

        public override void Execute(object? parameter)
        {
            _homeViewModel.Translation = _homeViewModel.AllNewPassedStatus switch
            {
                AllNewPassedStatus.All => _book.GetRandomTranslation(),
                AllNewPassedStatus.New => _book.GetRandomNewTranslation(),
                AllNewPassedStatus.Passed => _book.GetRandomPassedTranslation(),
                _ => throw new ArgumentOutOfRangeException()
            };

            switch (_homeViewModel.DomesticForeignStatus)
            {
                case DomesticForeignStatus.Domestic:
                    _homeViewModel.DomesticWord = _homeViewModel.Translation.DomesticWord;
                    _homeViewModel.ForeignWord = _homeViewModel.Translation.ForeignWords;
                    break;
                case DomesticForeignStatus.Foreign:
                    _homeViewModel.DomesticWord = GetRandomForeignWord(_homeViewModel.Translation);
                    _homeViewModel.ForeignWord = _book.GetDomesticTranslations(_homeViewModel.DomesticWord);
                    break;
                case DomesticForeignStatus.None:
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
