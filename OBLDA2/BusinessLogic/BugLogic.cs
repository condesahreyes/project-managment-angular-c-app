using System.Collections.Generic;
using BusinessLogicInterface;
using DataAccessInterface;
using DataAccess;
using Domain;
using System;
using DataAccess.Repositories;

namespace BusinessLogic
{
    public class BugLogic : IBugLogic
    {
        private IBugRepository bugRepository;
        private IRepository<State, Guid> stateRepository;

        public BugLogic(IBugRepository bugRepository, IRepository<State, Guid> stateRepository)
        {
            this.bugRepository = bugRepository;
            this.stateRepository = stateRepository;
        }

        public BugLogic()
        {
            bugRepository = new BugRepository();
            stateRepository = new Repository<State, Guid>();
        }

        public Bug Create(Bug bug)
        {
            IsValidBug(bug);
            bugRepository.Create(bug);
            return bug;
        }

        public Bug Get(int id)
        {
            return bugRepository.GetById(id);
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

        private void IsValidBug(Bug bug)
        {
            IsValidState(bug.State);
            Bug.AreCorrectData(bug);
        }

        private void IsValidState(State state)
        {
            List<State> states = stateRepository.GetAllGeneric();

            foreach (State oneState in states)
            {
                if (oneState.Name == state.Name.ToLower())
                {
                    return;
                }
            }

            throw new Exception("");
        }

    }
}