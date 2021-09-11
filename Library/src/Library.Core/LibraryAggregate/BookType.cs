using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Core.LibraryAggregate
{
    public enum BookType
    {
        [Display(Name = "Regular")]
        Regular,
        [Display(Name = "Restricted")]
        Restricted
    }
}
