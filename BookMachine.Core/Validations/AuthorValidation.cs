using BookMachine.Core.Interfaces.Core.Validations;
using BookMachine.Core.Models;

namespace BookMachine.Core.Validations
{
    public class AuthorValidation : IAuthorValidation
    {
        public static (string Name, List<string> Errors) NameValidation(string name, List<string> errors)
        {
            if (string.IsNullOrEmpty(name))
            {
                errors.Add($"Property [Name] contains empty string or value null.");

                return (name, errors);
            }

            if (name.Length > Author.MAX_NAME_LENGTH)
            {
                errors.Add($"Property [Name] exceeded the allowed string length.");
            }

            return (name, errors);
        }
    }
}
