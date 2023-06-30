using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Azure.Core;
using CimdoAdmin.Model;
using Newtonsoft.Json;

namespace CimdoAdmin.Window_or_Page;

public partial class MainPage : UserControl
{
    private string token;

    public MainPage() : this(null)
    {
    }

    public MainPage(string token)
    {
        InitializeComponent();
        BookListBox = this.FindControl<ListBox>("BookListBox");
        UserListBox = this.FindControl<ListBox>("UserListBox");
        this.token = token;
        ViewBook = this.FindControl<Button>("ViewBook");
        ViewUser = this.FindControl<Button>("ViewUser");
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }

    private async void ViewBook_OnClick(object? sender, RoutedEventArgs e)
    {
        ShowBookListBox();
        HideUserListBox();
        
        // Создаем экземпляр HttpClient
        HttpClient _httpClient = new HttpClient();

        try
        {
            // Добавляем токен в заголовок Authorization
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            // Отправляем GET-запрос к API
            var response = await _httpClient.GetAsync("https://petrusu.bsite.net/api/ForAllUsers/getbooks");

            // Проверяем успешность запроса
            if (response.IsSuccessStatusCode)
            {
                // Читаем ответ в виде строки
                var responseBody = await response.Content.ReadAsStringAsync();

                // Десериализуем полученные данные в список объектов Books
                var books = JsonConvert.DeserializeObject<IEnumerable<BooksCollection>>(responseBody);

                // Очищаем ListBox
                BookListBox.Items = books;
            }
            else
            {
                // Обработка ошибки, если запрос не успешен
                Console.WriteLine("Ошибка при получении данных из API.");
            }
        }
        catch (Exception ex)
        {
            // Обработка исключений, если произошла ошибка при выполнении запроса
            Console.WriteLine("Произошла ошибка: " + ex.Message);
        }


    }

    private async void ViewUser_OnClick(object? sender, RoutedEventArgs e)
    {
        ShowUserListBox();
        HideBookListBox();
        
        
        // Создаем экземпляр HttpClient
        HttpClient _httpClient = new HttpClient();

        try
        {
            // Добавляем токен в заголовок Authorization
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            // Отправляем GET-запрос к API
            var response = await _httpClient.GetAsync("https://petrusu.bsite.net/api/ForAdmin/getusers");

            // Проверяем успешность запроса
            if (response.IsSuccessStatusCode)
            {
                // Читаем ответ в виде строки
                var responseBody = await response.Content.ReadAsStringAsync();

                // Десериализуем полученные данные в список объектов Books
                var users = JsonConvert.DeserializeObject<IEnumerable<UsersCollection>>(responseBody);

                // Очищаем ListBox
                UserListBox.Items = users;
            }
            else
            {
                // Обработка ошибки, если запрос не успешен
                Console.WriteLine("Ошибка при получении данных из API.");
            }
        }
        catch (Exception ex)
        {
            // Обработка исключений, если произошла ошибка при выполнении запроса
            Console.WriteLine("Произошла ошибка: " + ex.Message);
        }
        
    }
    private void ShowBookListBox()
    {
        BookListBox.IsVisible = true;
    }

    private void HideBookListBox()
    {
        BookListBox.IsVisible = false;
    }

    private void ShowUserListBox()
    {
        UserListBox.IsVisible = true;
    }

    private void HideUserListBox()
    {
        UserListBox.IsVisible = false;
    }

    private void Exit_OnClick(object? sender, RoutedEventArgs e)
    {
        AutorizationPage autorizationPage = new AutorizationPage();
        NavigationManager.NavigateTo(autorizationPage);
    }


    private void DeliteUser_OnClick(object? sender, RoutedEventArgs e)
    {
        var button = (Button)sender;
        var user = (UsersCollection)button.CommandParameter;
    
        if (user != null)
        {
            DeliteUserPage del = new DeliteUserPage(user.IdUser, user.Login, token);
            NavigationManager.NavigateTo(del);
        }
    }

    
}