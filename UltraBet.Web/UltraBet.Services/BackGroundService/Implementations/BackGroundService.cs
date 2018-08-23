using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UltraBet.Services.BackGroundService.Contracts;
using UltraBet.Services.XmlService.Constracts;

namespace UltraBet.Services.BackGroundService.Implementations
{
    public class BackGroundService : IBackGroundService
    {
        private readonly IXmlService xmlService;

        public BackGroundService(IXmlService xmlService)
        {
            this.xmlService = xmlService;
        }

        public void Execute()
        {
            while (true)
            {
                this.xmlService.GetWebClient();

               Thread.Sleep(TimeSpan.FromMinutes(1));
            }
        }
    }
}
