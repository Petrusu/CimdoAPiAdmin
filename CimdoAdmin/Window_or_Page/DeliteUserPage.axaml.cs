using System;
using System.Net.Http;
using System.Net.Http.Headers;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;

namespace CimdoAdmin.Window_or_Page
{
    public partial class DeliteUserPage : UserControl
    {
        private string token;
        private int id_User;
        private string login;

        public DeliteUserPage() : this(0, string.Empty, token:null)
        {
        }

        public DeliteUserPage(int idUser, string login, string token)
        {
            InitializeComponent();
            this.id_User = idUser;
            this.login = login;
            this.token = token;

            UpdateTextBlocks();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }

        private void UpdateTextBlocks()
        {
            var idUserTextBlock = this.FindControl<TextBlock>("IdUserTextBlock");
            idUserTextBlock.Text = id_User.ToString();

            var loginTextBlock = this.FindControl<TextBlock>("LoginTextBlock");
            loginTextBlock.Text = login;
        }

        private async void DeliteBtn_OnClick(object? sender, RoutedEventArgs e)
        {
            try
            {
                // Создаем экземпляр HttpClient
                using (HttpClient client = new HttpClient())
                {
                    // Устанавливаем базовый адрес удаленного сервера
                    client.BaseAddress = new Uri("https://petrusu.bsite.net/api/ForAdmin/deliteuser");

                    int userId = id_User; // Замените на фактический идентификатор пользователя

                    // Устанавливаем заголовок Authorization с токеном
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                    // Отправляем DELETE-запрос на удаление пользователя
                    HttpResponseMessage response = await client.DeleteAsync($"deliteuser?id_user={userId}");

                    // Проверяем успешность операции
                    if (response.IsSuccessStatusCode)
                    {
                        MainPage mainPage = new MainPage(token);
                        NavigationManager.NavigateTo(mainPage);
                        Console.WriteLine("User deleted successfully.");
                    }
                    else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                    {
                        Console.WriteLine("User not found.");
                    }
                    else
                    {
                        Console.WriteLine("An error occurred while deleting the user.");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }

        private void BckBtn_OnClick(object? sender, RoutedEventArgs e)
        {
            MainPage mainPage = new MainPage(token);
            NavigationManager.NavigateTo(mainPage);
        }
    }
}