using System.Collections.Generic;
using Domain;
using System;

namespace BusinessLogicInterface
{
    public interface IProjectLogic
    {
        Project Create(Project projectToCreate);
        Project Update(Guid id, Project updatedProject);

        void Delete(Guid id);
        void DeleteUser(Guid oneProjectId, ref User user);
        void AssignUser(Guid oneProjectId, ref User user);
        void IsUserAssignInProject(string projectName, Guid userId);

        Project Get(Guid id);
        Project ExistProjectWithName(Project project);

        List<Project> GetAll();
        List<Bug> GetAllBugByProject(Guid project);
        List<User> GetAllTesters(Project oneProject);
        List<User> GetAllDevelopers(Project oneProject);
        List<User> GetAllUsersInOneProject(Guid projectId);
    }
}
