using System.ServiceModel;
using System.ServiceProcess;

namespace GTPriceImporterService
{
    public partial class GTPriceServiceImport : ServiceBase
    {
        public GTPriceServiceImport()
        {
            InitializeComponent();
        }

        ServiceHost service = new ServiceHost(typeof(GTPriceImporterWCF));

        protected override void OnStart(string[] args)
        {
            service.Open();
        }

        protected override void OnStop()
        {
            if (service != null)
            {
                service.Close();
                service = null;
            }
        }
    }
}
