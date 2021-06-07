using CP.Api.Gerenciamento.Procedimentos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CP.Api.Gerenciamento.Procedimentos.Repository
{
    public class ProcedimentoRepository : IProcedimentoRepository
    {
        private readonly ApplicationContext _context;

        public void Alterar(Procedimento procedimento)
        {
            _context.Procedimentos.Update(procedimento);
            _context.SaveChanges();
        }

        public void Cadastrar(Procedimento procedimento)
        {
            try
            {
                _context.Procedimentos.Add(procedimento);
                _context.SaveChanges();
            }
            catch (Exception e)
            {

                throw e.InnerException;
            }
        }

        public List<Procedimento> Consultar()
        {
            return _context.Procedimentos.ToList();
        }

        public List<Procedimento> ConsultarPorParametro(string nome)
        {
            return _context.Procedimentos.Where(_ => _.NomeProcedimento.Contains(nome)).ToList();
        }

        public void Excluir(Procedimento procedimento)
        {
            _context.Procedimentos.Remove(procedimento);
            _context.SaveChanges();
        }
    }
}
