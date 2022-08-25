using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForeignWords.BL.Models
{
    public record DomesticWordModel
    {
        public List<WordModel> Translations { get; set; } = new ();
    }
}
