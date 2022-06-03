using System.Threading.Tasks;
using ProEventos.Domain;

namespace ProEventos.Persistence.Contratos
{
    public interface ILotePersist
    {
        /// <summary>
        /// Método get que retornará uma lista de lotes por eventoId.
        /// </summary>
         /// <param name="eventoId">código chave tabela evento</param>
        /// <returns> Array de Lotes </returns>

         Task<Lote[]> GetLotesByEventoIdAsync(int eventoId);
         /// <summary>
         /// Métodos get que retornara apenas 1 Lote
         /// </summary>
         /// <param name="eventoId">código chave tabela evento</param>
         /// <param name="id">codigo chave da tabela lote</param>
         /// <returns> Apenas 1 Lote </returns>
         Task<Lote> GetLotesByIdAsync(int eventoId, int id );

    }
}