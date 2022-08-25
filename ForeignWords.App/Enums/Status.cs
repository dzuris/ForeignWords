using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForeignWords.App.Enums
{
    public enum Status
    {
        None = 0,
        RandomWordStatus = 1,
        TranslationStatus = 2,
        ResponseKnowStatus = 3,
        ResponseDoNotKnowStatus = 4,
        ResponseDidNotKnowStatus = 5
    }
}
