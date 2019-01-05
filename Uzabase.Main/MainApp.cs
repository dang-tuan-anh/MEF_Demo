using log4net;
using log4net.Config;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Uzabase.Interface;

namespace Uzabase
{
    /// <summary>
    /// Main application.
    /// Use MEF framework to import and active modules only
    /// </summary>
    public class MainApp : IMainApp
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(MainApp));
        
        /// <summary>
        /// Input parameters
        /// </summary>
        public string[] Input { get; set; }

        /// <summary>
		/// Module list
		/// </summary>
		[ImportMany]
        private List<IModule> ModuleList = null;


        /// <summary>
        /// Start point
        /// </summary>
        public void start(string[] args)
        {
            Input = args;
            XmlConfigurator.Configure();
            ImportModule();
            ActivateModule();
            Logger.Info("Press a key to end life cycle of the application.");
            Console.ReadKey();
        }

        /// <summary>
        /// Import modules (dlls prefixed with Uzabase.Module*) in the current directory.
        /// </summary>
        private void ImportModule()
        {
            var catalog = new AggregateCatalog();
            var directory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            catalog.Catalogs.Add(new DirectoryCatalog(directory, "Uzabase.Module*.dll"));
            var container = new CompositionContainer(catalog);
            container.ComposeParts(this);
        }

        /// <summary>
		/// Activate each module
		/// </summary>
		private void ActivateModule()
        {
            foreach (var module in ModuleList)
            {
                try
                {
                    module.Activate(this);
                }
                catch (Exception ex)
                {
                    Logger.Error(ex);
                    Logger.Error(string.Format("Some errors occured in module {0}. Press a key to continue...", module.GetType().ToString()));
                    Console.ReadKey();
                }
            }
        }
    }
}
