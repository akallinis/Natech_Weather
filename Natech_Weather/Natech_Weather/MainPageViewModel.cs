using Natech_Weather.Models;
using Natech_Weather.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Natech_Weather
{
    public class MainPageViewModel : BaseViewModel
    {
        private readonly INetworkService _networkService;
        private readonly IMediaPlayerService _playerService;

        #region properties
        private bool _showingData;
        public bool ShowingData
        {
            get { return _showingData; }
            set
            {
                _showingData = value;
                OnPropertyChanged(() => ShowingData);
            }
        }

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

        private string _temperature;
        public string Temperature
        {
            get { return _temperature; }
            set
            {
                _temperature = value;
                OnPropertyChanged(() => Temperature);
            }
        }

        private string _location;
        public string Location
        {
            get { return _location; }
            set
            {
                _location = value;
                OnPropertyChanged(() => Location);
            }
        }

        private string _windSpeed;
        public string WindSpeed
        {
            get { return _windSpeed; }
            set
            {
                _windSpeed = value;
                OnPropertyChanged(() => WindSpeed);
            }
        }

        private string _humidity;
        public string Humidity
        {
            get { return _humidity; }
            set
            {
                _humidity = value;
                OnPropertyChanged(() => Humidity);
            }
        }

        private string _visibility;
        public string Visibility
        {
            get { return _visibility; }
            set
            {
                _visibility = value;
                OnPropertyChanged(() => Visibility);
            }
        }
        #endregion

        #region commands
        public ICommand GetWeatherCommand { get => new Command(async () => await GetWeather()); }
        private async Task GetWeather()
        {
            WeatherData result = null;
            ShowingData = false;
            try
            {
                if (string.IsNullOrEmpty(CityInput))
                {
                    var request = new GeolocationRequest(GeolocationAccuracy.Best, TimeSpan.FromSeconds(10));
                    var location = await Geolocation.GetLocationAsync(request);
                    result = await _networkService.GetAsync<WeatherData>($"{Constants.OpenWeatherMapEndpoint}?lat={location.Latitude}&lon={location.Longitude}&units=metric&APPID={Constants.OpenWeatherMapAPIKey}");
                }
                else
                {
                    result = await _networkService.GetAsync<WeatherData>($"{Constants.OpenWeatherMapEndpoint}?q={CityInput}&units=metric&APPID={Constants.OpenWeatherMapAPIKey}");
                }
            }
            catch (FeatureNotSupportedException fnsEx)
            {
                _playerService.PlayError();
                return;
            }
            catch (FeatureNotEnabledException fnsEx)
            {
                _playerService.PlayError();
                return;
            }
            catch (PermissionException perEx)
            {
                _playerService.PlayError();
                return;
            }
            catch (Exception ex)
            {
                _playerService.PlayError();
                return;
            }



            if (result != null && result.Cod == 200)
            {
                PopulateData(result);
                _playerService.PlaySuccess();
            }
            else
            {
                _playerService.PlayError();
            }
        }
        #endregion

        public MainPageViewModel(INetworkService networkService, IMediaPlayerService mediaPlayerService)
        {
            _networkService = networkService;
            _playerService = mediaPlayerService;
        }

        #region helpers
        private void PopulateData(WeatherData data)
        {
            Temperature = $"{data.Main.Temperature}";
            Location = data.Title;
            WindSpeed = $"{data.Wind.Speed}";
            Humidity = $"{data.Main.Humidity}";
            Visibility = data.Weather.FirstOrDefault().Visibility;
            ShowingData = true;
        }
        #endregion
    }
}
