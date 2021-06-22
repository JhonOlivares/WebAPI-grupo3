using System.Collections.Generic;
using System.Threading.Tasks;
using WebAPIGrupo3.DBService;
using WebAPIGrupo3.Models;

namespace WebAPIGrupo3.Services
{
    public class EstanciaService
    {
        readonly FirestoreConn conn = new();
    }

    public interface IEstanciaService
    {
    }
}
