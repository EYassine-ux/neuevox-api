
using NeueVox.Model.DTOs;
using NeueVox.Model.DTOs.ReponseDTO;
using NeueVox.Model.NeuevoxModel;
using NeueVox.Repository;

namespace NeueVox.Service;


public interface IDocumentService : IBaseService<Document>
{
  Task<IEnumerable<DocumentResponseDTO>> GetAllDocumentsForClass(Guid classId);
  Task<IEnumerable<DocumentResponseDTO>> GetAllDocuments();
  Task<DocumentResponseDTO?> GetDocumentById(Guid documentId);
  Task<DocumentResponseDTO?> AddDocument(AddDocumentDTO addDocumentDTO);
  Task<DocumentResponseDTO?> UpdateDocument(AddDocumentDTO addDocumentDTO, Guid documentId);

}

public class DocumentService : BaseService<Document>, IDocumentService
{
  private readonly IDocumentRepository _documentRepository;
  public DocumentService(IDocumentRepository repository) : base(repository)
  {
    _documentRepository = repository;
  }

  public async Task<IEnumerable<DocumentResponseDTO>> GetAllDocumentsForClass(Guid classId)
  {
    var rawDocuments = await _documentRepository.GetAllDocumentsForClass(classId);

    var documents = rawDocuments.Select(d => new DocumentResponseDTO
    {
      DocumentId = d.DocumentId,
      FileName =  d.FileName,
      FileUrl = d.FileUrl,
      FileType = d.FileType,
    }).ToList();

    return documents;
  }

  public async Task<IEnumerable<DocumentResponseDTO>> GetAllDocuments()
  {
    var rawDocuments = await _documentRepository.GetAllAsync();


    var documents = rawDocuments.Select(MapToResponseDTO).ToList();
    return documents;
  }

  public async Task<DocumentResponseDTO?> GetDocumentById(Guid documentId)
  {
    var rawDocument =  await _documentRepository.GetByIdAsync(documentId);

    if (rawDocument == null)
    {
      return null;
    }

    var document = MapToResponseDTO(rawDocument);
    return document;
  }


  public async Task<DocumentResponseDTO?> AddDocument(AddDocumentDTO addDocumentDTO)
  {
    bool isExisting = await _documentRepository.ExistsAsync(d => d.ClassId == addDocumentDTO.ClassId && d.FileName == addDocumentDTO.FileName);
    if(isExisting) return null;

    var document = new Document
    {
      ClassId = addDocumentDTO.ClassId,
      FileName = addDocumentDTO.FileName,
      FileUrl = addDocumentDTO.FileUrl,
      FileType = addDocumentDTO.FileType,

    };

    var created = await _documentRepository.CreateAsync(document);

    return MapToResponseDTO(created);
  }


  public async Task<DocumentResponseDTO?> UpdateDocument(AddDocumentDTO addDocumentDTO, Guid documentId)
  {
    var document = await _documentRepository.GetByIdAsync(documentId);
    if (document == null) return null;

    document.ClassId = addDocumentDTO.ClassId;
    document.FileName = addDocumentDTO.FileName;
    document.FileUrl = addDocumentDTO.FileUrl;
    document.FileType = addDocumentDTO.FileType;

    var updated = await _documentRepository.UpdateAsync(document, documentId);

    return updated == null ? null : MapToResponseDTO(updated);
  }


  private DocumentResponseDTO MapToResponseDTO(Document d)
  {
    return new DocumentResponseDTO
    {
      DocumentId = d.DocumentId,
      ClassId =  d.ClassId,
      FileName =  d.FileName,
      FileUrl = d.FileUrl,
      FileType = d.FileType,
    };
  }
}

