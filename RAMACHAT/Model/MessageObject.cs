using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RAMACHAT.Model
{
    public class MessageObject
    {
        public bool success { get; set; }
        public int statusCode { get; set; }
        public string message { get; set; }
        public List<DataMessage> data { get; set; }
    }
    public class DataMessage
    {
        public string _id { get; set; }
        public string _roomId { get; set; }
        public string message { get; set; }
        public UserId _userId { get; set; }
        public string createdAt { get; set; }
        public int type { get; set; }
        public int __v { get; set; }
        public Files file { get; set; }
    }
    public class Files
    {
        public string _id { get; set; }
        public string name { get; set; }
        public string originalName { get; set; }
        public string extension { get; set; }
        public int size { get; set; }
        public int type { get; set; }
        public string createdAt { get; set; }
        public string thumbnail { get; set; }
    }
    public class UserId
    {
        public string _id { get; set; }
        public string username { get; set; }
        public string avatar { get; set; }
        public bool isOnline { get; set; }
        public bool isFollow { get; set; }
    }
}
