using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Web.Http;
using System.Web.Http.Controllers;

namespace TestOxygen.Controllers
{
	public class ValuesController : ApiController
	{
		protected override void Initialize(HttpControllerContext controllerContext)
		{
			base.Initialize(controllerContext);
		}

		[HttpGet]
		public HttpResponseMessage Files(string url)
		{
			string fileContent = "";
			using(var sr = new StreamReader(@"c:\dev\r_low_level_back_ends.xml"))
			{
				fileContent = sr.ReadToEnd();
			}

			// write the response body, set the file name and content-type
			var result = new HttpResponseMessage(HttpStatusCode.OK)
			{
				Content = new ByteArrayContent(Encoding.UTF8.GetBytes(fileContent))
			};
			result.Content.Headers.ContentDisposition =
				new ContentDispositionHeaderValue("attachment")
				{
					FileName = url.Substring(7)
				};

			result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");

			return result;
		}

	}
}
