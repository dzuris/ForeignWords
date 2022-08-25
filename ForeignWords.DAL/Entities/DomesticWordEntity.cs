using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForeignWords.DAL.Entities;

public record DomesticWordEntity : WordEntity
{
    public ICollection<ForeignWordEntity> Translations { get; init; } = new List<ForeignWordEntity>();
}