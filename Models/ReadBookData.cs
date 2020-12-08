using System.Threading;
using System.Xml.Linq;
using System.Security.Cryptography;
using System.Reflection.Metadata;
using System.Runtime.InteropServices;
using System.Security.AccessControl;
using System.Collections.Generic;
using API.Models.Interfaces;
using System.Data.SQLite;

namespace API.Models
{
    public class ReadBookData : IGetAllBooks, IGetBook
    {
        public List<Book> GetAllBooks()
        {
            List<Book> allBooks = new List<Book>();

            string cs = @"URI=file:C:\Users\kcmcc\OneDrive\Documents\MIS 321\repos\Database\book.db";
            using var con = new SQLiteConnection(cs);
            con.Open();

            string stm = "SELECT * FROM books";
            using var cmd = new SQLiteCommand(stm, con);

            using SQLiteDataReader rdr = cmd.ExecuteReader();

            while (rdr.Read())
            {
                Book temp = new Book(){Id=rdr.GetInt32(0), Title=rdr.GetString(1), Author=rdr.GetString(2)};
                allBooks.Add(temp);
            }

            return allBooks;
        }

        public Book GetBook(int id)
        {
            string cs = @"URI=file:C:\Users\kcmcc\OneDrive\Documents\MIS 321\repos\Database\book.db";
            using var con = new SQLiteConnection(cs);
            con.Open();

            string stm = "SELECT * FROM books WHERE id = @id";
            using var cmd = new SQLiteCommand(stm, con);
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Prepare();
            using SQLiteDataReader rdr = cmd.ExecuteReader();

            rdr.Read();
            return new Book(){Id = rdr.GetInt32(0), Title = rdr.GetString(1), Author = rdr.GetString(2)};
        }
    }
}