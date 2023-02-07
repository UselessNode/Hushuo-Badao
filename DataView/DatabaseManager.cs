using AppDB.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppDB.DataView
{

    class DatabaseManager
    {
        public DatabaseEntities Entities { get; set; }
        public List<Invoice> Invoices { get; set; }

        public DatabaseManager()
        {
            Entities = new DatabaseEntities();
        }

        List<Invoice> ReadData()
        {
            return Invoices;
            //List<Invoice> list = new List<Invoice>();
            //list = Entities.Invoice.ToList();
            //return list;
        }

    }
}
