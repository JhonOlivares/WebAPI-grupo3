using System.Collections.Generic;
using System.Threading.Tasks;
using WebAPIGrupo3.DBService;
using WebAPIGrupo3.Models;

namespace WebAPIGrupo3.Services
{
    public class HotelService : IHotelService
    {
        readonly FirestoreConn conn = new();

        public HotelService()
        {

        }
        public async Task<IEnumerable<Hotel>> GetAllHotels()
        {
            IList<Hotel> lista = new List<Hotel>();
            foreach (var document in await conn.GetCollectionAsync("hoteles"))
            {
                Hotel proc = document.ConvertTo<Hotel>();
                lista.Add(proc);
            }
            return lista;
        }

        public async Task<Hotel> GetHotel(string id)
        {
            var document = await conn.GetSingleDocumentAsync("hoteles", id);
            Hotel hotel = document?.ConvertTo<Hotel>();
            return hotel;
        }
    }








    //INTERFACE-------------------------////
    public interface IHotelService
    {
        Task<IEnumerable<Hotel>> GetAllHotels();
        Task<Hotel> GetHotel(string id);
        //Task<Book> Create(Book book);
        //Task Update(Book book);
        //Task Delete(int id);
    }
}
