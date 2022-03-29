using Npgsql;



var cs_slave = "Host=localhost:59571;Username=postgres;Password=my_password;Database=my_database";
var cs_master = "Host=localhost:59570;Username=postgres;Password=my_password;Database=my_database";

using var master_con = new NpgsqlConnection(cs_master);
master_con.Open();

var sql = "SELECT version()";

using var cmd = new NpgsqlCommand(sql, master_con);

var version = cmd.ExecuteScalar().ToString();
Console.WriteLine($"PostgreSQL version: {version}");

CreateAndInsert(cmd);




using var con_slave = new NpgsqlConnection(cs_slave);
con_slave.Open();

string sqlread = "SELECT * FROM cars";
using var cmdread = new NpgsqlCommand(sqlread, con_slave);

using NpgsqlDataReader rdr = cmdread.ExecuteReader();

while (rdr.Read())
{
    Console.WriteLine("{0} {1} {2}", rdr.GetInt32(0), rdr.GetString(1),
        rdr.GetInt32(2));
}



void CreateAndInsert(NpgsqlCommand npgsqlCommand)
{
    npgsqlCommand.CommandText = "DROP TABLE IF EXISTS cars";
    npgsqlCommand.ExecuteNonQuery();

    npgsqlCommand.CommandText = @"CREATE TABLE cars(id SERIAL PRIMARY KEY, 
        name VARCHAR(255), price INT)";
    npgsqlCommand.ExecuteNonQuery();

    npgsqlCommand.CommandText = "INSERT INTO cars(name, price) VALUES('Audi',52642)";
    npgsqlCommand.ExecuteNonQuery();

    npgsqlCommand.CommandText = "INSERT INTO cars(name, price) VALUES('Mercedes',57127)";
    npgsqlCommand.ExecuteNonQuery();

    npgsqlCommand.CommandText = "INSERT INTO cars(name, price) VALUES('Skoda',9000)";
    npgsqlCommand.ExecuteNonQuery();

    npgsqlCommand.CommandText = "INSERT INTO cars(name, price) VALUES('Volvo',29000)";
    npgsqlCommand.ExecuteNonQuery();

    npgsqlCommand.CommandText = "INSERT INTO cars(name, price) VALUES('Bentley',350000)";
    npgsqlCommand.ExecuteNonQuery();

    npgsqlCommand.CommandText = "INSERT INTO cars(name, price) VALUES('Citroen',21000)";
    npgsqlCommand.ExecuteNonQuery();

    npgsqlCommand.CommandText = "INSERT INTO cars(name, price) VALUES('Hummer',41400)";
    npgsqlCommand.ExecuteNonQuery();

    npgsqlCommand.CommandText = "INSERT INTO cars(name, price) VALUES('Volkswagen',21600)";
    npgsqlCommand.ExecuteNonQuery();

    Console.WriteLine("Table cars created");
}