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
    internal class RefreshCommand : CommandBase
    {
        private readonly HomeViewModel _homeViewModel;
        private readonly TranslationsBook _book;

        public RefreshCommand(HomeViewModel homeViewModel, TranslationsBook book)
        {
            _homeViewModel = homeViewModel;
            _book = book;
        }

        public override void Execute(object? parameter)
        {
            _homeViewModel.AllNewPassedStatus = _homeViewModel.AllNewPassedSelection switch
            {
                0 => AllNewPassedStatus.All,
                1 => AllNewPassedStatus.New,
                2 => AllNewPassedStatus.Passed,
                _ => AllNewPassedStatus.None
            };

            _homeViewModel.DomesticForeignStatus = _homeViewModel.DomesticForeignSelection switch
            {
                0 => DomesticForeignStatus.Domestic,
                1 => DomesticForeignStatus.Foreign,
                _ => DomesticForeignStatus.None
            };

            _homeViewModel.UpdateWordsCount();

            _homeViewModel.Score = 0;
            _homeViewModel.DomesticWord = string.Empty;
            _homeViewModel.ForeignWord = new List<string>();

            _homeViewModel.Status = Status.None;
        }
    }
}
