using Hangfire;
using Microsoft.AspNetCore.Builder;
using UltraBet.Infrastructure.BackgroundJobExtension.JobsUpload;
using UltraBet.Services.BackGroundService.Contracts;

namespace UltraBet.Infrastructure.BackgroundJobExtension
{
    public static class BackGroundJobExtension
    {
        public static void UseBackGroundJob(this IApplicationBuilder app, IBackGroundService bgr)
        {
            BackgroundJob.Enqueue(() => new UploadBackgroundJob(bgr).ExecuteJob());
        }
    }
}
