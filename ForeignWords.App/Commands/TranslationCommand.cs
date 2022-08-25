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
    internal class TranslationCommand : CommandBase
    {
        private readonly HomeViewModel _homeViewModel;

        public TranslationCommand(HomeViewModel homeViewModel)
        {
            _homeViewModel = homeViewModel;

            _homeViewModel.PropertyChanged += OnViewModelPropertyChanged;
        }

        public override bool CanExecute(object? parameter)
        {
            return _homeViewModel.Status is Status.RandomWordStatus 
                   && base.CanExecute(parameter);
        }

        public override void Execute(object? parameter)
        {
            _homeViewModel.Status = Status.TranslationStatus;
        }

        private void OnViewModelPropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(HomeViewModel.Status))
            {
                OnCanExecuteChanged();
            }
        }
    }
}
