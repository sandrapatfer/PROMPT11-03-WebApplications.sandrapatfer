using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mod03_ChelasMovies.DomainModel
{
    public class SearchableAttribute : Attribute
    {
        public string FilterFormat { get; set; }
    }
}
