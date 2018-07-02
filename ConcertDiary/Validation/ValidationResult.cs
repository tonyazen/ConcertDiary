
using Microsoft.Ajax.Utilities;

namespace ConcertDiary.Validation
{
    public class ValidationResult
    {
        public bool IsValid => ValidationMessage.IsNullOrWhiteSpace();
        public string ValidationMessage { get; set; }

        public ValidationResult(string validationMessage)
        {
            ValidationMessage = validationMessage;
        }

        public ValidationResult()
        {
            ValidationMessage = string.Empty;
        }
    }
}