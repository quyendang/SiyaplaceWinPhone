using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RAMACHAT.Model
{
    public class File
    {
    }
    public class ChatData
    {
        public string roomId { get; set; }
        public string title { get; set; }
        public string message { get; set; }
        public string senderId { get; set; }
        public string senderName { get; set; }
        public bool isNewRoom { get; set; }
        public string createdAt { get; set; }
        public string messageId { get; set; }
        public int type { get; set; }
        public File file { get; set; }
        public string sequence { get; set; }
        public string avatar { get; set; }
    }
    public class ChatResponse
    {
        public bool success { get; set; }
        public int statusCode { get; set; }
        public string message { get; set; }
        public ChatData data { get; set; }
    }
}
