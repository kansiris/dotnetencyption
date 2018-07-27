using googlelogin.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;


namespace googlelogin.Controllers
{
    public class HomeController : Controller
    {
        urlparaencrydecry objEDQueryString = new urlparaencrydecry();

        public ActionResult Index()
        {
            // string strName = "1", strAge = "112", strPhone = "1223";
            //strName = txtName.Text;
            //strAge = txtAge.Text;
            //strPhone = txtPhone.Text;

            //string strURL = "/home/contact?";
            //if (HttpContext.Current != null)
            //{
            // string strURLWithData = strURL + "S=" + EncryptQueryString(string.Format("Name={0}&Age={1}&Phone={2}", strName, strAge, strPhone));
            // HttpContext.CurrentHandler.ProcessRequest.strURLWithData();//  .Redirect(strURLWithData);
            // HttpContext.Current.Response.Redirect(strURLWithData);

            //}
            //  string strURLWithData = EncryptQueryString(string.Format("Name={0}&Age={1}&Phone={2}", strName, strAge, strPhone));

            //ViewBag.ksid = strURLWithData;
            // return RedirectToAction("/Home/Contact?S=" + strURLWithData);

            // return RedirectToAction("Contact", "Home", new { S = strURLWithData });
            return View();
        }


        public ActionResult updateprofile(string phone, string fname, string email)

        {


            string strName = phone, strAge = fname, strPhone = email;

            string strURLWithData = EncryptQueryString(string.Format("Name={0}&Age={1}&Phone={2}", strName, strAge, strPhone));

            return RedirectToAction("Contact", "Home", new { S = strURLWithData });


        }




        public string EncryptQueryString(string strQueryString)
        {
            return objEDQueryString.Encrypt(strQueryString, "r0b1nr0y");
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact(string S)
        {

            string strReq = "";
            //strReq = Request.RawUrl;
            //strReq = strReq.Substring(strReq.IndexOf('?') + 1);

            //if (!strReq.Equals(""))
            //{
            //  strReq = DecryptQueryString(strReq);
            strReq = DecryptQueryString(S);
                //Parse the value... this is done is very raw format.. you can add loops or so to get the values out of the query string...
                string[] arrMsgs = strReq.Split('&');


                string[] arrIndMsg;
                string strName = "", strAge = "", strPhone = "";
                arrIndMsg = arrMsgs[0].Split('='); //Get the Name
                strName = arrIndMsg[1].ToString().Trim();
                arrIndMsg = arrMsgs[1].Split('='); //Get the Age
                strAge = arrIndMsg[1].ToString().Trim();
                arrIndMsg = arrMsgs[2].Split('='); //Get the Phone
                strPhone = arrIndMsg[1].ToString().Trim();

            //lblName.Text = strName;
            //lblAge.Text = strAge;
            //lblPhone.Text = strPhone;
            //  ViewBag.Message = "Your contact page.";



            ViewBag.name = strName;
            ViewBag.age = strAge;
            ViewBag.phone = strPhone;
            return View();
        }

             private string DecryptQueryString(string strQueryString)
        {
            return objEDQueryString.Decrypt(strQueryString, "r0b1nr0y");
        }















        public interface IControllerActivator
        {
            IController Create(RequestContext requestContext, Type controllerType);
        }



        //public ActionResult RedirectToGoogle()
        //{
        //    string provider = "google";
        //    string returnUrl = "";
        //    return new ExternalLoginResult(provider, Url.Action("ExternalLoginCallback", new { ReturnUrl = returnUrl }));
        //}
        internal class ExternalLoginResult : ActionResult
        {
            public ExternalLoginResult(string provider, string returnUrl)
            {
                Provider = provider;
                ReturnUrl = returnUrl;
            }

            public string Provider { get; private set; }
            public string ReturnUrl { get; private set; }

            public override void ExecuteResult(ControllerContext context)
            {
                //OpenAuth.RequestAuthentication(Provider, ReturnUrl);
            }
        }

        [EncryptedActionParameter]
        public ActionResult urlen(string ks)
        {
            ViewBag.ksc = ks;
            return RedirectToAction("Contact", "Home");
        }


    }
}