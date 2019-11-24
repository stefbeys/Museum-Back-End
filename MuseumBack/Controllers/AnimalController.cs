using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MuseumBack.Model;
using MuseumBack.Models.Recognizer;

namespace MuseumBack.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnimalController : Controller
    {
        [HttpPost]
        public ActionResult GuessImage(ImageDTO image)
        {
            if (image != null && !String.IsNullOrEmpty(image.Base64))
            {
                var result= AIRecognize.Recognize(image.Base64);
                if (result!=null)
                {
                    return new OkObjectResult(result);
                }
                else
                {
                    return new NotFoundResult();
                }
            }
            return new BadRequestObjectResult("This request had no valid image send with it!");
        }

    }
}