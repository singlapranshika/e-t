using Newtonsoft.Json;
using RestSharp;
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Event_Attendees_Tracker.Controllers;
using System.Net;
using System.Web.Mvc;
using System.Web.SessionState;
using System.Collections.Generic;
using  Event_Attendees_Tracker_DAL.Models;
using System.Linq;
using System.Web;

namespace Event_Attendees_Tracker.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        [Obsolete]
        public void TestMethod1()
        {
           
            var restClient = new Mock<IRestClient>();
           
           // var req =new Mock<RestRequest>();
            restClient
                .Setup(x => x.ExecuteAsync(
                    It.IsAny<IRestRequest>(),
                    It.IsAny<Action<IRestResponse, RestRequestAsyncHandle>>()))
                .Callback<IRestRequest, Action<IRestResponse, RestRequestAsyncHandle>>((request, callback) =>
                {
                    callback(new RestResponse {StatusCode = HttpStatusCode.OK}, null);
                })
                ;
           
           // var controller=new OrganizerController();
           // ActionResult result = controller.Dashboard() as ViewResult;
         
            restClient.Object.ExecuteAsync(new RestRequest(), (response, handle) =>
            {
                Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            });

        }
    }
}
