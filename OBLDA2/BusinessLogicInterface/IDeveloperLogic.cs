﻿using System.Collections.Generic;
using Domain;
using System;

namespace BusinessLogicInterface
{
    public interface IDeveloperLogic
    {
        List<User> GetAll();
        List<Bug> GetAllBugs(Guid developer);
        List<Project> GetAllProjects(Guid developerId);
        List<Task> GetAllTask(Guid developerId);

        Bug UpdateState(int id, string state, Guid userResolved);

        void AssignDeveloperToProject(Guid projectId, Guid developerId);
        void DeleteDeveloperInProject(Guid projectId, Guid developerId);
        int CountBugDoneByDeveloper(Guid developer);
        
    }
}
