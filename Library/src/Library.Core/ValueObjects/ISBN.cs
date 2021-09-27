using Library.SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Library.Core.ValueObjects
{
    public class ISBN : ValueObject
    {
        private static readonly string ISBNPattern = @"^(?=(?:\D*\d){10}(?:(?:\D*\d){3})?$)[\d-]+$";

        public string Value { get; private set; }

        public ISBN(string value)
        {
            string trimmedValue = value.Trim();
            if (!Regex.IsMatch(trimmedValue, ISBNPattern))
                throw new ArgumentException();

            Value = trimmedValue;
        }
    }
}
