using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ForeignWords.Common;

namespace ForeignWords.DAL.Entities;

public record ForeignWordEntity : WordEntity
{
    public Language Language { get; set; }
}