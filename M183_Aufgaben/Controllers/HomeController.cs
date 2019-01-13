using Google.Authenticator;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace M183_Aufgaben.Controllers
{
	public class HomeController : Controller
	{
		// GET: Home
		public ActionResult Index()
		{
			return View();
		}

		//---------------Keylogger----------------------------
		public ActionResult Keylogger()
		{
			return View();
		}
		[HttpPost]
		public void KeyloggerPOST()
		{

			string data = Request["sentence"];
		}
		//-----------------------------------------------------


		//---------------Fakelogin----------------------------

		public ActionResult Fakelogin()
		{
			return View();
		}

		[HttpPost]
		public ActionResult FakeloginPOST()
		{
			string username = Request["username"];
			string password = Request["password"];

			return View();
		}

		//-----------------------------------------------------


		//---------------UIRedress----------------------------
		public ActionResult UIRedress()
		{
			return View();
		}
		//-----------------------------------------------------



		//---------------OTP----------------------------

		public ActionResult OTP()
		{
			ViewBag.Lvl = 1;
			return View();
		}

		[HttpPost]
		public ActionResult OTPLogin()
		{
			string username = Request["username"];
			string password = Request["password"];

			if (username == "test" && password == "test")
			{
				var request = (HttpWebRequest)WebRequest.Create("https://rest.nexmo.com/sms/json");

				var secret = "TEST_SECRET";

				var postdata = "api_key=4e00cf30";
				postdata += "&api_secret=4uKNRbtHHDFjapXR";
				postdata += "&to=41766989557";
				postdata += "&from=\"\"NEXMO\"\"";
				postdata += "&text=\"" + secret + "\"";

				var data = Encoding.ASCII.GetBytes(postdata);

				request.Method = "POST";
				request.ContentType = "application/x-www-form-urlencoded";
				request.ContentLength = data.Length;

				using (var stream = request.GetRequestStream())
				{
					stream.Write(data, 0, data.Length);
				}

				var response = (HttpWebResponse)request.GetResponse();

				var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();

				ViewBag.Message = responseString;
				ViewBag.Lvl = 2;
			}
			else
			{
				ViewBag.Message = "Wrong Credentials";
			}
			return View("OTP");
		}
		[HttpPost]
		public void OTPVerify()
		{
			string token = Request["token"];

			if (token == "TEST_SECRET")
			{

			}
			else
			{

			}

		}
		//-----------------------------------------------------


		//---------------TOTP----------------------------

		public ActionResult SetupAuthentication()
		{

			TwoFactorAuthenticator tfa = new TwoFactorAuthenticator();
			var setupInfo = tfa.GenerateSetupCode("TOTP", "yannitsar@gmail.com", "MY_SECRET_KEY", 300, 300);
			string qrCodeImageUrl = setupInfo.QrCodeSetupImageUrl;
			string manualEntrySetupCode = setupInfo.ManualEntryKey;

			ViewBag.Message = "<h2>QR-Code:</h2> <br><br> <img src='" + qrCodeImageUrl + "' /> <br><br><h2>Token for manual entry</h2><br>" + manualEntrySetupCode;

			return View();

		}
		public ActionResult TOTP()
		{
			return View();

		}
		[HttpPost]
		public void TOTPLogin()
		{
			string username = Request["username"];
			string password = Request["password"];
			string token = Request["token"];

			if (username == "test" && password == "test")
			{
				TwoFactorAuthenticator tfa = new TwoFactorAuthenticator();
				bool isCorrectPIN = tfa.ValidateTwoFactorPIN("MY_SECRET_KEY", token);

				if (isCorrectPIN)
				{
					ViewBag.Message = "Login and Token correct";
				}
				else
				{
					ViewBag.Message = "Wrong Credentials";
				}
			}

		}

		//-----------------------------------------------------


		//---------------Vigenere Cipher----------------------------


		public ActionResult VigenereCipher()
		{
			return View();

		}


		//-----------------------------------------------------

		//---------------Caesar Cipher----------------------------

		public ActionResult CaesarCipher()
		{
			return View();

		}
		//-----------------------------------------------------

	}
}