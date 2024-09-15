using System.ComponentModel.DataAnnotations;

namespace LeadSoft.TdcSP2024.RavenDB.DataModel.Tests.Domain.Entities.Products.Categories
{
    /// <summary>
    /// Representa uma categoria de produto, incluindo identificador e nome, com suporte para validação de dados.
    /// </summary>
    public partial class Category : Validation
    {
        /// <summary>
        /// Identificador único da categoria.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Nome da categoria, que é um campo obrigatório e validado quanto ao tamanho.
        /// </summary>
        /// <remarks>
        /// O nome deve ter entre 1 e 127 caracteres e é validado usando as anotações <see cref="RequiredAttribute"/> e <see cref="RangeAttribute"/>.
        /// </remarks>
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "O nome da categoria é obrigatório.")]
        [MaxLength(127, ErrorMessage = "O nome deve conter até 127 caracteres.")]
        public string Name { get; private set; }
    }
}
