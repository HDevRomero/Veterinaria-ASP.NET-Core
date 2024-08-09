namespace Veterinaria.Services.Interfaces
{
    public interface ICrud<T>
        where T : class
    {
        List<T> GetAll();
        T Add(T ob);
        T Update(T ob);
        T Delete(int id);
        T GetById(int id);        
    }
}
