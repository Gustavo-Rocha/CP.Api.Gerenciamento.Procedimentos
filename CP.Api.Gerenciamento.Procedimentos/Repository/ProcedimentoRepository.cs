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


        public ProcedimentoRepository(ApplicationContext _context)
        {
            this._context = _context;
        }
        public List<Procedimento> Consultar()
        {
            try
            {
                return _context.Procedimentos.ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public List<Procedimento> ConsultarPorParametro(string nome)
        {
            try
            {
                return _context.Procedimentos.Where(procedimento => procedimento.NomeProcedimento.Contains(nome)).ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void Alterar(Procedimento procedimento)
        {
            try
            {
                _context.Procedimentos.Update(procedimento);
                _context.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }   
        }
        public void Cadastrar(Procedimento procedimento)
        {
                _context.Procedimentos.Add(procedimento);
                _context.SaveChanges();   
        }
        public void Excluir(Procedimento procedimento)
        {
                _context.Procedimentos.Remove(procedimento);
                _context.SaveChanges();
        }
    }
}
