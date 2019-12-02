using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MuseumBack.Model;
using MuseumBack.Model.DataModels;
using MuseumBack.Models.Recognizer;
using Newtonsoft.Json;
using System.IO;
using MuseumBack.Models.Trainer;

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
                    const string assetsRelativePath = @"assets";
                    var assetsPath = AITrainer.GetAbsolutePath(assetsRelativePath);
                    List<AnimalDTO> res = JsonConvert.DeserializeObject<List<AnimalDTO>>(System.IO.File.ReadAllText(Path.Combine(assetsPath, "animals.json")));
                    return new OkObjectResult(res.Where(r => r.Name == result.Label).FirstOrDefault());
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