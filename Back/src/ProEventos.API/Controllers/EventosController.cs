using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProEventos.Application.Contratos;
using ProEventos.Domain;
using ProEventos.API.Dtos;

namespace ProEventos.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventosController : ControllerBase
    {


        private readonly IEventoService eventoService;

        public EventosController(IEventoService eventoService)
        {
            this.eventoService = eventoService;

        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                // em todos getAllEventosAsync(), eu atribui para var eventos
                var eventos = await this.eventoService.GetAllEventosAsync(true);
                // se eventos for null ou nao for encontrado return NotFound...
                if (eventos == null) return NotFound("Nenhum evento encontrado.");
                // criei uma listagem desse EventosDTO
                var eventosRetorno = new List<EventoDto>();

                foreach (var evento in eventos)
                {
                    // criada uma nova listagem Dto baseado nos campos abaixo 
                    eventosRetorno.Add(new EventoDto(){
                        Id = evento.Id,
                        Local = evento.Local,
                        DataEvento = evento.DataEvento.ToString(),
                        Tema = evento.Tema,
                        QtdPessoas = evento.QtdPessoas,
                        ImagemURL = evento.ImagemURL,
                        Telefone = evento.Telefone,
                        Email = evento.Email,
                    });
                }
                // agora eu tenho um dto sendo retornado,assim nao estou exponho exatamente aquilo que o meu doninio tem, para aquele que consumir minha api 
                return Ok(eventosRetorno);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                $"Erro ao tentar recuperar eventos. Erro: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var evento = await this.eventoService.GetEventoByIdAsync(id, true);
                if (evento == null) return NotFound("Nenhum evento por id encontrado.");

                return Ok(evento);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                $"Erro ao tentar recuperar eventos. Erro: {ex.Message}");
            }
        }

        [HttpGet("{tema}/tema")]
        public async Task<IActionResult> GetByTema(string tema)
        {
            try
            {
                var evento = await this.eventoService.GetAllEventosByTemaAsync(tema, true);
                if (evento == null) return NotFound("Nenhum evento por tema encontrado.");

                return Ok(evento);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                $"Erro ao tentar recuperar eventos. Erro: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post(Evento model)
        {
           try
            {
                var evento = await this.eventoService.AddEventos(model);
                if (evento == null) return BadRequest("Erro ao tentar adicionar evento.");

                return Ok(evento);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                $"Erro ao tentar adicionar eventos. Erro: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Evento model)
        {
            try
            {
                var evento = await this.eventoService.UpdateEvento(id, model);
                if (evento == null) return BadRequest("Erro ao tentar adicionar evento.");

                return Ok(evento);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                $"Erro ao tentar atualizar eventos. Erro: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {

                return await this.eventoService.DeleteEvento(id) ?
                     Ok("Evento deletado com sucesso.") : 
                     BadRequest("Evento não deletado.");
                
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                $"Erro ao tentar deletar eventos. Erro: {ex.Message}");
            }
            
        }
    }
}
