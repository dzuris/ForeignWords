using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ForeignWords.App.Models;
using ForeignWords.App.Services;
using ForeignWords.App.Stores;
using ForeignWords.App.ViewModels;

namespace ForeignWords.App.Commands
{
    internal class NavigateCommand : CommandBase
    {
        private readonly NavigationService _navigationService;

        public NavigateCommand(NavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        public override void Execute(object? parameter)
        {
            _navigationService.Navigate();
        }
    }
}
