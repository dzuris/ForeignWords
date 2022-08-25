using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForeignWords.App.Models;

public class Translation
{
    private string _domesticWord = "";
    private List<string> _foreignWords = new List<string>();

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

    public Translation(string domesticWords, List<string> foreignWords, int score = 0)
    {
        DomesticWord = domesticWords.ToLower();
        ForeignWords = foreignWords;
        Score = score;
    }
}