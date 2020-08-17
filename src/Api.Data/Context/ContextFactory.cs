using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Api.Data.Context
{
    public class ContextFactory : IDesignTimeDbContextFactory<MyContext>
    {
        public MyContext CreateDbContext(string[] args)
        {
            // var connectionString= "Server=localhost;Port=3306;DataBase=estudo_dotnet_core;Uid=root;Pwd=Fr@t3rn1d4d3";
            var connectionString= Environment.GetEnvironmentVariable("DB_CONNECTION");
            var optionsBuilder = new DbContextOptionsBuilder<MyContext>();
            // optionsBuilder.UseMySql(connectionString);
            optionsBuilder.UseSqlServer(connectionString);
            return new MyContext(optionsBuilder.Options);
        }
    }
}