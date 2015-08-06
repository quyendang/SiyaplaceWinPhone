using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RAMACHAT.Model
{
    public class HistoryResponse
    {
        public bool success { get; set; }
        public int statusCode { get; set; }
        public string message { get; set; }
        public List<Datum> data { get; set; }
    }
    public class Datum
    {
        public string _id { get; set; }
        public string _userId { get; set; }
        public string updatedAt { get; set; }
        public string title { get; set; }
        public List<Member> members { get; set; }
        public bool isAdmin { get; set; }
        public int unread { get; set; }
        public LastMessage lastMessage { get; set; }
    }
    public class LastMessage
    {
        public string message { get; set; }
        public UserIds _userId { get; set; }
        public string _id { get; set; }
        public string createdAt_format { get; set; }
    }
    public class Member
    {
        public string _id { get; set; }
        public string username { get; set; }
        public string avatar { get; set; }
        public bool isOnline { get; set; }
        public bool isFollow { get; set; }
    }
    public class UserIds
    {
        public string _id { get; set; }
        public string username { get; set; }
        public bool isOnline { get; set; }
        public int gender { get; set; }
        public bool isPublicSeen { get; set; }
    }
}
