using NumberInWords.Core;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CurrencyToWord.Entities
{
    public class NameModel
    {
        [Required(ErrorMessage = Constants.NameRequiredValidationError)]
        [DisplayName("Name *")]
        public string Name { get; set; }
    }
}
