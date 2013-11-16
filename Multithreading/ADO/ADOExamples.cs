using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oracle.DataAccess.Client;

namespace Examples.ADO
{
    class ADOExamples
    {
        private const string ConnectionString = "user id=scmsg;password=scmsg;Data Source=ORCL";

        [Example("Connection example", false)]
        public void ConnectExample()
        {
            using (var connection = new OracleConnection(ConnectionString))
            {
                connection.Open();

                var command = new OracleCommand("Select * from \"MCC_User\"");

                command.Connection = connection;
                command.CommandType = System.Data.CommandType.Text;

                using (var reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            Console.WriteLine(reader["Name"]);
                        }
                    }
                    var schema = reader.GetSchemaTable();

                    foreach (DataRow row in schema.Rows)
                        Console.WriteLine(row[0]);
                }
            }
            Console.ReadKey();
        }

        [Example("Insert example", false)]
        public void InsertExample()
        {
            using (var connection = new OracleConnection(ConnectionString))
            {
                connection.Open();

                var id = new Random().Next();

                var insertCommand = new OracleCommand();
                insertCommand.Connection = connection;
                insertCommand.CommandType = CommandType.Text;
                insertCommand.CommandText = "Insert into \"MCC_User\" (\"Id\",\"Name\",\"SabreID\",\"Pcc\") Values (:Id,'Roman', '123123','3213')";
                insertCommand.Parameters.Add(new OracleParameter(":Id", id));

                insertCommand.ExecuteNonQuery();

                var command = new OracleCommand("Select * from \"MCC_User\" where \"Name\" = 'Roman'");

                command.Connection = connection;
                command.CommandType = System.Data.CommandType.Text;

                using (var reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            Console.WriteLine(reader["Id"] + ": " + reader["Name"]);
                        }
                    }
                }

                command.CommandText = "select count(*) from \"MCC_User\"";
                Console.WriteLine(command.ExecuteScalar());

                var transaction = connection.BeginTransaction();
                command.CommandText = "Delete from \"MCC_User\" where \"Name\" = 'Roman'";
                command.ExecuteNonQuery();
                transaction.Rollback();

            }
            Console.ReadKey();
        }

        [Example("Data Adapter Example", false)]
        public void DataAdapterExample()
        {
            using (var connection = new OracleConnection(ConnectionString))
            {
                connection.Open();

                var id = new Random().Next();
         
                
                var command = new OracleCommand("Select * from \"MCC_User\" where \"Name\" = 'Roman'");

                command.Connection = connection;

                var adapter = new OracleDataAdapter(command);

                var dataSet = new DataSet();

                adapter.Fill(dataSet);

                var newRow = dataSet.Tables[0].NewRow();
                newRow["Id"] = new Random().Next();
                newRow["Name"] = "Romix";
                newRow["SabreID"] = "47d839";
                newRow["Pcc"] = "1111";

                //dataSet.Tables[0].Rows.Add(newRow);
                ///adapter.Update(dataSet.Tables[0]);

                //dataSet.Tables[0].Rows[0].Delete();
                    //adapter.Update(dataSet.Tables[0]);
                connection.Close();

                foreach (DataRow row in dataSet.Tables[0].Rows)
                {
                    Console.WriteLine(string.Format("{0}: {1}", row["Id"], row["Name"]));
                }
            }
            Console.ReadKey();
        }
    }
}
