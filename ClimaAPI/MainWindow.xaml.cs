using ClimaAPI.model.api;
using ClimaAPI.model.objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ClimaAPI
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void CityValidation(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^a-zA-Z]");
            e.Handled= regex.IsMatch(e.Text);
        }

        private void ClearWeatherGrids()
        {
            this.lbl_city.Content = string.Empty;
            this.lbl_city.Background = Brushes.Transparent;

            this.grid_1.Background = Brushes.Transparent;
            this.grid_2.Background = Brushes.Transparent;
            this.grid_3.Background = Brushes.Transparent;
            this.grid_4.Background = Brushes.Transparent;
            this.grid_5.Background = Brushes.Transparent;

            this.lbl_day_1.Content = string.Empty;
            this.lbl_day_2.Content = string.Empty;
            this.lbl_day_3.Content = string.Empty;
            this.lbl_day_4.Content = string.Empty;
            this.lbl_day_5.Content = string.Empty;

            this.img_weather_1.Source = null;
            this.img_weather_2.Source = null;
            this.img_weather_3.Source = null;
            this.img_weather_4.Source = null;
            this.img_weather_5.Source = null;

            this.lbl_weather_1.Content = string.Empty;
            this.lbl_weather_2.Content = string.Empty;
            this.lbl_weather_3.Content = string.Empty;
            this.lbl_weather_4.Content = string.Empty;
            this.lbl_weather_5.Content = string.Empty;

            this.lbl_temp_max_1.Content = string.Empty;
            this.lbl_temp_max_2.Content = string.Empty;
            this.lbl_temp_max_3.Content = string.Empty;
            this.lbl_temp_max_4.Content = string.Empty;
            this.lbl_temp_max_5.Content = string.Empty;

            this.lbl_temp_min_1.Content = string.Empty;
            this.lbl_temp_min_2.Content = string.Empty;
            this.lbl_temp_min_3.Content = string.Empty;
            this.lbl_temp_min_4.Content = string.Empty;
            this.lbl_temp_min_5.Content = string.Empty;
        }

        private async void ClickCheckCurrentWeather(object sender, RoutedEventArgs e)
        {
            string city = this.txt_city.Text;
            if (!string.IsNullOrEmpty(city))
            {
                ResponseServices responseServices = await WeatherServices.GetCurrentWeather(city);
                if(!responseServices.Error)
                {
                    if(responseServices.WeatherReport != null)
                    {
                        CurrentWeather weather = responseServices.WeatherReport;
                        this.ClearWeatherGrids();
                        this.lbl_city.Background = Brushes.LightBlue;
                        this.grid_3.Background = Brushes.LightBlue;
                        this.lbl_city.Content = weather.name;
                        this.lbl_day_3.Content = "Today";
                        this.img_weather_3.Source = WeatherServices.GenerateBitmapImage(weather.weather[0].icon);
                        this.lbl_weather_3.Content = weather.weather[0].description;
                        this.lbl_temp_max_3.Content = "Temp max: " + weather.main.temp_max;
                        this.lbl_temp_min_3.Content = "Temp min: " + weather.main.temp_min;
                    }
                    else
                    {
                        MessageBox.Show(responseServices.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                }
                else
                {
                    MessageBox.Show("The city was not found", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            else
            {
                MessageBox.Show("Type a valid city", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private async void ClickCheckWeatherForecast(object sender, RoutedEventArgs e)
        {
            string city = this.txt_city.Text;
            if (!string.IsNullOrEmpty(city))
            {
                ResponseServices responseServices = await WeatherServices.GetForecast(city);
                if (!responseServices.Error)
                {
                    if (responseServices.WeatherForecast != null)
                    {
                        WeatherForecast weather = responseServices.WeatherForecast;
                        this.ClearWeatherGrids();
                        this.lbl_city.Content = weather.city.name;
                        this.lbl_city.Background = Brushes.LightBlue;

                        // Weather is taken at 15:00:00
                        this.grid_1.Background = Brushes.LightBlue;
                        this.lbl_day_1.Content = weather.list[5].dt_txt.Substring(0,10);
                        this.img_weather_1.Source = WeatherServices.GenerateBitmapImage(weather.list[5].weather[0].icon);
                        this.lbl_weather_1.Content = weather.list[5].weather[0].description;
                        this.lbl_temp_max_1.Content = "Temp max: " + weather.list[5].main.temp_max;
                        this.lbl_temp_min_1.Content = "Temp min: " + weather.list[5].main.temp_min;

                        this.grid_2.Background = Brushes.LightBlue;
                        this.lbl_day_2.Content = weather.list[13].dt_txt.Substring(0, 10);
                        this.img_weather_2.Source = WeatherServices.GenerateBitmapImage(weather.list[13].weather[0].icon);
                        this.lbl_weather_2.Content = weather.list[13].weather[0].description;
                        this.lbl_temp_max_2.Content = "Temp max: " + weather.list[13].main.temp_max;
                        this.lbl_temp_min_2.Content = "Temp min: " + weather.list[13].main.temp_min;

                        this.grid_3.Background = Brushes.LightBlue;
                        this.lbl_day_3.Content = weather.list[21].dt_txt.Substring(0, 10);
                        this.img_weather_3.Source = WeatherServices.GenerateBitmapImage(weather.list[21].weather[0].icon);
                        this.lbl_weather_3.Content = weather.list[21].weather[0].description;
                        this.lbl_temp_max_3.Content = "Temp max: " + weather.list[21].main.temp_max;
                        this.lbl_temp_min_3.Content = "Temp min: " + weather.list[21].main.temp_min;

                        this.grid_4.Background = Brushes.LightBlue;
                        this.lbl_day_4.Content = weather.list[29].dt_txt.Substring(0, 10);
                        this.img_weather_4.Source = WeatherServices.GenerateBitmapImage(weather.list[29].weather[0].icon);
                        this.lbl_weather_4.Content = weather.list[29].weather[0].description;
                        this.lbl_temp_max_4.Content = "Temp max: " + weather.list[29].main.temp_max;
                        this.lbl_temp_min_4.Content = "Temp min: " + weather.list[29].main.temp_min;

                        this.grid_5.Background = Brushes.LightBlue;
                        this.lbl_day_5.Content = weather.list[37].dt_txt.Substring(0, 10);
                        this.img_weather_5.Source = WeatherServices.GenerateBitmapImage(weather.list[37].weather[0].icon);
                        this.lbl_weather_5.Content = weather.list[37].weather[0].description;
                        this.lbl_temp_max_5.Content = "Temp max: " + weather.list[37].main.temp_max;
                        this.lbl_temp_min_5.Content = "Temp min: " + weather.list[37].main.temp_min;
                    }
                    else
                    {
                        MessageBox.Show(responseServices.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                }
                else
                {
                    MessageBox.Show("The city was not found", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            else
            {
                MessageBox.Show("Type a valid city", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
}
