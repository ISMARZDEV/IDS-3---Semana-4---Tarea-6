using System;
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

            // Registrar un nuevo tripulante (utilizando stored procedure)
            Console.Write("Nombre: ");
            int idTripulante = InsertarNuevoTripulante(connection, Console.ReadLine());

            // Insertar consumo de manera interactiva
            InsertarConsumoInteractivo(connection, idTripulante);

            // Listar los consumos del tripulante (utilizando stored procedure)
            ListarTodosConsumos(connection);
        }
    }

    static void InsertarConsumoInteractivo(SqlConnection connection, int idTripulante)
    {
        Console.Write("Comentario de Consumo: ");
        string comentario = Console.ReadLine();
        Console.Write("Fecha de Ingreso (YYYY-MM-DD HH:mm:ss): ");
        DateTime fechaIngreso = Convert.ToDateTime(Console.ReadLine());
        Console.Write("Estado: ");
        string estado = Console.ReadLine();

        // Utilizar stored procedure para insertar el consumo
        InsertarConsumo(connection, idTripulante, comentario, fechaIngreso, estado);

        Console.WriteLine("Consumo registrado exitosamente");
    }

    static void InsertarConsumo(SqlConnection connection, int idTripulante, string comentario, DateTime fechaIngreso, string estado)
    {
        using (SqlCommand command = new SqlCommand("InsertarConsumo", connection))
        {
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@IdTripulante", idTripulante));
            command.Parameters.Add(new SqlParameter("@Comentario", comentario));
            command.Parameters.Add(new SqlParameter("@FechaIngreso", fechaIngreso));
            command.Parameters.Add(new SqlParameter("@Estado", estado));

            command.ExecuteNonQuery();
        }
    }

    static int InsertarNuevoTripulante(SqlConnection connection, string nombres)
    {
        using (SqlCommand command = new SqlCommand("InsertarTripulante", connection))
        {
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@Nombres", nombres));

            return Convert.ToInt32(command.ExecuteScalar());
        }
    }

    static void ListarTodosConsumos(SqlConnection connection)
    {
        using (SqlCommand command = new SqlCommand("ListarTodosConsumos", connection))
        {
            command.CommandType = System.Data.CommandType.StoredProcedure;

            using (SqlDataReader reader = command.ExecuteReader())
            {
                Console.WriteLine("Nombres\t\tComentario\t\tFecha");

                while (reader.Read())
                {
                    string nombres = reader["Nombres"].ToString();
                    string comentario = reader["Comentario"].ToString();
                    DateTime fechaIngreso = Convert.ToDateTime(reader["FechaIngreso"]);

                    Console.WriteLine($"{nombres}\t\t{comentario}\t\t{fechaIngreso}");
                }
            }
        }
    }


}

