using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Event_Attendees_Tracker_BAL.Models.ResponseModels
{
    public class Login_ResponseModel
   {
        public string RoleName { get; set; }
        public int UserID { get; set; }
        private static Login_ResponseModel obj=null;

        private Login_ResponseModel(string rolename,int userid)
        {
            this.RoleName = rolename;
            this.UserID = userid;
        }
        public static Login_ResponseModel GetInstance(string rolename,int userid)
        {
            if (obj == null)
            {
                obj = new Login_ResponseModel(rolename,userid);
            }

            return obj;
        }
    }
}
