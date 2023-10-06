using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Threading;
using System.Windows;
using ForeignWords.App.Models;
using ForeignWords.App.Services;
using ForeignWords.App.Stores;
using ForeignWords.App.ViewModels;
using Newtonsoft.Json;

namespace ForeignWords.App;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    private readonly TranslationsBook _book;
    private readonly NavigationStore _navigationStore;

    private const string DirectoryName = "Learn_New_Words_DA98E5B5-43B0-417F-B063-BBC8025C6607";
    private const string JsonFileName = "learn_new_words_appdata.json";
    private readonly string _jsonFilePath;

    public App()
    {
        Thread.CurrentThread.CurrentCulture = new CultureInfo("sk");
        Thread.CurrentThread.CurrentUICulture = new CultureInfo("sk");

        // Setting json initialization
        var appFolder = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        var dirPath = Path.Combine(appFolder, DirectoryName);

        // Create directory if it doesn't exist
        if (!Directory.Exists(dirPath))
        {
            Directory.CreateDirectory(dirPath);
        }

        _jsonFilePath = Path.Combine(dirPath, JsonFileName);

        _book = new TranslationsBook();
        LoadFileIntoBook(_book);

        _navigationStore = new NavigationStore();
    }

    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);

        // Setting home view
        _navigationStore.CurrentViewModel = CreateHomeViewModel();

        MainWindow = new MainWindow
        {
            DataContext = new MainViewModel(_navigationStore)
        };
        MainWindow.Show();
    }

    protected override void OnExit(ExitEventArgs e)
    {
        base.OnExit(e);

        WriteTranslationsIntoFile(_book);
    }

    private ModifyWordViewModel CreateAddNewWordViewModel()
    {
        return new ModifyWordViewModel(_book, new NavigationService(_navigationStore, CreateHomeViewModel));
    }

    private HomeViewModel CreateHomeViewModel()
    {
        return new HomeViewModel(_book, 
            new NavigationService(_navigationStore, CreateAddNewWordViewModel), 
            new NavigationService(_navigationStore, CreateWordsListViewModel));
    }

    private WordsListViewModel CreateWordsListViewModel()
    {
        return new WordsListViewModel(_book,
            new NavigationService(_navigationStore, CreateHomeViewModel),
            _navigationStore, new NavigationService(_navigationStore, CreateWordsListViewModel));
    }

    /// <summary>
    /// The function serializes data from a JSON file
    /// </summary>
    /// <param name="book">Object destination</param>
    private void LoadFileIntoBook(TranslationsBook book)
    {
        // Checks if json exists
        if (!File.Exists(_jsonFilePath)) return;

        var json = File.ReadAllText(_jsonFilePath);
        var allTranslations = JsonConvert.DeserializeObject<List<Translation>>(json);

        if (allTranslations is null)
        {
            return;
        }

        foreach (var translationModel in allTranslations)
        {
            book.AddTranslation(translationModel);
        }
    }

    /// <summary>
    /// The function deserializes data into a JSON file
    /// </summary>
    /// <param name="book">Object source</param>
    private void WriteTranslationsIntoFile(TranslationsBook book)
    {
        var content = JsonConvert.SerializeObject(book.GetAllTranslations(), Formatting.Indented);

        File.WriteAllText(_jsonFilePath, content);
    }
}