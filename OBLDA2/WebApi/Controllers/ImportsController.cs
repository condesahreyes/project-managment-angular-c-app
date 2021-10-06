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
    public class ImportsController : ApiBaseController
    {
        private readonly IBugsImport import;

        public ImportsController(IBugsImport import)
        {
            this.import = import;
        }

        [HttpPost]
        public IActionResult ImportBugs(ImportBugModel importBug)
        {
            List<Bug> bugs = this.import.ImportBugs(importBug.FileAddress);
            IEnumerable<BugEntryOutModel> bugsModel = bugs
                .Select(b => new BugEntryOutModel(b));

            return Ok(bugsModel);
        }

    }
}