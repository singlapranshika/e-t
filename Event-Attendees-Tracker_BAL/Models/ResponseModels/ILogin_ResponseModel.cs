using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Event_Attendees_Tracker_BAL.Models.ResponseModels
{
     interface ILogin_ResponseModel
    {
        string RoleName { get; set; }
        int UserID { get; set; } 
        //ILogin_ResponseModel GetInstance();
    }
}
