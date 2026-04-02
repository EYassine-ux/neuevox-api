using Microsoft.EntityFrameworkCore;
using NeueVox.Model.NeuevoxModel;
using NeueVox.Model.NeuevoxModel.Context;

namespace NeueVox.Repository;

public interface IStudentRepository : IBaseRepository<Student>
{
    Task<Student> GetStudentByEmail(string email);



}

public class StudentRepository : BaseRepository<Student>,IStudentRepository
{

    public StudentRepository(NeueVoxContext dbContext) : base(dbContext)
    {
    }

    public async Task<Student> GetStudentByEmail(string email)
    {
        var student = await DbSet.FirstOrDefaultAsync(s=>s.SchoolEmail == email);
        if(student == null) return null;
        return student;    }





}
