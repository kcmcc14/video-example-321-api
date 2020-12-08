using System.Security.AccessControl;
using System.Collections.Generic;

namespace API.Models.Interfaces
{
    public interface IGetAllBooks
    {
         public List<Book> GetAllBooks();
    }
}