using Microsoft.EntityFrameworkCore;
using NeueVox.Model.NeuevoxModel;
using NeueVox.Model.NeuevoxModel.Context;

namespace NeueVox.Repository;


public interface IDocumentRepository : IBaseRepository<Document>
{
  Task<IEnumerable<Document>> GetAllDocumentsForClass(Guid classId);
}

public class DocumentRepository : BaseRepository<Document>,IDocumentRepository
{

  public  DocumentRepository(NeueVoxContext dbContext) : base(dbContext)
  {
  }

  public async Task<IEnumerable<Document>> GetAllDocumentsForClass(Guid classId)
  {
    var documents = await DbContext.Documents
      .AsNoTracking()
      .Include(e => e.Class)
      .ThenInclude(c => c.Course)
      .Where(d => d.ClassId == classId)
      .ToListAsync();
    return documents;
  }

}
