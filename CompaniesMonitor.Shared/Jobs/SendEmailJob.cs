﻿using Quartz;
namespace CompaniesMonitor.Shared.Jobs
{
    public class SendEmailJob : IJob
    {
        
        /*  private readonly IDocumentsTypeService _documentsTypeService;
            public SendEmailJob(IDocumentsTypeService documentsTypeService)
            {
                _documentsTypeService = documentsTypeService;
            }*/
        public async Task Execute(IJobExecutionContext context)
        {


            //get the day only
        /*    var Datenow = DateTime.Now.Date ;

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
        */

        }
    }
}
