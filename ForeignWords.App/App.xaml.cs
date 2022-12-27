using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using ForeignWords.App.Models;
using ForeignWords.App.Repositories;
using ForeignWords.App.Services;
using ForeignWords.App.Stores;
using ForeignWords.App.ViewModels;

namespace ForeignWords.App;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    private readonly TranslationsBook _book;
    private readonly NavigationStore _navigationStore;

    public App()
    {
        Thread.CurrentThread.CurrentCulture = new CultureInfo("sk");
        Thread.CurrentThread.CurrentUICulture = new CultureInfo("sk");

        _book = new TranslationsBook();
        LoadFileIntoBook(_book);

        var a = new TranslationsRepository();

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

    private void LoadFileIntoBook(TranslationsBook book)
    {
        // string fileName = "db.json";
        // string jsonString = File.ReadAllText(fileName);
        //
        // List<Translation> allTranslations = JsonSerializer.Deserialize<List<Translation>>(jsonString)!;
        // foreach (var transaltionModel in allTranslations)
        // {
        //     book.AddTranslation(transaltionModel);
        // }

        IEnumerable<string> lines = File.ReadLines("words.txt");
        
        foreach (var line in lines)
        {
            string[] words = line.Split("|");
        
            if (words.Length != 3)
            {
                throw new ArgumentOutOfRangeException();
            }
        
            var domesticWord = words[0];
            var foreignWords = words[1].Split(",");
            var score = int.Parse(words[2]);
        
            var translation = new Translation(domesticWord, foreignWords.ToList(), score);
        
            book.AddTranslation(translation);
        }
    }

    private void WriteTranslationsIntoFile(TranslationsBook book)
    {
        var lines = new List<string>();

        foreach (var translation in book.GetAllTranslations())
        {
            var domesticWord = translation.DomesticWord;
            var foreignWords = string.Join(",", translation.ForeignWords);
            var score = translation.Score.ToString();

            var line = domesticWord + "|" + foreignWords + "|" + score;

            lines.Add(line);
        }

        File.WriteAllLines("words.txt", lines);
    }
}