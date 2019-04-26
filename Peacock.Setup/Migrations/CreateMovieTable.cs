using FluentMigrator;
using System;
using System.Collections.Generic;
using System.Text;

namespace Peacock.Setup.Migrations
{
    [Migration(1)]
    public class CreateMovieTable : Migration
    {
        private const string TABLE_NAME = "Movies";

        public override void Up()
        {
            Create.Table(TABLE_NAME)
                .WithIdColumn()
                .WithColumn("title").AsString()
                .WithColumn("releaseYear").AsInt32();
        }

        public override void Down()
        {
            Delete.Table(TABLE_NAME);
        }
    }
}
