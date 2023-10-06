using System.Collections.Generic;
using System.Windows.Input;
using ForeignWords.App.Commands;
using ForeignWords.App.Enums;
using ForeignWords.App.Models;
using ForeignWords.App.Services;

namespace ForeignWords.App.ViewModels;

public class HomeViewModel : ViewModelBase
{
    private readonly TranslationsBook _book;

    private int _score;
    private int _wordsCount;
    private Translation _translation = new(string.Empty, new List<string>());
    private string _domesticWord = string.Empty;
    private List<string> _foreignWord = new();

    private Status _status = Status.None;

    private int _allNewPassedSelection;
    private int _domesticForeignSelection;

    public Status Status
    {
        get => _status;
        set
        {
            _status = value;
            OnPropertyChanged(nameof(Status));
        }
    }

    public int AllNewPassedSelection
    {
        get => _allNewPassedSelection;
        set
        {
            _allNewPassedSelection = value;
            OnPropertyChanged(nameof(AllNewPassedSelection));

            SetDefaultScreen();
        }
    }

    public int DomesticForeignSelection
    {
        get => _domesticForeignSelection;
        set
        {
            _domesticForeignSelection = value;
            OnPropertyChanged(nameof(DomesticForeignSelection));

            SetDefaultScreen();
        }
    }

    public Translation Translation
    {
        get => _translation;
        set
        {
            _translation = value;
            OnPropertyChanged(nameof(Translation));
        }
    }

    public int Score
    {
        get => _score;
        set
        {
            _score = value;
            OnPropertyChanged(nameof(Score));
        }
    }

    public int WordsCount
    {
        get => _wordsCount;
        set
        {
            _wordsCount = value;
            OnPropertyChanged(nameof(WordsCount));
        }
    }

    public string DomesticWord
    {
        get => _domesticWord;
        set
        {
            _domesticWord = value;
            OnPropertyChanged(nameof(DomesticWord));
        }
    }

    public List<string> ForeignWord
    {
        get => _foreignWord;
        set
        {
            _foreignWord = value;
            OnPropertyChanged(nameof(ForeignWord));
        }
    }

    public ICommand RandomWordCommand { get; }
    public ICommand KnowCommand { get; }
    public ICommand DoNotKnowCommand { get; }
    public ICommand TranslationCommand { get; }
    public ICommand DidNotKnowCommand { get; }
    public ICommand AddNewWordCommand { get; }
    public ICommand ShowWordsCommand { get; }

    public HomeViewModel(TranslationsBook book, NavigationService addNewWordNavigationService, NavigationService showWordsNavigationService)
    {
        _book = book;

        WordsCount = _book.GetTranslationsCount();

        RandomWordCommand = new RandomWordCommand(this, _book);
        KnowCommand = new KnowCommand(this);
        DoNotKnowCommand = new IDoNotKnowCommand(this);
        DidNotKnowCommand = new DidNotKnowCommand(this);
        TranslationCommand = new TranslationCommand(this);

        AddNewWordCommand = new NavigateCommand(addNewWordNavigationService);
        ShowWordsCommand = new NavigateCommand(showWordsNavigationService);
    }

    public void UpdateWordsCount()
    {
        WordsCount = AllNewPassedSelection switch
        {
            0 => _book.GetTranslationsCount(),
            1 => _book.GetNewTranslationsCount(),
            2 => _book.GetPassedTranslationsCount(),
            _ => -1
        };
    }

    public void SetDefaultScreen()
    {
        UpdateWordsCount();

        Score = 0;
        DomesticWord = string.Empty;
        ForeignWord = new List<string>();

        Status = Status.None;
    }
}