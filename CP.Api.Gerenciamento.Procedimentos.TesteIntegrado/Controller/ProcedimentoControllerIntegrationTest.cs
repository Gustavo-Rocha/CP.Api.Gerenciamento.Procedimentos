using CP.Api.Gerenciamento.Procedimentos.Models;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CP.Api.Gerenciamento.Procedimentos.TesteIntegrado
{
    public class ProcedimentoControllerIntegrationTest
    {
        private WebApplicationFactory<Startup> _factory;
        private HttpClient _procedimento;
        public ApplicationContext _context = new ApplicationContext();

        [SetUp]
        public void Setup()
        {
            _factory = new WebApplicationFactory<Startup>();
            _procedimento = _factory.CreateClient();
        }

        public async Task ExcluirProcedimentosDoBancoAsync()
        {
            List<Procedimento> procs = _context.Procedimentos.ToList();
            
            foreach (Procedimento procList in procs)
            {
                _context.Procedimentos.Remove(procList);
                _context.SaveChanges();
            }
        }

        [Test]
        public async Task DeveConsultarProcedimentoComSucessoAsync()
        {
            //Arrange

            //Action
            HttpResponseMessage get = await _procedimento.GetAsync($"/api/Procedimento");
            string procedimentos = await get.Content.ReadAsStringAsync();
            var conversao = JsonConvert.DeserializeObject<List<Procedimento>>(procedimentos);

            var usuarios =  _context.Procedimentos.ToList();

            //Assert
            conversao.Count.Should().Be(usuarios.Count);
        }

        [Test]
        public async Task DeveCadastrarProcedimentoComSucessoAsync()
        {
            //Arrange
            var procedimento = new Procedimento
            {
                NomeProcedimento = "Depilacao de busto",
                valorProcedimento = "50,00",
                DuracaoProcedimento = DateTime.Now,
                DescricaoProcedimento = "retirada de pelo do busto"
            };

            //Action
            HttpResponseMessage post =  await _procedimento.PostAsync($"/api/Procedimento", new StringContent(JsonConvert.SerializeObject(procedimento), Encoding.UTF8, "Application/Json"));
            string procedimentos = await post.Content.ReadAsStringAsync();
            var conversao = JsonConvert.DeserializeObject<Procedimento>(procedimentos);

            //Assert
            post.Should().Be200Ok();
        }

        [Test]
        public async Task DeveAlterarProcedimentoComSucessoAsync()
        {
            //Arrange
            var procedimento = new ProcedimentoViewModelAlteracao
            {
                NomeProcedimentoAntigo= "raspagem de bigode",
                NomeProcedimento = "Depilacao de Busto",
                valorProcedimento = "50,00",
                DuracaoProcedimento = DateTime.Now,
                DescricaoProcedimento = "Retirada de pelos do Busto"
            };

            var procedimento2 = new Procedimento
            {
                NomeProcedimento = "raspagem de bigode",
                valorProcedimento = "50,00",
                DuracaoProcedimento = DateTime.Now,
                DescricaoProcedimento = "Retirada de pelos do Busto"
            };

            HttpResponseMessage post = await _procedimento.PostAsync($"/api/Procedimento", new StringContent(JsonConvert.SerializeObject(procedimento2), Encoding.UTF8, "Application/Json"));

            //Action
            HttpResponseMessage put = await _procedimento.PutAsync($"/api/Procedimento", new StringContent(JsonConvert.SerializeObject(procedimento), Encoding.UTF8, "Application/Json"));
            string procedimentos = await put.Content.ReadAsStringAsync();
            var conversao = JsonConvert.DeserializeObject<Procedimento>(procedimentos);

            //Assert
            put.Should().Be204NoContent();
        }

        [Test]
        public async Task DeveDEletarProcedimentoComSucessoAsync()
        {
            //Arrange
            var procedimento = new ProcedimentoViewModel
            {   
                NomeProcedimento = "Depilacao de Busto",
                valorProcedimento = "50,00",
                DuracaoProcedimento = DateTime.Now,
                DescricaoProcedimento = "Retirada de pelos do Busto"
            };

            //Action
            HttpResponseMessage delete = await _procedimento.DeleteAsync($"/api/Procedimento/{procedimento.NomeProcedimento}");
            string procedimentos = await delete.Content.ReadAsStringAsync();
            var conversao = JsonConvert.DeserializeObject<Procedimento>(procedimentos);
            //Assert
            delete.Should().Be200Ok();
        }

        [OneTimeTearDown]
        public async Task TearDown()
        {
            await ExcluirProcedimentosDoBancoAsync();
            _procedimento.Dispose();
            _factory.Dispose();
        }
    }
}