namespace Agendamento.API.Contracts.V1
{
    public static class ApiRoutes
    {
        public const string Root = "api";
        public const string Version = "v1";
        public const string Base = Root + "/" + Version;

        public static class Sala
        {
            public const string GetAll = Base + "/sala";
            public const string Get = Base + "/sala/{id}";
            public const string Create = Base + "/sala";
            public const string Update = Base + "/sala/";
            public const string Delete = Base + "/sala/{id}";
        }

        public static class Agenda
        {
            public const string GetAll = Base + "/agenda";
            public const string Get = Base + "/agenda/{id}";
            public const string Create = Base + "/agenda";
            public const string Update = Base + "/agenda";
            public const string Delete = Base + "/agenda/{id}";
        }
    }
}
