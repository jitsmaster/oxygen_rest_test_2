using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Xml.Linq;

namespace TestOxygen.Controllers
{
	public class ValuesController : ApiController
	{
		private const string filePath = @"c:\dev\r_low_level_back_ends.xml";

		protected override void Initialize(HttpControllerContext controllerContext)
		{
			base.Initialize(controllerContext);
		}

		[HttpGet]
		[HttpPut]
		public HttpResponseMessage Files(string url)
		{
			if (Request.Method.Method.ToLowerInvariant() != "put")
			{
				string fileContent = "";
				using (var sr = new StreamReader(filePath))
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
			else
			{
				XDocument doc = XDocument.Load(HttpContext.Current.Request.InputStream);

				doc.Save(filePath);

				return new HttpResponseMessage(HttpStatusCode.OK);
			}
		}
	}
}
