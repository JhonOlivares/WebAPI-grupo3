using System.Collections.Generic;
using System.Threading.Tasks;
using WebAPIGrupo3.DBService;
using WebAPIGrupo3.Models;

namespace WebAPIGrupo3.Services
{
    public class TuristaService : ITuristaService
    {
        readonly FirestoreConn conn = new();
        public TuristaService()
        {

        }

        

        public async Task<IEnumerable<Turista>> GetAllTuristas()
        {
            IList<Turista> lista = new List<Turista>();
            foreach (var document in await conn.GetCollectionAsync("turistas"))
            {
                Turista proc = document.ConvertTo<Turista>();
                lista.Add(proc);
            }
            return lista;
        }

        public async Task<Turista> GetTurista(string id)
        {
            var document = await conn.GetSingleDocumentAsync("turistas", id);
            Turista turista = document?.ConvertTo<Turista>();
            return turista;
        }

        public async Task<string> Create(Turista turista)
        {
            var document = await conn?.UploadDocToFirestore("turistas", turista, null);
            if (document == null)
                return string.Empty;
            else
            {
                return document.Id;
            }
        }
    }
    
    public interface ITuristaService
    {
        Task<IEnumerable<Turista>> GetAllTuristas();
        Task<Turista> GetTurista(string id);
        Task<string> Create(Turista turista);
    }
}
