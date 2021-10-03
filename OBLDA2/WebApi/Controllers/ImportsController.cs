using Domain;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using OBLDA2.Models;
using System.Linq;
using System;
using BusinessLogic.Imports;
using BusinessLogicInterface.Imports;

namespace OBLDA2.Controllers
{
    [ApiController]
    [Route("api/v1/administrators")]
    public class ImportsController<T> : ControllerBase where T : class
    {
        private readonly IBugsImport<T> import;

        public ImportsController(IBugsImport<T> txtImport)
        {
            this.import = txtImport;
        }

        [HttpPost]
        public IActionResult ImportBugsTxt(string fileAddress)
        {
             throw new Exception();
        }

        [HttpPost]
        public IActionResult ImportBugsXml(string fileAddress)
        {
            throw new Exception();
        }

    }
}