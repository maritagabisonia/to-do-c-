namespace ToDoApi.Packages
{
    public class PKG_BASE
    {
        string connStr;

        IConfiguration configuration;


        public PKG_BASE(IConfiguration configuration)
        {
            this.configuration = configuration;
            connStr = this.configuration.GetConnectionString("SqlConnStr");
        }
        protected string Connstr
        {
            get { return connStr; }
        }
    }
}
