using SchoolManagement.Abstraction.Repositories;
using SchoolManagement.Abstraction.Repositories.SchoolRepository;
using SchoolManagement.Entities;

namespace SchoolManagement.Abstraction;

public interface IUnitOfWork : IDisposable
{
    IRepository<T> GetRepository<T>() where T : BaseEntity;
    Task<int> Commit();
}