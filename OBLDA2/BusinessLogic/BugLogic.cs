using System.Collections.Generic;
using BusinessLogicInterface;
using DataAccessInterface;
using Exceptions;
using Domain;
using System;

namespace BusinessLogic
{
    public class BugLogic : IBugLogic
    {
        private const string notExistBug = "Bug does not exist whit this id";
        private const string existingBug = "The Bug whit that id alredy exist";
        private const string invalidState = "It´s not a state bug";

        private IBugRepository bugRepository;
        private IRepository<State, Guid> stateRepository;

        public BugLogic(IBugRepository bugRepository, IRepository<State, Guid> stateRepository)
        {
            this.bugRepository = bugRepository;
            this.stateRepository = stateRepository;
        }

        public BugLogic() { }

        public Bug Create(Bug bug)
        {
            NotExistBug(bug.Id);
            IsValidBug(bug);
            bugRepository.Create(bug);
            return bug;
        }

        private void NotExistBug(int id)
        {
            Bug bug = bugRepository.GetById(id);

            if(bug != null)
            {
                throw new ExistingObjectException(existingBug);
            }
        }

        public Bug Get(int id)
        {
            Bug bug = bugRepository.GetById(id);

            if(bug == null)
            {
                throw new NoObjectException(notExistBug);
            }

            return bug;
        }

        public void ExistBug(int id)
        {
            Get(id);
        }

        public void Delete(int id)
        {
            ExistBug(id);
            bugRepository.Delete(id);
        }

        public List<Bug> GetAll()
        {
            return bugRepository.GetAll();
        }

        public Bug Update(int id, Bug bugUpdate)
        {
            ExistBug(id);
            IsValidBug(bugUpdate);

            return bugRepository.Update(id, bugUpdate);
        }

        public Bug UpdateState(int id, string state)
        {
            ExistBug(id);

            if (state.ToLower() == State.active.ToLower())
                return UpdateStateToActiveBug(id);
            else if (state.ToLower() == State.done.ToLower())
                return UpdateStateToDoneBug(id);

            throw new InvalidDataObjException(invalidState);
        }

        public Bug UpdateStateToActiveBug(int id)
        {
            Bug activeBug = bugRepository.Get(id);

            activeBug.State.Name = State.active;

            bugRepository.Update(id, activeBug);

            return activeBug;
        }

        public Bug UpdateStateToDoneBug(int id)
        {
            Bug doneBug = bugRepository.Get(id);

            doneBug.State.Name = State.done;

            bugRepository.Update(id, doneBug);

            return doneBug;
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
                if (oneState.Name.ToLower() == state.Name.ToLower())
                {
                    return;
                }
            }

            throw new InvalidDataObjException(invalidState);
        }

    }
}