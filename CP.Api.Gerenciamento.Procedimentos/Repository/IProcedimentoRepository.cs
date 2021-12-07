using CP.Api.Gerenciamento.Procedimentos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CP.Api.Gerenciamento.Procedimentos.Repository
{
    public interface IProcedimentoRepository
    {
        void Cadastrar(Procedimento procedimento);
        void Excluir(Procedimento procedimento);
        void Alterar(Procedimento procedimento);
        List<Procedimento> Consultar();
        List<Procedimento> ConsultarPorParametro(string nome);
    }
}
