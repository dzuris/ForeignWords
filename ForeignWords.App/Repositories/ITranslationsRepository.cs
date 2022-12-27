using System.Collections.Generic;
using ForeignWords.App.Models;

namespace ForeignWords.App.Repositories;

public interface ITranslationsRepository
{
    public List<Translation> GetAllTranslations();
    public Translation? GetTranslationByDomesticWord(string domesticWord);
    public List<Translation> GetTranslationsByForeignWord(string foreignWord);
    public bool Insert(Translation translation);
    public bool Update(Translation translation);
    public void Remove(Translation translation);
    public bool Exists(Translation translation);
}
