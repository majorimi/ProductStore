using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Migrations.Model;
using System.Data.Entity.SqlServer;

namespace ProductStore.Domain.Data.Context
{
	public partial class ProductStoreEfContext
	{
		public class CustomSqlServerMigrationSqlGenerator : SqlServerMigrationSqlGenerator
		{
			protected override void Generate(AddColumnOperation addColumnOperation)
			{
				SetCreatedUtcColumn(addColumnOperation.Column);
				base.Generate(addColumnOperation);
			}

			protected override void Generate(CreateTableOperation createTableOperation)
			{
				SetCreatedUtcColumn(createTableOperation.Columns);
				base.Generate(createTableOperation);
			}

			protected override void Generate(AlterColumnOperation alterColumnOperation)
			{
				SetCreatedUtcColumn(alterColumnOperation.Column);
				base.Generate(alterColumnOperation);
			}

			protected override void Generate(AlterTableOperation alterTableOperation)
			{
				SetCreatedUtcColumn(alterTableOperation.Columns);
				base.Generate(alterTableOperation);
			}

			private static void SetCreatedUtcColumn(IEnumerable<ColumnModel> columns)
			{
				foreach (var columnModel in columns)
				{
					SetCreatedUtcColumn(columnModel);
				}
			}

			private static void SetCreatedUtcColumn(PropertyModel column)
			{
				if (column.Type == PrimitiveTypeKind.DateTime &&
				    column.Name.EndsWith("Utc", StringComparison.OrdinalIgnoreCase))
				{
					column.DefaultValueSql = "GETUTCDATE()";
				}
			}
		}
	}
}