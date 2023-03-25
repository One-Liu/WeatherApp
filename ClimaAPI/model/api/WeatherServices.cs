using ClimaAPI.model.objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace ClimaAPI.model.api
{
    public class WeatherServices
    {
        private static readonly string URL_BASE = "https://api.openweathermap.org/data/2.5/";
        private static readonly string APP_ID = ""; // your api key

        private static readonly string IMG_URL_BASE_START = "https://openweathermap.org/img/wn/";
        private static readonly string IMG_URL_BASE_END = "@2x.png";

        public static async Task<ResponseServices> GetCurrentWeather(string cityName)
        {
            ResponseServices responseServices = new ResponseServices();
            using(var httpClient = new HttpClient())
            {
                HttpRequestMessage requestMessage;
                HttpResponseMessage responseMessage;

                try
                {
                    string url = string.Format("{0}weather?q={1}&appid={2}&lang=en&units=metric", URL_BASE, cityName, APP_ID);
                    requestMessage = new HttpRequestMessage(HttpMethod.Get, url);
                    responseMessage = await httpClient.SendAsync(requestMessage);

                    if(responseMessage != null)
                    {
                        if(responseMessage.IsSuccessStatusCode)
                        {
                            CurrentWeather currentWeather = await responseMessage.Content.ReadFromJsonAsync<CurrentWeather>();
                            if(currentWeather != null)
                            {
                                responseServices.Error = false;
                                responseServices.Message = "OK";
                                responseServices.WeatherReport = currentWeather;
                            }
                        }
                        else
                        {
                            responseServices.Error = true;
                            responseServices.Message = String.Format("Error: {0} - {1}", (int)responseMessage.StatusCode, responseMessage.StatusCode);
                        }
                    } 
                    else
                    {
                        responseServices.Error = true;
                        responseServices.Message = "No se puede obtener respuesta del servicio web";
                    }
                } 
                catch (Exception e)
                {
                    responseServices.Error = true;
                    responseServices.Message = e.Message;
                }

                return responseServices;
            }
        }

        public static async Task<ResponseServices> GetForecast(string cityName)
        {
            ResponseServices responseServices = new ResponseServices();
            using (var httpClient = new HttpClient())
            {
                HttpRequestMessage requestMessage;
                HttpResponseMessage responseMessage;

                try
                {
                    string url = string.Format("{0}forecast?q={1}&appid={2}&lang=en&units=metric", URL_BASE, cityName, APP_ID);
                    requestMessage = new HttpRequestMessage(HttpMethod.Get, url);
                    responseMessage = await httpClient.SendAsync(requestMessage);

                    if (responseMessage != null)
                    {
                        if (responseMessage.IsSuccessStatusCode)
                        {
                            WeatherForecast weatherForecast = await responseMessage.Content.ReadFromJsonAsync<WeatherForecast>();
                            if (weatherForecast != null)
                            {
                                responseServices.Error = false;
                                responseServices.Message = "OK";
                                responseServices.WeatherForecast = weatherForecast;
                            }
                        }
                        else
                        {
                            responseServices.Error = true;
                            responseServices.Message = String.Format("Error: {0} - {1}", (int)responseMessage.StatusCode, responseMessage.StatusCode);
                        }
                    }
                    else
                    {
                        responseServices.Error = true;
                        responseServices.Message = "No se puede obtener respuesta del servicio web";
                    }
                }
                catch (Exception e)
                {
                    responseServices.Error = true;
                    responseServices.Message = e.Message;
                }

                return responseServices;
            }
        }

        public static BitmapImage GenerateBitmapImage(string icon)
        {
            BitmapImage bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.UriSource = new Uri(IMG_URL_BASE_START + icon + IMG_URL_BASE_END);
            bitmap.EndInit();
            return bitmap;
        }
    }
}
