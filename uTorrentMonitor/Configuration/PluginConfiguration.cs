using MediaBrowser.Model.Plugins;

namespace uTorrentMonitor.Configuration
{
    /// <summary>
    /// Class PluginConfiguration
    /// </summary>
    public class PluginConfiguration : BasePluginConfiguration
    {

        /// <summary>
        /// My plug-in optin
        /// </summary>
        /// <value>The option.</value>
        public int inp1 { get; set; }
        public int inp2 { get; set; }
        public int inp3 { get; set; }
        public int inp4 { get; set; }
        public int uTorrentPort { get; set; }
        public bool SeedingRemove { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }


        /// <summary>
        /// Initializes a new instance of the <see cref="PluginConfiguration" /> class.
        /// </summary>
        public PluginConfiguration()
        {
   
        }
    }
}
