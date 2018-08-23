using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UltraBet.Services.BackGroundService.Contracts;

namespace UltraBet.Infrastructure.BackgroundJobExtension.JobsUpload
{
    public class UploadBackgroundJob
    {
        private readonly IBackGroundService backGroundService;

        public UploadBackgroundJob(IBackGroundService backGroundService)
        {
            this.backGroundService = backGroundService;
        }

        public void ExecuteJob()
        {
            this.backGroundService.Execute();
        }
    }
}
