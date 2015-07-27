using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RAMACHAT.SocketIO
{
    public class Constant
    {
        public static long DELAY_ON_DRAWER_CLICK = 250L;

        public static int STATUS_CODE_SUCCESS = 200;
        public static int JSON_PARSE_ERROR_CODE = 1002;

        // SOCKET EVENT
        public static String SOCKET_EVENT_JOIN = "join";
        public static String SOCKET_EVENT_ADD = "add";
        public static String SOCKET_EVENT_LEAVE = "leave";
        public static String SOCKET_EVENT_CHANGE_ROOM_TITLE = "changeRoomTitle";
        public static String SOCKET_EVENT_CHAT = "chat";

        // CHAT TYPE
        public static int CHAT_TYPE_TEXT = 1;
        public static int CHAT_TYPE_IMAGE = 2;

        public static int CHAT_TYPE_TEXT_LEFT_WITH_AVATAR = 0;
        public static int CHAT_TYPE_TEXT_RIGHT_WITH_AVATAR = 1;
        public static int CHAT_TYPE_TEXT_LEFT_WITHOUT_AVATAR = 2;
        public static int CHAT_TYPE_TEXT_RIGHT_WITHOUT_AVATAR = 3;
        public static int CHAT_TYPE_IMAGE_RIGHT_WITH_AVATAR = 4;
        public static int CHAT_TYPE_IMAGE_LEFT_WITH_AVATAR = 5;

        //CHOOSE IMAGE FROM....
        public static int PICK_FROM_CAMERA = 1;
        public static int PICK_FROM_FILE = 2;
    }
}
