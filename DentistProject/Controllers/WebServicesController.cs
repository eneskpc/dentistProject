using DentistProject.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Services;

namespace DentistProject.Controllers
{
    public class WebServicesController : Controller
    {
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public JsonResult GetAesEncrypt(string plainText)
        {
            string EncryptedText = string.Join("", CryptoHelper.EncrytCustomMD5("OnurSokullu").Split('-'));
            return Json(EncryptedText, JsonRequestBehavior.AllowGet);
        }
    }
}