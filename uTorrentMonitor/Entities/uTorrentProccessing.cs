using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UTorrentAPI;

namespace uTorrentMonitor.Entities
{
    public class uTorrentProccessing
    {

        public void Add_toList(string TorrentName, string TorrentStatus, string TorrentETA, string torrentSize, string torrentDownloadedSize, string torrentDownloadSpeed, string torrentUploadSpeed)
        {
          //  var item1 = new ListViewItem(new[] { TorrentName, TorrentStatus, TorrentETA, torrentSize, torrentDownloadedSize, torrentDownloadSpeed, torrentUploadSpeed });
           // torrenttable.Items.Add(item1);


        }

        private string ConvertRate(long TorrentRateBytes)
        {
            long answer;
            string answerstring;
            if (TorrentRateBytes >= 1000000000)
            {
                answer = TorrentRateBytes / 1024000000;
                answerstring = answer.ToString() + " GB/sec";
            }

            else if (TorrentRateBytes >= 1000000)
            {
                answer = TorrentRateBytes / 1024000;
                answerstring = answer.ToString() + " MB/sec";
            }
            else if (TorrentRateBytes >= 1000)
            {
                answer = TorrentRateBytes / 1024;
                answerstring = answer.ToString() + " KB/sec";

            }
            else
            {
                answerstring = "0 B/sec";

            }




            return answerstring;
        }

        private string ConvertByte(long TorrentSizeBytes)
        {
            string answerstring;
            long answer;
            answerstring = "";

            if (TorrentSizeBytes >= 1000000000)
            {
                answer = TorrentSizeBytes / 1024000000;
                answerstring = answer.ToString() + " GB";
            }

            else if (TorrentSizeBytes >= 1000000)
            {
                answer = TorrentSizeBytes / 1024000;
                answerstring = answer.ToString() + " MB";
            }
            else if (TorrentSizeBytes >= 1000)
            {
                answer = TorrentSizeBytes / 1024;
                answerstring = answer.ToString() + " KB";

            }
            else
            {
                answerstring = "0 Bytes";

            }

            return answerstring;


        }

        public string ConvertETA(int ETAinSeconds)
        {


            if (ETAinSeconds > 0)
            {
                TimeSpan t = TimeSpan.FromSeconds(ETAinSeconds);


                String Days = string.Format("{0:D2}", t.Days);
                string Hours = string.Format("{0:D2}", t.Hours);
                String Minutes = string.Format("{0:D2}", t.Minutes);
                string Seconds = string.Format("{0:D2}", t.Seconds);

                if (Days == "00")
                {
                    if (Hours == "00")
                    {
                        if (Minutes == "00")
                        {
                            return Seconds + "Seconds";
                        }
                        else
                        {
                            return Minutes + "m:" + Seconds + "s";
                        }

                    }
                    else
                    {
                        return Hours + "h:" + Minutes + "m:" + Seconds + "s";
                    }

                }
                else
                {
                    return Days + "d:" + Hours + "h:" + Minutes + "m:" + Seconds + "s";
                }

            }
            return double.PositiveInfinity.ToString();
        }

        public void MainWork()
        {
             
            var Username = Plugin.Instance.Configuration.Username;
            var Password = Plugin.Instance.Configuration.Password;
            var IPddress = Plugin.Instance.Configuration.inp1+"."+ Plugin.Instance.Configuration.inp2+"."+Plugin.Instance.Configuration.inp3+"."+ Plugin.Instance.Configuration.inp4+":"+Plugin.Instance.Configuration.uTorrentPort+"/gui/";
            var SeedingRemove = Plugin.Instance.Configuration.SeedingRemove;

            UTorrentClient client = new UTorrentClient(new System.Uri(IPddress), Username,Password);
            string torrentSize;
            string TorrentDownloadedSize;
            string TorrentDownloadRate;
            string TorrentUploadRate;
            string TorrentEstimateTime;
            string TorrentStatus;

           // torrenttable.Clear();
           // InitializeListView();
           // torrenttable.Visible = true;


            foreach (UTorrentAPI.Torrent CurrentTorrent in client.Torrents)
            {
                torrentSize = "";
                TorrentDownloadedSize = "";
                TorrentDownloadRate = "";
                TorrentUploadRate = "";
                torrentSize = ConvertByte(CurrentTorrent.SizeInBytes);
                TorrentDownloadedSize = ConvertByte(CurrentTorrent.DownloadedBytes);
                TorrentDownloadRate = ConvertRate(CurrentTorrent.DownloadBytesPerSec);
                TorrentUploadRate = ConvertRate(CurrentTorrent.UploadBytesPerSec);
                TorrentEstimateTime = ConvertETA(CurrentTorrent.EtaInSecs);
                TorrentStatus = CurrentTorrent.StatusMessage.ToString();
                if (TorrentStatus.Contains("Seeding") == true )
                {
                    
                    if (SeedingRemove != false) 
                    {
                        client.Torrents.Remove(CurrentTorrent);
                    }
              
                }
                else if (TorrentStatus.Contains("Finished") == true)
                {
                    client.Torrents.Remove(CurrentTorrent);
                    
                }
                else
                {
                   
                    //Add_toList(CurrentTorrent.Name, CurrentTorrent.StatusMessage, TorrentEstimateTime, torrentSize, TorrentDownloadedSize, TorrentDownloadRate, TorrentUploadRate);
                }
            }
        }

    }
}
