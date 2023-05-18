using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ForeignWords.App.Exceptions;

namespace ForeignWords.App.Models;

public class TranslationsBook
{
    private const int ScoreBorder = 5;

    private readonly List<Translation> _translations;

    public TranslationsBook()
    {
        _translations = new List<Translation>();
    }

    public void AddTranslation(Translation translation)
    {
        if (_translations.Any(d => d.DomesticWord == translation.DomesticWord))
        {
            var trans = _translations.First(d => d.DomesticWord == translation.DomesticWord);

            for (var index = 0; index < translation.ForeignWords.Count; index++)
            {
                var forWord = translation.ForeignWords[index];

                if (!trans.ForeignWords.Contains(forWord))
                {
                    trans.ForeignWords.Add(forWord);
                }
            }
        }
        else
        {
            _translations.Add(translation);
        }
    }

    public void DeleteTranslation(Translation translation)
    {
        _translations.Remove(translation);
    }

    public Translation GetRandomTranslation()
    {
        var rn = new Random();
        var index = rn.Next(_translations.Count);

        return _translations[index];
    }

    public Translation GetRandomNewTranslation()
    {
        var rn = new Random();
        var list = _translations.Where(d => d.Score < ScoreBorder).ToArray();

        return list.ElementAt(rn.Next(list.Length));
    }

    public Translation GetRandomPassedTranslation()
    {
        var rn = new Random();
        var list = _translations.Where(d => d.Score >= ScoreBorder).ToArray();

        return list.ElementAt(rn.Next(list.Length));
    }

    public List<string> GetDomesticTranslations(string foreignWord)
    {
        var translations = _translations.Where(d => d.ForeignWords.Contains(foreignWord));

        return translations.Select(translationItem => translationItem.DomesticWord).ToList();
    }

    public List<Translation> GetAllTranslations()
    {
        return _translations;
    }

    public IEnumerable<Translation> GetFilteredTranslations(string filterText)
    {
        return _translations.Where(d => d.DomesticWord.Contains(filterText, StringComparison.OrdinalIgnoreCase) 
                                        || d.ForeignWords.Any(i => i.Contains(filterText, StringComparison.OrdinalIgnoreCase)));
    }

    public int GetTranslationsCount()
    {
        return _translations.Count;
    }

    public int GetNewTranslationsCount()
    {
        return _translations.Count(d => d.Score < ScoreBorder);
    }

    public int GetPassedTranslationsCount()
    {
        return _translations.Count(d => d.Score >= ScoreBorder);
    }
}