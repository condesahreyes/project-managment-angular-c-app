using BusinessLogicInterface.Imports;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using WebApi.Filters;
using OBLDA2.Models;
using System.Linq;
using Domain;

namespace OBLDA2.Controllers
{
    [Route("penguin/imports")]
    [AuthorizationFilter(Autorization.Administrator)]
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
            List<Bug> bugs = this.import.ImportBugs(fileAddress);
            IEnumerable<BugEntryOutModel> bugsModel = bugs.Select(b => new BugEntryOutModel(b));

            return Ok(bugsModel);
        }

        [HttpPost]
        public IActionResult ImportBugsXml(string fileAddress)
        {
            List<Bug> bugs = this.import.ImportBugs(fileAddress);
            IEnumerable<BugEntryOutModel> bugsModel = bugs.Select(b => new BugEntryOutModel(b));

            return Ok(bugsModel);
        }

    }
}