using Natech_Weather.Models;
using Natech_Weather.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Natech_Weather
{
    public class MainPageViewModel : BaseViewModel
    {
        private readonly INetworkService _networkService;

        #region properties
        private string _cityInput;
        public string CityInput
        {
            get { return _cityInput; }
            set
            {
                _cityInput = value;
                OnPropertyChanged(() => CityInput);
            }
        }
        #endregion

        #region commands
        public ICommand GetWeatherCommand { get => new Command(async () => await GetWeather()); }
        private async Task GetWeather()
        {
            var result = await _networkService.GetAsync<WeatherData>($"{Constants.OpenWeatherMapEndpoint}?q={CityInput}&units=metric&APPID={Constants.OpenWeatherMapAPIKey}");

        }
        #endregion

        public MainPageViewModel(INetworkService networkService)
        {
            _networkService = networkService;
        }
    }
}
