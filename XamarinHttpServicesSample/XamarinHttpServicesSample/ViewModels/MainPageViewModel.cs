using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using XamarinHttpServicesSample.Models;
using XamarinHttpServicesSample.Services;

namespace XamarinHttpServicesSample.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        private readonly IPersonService _personService;

        private IEnumerable<Person> _persons;

        public IEnumerable<Person> Persons
        {
            get => _persons;
            set => SetProperty(ref _persons, value);
        }

        public ICommand ReloadPersonsCommand
        {
            get { return new Command(async () => { await LoadPersons(); }); }
        }

        public MainPageViewModel(INavigationService navigationService, IPersonService personService) 
            : base (navigationService)
        {
            _personService = personService;
        }

        public override async void OnNavigatingTo(NavigationParameters parameters)
        {
            base.OnNavigatingTo(parameters);
            await LoadPersons();
        }

        private async Task LoadPersons()
        {
            if (IsBusy) return;

            IsBusy = true;
            Persons = await _personService.GetPersonsAsync();
            IsBusy = false;
        }
    }
}
