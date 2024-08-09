using System.Data;
using Veterinaria.Data;
using Veterinaria.Models;
using Veterinaria.Services.Interfaces;

namespace Veterinaria.Services.Implementations
{
    public class AnimalTypeService<T> : DBConnection, ICrud<AnimalTypeModel>
    {
        public AnimalTypeModel Add(AnimalTypeModel model)
        {
            Open();
            if(Conn == null || Conn.State != ConnectionState.Open)
                throw new Exception("La conexión a la base de datos no se pudo establecer o está cerrada.");

            try
            {
                using (var cmd = Conn.CreateCommand())
                {
                    cmd.CommandText = "InsertAnimalType";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("tipNombre", model.TipNombre);
                    cmd.Parameters.AddWithValue("tipDescripcion", model.TipDescripcion);
                    cmd.ExecuteNonQuery();
                }
            }
            catch(Exception ex)
            {
                throw new Exception($"Error al guardar los datos. {ex.Message}");
            }
            finally
            {
                Close();
            }
            return model;
        }
        public AnimalTypeModel Delete(int idAnimalType)
        {
            var AnimalType = new AnimalTypeModel();
            Open();
            if (Conn == null)
                throw new Exception("La conexión a la base de datos no se pudo establecer.");

            try
            {
                using (var cmd = Conn.CreateCommand())
                {
                    cmd.CommandText = "DeleteAnimalType";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("tipId", idAnimalType);
                    cmd.ExecuteNonQuery();
                }
            }
            catch(Exception ex)
            {
                throw new Exception($"No se pudo eliminar el tipo de animal en la base de datos {ex}");
            }
            finally
            {
                Close();
            }
            AnimalType.TipId = idAnimalType;
            return AnimalType;
        }
        public List<AnimalTypeModel> GetAll()
        {
            var animalTypesList = new List<AnimalTypeModel>();
            Open();
            if(Conn == null)
                throw new Exception("La conexión a la base de datos no se pudo establecer.");

            try
            {
                using (var cmd = Conn.CreateCommand())
                {
                    cmd.CommandText = "GetAllAnimalType";
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (var dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            animalTypesList.Add(new AnimalTypeModel()
                            {
                                TipId = Convert.ToInt32(dr["tipId"]),
                                TipNombre = dr["tipNombre"].ToString(),
                                TipDescripcion = dr["tipDescripcion"].ToString()
                            });
                        }
                    }
                }                
            }catch(Exception e)
            {
                throw new Exception("No se lograron obtener los datos de la base de datos.", e);
            }
            finally
            {
                Close();
            }
            return animalTypesList;
        }
        public AnimalTypeModel GetById(int idAnimalType)
        {
            var animalType = new AnimalTypeModel();
            Open();
            if(Conn == null)
                throw new Exception("La conexión a la base de datos no se pudo establecer.");

            try
            {
                using(var cmd = Conn.CreateCommand())
                {
                    cmd.CommandText = "GetAnimalTypeById";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("tipId", idAnimalType);
                    using (var dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            animalType.TipId = Convert.ToInt32(dr["tipId"]);
                            animalType.TipNombre = dr["tipNombre"].ToString();
                            animalType.TipDescripcion = dr["tipDescripcion"].ToString();
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                throw new Exception($"No se pudieron obtener los datos de la DB. {ex}");
            }
            finally
            {
                Close();
            }
            return animalType;
        }
        public AnimalTypeModel Update(AnimalTypeModel model)
        {
            Open();
            if(Conn == null || Conn.State != ConnectionState.Open)
                throw new Exception("La conexión a la base de datos no se pudo establecer o no está abierta.");

            try
            {
                using (var cmd = Conn.CreateCommand())
                {
                    cmd.CommandText = "UpdateAnimalType";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@tipId", model.TipId);
                    cmd.Parameters.AddWithValue("tipNombre", model.TipNombre);
                    cmd.Parameters.AddWithValue("tipDescripcion", model.TipDescripcion);
                    var rowsAffected = cmd.ExecuteNonQuery();
                    if(rowsAffected == 0)
                        throw new Exception("No se encontró el contacto para actualizar.");
                }               
            }
            catch(Exception ex)
            {
                throw new Exception("Error al intentar actualizar el tipo de animal en la base de datos.", ex);
            }
            finally
            {
                Close();
            }
            return model;
        }
    }
}