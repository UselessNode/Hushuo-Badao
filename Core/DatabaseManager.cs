using System;
using AppDB;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Core.Objects;
using System.Runtime.Remoting.Contexts;
using AppDB.View;

namespace AppDB.Core
{
    internal class DatabaseManager : DbContext
    {
        public SupplementEntities Context { get; set; }

        public DbSet<FORWARDERS>    Forwarders  { get; set; }
        public DbSet<INVOICES>      Invoices    { get; set; }
        public DbSet<PRODUCTS>      Products    { get; set; }
        public DbSet<PURVEYORS>     Purveyors   { get; set; }
        public DbSet<SUPPLY_TYPES>  SupplyTypes { get; set; }

    }
}
