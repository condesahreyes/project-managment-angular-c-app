using System.Collections.Generic;
using BusinessLogicInterface;
using DataAccessInterface;
using System.Linq;
using Exceptions;
using Domain;
using System;

namespace BusinessLogic
{
    public class BugLogic : IBugLogic
    {
        private const string notExistBug = "Bug does not exist whit this id";
        private const string existingBug = "The Bug whit that id alredy exist";
        private const string invalidState = "It´s not a valid state bug";

        private IBugRepository bugRepository;
        private IRepository<State, Guid> stateRepository;
        private IProjectLogic projectLogic;

        public BugLogic(IBugRepository bugRepository, 
            IRepository<State, Guid> stateRepository, IProjectLogic projectLogic)
        {
            this.bugRepository = bugRepository;
            this.stateRepository = stateRepository;
            this.projectLogic = projectLogic;
        }

        public Bug CreateByUser(Bug bug, Guid userId)
        {
            projectLogic.IsUserAssignInProject(bug.Project.Name, userId);
            return Create(bug);
        }

        public Bug Create(Bug bug)
        {
            NotExistBug(bug.Id);
            IsValidBug(ref bug);
            
            bugRepository.Create(bug);

            return bug;
        }

        public Bug Get(int id, Guid userId)
        {
            Bug bug = bugRepository.GetById(id);

            if(bug == null)
            {
                throw new NoObjectException(notExistBug);
            }

            projectLogic.IsUserAssignInProject(bug.Project.Name, userId);

            return bug;
        }

        public void Delete(int id, Guid userId)
        {
            Get(id, userId);
            bugRepository.Delete(id);
        }

        public List<Bug> GetAll()
        {
            return bugRepository.GetAll();
        }

        public Bug Update(int id, Bug bugUpdate, Guid userId)
        {
            Get(id, userId);
            IsValidBug(ref bugUpdate);

            bugUpdate.Id = id;

            return bugRepository.Update(id, bugUpdate);
        }

        private void NotExistBug(int id)
        {
            Bug bug = bugRepository.GetById(id);

            if (bug != null)
            {
                throw new ExistingObjectException(existingBug);
            }
        }

        private void IsValidBug(ref Bug bug)
        {
            bug.State = IsValidState(bug.State);
            bug.Project = projectLogic.ExistProjectWithName(bug.Project);
            Bug.AreCorrectData(bug);
        }

        private State IsValidState(State state)
        {
            List<State> states = stateRepository.GetAllGeneric();

            try
            {
                return states.Where(s => s.Name.ToLower() == state.Name.ToLower()).First();
            }
            catch (InvalidOperationException)
            {
                throw new InvalidDataObjException(invalidState);
            }
        }

    }
}