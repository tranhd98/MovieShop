namespace ApplicationCore.Contracts.Repositories;

public interface IRespository<T> where T: class
{
    T GetById(int id);
    IEnumerable<T> GetAll();
    T Add(T entity);
    T Delete(T entity);
    T Update(T entity);
}