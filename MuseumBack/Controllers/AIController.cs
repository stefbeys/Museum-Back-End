using Microsoft.AspNetCore.Mvc;
using MuseumBack.Model;
using MuseumBack.Models.Trainer;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;

namespace MuseumBack.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AIController : Controller
    {
        [HttpPost("Start")]
        public ActionResult StartLearning(LearningDTO simplepassword)
        {
            if (simplepassword.Password == "Flamingo007")
            {
                const string assetsRelativePath = @"assets";
                string path = AITrainer.GetAbsolutePath(assetsRelativePath);
                AITrainer.Train(Path.Combine(path, "images"));
                return new OkResult();
            }
            return new UnauthorizedResult();
        }
        [HttpGet("Categories")]
        public ActionResult GetCategories()
        {
            const string assetsRelativePath = @"assets";
            string path = AITrainer.GetAbsolutePath(assetsRelativePath);
            Console.WriteLine(Directory.Exists(path));
            Console.WriteLine(path);
            var names = Directory.GetDirectories(Path.Combine(path, "images")).Select(dir=> Path.GetFileName(dir));
            dynamic obj = new ExpandoObject();
            obj.categories = names;
            return new OkObjectResult(obj);
            
        }
        [HttpPost("addImages")]
        public ActionResult AddTrainingMaterial(List<ImageDTO> images)
        {

            if (images != null)
            {
                const string assetsRelativePath = @"assets";
                string path= AITrainer.GetAbsolutePath(assetsRelativePath);
                foreach (var image in images)
                {
                    if (!Directory.Exists(Path.Combine(path,"images", image.Category))){
                        Directory.CreateDirectory(Path.Combine(path,"images", image.Category));
                    }

                    System.IO.File.WriteAllBytes(Path.Combine(path,"images", image.Category, Guid.NewGuid() + ".png"), Convert.FromBase64String(image.Base64));
                }
                return new OkResult();
            }
            return new BadRequestObjectResult("No Images Send");
        }
    }
}