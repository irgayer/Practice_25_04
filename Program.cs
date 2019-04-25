using DbUp;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Practice_25_04
{
    class Program
    {
        private static string connectionString =
           ConfigurationManager.ConnectionStrings["appConnection"].ConnectionString;

        static void Main(string[] args)
        {
            Initial();
            NewsApp app = new NewsApp(connectionString);
            app.Run();

        }

        private static void Initial()
        {
            EnsureDatabase.For.SqlDatabase(connectionString);

            var upgrader =
            DeployChanges.To
            .SqlDatabase(connectionString)
            .WithScriptsEmbeddedInAssembly(Assembly.GetExecutingAssembly())
            .LogToConsole()
            .Build();

            var result = upgrader.PerformUpgrade();

            if (!result.Successful)
            {
                throw new Exception("Ошибка соеденения");
            }
            else
            {
                
            }
        }
    }
}
