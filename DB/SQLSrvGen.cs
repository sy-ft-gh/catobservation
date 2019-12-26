using System.Collections.Generic;
using System.Data.Entity.Migrations.Model;
using System.Data.Entity.SqlServer;
using System.Data.Entity.Infrastructure.Annotations;

namespace cat.DB {
    internal class DefaultValueSqlServerMigrationSqlGenerator : SqlServerMigrationSqlGenerator {
        protected override void Generate(AddColumnOperation addColumnOperation) {
            SetAnnotatedColumn(addColumnOperation.Column);
            base.Generate(addColumnOperation);
        }

        protected override void Generate(AlterColumnOperation alterColumnOperation) {
            SetAnnotatedColumn(alterColumnOperation.Column);
            base.Generate(alterColumnOperation);
        }

        protected override void Generate(CreateTableOperation createTableOperation) {
            SetAnnotatedColumns(createTableOperation.Columns);
            base.Generate(createTableOperation);
        }

        protected override void Generate(AlterTableOperation alterTableOperation) {
            SetAnnotatedColumns(alterTableOperation.Columns);
            base.Generate(alterTableOperation);
        }

        private void SetAnnotatedColumn(ColumnModel col) {
            AnnotationValues values;
            if (col.Annotations.TryGetValue("DefaultValue", out values)) {
                if (values.NewValue != null) {
                    col.DefaultValueSql = (string)values.NewValue;
                } else {
                    col.DefaultValueSql = null;
                }
            }
        }

        private void SetAnnotatedColumns(IEnumerable<ColumnModel> columns) {
            foreach (var column in columns) {
                SetAnnotatedColumn(column);
            }
        }
    }
}
