using FluentMigrator.Builders.Create.Table;
using System;
using System.Collections.Generic;
using System.Text;

namespace Peacock.Setup.Migrations
{
    public static class MigrationExtensions
    {
        public static ICreateTableColumnOptionOrWithColumnSyntax WithIdColumn(this ICreateTableWithColumnSyntax tableWithColumnSyntax)
        {
            return tableWithColumnSyntax
                .WithColumn("id")
                .AsInt32()
                .NotNullable()
                .PrimaryKey()
                .Identity();
        }
    }
}
