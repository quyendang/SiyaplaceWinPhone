using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RAMACHAT.ApiClient
{
    public class ApiLink
    {
        public static string HOSTNAME = "http://128.199.113.218:3000";
        public static String getLoginLink()
        {
            return HOSTNAME + "/users/login";
        }

        public static String getSignUpLink()
        {
            return HOSTNAME + "/users/signup";
        }

        public static String getUserLink()
        {
            return HOSTNAME + "/users/getUserById/";
        }

        public static String getAllRoomLink()
        {
            return HOSTNAME + "/rooms/getAllRooms";
        }

        public static String getAllUserLink()
        {
            return HOSTNAME + "/users/getAllFriends";
        }

        public static String getRoomMessageByRoomIdLink()
        {
            return HOSTNAME + "/messages/getRoomMessagesByRoomId/";
        }

        public static String getRoomMessageByUserIdLink()
        {
            return HOSTNAME + "/messages/getRoomMessagesByUserId/";
        }

        public static String createEventLink()
        {
            return HOSTNAME + "/events/createEvent";
        }

        public static String getEventByIdLink()
        {
            return HOSTNAME + "/events/getEventById/";
        }

        public static String getUploadFileLink()
        {
            return HOSTNAME + "/files/uploadFile";
        }
    }
}
