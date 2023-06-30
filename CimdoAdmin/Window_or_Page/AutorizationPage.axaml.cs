using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using CimdoAdmin.Window_or_Page;
using CimdoClassLibrary;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace CimdoAdmin;

public partial class AutorizationPage : UserControl
{
    
    public AutorizationPage()
    {
        InitializeComponent();
    }
    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
        LogInBox = this.Find<TextBox>("LogInBox");
        PasswordBox = this.Find<TextBox>("PasswordBox");
    }

    private void Exit_OnClick(object? sender, RoutedEventArgs e)
    {
        Environment.Exit(0);
    }

    private async void LogIn_OnClick(object? sender, RoutedEventArgs e)
    {
        // Создаем экземпляр HttpClient
        using (HttpClient client = new HttpClient())
        {
            string login = LogInBox.Text;
            string password = PasswordBox.Text;

            LogInUser user = new LogInUser();
            user.Login = login;
            user.Password = password;

            string jsonData = JsonConvert.SerializeObject(user);

            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

            // Отправляем POST-запрос к локальному API
            HttpResponseMessage response =
                await client.PostAsync($"https://petrusu.bsite.net/api/ForAllUsers/login?login={login}&password={password}", null);

            // Проверяем успешность запроса
            if (response.IsSuccessStatusCode)
            {
                // Читаем содержимое ответа
                string responseBody = await response.Content.ReadAsStringAsync();
                var responseObject = JsonConvert.DeserializeObject<dynamic>(responseBody);
                string jwt = responseObject.loginResponse.token; // Извлекаем токен из поля "token" внутри "loginResponse"
    
                MainPage mainPage = new MainPage(jwt);
                NavigationManager.NavigateTo(mainPage);
                
            }
            else
            {
                Console.WriteLine("Ошибка: " + response.StatusCode);
            }
        }


    }
}