using Microsoft.Extensions.Configuration;
using BusinessLogicInterface.Imports;
using System.Collections.Generic;
using BusinessLogicInterface;
using BusinessLogic.Imports;
using System.Reflection;
using OBLDA2.Models;
using System.Linq;
using Exceptions;
using System.IO;
using Domain;
using System;

namespace Imports
{
    public class BugsImport : IBugsImport
    {
        private const string invalidFormat = "Error, it´s not a valid format";
        private const string txtExtension = ".txt";
        private const string xmlExtension = ".xml";

        private IBugLogic bugLogic;

        public BugsImport(IBugLogic bugLogic)
        {
            this.bugLogic = bugLogic;
        }

        public List<Bug> CreateBugs(List<Bug> bugs)
        {
            List<Bug> bugsSaved = new List<Bug>();

            foreach (Bug bug in bugs)
            {
                bugsSaved.Add(bugLogic.Create(bug));
            }

            return bugsSaved;
        }

        public List<Bug> ImportBugs(string fileAddress)
        {
            List<Bug> bugsToSaved = new List<Bug>();
            string extension = Path.GetExtension(fileAddress).ToLower();

            if (extension == txtExtension)
            {
                BugsImportTxt txt = new BugsImportTxt();
                bugsToSaved = txt.ImportBugsTxt(fileAddress);
            }
            else if (extension == xmlExtension)
            {
                BugsImportXml xml = new BugsImportXml();
                bugsToSaved = xml.ImportBugsXml(fileAddress);
            }
            else
            {
                bugsToSaved = ImportBugAssembly(fileAddress, extension);
            }

            return CreateBugs(bugsToSaved);
        }

        public List<Bug> ImportBugAssembly(string fileAddress, string extension)
        {
            try{
                Assembly myAssembly = GetAssembly(extension);
                List<Type> implementations = 
                    GetImplementationsInAssembly<IBugsImportGeneric>(myAssembly);

                IBugsImportGeneric bugImportGeneric = 
                    (IBugsImportGeneric)Activator.CreateInstance(implementations.First());

                List<BugImportModel> bugsModel = bugImportGeneric.ImportBugs(fileAddress);

                return BugImportModel.ListBugs(bugsModel);

            }catch
            {
                throw new InvalidDataObjException(invalidFormat);
            }
        }

        private Assembly GetAssembly(string extension)
        {
            Assembly myAssembly = null;

            IConfiguration configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("Configuration.json", optional: false).Build();

            string path = configuration["PathImporter"];

            DirectoryInfo directory = new DirectoryInfo(path);

            FileInfo[] files = directory.GetFiles("*.dll");

            foreach (var file in files)
            {
                if (file.Name.ToLower().Contains(extension.Remove(0)))
                {
                    myAssembly = Assembly.LoadFile(file.FullName);
                }
            }

            return myAssembly;
        }

        private List<Type> GetImplementationsInAssembly<Interface>(Assembly myAssembly)
        {
            List<Type> types = new List<Type>();

            foreach (var type in myAssembly.GetTypes())
            {
                if (typeof(Interface).IsAssignableFrom(type))
                    types.Add(type);
            }

            return types;
        }

    }
}
