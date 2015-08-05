using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RAMACHAT.Model
{
    public class Room
    {
        public string _id { get; set; }
        public string title { get; set; }
        public bool isNewRoom { get; set; }
        public bool isGroup { get; set; }
        public List<string> members { get; set; }
    }

    public class Sender
    {
        public string _id { get; set; }
        public string username { get; set; }
        public string avatar { get; set; }
    }

    public class File
    {
    }

    public class Message
    {
        public string _id { get; set; }
        public string createdAt { get; set; }
        public File file { get; set; }
        public int type { get; set; }
        public string message { get; set; }
    }
    public class ChatData
    {
        public Room room { get; set; }
        public Sender sender { get; set; }
        public Message message { get; set; }
        public string sequence { get; set; }
    }
    public class ChatResponse
    {
        public bool success { get; set; }
        public int statusCode { get; set; }
        public string message { get; set; }
        public ChatData data { get; set; }
    }
}
