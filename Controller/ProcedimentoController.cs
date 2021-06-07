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
    public class ProcedimentoController:ControllerBase
    {
        private readonly IProcedimentoRepository procedimentoRepository;

        public ProcedimentoController(IProcedimentoRepository procedimentoRepository)
        {
            this.procedimentoRepository = procedimentoRepository;
        }


        [HttpGet]
        [Authorize]
        public ActionResult<IEnumerable<Procedimento>> Get()
        {
            var retorno = procedimentoRepository.Consultar();
            return Ok(retorno);
        }

        [HttpGet("{Procedimento}")]
        [Authorize]
        public ActionResult<IEnumerable<Procedimento>> Get([Required] string Procedimento)
        {
            var retorno = procedimentoRepository.ConsultarPorParametro(Procedimento);
            return Ok(retorno);
        }


    }
}
