using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RAMACHAT.Model
{
    public class LoginResponse
    {
        public bool success { get; set; }
        public int statusCode { get; set; }
        public string message { get; set; }
        public Data data { get; set; }
    }
    public class Statistic
    {
        public int joiningEvent { get; set; }
        public int followings { get; set; }
        public int followers { get; set; }
    }

    public class Data
    {
        public string _id { get; set; }
        public string username { get; set; }
        public string country { get; set; }
        public int __v { get; set; }
        public bool isOnline { get; set; }
        public Statistic statistic { get; set; }
        public string lastActivedAt { get; set; }
        public string createdAt { get; set; }
        public int role { get; set; }
        public int gender { get; set; }
        public bool isPublicSeen { get; set; }
        public int status { get; set; }
        public string avatar { get; set; }
        public bool isFollow { get; set; }
        public string token { get; set; }
    }
}
