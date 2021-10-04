using Microsoft.Extensions.DependencyInjection;
using BusinessLogicInterface.Imports;
using BusinessLogicInterface;
using BusinessLogic.Imports;
using BusinessLogic.UserRol;
using BusinessLogic;

namespace Factory
{
    public class BusinessLogicFactory
    {
        private readonly IServiceCollection _serviceCollection;

        public BusinessLogicFactory(IServiceCollection serviceCollection)
        {
            this._serviceCollection = serviceCollection;
        }

        public void AddBusinessLogicServices()
        {
            this._serviceCollection.AddScoped<IUserLogic, UserLogic>();
            this._serviceCollection.AddScoped<ISessionLogic, SessionLogic>();
            this._serviceCollection.AddScoped<IAdministratorLogic, AdministratorLogic>();
            this._serviceCollection.AddScoped<IDeveloperLogic, DeveloperLogic>();
            this._serviceCollection.AddScoped<ITesterLogic, TesterLogic>();
            this._serviceCollection.AddScoped<IProjectLogic, ProjectLogic>();
            this._serviceCollection.AddScoped<IBugLogic, BugLogic>();
            this._serviceCollection.AddScoped<IBugsImport<BugsImportXml>, BugsImportXml>();
            this._serviceCollection.AddScoped<IBugsImport<BugsImportTxt>, BugsImportTxt>();
        }

    }
}
