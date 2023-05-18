using System.Collections.Generic;

namespace ForeignWords.App.Models;

public class Translation
{
    private string _domesticWord = "";
    private List<string> _foreignWords = new();

    public string DomesticWord
    {
        get => _domesticWord;
        set => _domesticWord = value.ToLower();
    }
    public List<string> ForeignWords
    {
        get => _foreignWords;
        set => _foreignWords = value.ConvertAll(d => d.ToLower());
    }
    public int Score { get; set; }

    public Translation()
    {
        DomesticWord = string.Empty;
        ForeignWords = new List<string>();
        Score = 0;
    }

    public Translation(string domesticWords, List<string> foreignWords, int score = 0)
    {
        DomesticWord = domesticWords;
        ForeignWords = foreignWords;
        Score = score;
    }
}