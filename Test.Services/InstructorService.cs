using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Test.Data.Interfaces;
using Test.Data.Models;

namespace Test.Services
{
    public class InstructorService
    {
        private readonly IInstructorDao _dao;

        public InstructorService(IInstructorDao dao)
        {
            _dao = dao;
        }

        public async Task<IEnumerable<Instructor>> Get(InstructorGetOptions options)
        {
            return await _dao.Get(options);
        }

        public async Task<Instructor> Create(Instructor instructor)
        {
            return await _dao.Create(instructor);
        }

        public async Task<Instructor> Update(Instructor instructor)
        {
            return await _dao.Update(instructor);
        }

        public async Task Delete(int id)
        {
            await _dao.Delete(id);
        }
    }
}