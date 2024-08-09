using System.Data;
using Veterinaria.Data;
using Veterinaria.Models;
using Veterinaria.Services.Interfaces;

namespace Veterinaria.Services.Implementations
{
    public class OwnerService<T> : DBConnection, ICrud<OwnerModel>
    {
        public OwnerModel Add(OwnerModel model)//OK
        {
            Open();
            if(Conn == null || Conn.State != ConnectionState.Open)
                throw new Exception("La conexión a la base de datos no se pudo establecer o está cerrada.");

            try
            {
                using (var cmd = Conn.CreateCommand())
                {
                    cmd.CommandText = "InsertOwner";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("proNombre", model.ProNombre);
                    cmd.Parameters.AddWithValue("proApellido", model.ProApellido);
                    cmd.Parameters.AddWithValue("proTelefono", model.ProTelefono);
                    cmd.Parameters.AddWithValue("proDireccion", model.ProDireccion);
                    cmd.Parameters.AddWithValue("proDocumento", model.ProDocumento);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al guardar los datos. {ex.Message}");
            }
            finally
            {
                Close();
            }
            return model;
        }
        public OwnerModel Delete(int idOwner)
        {
            throw new NotImplementedException();
        }
        public OwnerModel Delete(string idOwner)//OK
        {
            var owner = new OwnerModel();
            Open();
            if (Conn == null)
                throw new Exception("La conexión a la base de datos no se pudo establecer.");

            try
            {
                using (var cmd = Conn.CreateCommand())
                {
                    cmd.CommandText = "DeleteOwner";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("proDocumento", idOwner);
                    cmd.ExecuteNonQuery();
                }               
            }
            catch (Exception ex)
            {
                throw new Exception($"No se pudo eliminar el propietario en la base de datos {ex}");
            }
            finally
            {
                Close();
            }
            owner.ProDocumento = idOwner;
            return owner;
        }
        public List<OwnerModel> GetAll()//OK
        {       
            var ownerList = new List<OwnerModel>();
            Open();
            if (Conn == null)
                throw new Exception("La conexión a la base de datos no se pudo establecer.");

            try
            {
                using (var cmd = Conn.CreateCommand())
                {
                    cmd.CommandText = "GetAllOwners";
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (var dr = cmd.ExecuteReader())
                    {                        
                        while (dr.Read())
                        {
                            ownerList.Add(new OwnerModel()
                            {
                                ProDocumento = dr["proDocumento"].ToString(),
                                ProNombre = dr["proNombre"].ToString(),
                                ProApellido = dr["proApellido"].ToString(),
                                ProTelefono = dr["proTelefono"].ToString(),
                                ProDireccion = dr["proDireccion"].ToString()
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
            return ownerList;
        }
        public OwnerModel GetById(int idOwner)
        {
            throw new NotImplementedException();
        }
        public OwnerModel GetById(string idOwner)//OK
        {
            var owner = new OwnerModel();
            Open();
            if (Conn == null)
                throw new Exception("La conexión a la base de datos no se pudo establecer.");

            try
            {
                using( var cmd = Conn.CreateCommand())
                {
                    cmd.CommandText = "GetOwnerById";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("proDocumento", idOwner);
                    using (var dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            owner.ProDocumento = dr["proDocumento"].ToString();
                            owner.ProNombre = dr["proNombre"].ToString();
                            owner.ProApellido = dr["proApellido"].ToString();
                            owner.ProTelefono = dr["proTelefono"].ToString();
                            owner.ProDireccion = dr["proDireccion"].ToString();
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
            return owner;
        }
        public OwnerModel Update(OwnerModel model)//OK
        {
            Open();
            if (Conn == null || Conn.State != ConnectionState.Open)
                throw new Exception("La conexión a la base de datos no se pudo establecer o no está abierta.");

            try
            {
                using (var cmd = Conn.CreateCommand())
                {
                    cmd.CommandText = "UpdateOwner";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@proDocumento", model.ProDocumento);
                    cmd.Parameters.AddWithValue("proNombre", model.ProNombre);
                    cmd.Parameters.AddWithValue("proApellido", model.ProApellido);
                    cmd.Parameters.AddWithValue("proTelefono", model.ProTelefono);
                    cmd.Parameters.AddWithValue("proDireccion", model.ProDireccion);
                    var rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected == 0)
                        throw new Exception("No se encontró el contacto para actualizar.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al intentar actualizar el propietario en la base de datos.", ex);
            }
            finally
            {
                Close();
            }
            return model;
        }
    }
}