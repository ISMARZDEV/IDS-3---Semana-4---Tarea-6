using System;
using System.Data;
using System.Data.SqlClient;

class Program
{
    static void Main()
    {
        string connectionString = "Server=localhost; Database=BDProject; User Id=sa; Password=20186947Ismael";

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();
            Console.WriteLine("Conexión exitosa");

            string salir = "N";

            while (salir.ToUpper() == "N")
            {
                // Insertar un pasajero
                InsertarPasajero(connection);

                Console.Write("¿Desea salir? (Y/N): ");
                salir = Console.ReadLine().ToUpper();
            }
        }
    }

    static void InsertarPasajero(SqlConnection connection)
    {
        using (SqlCommand insertCommand = new SqlCommand("InsertarPasajeroArca", connection))
        {
            insertCommand.CommandType = CommandType.StoredProcedure;

            // Parámetros del pasajero
            Console.Write("Tipo: ");
            string tipo = Console.ReadLine();
            Console.Write("Nombres: ");
            string nombres = Console.ReadLine();
            Console.Write("Fecha de Nacimiento (YYYY-MM-DD): ");
            DateTime fechaNacimiento = Convert.ToDateTime(Console.ReadLine());
            Console.Write("Sexo: ");
            string sexo = Console.ReadLine();
            Console.Write("Estado: ");
            string estado = Console.ReadLine();
            Console.Write("Nota: ");
            string nota = Console.ReadLine();

            insertCommand.Parameters.AddWithValue("@Tipo", tipo);
            insertCommand.Parameters.AddWithValue("@Nombres", nombres);
            insertCommand.Parameters.AddWithValue("@FechaNacimiento", fechaNacimiento);
            insertCommand.Parameters.AddWithValue("@Sexo", sexo);
            insertCommand.Parameters.AddWithValue("@Estado", estado);
            insertCommand.Parameters.AddWithValue("@Nota", nota);

            insertCommand.ExecuteNonQuery();
            Console.WriteLine("Pasajero registrado exitosamente");
        }
    }
}

