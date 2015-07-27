using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RAMACHAT.Model
{
    public class User
    {
        public string _id { get; set; }
        public string username { get; set; }
        public string avatar { get; set; }
        public bool isOnline { get; set; }
        public bool isFollow { get; set; }
    }
    public class GetFriendResponse
    {
        public bool success { get; set; }
        public int statusCode { get; set; }
        public string message { get; set; }
        public List<User> data { get; set; }
    }
}
