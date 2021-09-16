using BusinessLogicInterface;
using DataAccessInterface;
using Domain;
using System;
using System.Collections.Generic;

namespace BusinessLogic
{
    public class BugLogic:IBugLogic
    {
        private string[] possibleStates = { "activo", "resuelto" };

        private IRepository<Bug> bugRepository;

        public BugLogic(IRepository<Bug> bugRepository)
        {
            this.bugRepository = bugRepository;
        }

        public Bug Create(Bug bug)
        {
            AreCorrectData(bug);
            bugRepository.Create(bug);
            return bug;
        }

        public void Delete(Bug bug)
        {
            bugRepository.Delete(bug);
        }

        public IEnumerable<Bug> GetAll()
        {
            return bugRepository.GetAll();
        }

        public void Update(Bug bug, Bug bugUpdate)
        {
            AreCorrectData(bugUpdate);

            bugRepository.Update(bug, bugUpdate);
        }

        private void AreCorrectData(Bug oneBug)
        {
            IsValidId(oneBug.Id);
            IsValidName(oneBug.Name);
            IsValidDomain(oneBug.Domain);
            IsValidVersion(oneBug.Version);
            IsValidState(oneBug.State);
        }

        private void IsValidId(int oneId)
        {
            if (!(oneId <= 9999 && oneId >= 0))
                throw new Exception("");
        }

        private void IsValidName(string oneName)
        {
            int nameLength = oneName.Length;

            if(!(nameLength <= 60 && nameLength > 0))
                throw new Exception("");
        }

        private void IsValidDomain(string oneDomain)
        {
            int domainLength = oneDomain.Length;

            if (!(domainLength <= 150 && domainLength > 0))
                throw new Exception("");
        }

        private void IsValidVersion(string oneVersion)
        {
            int versionLength = oneVersion.Length;

            if (!(versionLength <= 10 && versionLength > 0))
                throw new Exception("");
        }

        private void IsValidState(string oneVersion)
        {
            bool isValidState = false;

            for (int i = 0; i < possibleStates.Length && !isValidState; i++)
                if (possibleStates[i] == oneVersion.ToLower())
                    isValidState = true;

            if(!isValidState)
                throw new Exception("");
        }

        public Bug Get(int id)
        {
            return bugRepository.GetById(id);
        }
    }
}
