using System;
using System.Collections.Generic;
using System.Data.Common;
using System.IO;

namespace vyger.Database
{
    class Exporter
    {
        public void Export()
        {
            using (DbConnection conn = Program.GetOpenConnection())
            {
                IList<string> tables = GetTables(conn);

                foreach (string table in tables)
                {
                    ExportTable(conn, table);
                }
            }
        }

        private IList<string> GetTables(DbConnection conn)
        {
            List<string> tables = new List<string>();

            using (DbCommand cmd = conn.CreateCommand())
            {
                cmd.CommandText = "select name from sys.tables where name != 'AugmentRegistry' order by name";

                using (DbDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        tables.Add(reader[0] as string);
                    }
                }
            }

            return tables;
        }

        private void ExportTable(DbConnection conn, string table)
        {
            string file = Program.GetProjectPath($@"Export\{table}.dat");

            using (StreamWriter sw = new StreamWriter(file))
            {
                using (DbCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = $"select * from {table}";

                    using (DbDataReader reader = cmd.ExecuteReader())
                    {
                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            if (i > 0)
                            {
                                sw.Write("\t");
                            }
                            sw.Write(reader.GetName(i));
                        }

                        sw.WriteLine();

                        while (reader.Read())
                        {
                            for (int i = 0; i < reader.FieldCount; i++)
                            {
                                if (i > 0)
                                {
                                    sw.Write("\t");
                                }

                                string value = reader[i].ToString();

                                if (reader[i] is DBNull)
                                {
                                    value = "NULL";
                                }

                                sw.Write(value);
                            }

                            sw.WriteLine();
                        }
                    }
                }
            }
        }
    }
}
