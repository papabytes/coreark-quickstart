using System;
using CoreArk.Packages.Core;
using Entity.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Migrations.MySql
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }
    }
    public class TemporaryDbContextFactory : IDesignTimeDbContextFactory<DataContext>
    {
        public DataContext CreateDbContext(string[] args)
        {
            const string connectionString =
                "Server=<mysql-server>;Database=<mysql-database>;Uid=<mysql-user>;Pwd=<mysql-password>;Connection Timeout=60;default command timeout=60;";

            var builder = new DbContextOptionsBuilder<DataContext>(new DbContextOptions<DataContext>());

            builder.UseMySql(connectionString,
                b => b.MigrationsAssembly("Migrations.MySql"));

            return new DataContext(builder.Options, new ConfigurationAssemblyResolver());
        }
    }

}