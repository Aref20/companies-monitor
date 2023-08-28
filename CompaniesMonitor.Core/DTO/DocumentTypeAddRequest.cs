using CompaniesMonitor.Core.Entities;

namespace MSGCompaniesMonitor.DTO
{
    public class DocumentTypeAddRequest
    {
        public ICollection<Document> Documentes { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime ExpireyDate { get; set; }

   /*     public DocumentType ToDocumentType()
        {
            return new DocumentType()
            {
                StartDate = StartDate,
                ExpireyDate = ExpireyDate,
                Documentes = Documentes

            };

        }*/
    }
}
