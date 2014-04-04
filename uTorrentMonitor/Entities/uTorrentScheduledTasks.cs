using MediaBrowser.Common.Net;
using MediaBrowser.Common.ScheduledTasks;
using MediaBrowser.Controller.Entities;
using MediaBrowser.Controller.Library;
using MediaBrowser.Controller.Providers;
using MediaBrowser.Model.Entities;
using MediaBrowser.Model.Logging;
using MediaBrowser.Model.Serialization;
using uTorrentMonitor.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace uTorrentMonitor.ScheduledTasks
{
  
    
    public class uTorrentScheduledTasks : IScheduledTask
    {

       
        public IEnumerable<ITaskTrigger> GetDefaultTriggers()
        {
            return new ITaskTrigger[] 
            { 
                new IntervalTrigger{ Interval = TimeSpan.FromHours(24)},
                new IntervalTrigger{ Interval = TimeSpan.FromMinutes(10)}
            };
        }
     
        public async Task Execute(CancellationToken cancellationToken, IProgress<double> progress)
        {
            uTorrentProccessing TorrentProcessing = new uTorrentProccessing();
            TorrentProcessing.MainWork();
        }

        public string Name
        {
            get { return "Proccess uTorrent Torrents"; }
        }
        public string Description
        {
            get
            {
                return
                    "Proccess uTorrent downloads and remove any finished or seeding (If selected)";
            }
        }
        public string Category
        {
            get
            {
                return "uTorrent Monitor";
            }
        }

    }
}
