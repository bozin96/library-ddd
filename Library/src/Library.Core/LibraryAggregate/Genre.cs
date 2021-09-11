using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Core.LibraryAggregate
{
    public enum Genre
    {
        [Display(Name ="Drama")]
        Drama,
        [Display(Name = "Crime")]
        Crime,
        [Display(Name = "Novel")]
        Novel,
        [Display(Name = "Novella")]
        Novella,
        [Display(Name = "Philosophy")]
        Philosophy,
        [Display(Name = "Psychology")]
        Psychology,
        [Display(Name = "History")]
        History,
        [Display(Name = "Epic")]
        Epic,
        [Display(Name = "Science Fiction")]
        ScienceFiction,
        [Display(Name = "Sociology")]
        Sociology
    }
}
