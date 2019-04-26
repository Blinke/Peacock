using FluentMigrator.Runner;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Peacock.Database;
using Peacock.Database.Repositories;
using Peacock.Setup.Migrations;
using System;
using System.Collections.Generic;

namespace Peacock.Setup
{
    class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                Migration(runner => runner.MigrateUp());

                PopulateDb();
                Console.WriteLine("Setup done");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private static void PopulateDb()
        {
            var servicesProvider = new ServiceCollection()
                .AddTransient<IMovieRepository, MovieRepository>()
                .AddDbContext<MovieContext>(options => options.UseSqlServer(Settings.Default.ConnectionString))
                .BuildServiceProvider();

            var movies = new List<Movie>
            {
                new Movie
                {
                    Title = "Alien",
                    ReleaseYear = 1979
                },
                new Movie
                {
                    Title = "The Thing",
                    ReleaseYear = 1982
                },
                new Movie
                {
                    Title = "The Matrix",
                    ReleaseYear = 1999
                },
                new Movie
                {
                    Title = "Interstellar",
                    ReleaseYear = 2014
                }
            };

            var movieRepository = servicesProvider.GetService<IMovieRepository>();

            movieRepository.AddMovies(movies).GetAwaiter().GetResult();
        }

        private static void Migration(Action<IMigrationRunner> migrateAction)
        {
            var migrationServiceProvider = new ServiceCollection()
                .AddFluentMigratorCore()
                .ConfigureRunner(rb => rb
                    .AddSqlServer()
                    .WithGlobalConnectionString(Settings.Default.ConnectionString)
                    .ScanIn(typeof(CreateMovieTable).Assembly).For.Migrations())
                .AddLogging(lb => lb.AddFluentMigratorConsole())
                .BuildServiceProvider();

            using (var migrationScope = migrationServiceProvider.CreateScope())
            {
                var runner = migrationScope.ServiceProvider.GetRequiredService<IMigrationRunner>();

                migrateAction(runner);
            }
        }
    }
}
