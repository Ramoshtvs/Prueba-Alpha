
using Microsoft.Data.SqlClient;
using Prueba_Alpha.Class;
using System.Data;

namespace Prueba_Alpha.ClasesDAL

{
    public class CrudAlpha
    {
        IConfigurationRoot configuration = new ConfigurationBuilder().SetBasePath(Environment.CurrentDirectory).AddJsonFile("appsettings.json").Build();


        public List<Productos> ObtenerDatos()
        {   
            List<Productos> productosall = new List<Productos>();

            using (SqlConnection conn = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
            {
                try
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("GetDataProducts", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    using (var rdr = cmd.ExecuteReader()) {

                        while (rdr.Read()) {
                            productosall.Add(
                                new Productos {
                                    Id_Producto = Convert.ToInt32(rdr[0]),
                                    Name = rdr[1].ToString(),
                                    Categoria = rdr[2].ToString()
                                });
                        }
                    }
                }
                catch (Exception)
                {
                    conn.Close();
                    conn.Dispose();
                    throw;
                }
            }

            return productosall;


        }


        public List<Categorias> ObtenerDatosCategorias()
        {
            List<Categorias> categoriasall = new List<Categorias>();

            using (SqlConnection conn = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
            {
                try
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("GetCategorias", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    using (var rdr = cmd.ExecuteReader())
                    {

                        while (rdr.Read())
                        {
                            categoriasall.Add(
                                new Categorias
                                {
                                    Id_categoria = Convert.ToInt32(rdr[0]),
                                    Name = rdr[1].ToString()
                                });
                        }
                    }
                }
                catch (Exception)
                {
                    conn.Close();
                    conn.Dispose();
                    throw;
                }
            }

            return categoriasall;


        }





        public int AgregarProducto(string nombrep, string nombrec)
        {
            int idAgregado = 0;

            using (SqlConnection conn = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
            {
                try
                {
                    
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("AgregarProducto", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@nombrep", SqlDbType.VarChar) { Value = Convert.ToString(nombrep) });
                    cmd.Parameters.Add(new SqlParameter("@nombrec", SqlDbType.VarChar) { Value = Convert.ToString(nombrec) });


                    object rdr = cmd.ExecuteScalar();

                    if (rdr != null && rdr != DBNull.Value)
                    {
                        idAgregado = Convert.ToInt32(rdr);
                    }
                    else {
                        
                    }
                    
                }
                catch (Exception)
                {
                    conn.Close();
                    conn.Dispose();
                    throw;
                }
            }

            return idAgregado;


        }



        public int EditarProducto(int id, string nombrep, string nombrec)
        {
            int editado = 0;

            using (SqlConnection conn = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
            {
                try
                {

                    conn.Open();

                    SqlCommand cmd = new SqlCommand("EditarProducto", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@idProd", SqlDbType.Int) { Value = Convert.ToString(id) });
                    cmd.Parameters.Add(new SqlParameter("@nombrep", SqlDbType.VarChar) { Value = Convert.ToString(nombrep) });
                    cmd.Parameters.Add(new SqlParameter("@nombrec", SqlDbType.VarChar) { Value = Convert.ToString(nombrec) });


                    using (SqlDataReader rdr = cmd.ExecuteReader()) {

                        if (rdr.Read())
                        {
                            string result = rdr["Resultado"].ToString();

                            if (result == "OK")
                            {
                                editado = 1;
                            }
                        }
                    }

                }
                catch (Exception)
                {
                    conn.Close();
                    conn.Dispose();
                    throw;
                }
            }

            return editado;


        }




        public int EliminarProducto(int id)
        {
            int editado = 0;

            using (SqlConnection conn = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
            {
                try
                {

                    conn.Open();

                    SqlCommand cmd = new SqlCommand("EliminarProducto", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@idProd", SqlDbType.Int) { Value = Convert.ToInt32(id) });
                  
                    using (SqlDataReader rdr = cmd.ExecuteReader())
                    {

                        if (rdr.Read())
                        {
                            string result = rdr["Resultado"].ToString();

                            if (result == "OK")
                            {
                                editado = 1;
                            }
                        }
                    }

                }
                catch (Exception)
                {
                    conn.Close();
                    conn.Dispose();
                    throw;
                }
            }

            return editado;


        }
    }
}
