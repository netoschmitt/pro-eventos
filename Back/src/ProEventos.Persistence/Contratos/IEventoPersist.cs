using System.Threading.Tasks;
using ProEventos.Domain;

namespace ProEventos.Persistence.Contratos
{
    public interface IEventoPersist
    {

         Task<Evento[]> GetAllEventosByTemaAsync(string tema, bool includePalestrantes);
         Task<Evento[]> GetAllEventosAsync(string tema,  bool includePalestrantes);
         Task<Evento> GetEventoByIdAsync(int EventoId,  bool includePalestrantes);

        
    }
}