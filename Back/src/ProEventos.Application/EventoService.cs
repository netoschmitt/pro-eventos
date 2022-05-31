using System;
using System.Threading.Tasks;
using AutoMapper;
using ProEventos.Application.Contratos;
using ProEventos.Application.Dtos;
using ProEventos.Domain;
using ProEventos.Persistence.Contratos;

namespace ProEventos.Application
{
    public class EventoService : IEventoService
    {
        private readonly IGeralPersist _geralPersist;
        private readonly IEventoPersist _eventoPersist;
        private readonly IMapper mapper;
        public EventoService(IGeralPersist geralPersist,
                             IEventoPersist eventoPersist,
                             IMapper mapper)
        {
            _geralPersist = geralPersist;
            _eventoPersist = eventoPersist;
            this.mapper = mapper;
        }

    public async Task<EventoDto> AddEventos(EventoDto model)
    {
        try
        {
            var evento = mapper.Map<Evento>(model);

            _geralPersist.Add<Evento>(evento);

            if (await _geralPersist.SaveChangesAsync())
            {
                var eventoRetorno = await _eventoPersist.GetEventoByIdAsync(evento.Id, false);

                return mapper.Map<EventoDto>(eventoRetorno);
            }
            return null;
        }
        catch (Exception ex)
        {

            throw new Exception(ex.Message);
        }
    }


    public async Task<EventoDto> UpdateEvento(int eventoId, EventoDto model)
    {
        return null;

        // try
        // {
        //     var evento = await _eventoPersist.GetEventoByIdAsync(eventoId, false);
        //     if (evento == null) return null;

        //     model.Id = evento.Id;

        //     _geralPersist.Update(model);
        //     if (await _geralPersist.SaveChangesAsync())
        //     {
        //         return await _eventoPersist.GetEventoByIdAsync(model.Id, false);
        //     }
        //     return null;

        // }
        // catch (Exception ex)
        // {

        //     throw new Exception(ex.Message);
        // }
    }

    public async Task<bool> DeleteEvento(int eventoId)
    {
        try
        {
            var evento = await _eventoPersist.GetEventoByIdAsync(eventoId, false);
            if (evento == null) throw new Exception("Evento para delete não foi encontrado.");

            _geralPersist.Delete<Evento>(evento);
            return await _geralPersist.SaveChangesAsync();

        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task<EventoDto[]> GetAllEventosAsync(bool includePalestrantes = false)
    {
        try
        {
            var eventos = await _eventoPersist.GetAllEventosAsync(includePalestrantes);
            if (eventos == null) return null;

            var resultado = mapper.Map<EventoDto[]>(eventos);

            return resultado;
        }
        catch (Exception ex)
        {

            throw new Exception(ex.Message);
        }
    }

    public async Task<EventoDto[]> GetAllEventosByTemaAsync(string tema, bool includePalestrantes = false)
    {
        try
        {
            var eventos = await _eventoPersist.GetAllEventosByTemaAsync(tema, includePalestrantes);
            if (eventos == null) return null;

            var resultado = mapper.Map<EventoDto[]>(eventos);

            return resultado;
        }
        catch (Exception ex)
        {

            throw new Exception(ex.Message);
        }
    }

    public async Task<EventoDto> GetEventoByIdAsync(int eventoId, bool includePalestrantes = false)
    {
        try
        {
            var evento = await _eventoPersist.GetEventoByIdAsync(eventoId, includePalestrantes);
            if (evento == null) return null;
            // eu quero mapear o meu evento dado meu obj EventoDto
            var resultado = mapper.Map<EventoDto>(evento);


            /* // mapeamento feito na mao
            var eventoRetorno = new EventoDto()
            {
                Id = evento.Id,
                Local = evento.Local,
                DataEvento = evento.DataEvento.ToString(),
                Tema = evento.Tema,
                QtdPessoas = evento.QtdPessoas,
                ImagemURL = evento.ImagemURL,
                Telefone = evento.Telefone,
                Email = evento.Email,
            };
 */
            return resultado;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}
}