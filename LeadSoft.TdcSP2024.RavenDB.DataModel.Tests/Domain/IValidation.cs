using System.ComponentModel.DataAnnotations;

namespace LeadSoft.TdcSP2024.RavenDB.DataModel.Tests.Domain
{
    /// <summary>
    /// Define um contrato para validação de objetos.
    /// </summary>
    public interface IValidation
    {
        /// <summary>
        /// Realiza a validação do objeto atual e retorna os resultados da validação.
        /// </summary>
        /// <param name="oValidationResults">Uma lista de <see cref="ValidationResult"/> que será preenchida com os resultados da validação.</param>
        /// <returns>
        /// Retorna <see langword="true"/> se o objeto for válido, ou <see langword="false"/> se houver erros de validação.
        /// </returns>
        bool IsValid(out IList<ValidationResult> oValidationResults);
    }
}
