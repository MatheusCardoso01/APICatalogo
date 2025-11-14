using System.ComponentModel.DataAnnotations;

namespace APICatalogo.Validations
{
    public class PermissoesDeSexoAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        { 
            if (value is null) return ValidationResult.Success;

            var sexo = value.ToString();

            if (sexo is not null && sexo.Equals("masculino", StringComparison.OrdinalIgnoreCase)
                || sexo.Equals("feminino", StringComparison.OrdinalIgnoreCase))
            { 
                return ValidationResult.Success;
            }

            return new ValidationResult("O campo sexo deve ser 'Masculino' ou 'Feminino'");
        }
    }
}
