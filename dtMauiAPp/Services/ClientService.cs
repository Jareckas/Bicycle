using dtMauiAPp.Models;
using Microsoft.Maui.ApplicationModel.Communication;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
                await CreateSettingsEntryForUser(model.Email);

                await Shell.Current.DisplayAlert("Alert", "Successfully Registered", "Ok");       
            }
            else
            {
                await Shell.Current.DisplayAlert("Alert", result.ReasonPhrase, "Ok");
            }
        }

        private async Task CreateSettingsEntryForUser(string email)
        {
            try
            {
                var httpClient = httpClientFactory.CreateClient("custom-httpclient");
                var response = await httpClient.PostAsync("/Settings/SettingsEntry?email=" + email, null);
                response.EnsureSuccessStatusCode();
            }
            catch (Exception ex)
            {
                // Handle exceptions
                Console.WriteLine($"Error creating settings entry: {ex.Message}");
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

        public async Task UpdateUsernameAsync(string newUsername)
        {
            try
            {
                var serializedLoginResponseInStorage = await SecureStorage.Default.GetAsync("Authentication");
                string token = JsonSerializer.Deserialize<LoginResponse>(serializedLoginResponseInStorage)!.AccessToken!;

                var httpClient = httpClientFactory.CreateClient("custom-httpclient");
                var updateUsernameModel = new { NewUsername = newUsername };

                httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                var result = await httpClient.PostAsJsonAsync("/Settings/UpdateUsername", updateUsernameModel);

                if (result.IsSuccessStatusCode)
                {
                    await Shell.Current.DisplayAlert("Success", "Username updated successfully.", "OK");
                }
                else
                {
                    // Failed to update username
                    await Shell.Current.DisplayAlert("Error", "Failed to update username.", "OK");
                }
            }
            catch (Exception ex)
            {
                // Log or handle the exception
                await Shell.Current.DisplayAlert("Error", $"An error occurred: {ex.Message}", "OK");
            }
        }

        public async Task<string> GetUsernameFromDatabase()
        {
            try
            {
                var serializedLoginResponseInStorage = await SecureStorage.Default.GetAsync("Authentication");
                string token = JsonSerializer.Deserialize<LoginResponse>(serializedLoginResponseInStorage)!.AccessToken!;
                if (serializedLoginResponseInStorage != null)
                {
                    var loginResponse = JsonSerializer.Deserialize<LoginResponse>(serializedLoginResponseInStorage);
                    if (loginResponse != null)
                    {
                        var httpClient = httpClientFactory.CreateClient("custom-httpclient");
                        httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                        var result = await httpClient.GetAsync("/Settings/GetUsername");
                        if (result.IsSuccessStatusCode)
                        {
                            var username = await result.Content.ReadAsStringAsync();
                            return username;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Log or handle the exception
                Console.WriteLine($"An error occurred while fetching username: {ex.Message}");
            }
            return null;
        }
    }
}
