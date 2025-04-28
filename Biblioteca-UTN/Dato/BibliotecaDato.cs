using Biblioteca_UTN.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;

namespace Biblioteca_UTN.Dato
{
    public class BibliotecaDato 
    {
        private static string conexionString = "Data Source=DESKTOP-9EFK2TV;Initial Catalog=BibliotecaUTN-BD;Integrated Security=True;Trust Server Certificate=True";


        public List<Autor> ListarAutores(int id)
        {
            List<Autor> lista = new List<Autor>();
            try
            {
                using (SqlConnection con = new SqlConnection(conexionString))
                {
                    string query = "SELECT * FROM Autores";
                    if (id > 0)
                    {
                        query += $" WHERE Id = @Id";
                    }

                    con.Open();
                    SqlCommand cmd = new SqlCommand(query, con);
                    if (id > 0)
                    {
                       cmd.Parameters.AddWithValue("Id", id);
                    }

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        lista.Add(new Autor()
                        {
                            Id = (int)reader["Id"],
                            Nombre = (string)reader["Nombre"],
                            Descripcion = (string)reader["Descripcion"],
                            FechaNacimiento = (DateTime)reader["FechaNacimiento"],
                            Nacionalidad = (string)reader["Nacionalidad"]
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return lista;
            
           
        }

        public string CrearAutor(Autor autor)
        {

            string query = $"INSERT INTO Autores (Nombre, Descripcion, FechaNacimiento, Nacionalidad) VALUES (@Nombre, @Descripcion, @FechaNacimiento, @Nacionalidad)";
            try
            {
                using (SqlConnection con = new SqlConnection(conexionString))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@Nombre", autor.Nombre);
                        cmd.Parameters.AddWithValue("@Descripcion", autor.Descripcion);
                        cmd.Parameters.AddWithValue("@FechaNacimiento", autor.FechaNacimiento);
                        cmd.Parameters.AddWithValue("@Nacionalidad", autor.Nacionalidad);
                        int filasAfectadas = cmd.ExecuteNonQuery();
                        if (filasAfectadas > 0)
                        {
                            return "Guardado correctamente.";
                        }
                        else
                        {
                            return "No se insertó ningún registro.";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return $"Error: {ex.Message}";
            }
        }

        public string EditarAutor(Autor autor)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(conexionString))
                {
                    string query = $"UPDATE Autores SET Nombre = @Nombre, Nacionalidad = @Nacionalidad WHERE Id = @Id";
                    con.Open();
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@Id", autor.Id);
                    cmd.Parameters.AddWithValue("@Nombre", autor.Nombre);
                    cmd.Parameters.AddWithValue("@Nacionalidad", autor.Nacionalidad);
                    cmd.ExecuteNonQuery();
                    return "Guardado correctamente.";
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public string EliminarAutor(int id)
        {
            string query = $"DELETE FROM Autores WHERE Id = @Id";
            try
            {
                using (SqlConnection con = new SqlConnection(conexionString))
                {                 
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@Id", id);
                        int filasAfectadas = cmd.ExecuteNonQuery();
                        if (filasAfectadas > 0)
                        {
                            return "Autor eliminada correctamente";
                        }
                        else
                        {
                            return "No se encontró el autor que desea eliminar";
                        }
                    } 
                }
            }
            catch (Exception ex)
            {
                return $"Error al eliminar: {ex.Message}";
            }
        }

        public List<Editorial> ListarEditorial(int id)
        {
            List<Editorial> lista = new List<Editorial>();
            try
            {
                using (SqlConnection con = new SqlConnection(conexionString))
                {
                    string query = "SELECT * FROM Editoriales";
                    if (id > 0)
                    {
                        query += $" WHERE Id = @Id";
                    }

                    con.Open();
                    SqlCommand cmd = new SqlCommand(query, con);

                    if (id > 0)
                    {
                        cmd.Parameters.AddWithValue ("Id", id);
                    }

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        lista.Add(new Editorial()
                        {
                            Id = (int)reader["Id"],
                            Nombre = (string)reader["Nombre"]
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            
            return lista;
        }

        public string CrearEditorial(Editorial editorial)
        {
            string query = $"INSERT INTO Editoriales (Nombre) VALUES (@Nombre)";
            try
            {
                using (SqlConnection con = new SqlConnection(conexionString))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@Nombre", editorial.Nombre);
                        cmd.ExecuteNonQuery();
                    }
                    return "Guardado correctamente.";
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public string EditarEditorial(Editorial editorial)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(conexionString))
                {
                    string query = $"UPDATE Editoriales SET Nombre = @Nombre WHERE Id = @Id";
                    con.Open();
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@Id", editorial.Id);
                    cmd.Parameters.AddWithValue("@Nombre", editorial.Nombre);
                    cmd.ExecuteNonQuery();
                    return "Guardado correctamente.";
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public string EliminarEditorial(int id)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(conexionString))
                {
                    string query = $"DELETE FROM Editoriales WHERE Id = @Id";
                    con.Open();
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@Id", id);
                    int filasAfectadas = cmd.ExecuteNonQuery();
                    if (filasAfectadas > 0)
                    {
                        return "Editorial eliminada correctamente";
                    }
                    else
                    {
                        return "No se encontró la editorial que desea eliminar";
                    }
                }
            }
            catch (Exception ex)
            {
                return $"Error al eliminar: {ex.Message}";
            }
        }

        public List<Libro> ListarLibros(int id)
        {
            List<Libro> lista = new List<Libro>();
            try
            {
                using (SqlConnection con = new SqlConnection(conexionString))
                {
                    string query = " SELECT * FROM Libros";
                    if (id > 0)
                    {
                        query += $" WHERE Id = @Id";
                    }

                    con.Open();
                    SqlCommand cmd = new SqlCommand(query, con);

                    if (id > 0)
                    {
                        cmd.Parameters.AddWithValue("Id", id);
                    }

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        lista.Add(new Libro()
                        {
                            Id = (int)reader["Id"],
                            Nombre = (string)reader["Nombre"],
                            IdAutor = (int)reader["IdAutor"],
                            IdEditorial = (int)reader["IdEditorial"],
                            IdGenero = (int)reader["IdGenero"],
                            IdUbicacion = (int)reader["IdUbicacion"],
                            CantCopias = (int)reader["CantCopias"]
                        });
                    }
                }
              
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return lista;
        }

        public string CrearLibro(Libro libro)
        {
            string query = $" INSERT INTO Libros (Nombre, IdAutor, IdEditorial, IdGenero, IdUbicacion, CantCopias) VALUES(@Nombre, @IdAutor, @IdEditorial, @IdGenero, @IdUbicacion, @CantCopias)";
            try
            {
                using (SqlConnection con = new SqlConnection(conexionString))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue ("Nombre", libro.Nombre);
                        cmd.Parameters.AddWithValue ("IdAutor", libro.IdAutor);
                        cmd.Parameters.AddWithValue ("IdEditorial", libro.IdEditorial);
                        cmd.Parameters.AddWithValue ("IdGenero", libro.IdGenero);
                        cmd.Parameters.AddWithValue ("IdUbicacion", libro.IdUbicacion);
                        cmd.Parameters.AddWithValue ("CantCopias", libro.CantCopias);
                        cmd.ExecuteNonQuery ();
                    }
                    return "Guardado correctamente.";
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public string EditarLibro(Libro libro)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(conexionString))
                {
                    string query = $" UPDATE Libros SET Nombre = @Nombre, IdAutor = @IdAutor, IdEditorial = @IdEditorial" +
                        $", IdGenero = @IdGenero, IdUbicacion = @IdUbicacion, CantCopias = @CantCopias WHERE Id = @Id";
                    con.Open();
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@Id", libro.Id);
                    cmd.Parameters.AddWithValue("@Nombre", libro.Nombre);
                    cmd.Parameters.AddWithValue("@IdAutor", libro.IdAutor);
                    cmd.Parameters.AddWithValue("@IdEditorial", libro.IdEditorial);
                    cmd.Parameters.AddWithValue("@IdGenero", libro.IdGenero);
                    cmd.Parameters.AddWithValue("@IdUbicacion", libro.IdUbicacion);
                    cmd.Parameters.AddWithValue("@CantCopias", libro.CantCopias);
                    cmd.ExecuteNonQuery();
                    return "Guardado correctamente.";
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        public string EliminarLibro(int id)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(conexionString))
                {
                    string query = $" DELETE FROM Libros WHERE Id = @Id";
                    con.Open();
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@Id", id);
                    int filasAfectadas = cmd.ExecuteNonQuery();
                    if (filasAfectadas > 0)
                    {
                        return "Libro eliminada correctamente";
                    }
                    else
                    {
                        return "No se encontró el libro que desea eliminar";
                    }
                }
            }
            catch (Exception ex)
            {
                return $"Error al eliminar: {ex.Message}";
            }

        }
        public List<Genero> ListarGenero(int id)
        {
            List<Genero> lista = new List<Genero>();
            try
            {
                using (SqlConnection con = new SqlConnection(conexionString))
                {
                    string query = " SELECT * FROM Generos";
                    if (id > 0)
                    {
                        query += $" WHERE Id = @Id";
                    }

                    con.Open();
                    SqlCommand cmd = new SqlCommand(query, con);

                    if (id > 0)
                    {
                        cmd.Parameters.AddWithValue("Id", id);
                    }

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        lista.Add(new Genero()
                        {
                            Id = (int)reader["Id"],
                            Nombre = (string)reader["Nombre"]
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }           
            return lista;
        }
        public string CrearGenero(Genero genero)
        {
            string query = $"INSERT INTO Generos (Nombre) VALUES (@Nombre)";
            try
            {
                using (SqlConnection con = new SqlConnection(conexionString))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@Nombre", genero.Nombre);
                        cmd.ExecuteNonQuery();
                    }
                    return "Guardado correctamente.";
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        public string EditarGenero(Genero genero)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(conexionString))
                {
                    string query = $"UPDATE Generos SET Nombre = @Nombre WHERE Id = @Id";
                    con.Open();
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@Id", genero.Id);
                    cmd.Parameters.AddWithValue("@Nombre", genero.Nombre);
                    cmd.ExecuteNonQuery();
                    return "Guardado correctamente.";
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        public string EliminarGenero(int id)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(conexionString))
                {
                    string query = $"DELETE FROM Generos WHERE Id = @Id";
                    con.Open();
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@Id", id);
                    int filasAfectadas = cmd.ExecuteNonQuery();
                    if (filasAfectadas > 0)
                    {
                        return "Género eliminada correctamente";
                    }
                    else
                    {
                        return "No se encontró el género que desea eliminar";
                    }
                }
            }
            catch (Exception ex)
            {
                return $"Error al eliminar: {ex.Message}";
            }
            
        }

        public List<Ubicacion> ListarUbicacion(int id)
        {
            List<Ubicacion> lista = new List<Ubicacion>();
            try
            {
                using (SqlConnection con = new SqlConnection(conexionString))
                {
                    string query = "SELECT * FROM Ubicaciones";
                    if (id > 0)
                    {
                        query += $" WHERE Id = @Id";
                    }

                    con.Open();
                    SqlCommand cmd = new SqlCommand(query, con);

                    if (id > 0)
                    {
                        cmd.Parameters.AddWithValue("Id", id);
                    }

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        lista.Add(new Ubicacion()
                        {
                            Id = (int)reader["Id"],
                            Nombre = (string)reader["Nombre"]
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            
            return lista;
        }
        public string CrearUbicacion(Ubicacion ubicacion)
        {
            string query = $"INSERT INTO Ubicaciones (Nombre) VALUES (@Nombre)";
            try
            {
                using (SqlConnection con = new SqlConnection(conexionString))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue ("@Nombre", ubicacion.Nombre);
                        cmd.ExecuteNonQuery();
                    }
                    return "Guardado correctamente.";
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        public string EditarUbicacion(Ubicacion ubicacion)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(conexionString))
                {
                    string query = $"UPDATE Ubicaciones SET Nombre = @Nombre WHERE Id = @Id";
                    con.Open();
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@Id", ubicacion.Id);
                    cmd.Parameters.AddWithValue("@Nombre", ubicacion.Nombre);
                    cmd.ExecuteNonQuery();
                    return "Guardado correctamente.";
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        public string EliminarUbicacion(int id)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(conexionString))
                {
                    string query = $"DELETE FROM Ubicaciones WHERE Id = @Id";
                    con.Open();
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@Id", id);
                    int filasAfectadas = cmd.ExecuteNonQuery();
                    if (filasAfectadas > 0)
                    {
                        return "Ubicación eliminada correctamente";
                    }
                    else
                    {
                        return "No se encontró la ubicación que desea eliminar";
                    }
                }
            }
            catch (Exception ex)
            {
                return $"Error al eliminar: {ex.Message}";
            }
            
        }
    }
}
