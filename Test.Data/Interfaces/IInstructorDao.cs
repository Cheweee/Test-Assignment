using System.Collections.Generic;
using System.Threading.Tasks;
using Test.Data.Models;

namespace Test.Data.Interfaces
{
    public interface IInstructorDao
    {
        Task<IEnumerable<Instructor>> Get(InstructorGetOptions options);
        Task<Instructor> Create(Instructor instructor);
        Task<Instructor> Update(Instructor instructor);
        Task Delete(int id);
    }
}