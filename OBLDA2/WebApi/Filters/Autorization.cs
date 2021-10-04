using Domain;

namespace WebApi.Filters
{
    public static class Autorization
    {
        public const string AllAutorization = 
            Rol.administrator + "," + Rol.developer + "," + Rol.tester;

        public const string Administrator =
            Rol.administrator;

        public const string Developer =
            Rol.administrator;

        public const string Tester =
            Rol.administrator;

        public const string DeveloperAndTester = Developer + ","+ Tester;

        public const string DeveloperAndAdmin = Developer + "," + Administrator;

        public const string AdministratorAndTester = Administrator + "," + Tester;
    }
}
