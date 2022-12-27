using System.Collections.Generic;
using ForeignWords.App.Models;

namespace ForeignWords.App.Repositories;

public class TranslationsRepository : ITranslationsRepository
{
    private readonly string _dbFileName = "db.json";

    public List<Translation> GetAllTranslations()
    {
        throw new System.NotImplementedException();
    }

    public Translation? GetTranslationByDomesticWord(string domesticWord)
    {
        throw new System.NotImplementedException();
    }

    public List<Translation> GetTranslationsByForeignWord(string foreignWord)
    {
        throw new System.NotImplementedException();
    }

    public bool Insert(Translation translation)
    {
        throw new System.NotImplementedException();
    }

    public bool Update(Translation translation)
    {
        throw new System.NotImplementedException();
    }

    public void Remove(Translation translation)
    {
        throw new System.NotImplementedException();
    }

    public bool Exists(Translation translation)
    {
        throw new System.NotImplementedException();
    }
}
