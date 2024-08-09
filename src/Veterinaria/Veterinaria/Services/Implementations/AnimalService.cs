using System.Data;
using Veterinaria.Data;
using Veterinaria.Models;
using Veterinaria.Services.Interfaces;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Veterinaria.Services.Implementations
{
    public class AnimalService<T> : DBConnection, ICrud<AnimalModel>
    {        
        public AnimalModel Add(AnimalModel model)
        {
            Open();
            if(Conn == null || Conn.State != ConnectionState.Open)
                throw new Exception("La conexión a la base de datos no se pudo establecer o está cerrada.");

            try
            {
                using (var cmd = Conn.CreateCommand())
                {
                    cmd.CommandText = "InsertAnimal";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@aniNombre", model.AniNombre);
                    cmd.Parameters.AddWithValue("@proId_fk", model.ProIdFk);
                    cmd.Parameters.AddWithValue("@tipId_fk", model.TipIdFk);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al guardar los datos en la DB. {ex.Message}");
            }
            finally
            {
                Close();
            }
            return model;
        }
        public AnimalModel Delete(int idAnimal)
        {
            var animal = new AnimalModel();
            Open();
            if (Conn == null)
                throw new Exception("No se pudo establecer la conexión a la base de datos.");

            try
            {
                using( var cmd = Conn.CreateCommand())
                {
                    cmd.CommandText = "DeleteAnimal";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("aniId", idAnimal);
                    cmd.ExecuteNonQuery();
                }
            }
            catch(Exception ex)
            {
                throw new Exception($"No se pudo eliminar el animal de la DB. {ex}");
            }
            finally
            {
                Close();
            }
            animal.AniId = idAnimal;
            return animal;
        }        
        public List<AnimalModel> GetAll()
        {
            List<AnimalModel> AnimalList = new List<AnimalModel>();
            Open();
            if (Conn == null)
                throw new Exception("La conexión a la base de datos no se pudo establecer.");

            try
            {
                using (var cmd = Conn.CreateCommand())
                {
                    cmd.CommandText = "GetAnimalDetails";
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (var dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            AnimalList.Add(new AnimalModel()
                            {
                                AniId = Convert.ToInt32(dr["aniId"]),
                                AniNombre = dr["aniNombre"].ToString(),
                                ProIdFk = dr["proId_fk"].ToString(),
                                ProNombreCompleto = dr["proNombreCompleto"].ToString(),
                                TipIdFk = Convert.ToInt32(dr["tipId_fk"]),
                                TipNombre = dr["tipNombre"].ToString()
                            });
                        }
                    }
                }                
            }
            catch (Exception e)
            {
                throw new Exception("No se lograron obtener los datos de la base de datos.", e);
            }
            finally
            {
                Close();
            }
            return AnimalList;
        }
        public AnimalModel GetById(int idAnimal)
        {
            var animal = new AnimalModel();
            Open();
            if (Conn == null)
                throw new Exception("La conexión a la base de datos no se pudo establecer.");

            try
            {
                using (var cmd = Conn.CreateCommand())
                {
                    cmd.CommandText = "GetAnimalById";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("aniId", idAnimal);
                    using (var dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            animal.AniId = Convert.ToInt32(dr["aniId"]);
                            animal.AniNombre = dr["aniNombre"].ToString();
                            animal.TipIdFk = Convert.ToInt32(dr["tipId_fk"]);
                            animal.ProIdFk = dr["proId_fk"].ToString();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"No se pudieron obtener los datos de la DB. {ex}");
            }
            finally
            {
                Close();
            }
            return animal;
        }
        public AnimalModel Update(AnimalModel model)
        {
            throw new NotImplementedException();
        }       
        public List<SelectListItem> GetOwners()
        {
            var ownersList = new List<SelectListItem>();
            
            Open();
            if (Conn == null)
                throw new Exception("La conexión a la base de datos no se pudo establecer.");

            try
            {
                using(var cmd = Conn.CreateCommand())
                {
                    cmd.CommandText = "SelectGetOwner";
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (var dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            ownersList.Add(new SelectListItem
                            {
                                Value = dr["proDocumento"].ToString(),
                                Text = dr["proNombreCompleto"].ToString()
                            });
                        }
                    }
                }               
            }
            catch (Exception e)
            {
                throw new Exception("No se lograron obtener los datos de la base de datos.", e);
            }
            finally
            {
                Close();
            }
            return ownersList;
        }
        public List<SelectListItem> GetAnimalTypes()
        {
            var animalTypesList = new List<SelectListItem>();
            Open();
            if (Conn == null)
                throw new Exception("La conexión a la base de datos no se pudo establecer.");

            try
            {
                using(var cmd = Conn.CreateCommand())
                {
                    cmd.CommandText = "SelectGetAnimalTypes";
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (var dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            animalTypesList.Add(new SelectListItem
                            {
                                Value = dr["tipId"].ToString(),
                                Text = dr["tipNombre"].ToString()
                            });
                        }
                    }
                }               
            }
            catch (Exception e)
            {
                throw new Exception("No se lograron obtener los datos de la base de datos.", e);
            }
            finally
            {
                Close();
            }
            return animalTypesList;
        }
    }
}