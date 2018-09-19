using NumberInWords.Core;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CurrencyToWord.Entities
{
    public class InputModel : NameModel
    {
        [Required(ErrorMessage = Constants.NumberRequiredValidationError)]
        [Range(-999999999999999.99, 999999999999999.99, ErrorMessage = Constants.NumberRangeValidationError)]
        [DisplayName("Number *")]
        public decimal Number { get; set; }
    }
}
