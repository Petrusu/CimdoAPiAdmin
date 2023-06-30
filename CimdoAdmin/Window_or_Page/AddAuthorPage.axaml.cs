using System;
using System.ComponentModel;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using CimdoAdmin.Model;
using CimdoApi;
using Newtonsoft.Json;

namespace CimdoAdmin.Window_or_Page;

public partial class AddAuthorPage : UserControl
{
    private string token;

    public AddAuthorPage() : this(null)
    {
    }
    public AddAuthorPage(string token)
    {
        InitializeComponent();
        this.token = token;
        NewAuthorTextBox = this.FindControl<TextBox>("NewAuthorTextBox");
        NewAuthorBtn = this.FindControl<Button>("NewAuthorBtn");
        BckBtn = this.FindControl<Button>("BckBtn");
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }

    private async void NewAuthorBtn_OnClick(object? sender, RoutedEventArgs e)
    {
        string name = NewAuthorTextBox.Text;

        AuthorsCollection authorsCollection = new AuthorsCollection()
        {
            Author1 = name
        };

        HttpClient httpClient = new HttpClient();
        string apiUrl = "https://petrusu.bsite.net/api/ForAdmin/addauthor";

        try
        {
            // Добавляем токен в заголовок Authorization
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            string json = JsonConvert.SerializeObject(authorsCollection);
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await httpClient.PostAsync(apiUrl, content);

            if (response.IsSuccessStatusCode)
            {
                string responseContent = await response.Content.ReadAsStringAsync();
                // Обработка успешного ответа от сервера
                Console.WriteLine(responseContent);
            }
            else
            {
                // Обработка ошибки
                Console.WriteLine("Error: " + response.StatusCode);
            }
        }
        catch (Exception ex)
        {
            // Обработка исключения
            Console.WriteLine("Exception: " + ex.Message);
        }
    }

    private void BckBtn_OnClick(object? sender, RoutedEventArgs e)
    {
        MainPage mainPage = new MainPage(token);
        NavigationManager.NavigateTo(mainPage);
    }
}