using Microsoft.ML;
using MuseumBack.Model;
using MuseumBack.Models.DataModels;
using MuseumBack.Models.Trainer;
using System;
using System.IO;
using System.Linq;

namespace MuseumBack.Models.Recognizer
{
    public static class AIRecognize
    {
        static MLContext mlContext = new MLContext(seed: 1);

        public static PredictionDTO Recognize(string base64,string imagepath=null)
        {
            const string assetsRelativePath = @"assets";
            var assetsPath = AITrainer.GetAbsolutePath(assetsRelativePath);
            string imageForPrediction;
            if (imagepath == null)
                imageForPrediction= SaveTempImage(base64, assetsPath);
            else
            {
                imageForPrediction = CopyTestImage(imagepath, assetsPath);
            }
                
            var imageClassifierModelZipFilePath = Path.Combine(assetsPath, "inputs", "model", "imageClassifier.zip");
            try
            {
                var loadedModel = mlContext.Model.Load(imageClassifierModelZipFilePath, out var modelInputSchema);
                var predictionEngine = mlContext.Model.CreatePredictionEngine<InMemoryImageData, ImagePrediction>(loadedModel);
                var imagesToPredict = FileUtils.LoadInMemoryImagesFromDirectory(Path.Combine(assetsPath, "inputs", "toguess"), false);
                var imageToPredict = imagesToPredict.Where(i =>
                i.ImageFileName ==Path.Combine(assetsPath, "inputs", "toguess", imageForPrediction)||i.ImageFileName==imageForPrediction).FirstOrDefault();
                if (imageToPredict != null)
                {
                    var prediction = predictionEngine.Predict(imageToPredict);
                    if (prediction.Score.Max() * 100 > 85)
                    {
                        var maxindext = prediction.Score.ToList().IndexOf(prediction.Score.Max());
                        return new PredictionDTO { Label = prediction.PredictedLabel,Percentage=prediction.Score.Max()*100 };
                    }

                }
            }
            catch (Exception e)
            {

            }
            return null;

        }

        private static string CopyTestImage(string imagepath, string assetsPath)
        {
            string filename= Guid.NewGuid().ToString() + ".jpg";
            File.Move(imagepath, Path.Combine(assetsPath, "inputs", "toguess", filename));
            return filename;
        }

        private static string SaveTempImage(string base64, string assetsPath)
        {
            string filename= Guid.NewGuid().ToString() + ".png";
            File.WriteAllBytes(Path.Combine(assetsPath, "inputs", "toguess", filename), Convert.FromBase64String(base64));
            return filename;
        }
    }
}
