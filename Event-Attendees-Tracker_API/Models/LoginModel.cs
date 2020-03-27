using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Event_Attendees_Tracker_API.Models
{
    /// <summary>
    /// Login Model For Web API
    /// </summary>
    public class LoginModel
    {
        //private static LoginModel obj;
        //private LoginModel()
        //{ }
        //public static LoginModel GetInstance()
        //{
        //    if (obj == null)
        //    {
        //        obj = new LoginModel();
        //    }

        //    return obj;
        //}
        #region "Properties"

        public string Email { get; set; }
        public string Password { get; set; }

        #endregion
       
    }
}