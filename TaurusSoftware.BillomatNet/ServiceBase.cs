namespace TaurusSoftware.BillomatNet
{
    public abstract class ServiceBase
    {
        protected Configuration Configuration { get; private set; }

        protected ServiceBase(Configuration configuration)
        {
            Configuration = configuration;
        }
    }
}