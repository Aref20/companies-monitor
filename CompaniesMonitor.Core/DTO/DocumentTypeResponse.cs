using CompaniesMonitor.Core.Entities;
namespace MSGCompaniesMonitor.DTO
{
    public class DocumentTypeResponse
    {

        public int Id { get; set; }
        public ICollection<Document> Documentes { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime ExpireyDate { get; set; }
    }

    /* public static class DocumentTypeExtention
     {
         public static DocumentTypeResponse ToDocumentTypeResponse
             (this DocumentType documentType)
         {
             return new DocumentTypeResponse
             {
                 Id = documentType.Id,
                 StartDate = documentType.StartDate,
                 ExpireyDate = documentType.ExpireyDate,
                 Document = documentType.Document
             };
         }

     }*/
}
