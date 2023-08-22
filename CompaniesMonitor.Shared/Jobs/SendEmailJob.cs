using MSGCompaniesMonitor.Models;
using MSGCompaniesMonitor.ServiceContracts;
using MSGCompaniesMonitor.Services;
using Quartz;
namespace MSGCompaniesMonitor.Jobs
{
    public class SendEmailJob : IJob
    {
        private readonly IDocumentsTypeService _documentsTypeService;
        public SendEmailJob(IDocumentsTypeService documentsTypeService)
        {
            _documentsTypeService = documentsTypeService;
        }
        public async Task Execute(IJobExecutionContext context)
        {
            if (true) { 
            Console.WriteLine(DateTime.Now.Date);
            }
            //get the day only
            var Datenow = DateTime.Now.Date ;

               List<DocumentType> documentsType = await _documentsTypeService.GetAllDocumentsTypeAsync();

                   foreach(var documentType in documentsType)
                 {
                      Console.WriteLine("rrrrrrrrrrrrrrrrrrr");
                      var expireDate = documentType.ExpireyDate.Date;
                      if (Datenow >= expireDate)
                      {
                          Console.WriteLine("Document ID {documentType.Id} will expire in {documentType.ExpireyDate}", documentType.Id, documentType.ExpireyDate);
                      }
                  }


        }
    }
}
