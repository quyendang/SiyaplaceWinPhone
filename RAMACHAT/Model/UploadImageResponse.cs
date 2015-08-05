using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RAMACHAT.Model
{
    public class Stats
    {
        public int likes { get; set; }
        public int comments { get; set; }
    }

    public class DataUpload
    {
        public int __v { get; set; }
        public string __t { get; set; }
        public string _userId { get; set; }
        public string name { get; set; }
        public string originalName { get; set; }
        public string extension { get; set; }
        public int size { get; set; }
        public string temporaryName { get; set; }
        public string _id { get; set; }
        public Stats statistic { get; set; }
        public int status { get; set; }
        public int type { get; set; }
        public string createdAt { get; set; }
        public string url { get; set; }
    }

    public class UploadImageResponse
    {
        public bool success { get; set; }
        public int statusCode { get; set; }
        public string message { get; set; }
        public DataUpload data { get; set; }
    }
}
