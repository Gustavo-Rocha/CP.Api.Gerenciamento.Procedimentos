using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CP.Api.Gerenciamento.Procedimentos.Models
{
    public class ProcedimentoViewModelAlteracao
    {
        [StringLength(60, MinimumLength = 3)]
        [RegularExpression(@"^[a-zA-Z''-'\s]{1,40}$", ErrorMessage = "Nome do Procedimento Inválido")]
        public string NomeProcedimentoAntigo { get; set; }

        [StringLength(60, MinimumLength = 3)]
        [RegularExpression(@"^[a-zA-Z''-'\s]{1,40}$", ErrorMessage = "Nome do Procedimento Inválido")]
        public string NomeProcedimento { get; set; }

        //[RegularExpression(@"^[a-zA-Z''-'\s]", ErrorMessage = "Valor do Procedimento Inválido")]
        public string valorProcedimento { get; set; }
        public DateTime DuracaoProcedimento { get; set; }

        [StringLength(100, MinimumLength = 3)]
        public string DescricaoProcedimento { get; set; }
    }
}
