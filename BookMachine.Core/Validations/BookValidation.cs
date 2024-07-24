using BookMachine.Core.Interfaces.Core.Validations;
using BookMachine.Core.Models;

namespace BookMachine.Core.Validations
{
    public class BookValidation : IBookValidation
    {
        public static (string Title, List<string> Errors) TitleValidation(string title, List<string> errors)
        {
            if (string.IsNullOrEmpty(title))
            {
                errors.Add($"Property [Title] contains empty string or value null.");

                return (title, errors);
            }

            if (title.Length > Book.MAX_TITLE_LENGTH)
            {
                errors.Add($"Property [Title] exceeded the allowed string length.");
            }

            return (title, errors);
        }
    }
}
