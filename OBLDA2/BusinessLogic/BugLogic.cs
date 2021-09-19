using System.Collections.Generic;
using BusinessLogicInterface;
using DataAccessInterface;
using DataAccess;
using Domain;
using System;

namespace BusinessLogic
{
    public class BugLogic : IBugLogic
    {
        private string[] possibleStates = { "activo", "resuelto" };

        private IRepository<Bug, int> bugRepository;

        public BugLogic(IRepository<Bug, int> bugRepository)
        {
            this.bugRepository = bugRepository;
        }

        public BugLogic()
        {
            bugRepository = new Repository<Bug, int>();
        }

        public Bug Create(Bug bug)
        {
            IsValidBug(bug);
            bugRepository.Create(bug);
            return bug;
        }

        public void Delete(int id)
        {
            bugRepository.Delete(id);
        }

        public List<Bug> GetAll()
        {
            return bugRepository.GetAll();
        }

        public Bug Update(int id, Bug bugUpdate)
        {
            IsValidBug(bugUpdate);

            return bugRepository.Update(id, bugUpdate);
        }

        private void IsValidBug(Bug oneBug)
        {
            Bug.AreCorrectData(oneBug);
            IsValidState(oneBug.State);
        }

        private void IsValidState(string oneVersion)
        {
            bool isValidState = false;

            for (int i = 0; i < possibleStates.Length && !isValidState; i++)
                if (possibleStates[i] == oneVersion.ToLower())
                    isValidState = true;

            if (!isValidState)
                throw new Exception("");
        }

        public Bug Get(int id)
        {
            return bugRepository.Get(id);
        }
    }
}
