using AutoMapper;
using CP.Api.Gerenciamento.Procedimentos.Models;
using CP.Api.Gerenciamento.Procedimentos.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CP.Api.Gerenciamento.Procedimentos.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProcedimentoController : ControllerBase
    {
        private readonly IProcedimentoRepository procedimentoRepository;
        private readonly IMapper mapper;

        public ProcedimentoController(IProcedimentoRepository procedimentoRepository, IMapper mapper)
        {
            this.procedimentoRepository = procedimentoRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        //[Authorize]
        public async Task<ActionResult<IEnumerable<Procedimento>>> Get() =>
            Ok(procedimentoRepository.Consultar());

        [HttpGet("{Procedimento}")]
        //[Authorize]
        public async Task<ActionResult<IEnumerable<Procedimento>>> Get([Required] string Procedimento) =>
            Ok(procedimentoRepository.ConsultarPorParametro(Procedimento));

        [HttpPost]
        public ActionResult<ProcedimentoViewModel> Post(ProcedimentoViewModel procedimentoViewModel)
      {
            var procedimento = mapper.Map<Procedimento>(procedimentoViewModel);

            try
            {
                procedimentoRepository.Cadastrar(procedimento);
                return Ok();
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPut]
        public ActionResult<Procedimento> Put(ProcedimentoViewModelAlteracao procedimentoViewModel)
        {
            var procedimento = mapper.Map<Procedimento>(procedimentoViewModel);
            var Procuraprocedimento = procedimentoRepository.ConsultarPorParametro(procedimentoViewModel.NomeProcedimentoAntigo);
            try
            {
                Procuraprocedimento[0].NomeProcedimento = procedimento.NomeProcedimento;
                Procuraprocedimento[0].valorProcedimento = procedimento.valorProcedimento;
                Procuraprocedimento[0].DuracaoProcedimento = procedimento.DuracaoProcedimento;
                Procuraprocedimento[0].DescricaoProcedimento = procedimento.DescricaoProcedimento;

                procedimentoRepository.Alterar(Procuraprocedimento[0]);
                return NoContent();
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpDelete("{nomeProcedimento}")]
        public ActionResult Delete(string nomeProcedimento)
        {
            var Procuraprocedimento = procedimentoRepository.ConsultarPorParametro(nomeProcedimento);

            if (Procuraprocedimento != null)
            {
                procedimentoRepository.Excluir(Procuraprocedimento[0]);
                return Ok(Procuraprocedimento[0]);
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
