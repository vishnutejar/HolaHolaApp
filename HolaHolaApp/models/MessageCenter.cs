using System;
using System.Collections.Generic;
using System.Text;

namespace HolaHolaApp.models
{
   public class MessageCenter
    {
        public string PhoneNumber { get; set; }

        public string Messages { get; set; }
        public string MsgDate { get; set; }

        public MessageCenter() => MsgDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
    }
}
