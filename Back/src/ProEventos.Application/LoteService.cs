using System;
using System.Threading.Tasks;
using AutoMapper;
using ProEventos.Application.Contratos;
using ProEventos.Application.Dtos;
using ProEventos.Domain;
using ProEventos.Persistence.Contratos;

namespace ProEventos.Application
{
    public class LoteService : ILoteService
    {
        private readonly IGeralPersist _geralPersist;
        private readonly ILotePersist _lotePersist;
        private readonly IMapper _mapper;
        public LoteService(IGeralPersist geralPersist,
                            ILotePersist lotePersist,
                            IMapper mapper)
        {
            _geralPersist = geralPersist;
            _lotePersist = lotePersist;
            _mapper = mapper;
        }

    public async Task<LoteDto> AddEventos(LoteDto model)
    {
        try
        {
            var evento = _mapper.Map<Evento>(model);

            _geralPersist.Add<Evento>(evento);

            if (await _geralPersist.SaveChangesAsync())
            {
                var eventoRetorno = await _lotePersist.GetEventoByIdAsync(evento.Id, false);

                return _mapper.Map<LoteDto>(eventoRetorno);
            }
            return null;
        }
        catch (Exception ex)
        {

            throw new Exception(ex.Message);
        }
    }


    public async Task<LoteDto> UpdateEvento(int eventoId, LoteDto model)
    {
        
        try
        {
            var evento = await _lotePersist.GetEventoByIdAsync(eventoId, false);
            if (evento == null) return null;

            model.Id = evento.Id;

            // mapeando do model com destino para o evento (obj para obj)
            _mapper.Map(model, evento);

            _geralPersist.Update<Evento>(evento);

            if (await _geralPersist.SaveChangesAsync())
            {
                var eventoRetorno =  await _lotePersist.GetEventoByIdAsync(evento.Id, false);

                return _mapper.Map<LoteDto>(eventoRetorno);
            }
            return null;

        }
        catch (Exception ex)
        {

            throw new Exception(ex.Message);
        }
    }

    public async Task<bool> DeleteLote(int eventoId, int loteId)
    {
        try
        {
            var lote = await _lotePersist.GetLoteByIdsAsync(eventoId, loteId);
            if (lote == null) throw new Exception("Lote para delete não foi encontrado.");

            _geralPersist.Delete<Lote>(lote);
            return await _geralPersist.SaveChangesAsync();

        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }


    public async Task<LoteDto[]> GetLotesByEventoIdAsync(int eventoId)
    {
        try
        {
            var lotes = await _lotePersist.GetLotesByEventoIdAsync(eventoId);
            if (lotes == null) return null;

            var resultado = _mapper.Map<LoteDto[]>(lotes);

            return resultado;
        }
        catch (Exception ex)
        {

            throw new Exception(ex.Message);
        }
    }

    public async Task<LoteDto> GetLoteByIdsAsync(int eventoId, int loteId)
    {
        try
        {
            var lote = await _lotePersist.GetLoteByIdsAsync(eventoId, loteId);
            if (lote == null) return null;

            var resultado = _mapper.Map<LoteDto>(lote);

            return resultado;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}
}