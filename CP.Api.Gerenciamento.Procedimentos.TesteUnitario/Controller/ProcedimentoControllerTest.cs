using AutoMapper;
using CP.Api.Gerenciamento.Procedimentos.Controller;
using CP.Api.Gerenciamento.Procedimentos.Models;
using CP.Api.Gerenciamento.Procedimentos.Repository;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace CP.Api.Gerenciamento.Procedimentos.TesteUnitario
{
    public class ProcedimentoControllerTest
    {
        private readonly IProcedimentoRepository _mockRepository = Substitute.For<IProcedimentoRepository>();
        // private readonly ProcedimentoController _procedimentoController = Substitute.For<ProcedimentoController>(); 
        private ProcedimentoController procedimentoController;
        private readonly IMapper _mapper;

        public ProcedimentoControllerTest()
        {
            //_procedimentoController = new ProcedimentoController(_mockRepository, _mapper);
            procedimentoController = new ProcedimentoController(_mockRepository, _mapper);
        }

        [Fact]
        public async Task GetDeveRetornarProcedimentos()
        {

            Procedimento proc = new Procedimento
            {
                IDProcedimento = 1,
                NomeProcedimento = "Bigodeira",
                valorProcedimento = "80",
                DuracaoProcedimento = DateTime.Now,
                DescricaoProcedimento = "bigoda Mil Grau"
            };

            var listaProc = new List<Procedimento>();
            listaProc.Add(proc);

            _mockRepository.Consultar().Returns(listaProc);

            var resposta = await procedimentoController.Get();

            var result = resposta.Result as OkObjectResult;
            result.StatusCode.Should().Be(200);
            //result.Value.Should().Be(proc);
        }

        [Fact]
        public async Task GetDeveRetornarErro()
        {

            Procedimento proc = new Procedimento
            {
                IDProcedimento = 1,
                NomeProcedimento = "Bigodeira",
                valorProcedimento = "80",
                DuracaoProcedimento = DateTime.Now,
                DescricaoProcedimento = "bigoda Mil Grau"
            };

            var listaProc = new List<Procedimento>();
            listaProc.Add(proc);

            _mockRepository.Consultar().Throws<ArgumentException>();

            //var resposta = await procedimentoController.Get();

            Assert.ThrowsAsync<ArgumentException>(() => procedimentoController.Get());
            //var result = resposta.Result as OkObjectResult;
            //result.StatusCode.Should().Be(200);
            //result.Value.Should().Be(proc);
        }

        [Fact]
        public async Task GetComParametroDeveRetornarProcedimentos()
        {
            Procedimento proc = new Procedimento
            {
                IDProcedimento = 1,
                NomeProcedimento = "Bigodeira",
                valorProcedimento = "80",
                DuracaoProcedimento = DateTime.Now,
                DescricaoProcedimento = "bigoda Mil Grau"
            };

            var listaProc = new List<Procedimento>();
            listaProc.Add(proc);

            _mockRepository.ConsultarPorParametro(proc.NomeProcedimento).Returns(listaProc);

            var resposta = await procedimentoController.Get(proc.NomeProcedimento);

            var result = resposta.Result as OkObjectResult;
            result.StatusCode.Should().Be(200);
            //result.Value.Should().Be(proc);
        }

        [Fact]
        public async Task GetComParametroDeveRetornarErro()
        {
            Procedimento proc = new Procedimento
            {
                IDProcedimento = 1,
                NomeProcedimento = "Bigodeira",
                valorProcedimento = "80",
                DuracaoProcedimento = DateTime.Now,
                DescricaoProcedimento = "bigoda Mil Grau"
            };

            var listaProc = new List<Procedimento>();
            listaProc.Add(proc);

            _mockRepository.ConsultarPorParametro(proc.NomeProcedimento).Throws<ArgumentException>();

            Assert.ThrowsAsync<ArgumentException>(() => procedimentoController.Get(proc.NomeProcedimento));
        }

        [Fact]
        public async Task PostDeveRetornarComSucesso()
        {
            //Arrange
            ProcedimentoViewModel proc = new ProcedimentoViewModel
            {

                NomeProcedimento = "Bigodeira",
                valorProcedimento = "80",
                DuracaoProcedimento = DateTime.Now,
                DescricaoProcedimento = "bigoda Mil Grau"
            };

            var mapperConfiguration = new MapperConfiguration(config =>
            {
                config.CreateMap<ProcedimentoViewModel, Procedimento>();
            }).CreateMapper();
            var procMapper = mapperConfiguration.Map<Procedimento>(proc);

            var listaProc = new List<Procedimento>();
            listaProc.Add(procMapper);

            //Action
            _mockRepository.Cadastrar(procMapper);
            procedimentoController = new ProcedimentoController(_mockRepository, mapperConfiguration);
            var resposta = procedimentoController.Post(proc);

            //Assert
            var result = resposta.Result as OkResult;
            result.StatusCode.Should().Be(200);
        }

        [Fact]
        public async Task PostDeveRetornarErro()
        {
            //Arrange
            ProcedimentoViewModel proc = new ProcedimentoViewModel
            {

                NomeProcedimento = "Bigodeira",
                valorProcedimento = "80",
                DuracaoProcedimento = DateTime.Now,
                DescricaoProcedimento = "bigoda Mil Grau"
            };

            var mapperConfiguration = new MapperConfiguration(config =>
            {
                config.CreateMap<ProcedimentoViewModel, Procedimento>();
            }).CreateMapper();
            var procMapper = mapperConfiguration.Map<Procedimento>(proc);

            var listaProc = new List<Procedimento>();
            listaProc.Add(procMapper);

            //Action
            //_mockRepository.Cadastrar(It.Is<Procedimento>(_ => _.NomeProcedimento == proc.NomeProcedimento)).Throws<ArgumentException>();
            //_procedimentoController.Post(proc).Throws<ArgumentException>();
            _mockRepository.When(mk => mk.Cadastrar(Arg.Is<Procedimento>(p=>p.NomeProcedimento==proc.NomeProcedimento)))
                .Do(d => { throw new ArgumentException(); });


            procedimentoController = new ProcedimentoController(_mockRepository, mapperConfiguration);
            //var resposta = procedimentoController.Post(proc);

            //Assert
            //var result = resposta.Result as OkResult;
            Assert.Throws<ArgumentException>(() => procedimentoController.Post(proc));
        }

        [Fact]
        public void PutDeveRetornarComSucesso()
        {
            //Arrange
            ProcedimentoViewModelAlteracao proc = new ProcedimentoViewModelAlteracao
            {
                NomeProcedimentoAntigo = "Bigodeira",
                NomeProcedimento = "depilação de Busto",
                valorProcedimento = "80",
                DuracaoProcedimento = DateTime.Now,
                DescricaoProcedimento = "bigoda Mil Grau"
            };

            var mapperConfiguration = new MapperConfiguration(config =>
            {
                config.CreateMap<ProcedimentoViewModelAlteracao, Procedimento>();
            }).CreateMapper();
            var procMapper = mapperConfiguration.Map<Procedimento>(proc);

            var listaProc = new List<Procedimento>();
            listaProc.Add(procMapper);

            //Action
            _mockRepository.ConsultarPorParametro(proc.NomeProcedimentoAntigo).Returns(listaProc);
            _mockRepository.Alterar(procMapper);
            
            //_procedimentoController.Post(proc).Throws<ArgumentException>();

            procedimentoController = new ProcedimentoController(_mockRepository, mapperConfiguration);
            var resposta = procedimentoController.Put(proc);

            //Assert
            var result = resposta.Result as NoContentResult;
            result.StatusCode.Should().Be(204);
        }

    }
}
