using System;
using System.Net.Http;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;

namespace CimdoAdmin;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        contentControl = this.FindControl<ContentControl>("contentControl");
        NavigationManager.Initialize(contentControl);
        ShowAutorizationPage();
    }

    private void ShowAutorizationPage()
    {
        contentControl.Content = new AutorizationPage();
    }
        
    
}