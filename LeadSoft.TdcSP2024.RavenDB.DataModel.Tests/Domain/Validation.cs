using System.ComponentModel.DataAnnotations;

namespace LeadSoft.TdcSP2024.RavenDB.DataModel.Tests.Domain
{
    /// <summary>
    /// Classe abstrata base que implementa o comportamento de validação para objetos. 
    /// Esta classe implementa a interface <see cref="IValidation"/> para fornecer um mecanismo padrão de validação.
    /// </summary>
    public abstract class Validation : IValidation
    {
        /// <summary>
        /// Realiza a validação do objeto atual utilizando as anotações de dados aplicadas em suas propriedades.
        /// </summary>
        /// <param name="oValidationResults">Lista que será preenchida com os resultados da validação, caso existam erros.</param>
        /// <returns>
        /// Retorna <see langword="true"/> se a validação for bem-sucedida, ou <see langword="false"/> se houver erros de validação.
        /// </returns>
        /// <remarks>
        /// O método utiliza o <see cref="Validator.TryValidateObject"/> para verificar se o objeto atende às regras de validação definidas pelas anotações de dados. 
        /// Se a validação for bem-sucedida, a lista <paramref name="oValidationResults"/> é esvaziada. Caso contrário, ela é preenchida com os erros de validação.
        /// </remarks>
        public bool IsValid(out IList<ValidationResult> oValidationResults)
        {
            oValidationResults = [];

            ValidationContext context = new(this);

            if (Validator.TryValidateObject(this, context, oValidationResults, true))
            {
                oValidationResults.Clear();
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
