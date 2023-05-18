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
    private const string FileName = "C:\\Users\\adamd\\source\\repos\\ForeignWords\\ForeignWords\\ForeignWords.App\\db.json";

    public App()
    {
        Thread.CurrentThread.CurrentCulture = new CultureInfo("sk");
        Thread.CurrentThread.CurrentUICulture = new CultureInfo("sk");

        _book = new TranslationsBook();
        LoadFileIntoBook(_book);

        _navigationStore = new NavigationStore();
    }

    protected override void OnStartup(StartupEventArgs e)
    {
        _navigationStore.CurrentViewModel = CreateHomeViewModel();

        MainWindow = new MainWindow
        {
            DataContext = new MainViewModel(_navigationStore)
        };
        MainWindow.Show();

        base.OnStartup(e);
    }

    protected override void OnExit(ExitEventArgs e)
    {
        WriteTranslationsIntoFile(_book);

        base.OnExit(e);
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

    private static void LoadFileIntoBook(TranslationsBook book)
    {
        var json = File.ReadAllText(FileName);
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

    private static void WriteTranslationsIntoFile(TranslationsBook book)
    {
        File.WriteAllText(FileName, JsonConvert.SerializeObject(book.GetAllTranslations(), Formatting.Indented));
    }
}