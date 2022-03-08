namespace ApplicationCore.Contracts.Repositories;

public interface IRespository<T> where T: class
{
    Task<T> GetById(int id);
    Task<IEnumerable<T>> GetAll();
    Task<T> Add(T entity);
    Task<T> Delete(T entity);
    Task<T> Update(T entity);
}