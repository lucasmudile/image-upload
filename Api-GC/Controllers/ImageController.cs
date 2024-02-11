using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api_GC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageController : ControllerBase
    {


        private readonly IConfiguration _confuguration;
        private readonly IWebHostEnvironment _env;

        public ImageController(IConfiguration confuguration, IWebHostEnvironment env)
        {
            _confuguration = confuguration;
            _env = env;
        }





        [Route("SaveFile")]
        [HttpPost]

        public JsonResult SaveFile()
        {
            try
            {
                var httpRequest = Request.Form;
                var postedFile = httpRequest.Files[0];
                string filename = postedFile.FileName;
                var physicalPath = _env.ContentRootPath + "/Photos/" + filename;


                using (var stream = new FileStream(physicalPath, FileMode.Create))
                {
                    postedFile.CopyTo(stream);

                }

                var url = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}";

                return new JsonResult(url+ "/Photos/" + filename);

            }
            catch (Exception)
            {
                return new JsonResult("sasuke-1.png");
            }
        }


    }
}
