using dtMauiAPp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace dtMauiAPp.Services
{
    public class ClientService
    {
        private readonly IHttpClientFactory httpClientFactory;
        public ClientService(IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory;
        }

        public async Task Register(RegisterModel model)
        {
            // Validate password format
            if (!Regex.IsMatch(model.Password, @"^(?=.*[A-Z])(?=.*\d)(?=.*[^a-zA-Z\d\s]).{8,}$"))
            {
                await Shell.Current.DisplayAlert("Error", "Password must contain at least one capital letter, one number, and one symbol.", "Ok");
                return;
            }

            var httpClient = httpClientFactory.CreateClient("custom-httpclient");
            var result = await httpClient.PostAsJsonAsync("/register", model);
            if (result.IsSuccessStatusCode)
            {
                await Shell.Current.DisplayAlert("Alert", "Successfully Registered", "Ok");
            }
            else
            {
                await Shell.Current.DisplayAlert("Alert", result.ReasonPhrase, "Ok");
            }
        }

        public async Task<bool> Login(LoginModel model)
        {
            var httpClient = httpClientFactory.CreateClient("custom-httpclient");
            var result = await httpClient.PostAsJsonAsync("/login", model);

            if (result.IsSuccessStatusCode)
            {
                var response = await result.Content.ReadFromJsonAsync<LoginResponse>();
                if (response is not null)
                {
                    var serializeResponse = JsonSerializer.Serialize(
                        new LoginResponse() { AccessToken = response.AccessToken, RefreshToken = response.RefreshToken, UserName = model.Email });
                    await SecureStorage.Default.SetAsync("Authentication", serializeResponse);
                    return true;
                }
            }
            else
            {
                await Shell.Current.DisplayAlert("Alert", "Invalid Credentials", "Ok");
                return false;
            }

            return false;
        }

        public async Task<WeatherForecast[]> GetWeatherForeCastData()
        {
            var serializedLoginResponseInStorage = await SecureStorage.Default.GetAsync("Authentication");
            if (serializedLoginResponseInStorage is null) return null!;

            string token = JsonSerializer.Deserialize<LoginResponse>(serializedLoginResponseInStorage)!.AccessToken!;
            var httpClient = httpClientFactory.CreateClient("custom-httpclient");
            httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            var result = await httpClient.GetFromJsonAsync<WeatherForecast[]>("/WeatherForecast");
            return result!;
        }
    }
}
