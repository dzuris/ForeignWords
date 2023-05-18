using System;
using System.Collections.Generic;
using System.IO.Packaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ForeignWords.App.Commands;
using ForeignWords.App.Models;
using ForeignWords.App.Services;
using ForeignWords.App.Resources.Texts;
using ForeignWords.App.Stores;

namespace ForeignWords.App.ViewModels;

public class ModifyWordViewModel : ViewModelBase
{
    private Translation _translation;
    private string _translatedWord = string.Empty;
    private string _domesticWord = string.Empty;
    private List<string> _foreignWords = new();

    public string Title { get; set; } = ModifyWordResources.Header_New_Word_Content;
    public string SaveUpdateButtonContent { get; set; } = ModifyWordResources.Save_Button_Content;

    public Translation Translation
    {
        get => _translation;
        set
        {
            _translation = value;
            OnPropertyChanged(nameof(Translation));
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

    public string TranslatedWord
    {
        get => _translatedWord;
        set
        {
            _translatedWord = value;
            OnPropertyChanged(nameof(TranslatedWord));
        }
    }

    public List<string> ForeignWords
    {
        get => _foreignWords;
        set
        {
            _foreignWords = value;
            OnPropertyChanged(nameof(ForeignWords));
        }
    }

    public bool IsAddingMode { get; set; } = true;

    public ICommand AddCommand { get; }
    public ICommand RemoveLastCommand { get; }
    public ICommand CancelCommand { get; }
    public ICommand SaveCommand { get; }

    public ModifyWordViewModel(TranslationsBook book, NavigationService navigationService, Translation? translation = null)
    {
        if (translation is not null)
        {
            Title = ModifyWordResources.Header_Edit_Word_Content;
            SaveUpdateButtonContent = ModifyWordResources.Update_Button_Content;
            IsAddingMode = false;
        }
        
        _translation = translation ?? new Translation(string.Empty, new List<string>());

        if (!IsAddingMode)
        {
            DomesticWord = Translation.DomesticWord;
            ForeignWords = Translation.ForeignWords.ToList();
        }

        AddCommand = new AddTranslationCommand(this);
        RemoveLastCommand = new RemoveLastTranslationCommand(this);
        SaveCommand = IsAddingMode ? new SaveNewWordCommand(this, book) : new UpdateWordCommand(this);

        CancelCommand = new NavigateCommand(navigationService);
    }
}