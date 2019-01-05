using log4net;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Uzabase.Interface;
using Uzabase.Module1.Interface;
using Uzabase.RSSConversion;

namespace Uzabase.Module1
{
    /// <summary>
    /// This class implements IModule interface to make it works.
    /// </summary>
    [Export(typeof(IModule))]
    public class Module1 : IModule
    {
        /// <summary>
        /// Logger
        /// </summary>
        private static readonly ILog Logger = LogManager.GetLogger(typeof(Module1));

        /// <summary>
        /// Create an instance of IRSSConverter '(RSSConverter) and call convertRSS() method
        /// </summary>
        /// <param name="main_app"></param>
        public void Activate(IMainApp main_app)
        {
            var result = new RSSConverter().convertRSS((main_app as MainApp).Input[0]);
            Logger.Info("\n" + result);
            Logger.Info(string.Format("Press a key to end life cycle of module {0}.", this.GetType().ToString()));
            Console.ReadKey();
        }
    }
}
