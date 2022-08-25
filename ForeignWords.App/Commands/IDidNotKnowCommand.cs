using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ForeignWords.App.Enums;
using ForeignWords.App.ViewModels;

namespace ForeignWords.App.Commands
{
    internal class IDidNotKnowCommand : CommandBase
    {
        private readonly HomeViewModel _homeViewModel;

        public IDidNotKnowCommand(HomeViewModel homeViewModel)
        {
            _homeViewModel = homeViewModel;

            _homeViewModel.PropertyChanged += OnViewModelPropertyChanged;
        }

        public override bool CanExecute(object? parameter)
        {
            return _homeViewModel.Status is Status.ResponseKnowStatus 
                   && base.CanExecute(parameter);
        }

        public override void Execute(object? parameter)
        {
            if (_homeViewModel.Translation.Score > 1)
            {
                _homeViewModel.Translation.Score -= 2;
            }
            else
            {
                _homeViewModel.Translation.Score = 0;
            }

            _homeViewModel.Status = Status.ResponseDidNotKnowStatus;
            _homeViewModel.UpdateWordsCount();
        }

        private void OnViewModelPropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName is nameof(HomeViewModel.Status))
            {
                OnCanExecuteChanged();
            }
        }
    }
}
