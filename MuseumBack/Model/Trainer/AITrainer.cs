using Microsoft.ML;
using MuseumBack.Models.DataModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using static Microsoft.ML.Transforms.ValueToKeyMappingEstimator;

namespace MuseumBack.Models.Trainer
{
    public static class AITrainer
    {
        public static void Train(string ImagePath)
        {
            const string assetRelativePath = @"assets";
            string assetsPath = GetAbsolutePath(assetRelativePath);
            string outputMlNetModelFilePath = Path.Combine(assetsPath, "inputs","model", "imageClassifier.zip");

            var mlContext = new MLContext(seed: 1);
            #region getImageSet
            IEnumerable<ImageData> images = LoadImagesFromDirectory(folder: ImagePath, useFolderNameAsLabel: true);
            IDataView fullImageDataSet = mlContext.Data.LoadFromEnumerable(images);
            IDataView shuffledFullImageFilePathsDataset = mlContext.Data.ShuffleRows(fullImageDataSet);
            #endregion
            #region loadImages
            IDataView shuffledFullImagesDataset = mlContext.Transforms.Conversion.
                    MapValueToKey(outputColumnName: "LabelAsKey", inputColumnName: "Label", keyOrdinality: KeyOrdinality.ByValue)
                .Append(mlContext.Transforms.LoadRawImageBytes(
                                                outputColumnName: "Image",
                                                imageFolder: ImagePath,
                                                inputColumnName: "ImagePath"))
                .Fit(shuffledFullImageFilePathsDataset)
                .Transform(shuffledFullImageFilePathsDataset);
            #endregion
            #region splitdata
            var trainTestData = mlContext.Data.TrainTestSplit(shuffledFullImagesDataset, testFraction: 0.2);
            IDataView trainDataView = trainTestData.TrainSet;
            IDataView testDataView = trainTestData.TestSet;
            #endregion
            #region Define trainingpipeline
            var pipeline = mlContext.MulticlassClassification.Trainers
                   .ImageClassification(featureColumnName: "Image",
                                        labelColumnName: "LabelAsKey",
                                        validationSet: testDataView)
               .Append(mlContext.Transforms.Conversion.MapKeyToValue(outputColumnName: "PredictedLabel",
                                                                     inputColumnName: "PredictedLabel"));
            #endregion
            //start timer to see how long it took to learn
            var watch = Stopwatch.StartNew();

            #region train
            ITransformer trainedModel = pipeline.Fit(trainDataView);
            watch.Stop();
            var elapsedMs = watch.ElapsedMilliseconds;
            Console.WriteLine($"Training with transfer learning took: {elapsedMs / 1000} seconds");
            #endregion
            EvaluateModel(mlContext, testDataView, trainedModel);
            mlContext.Model.Save(trainedModel, trainDataView.Schema, outputMlNetModelFilePath);
        }
        private static void EvaluateModel(MLContext mlContext, IDataView testDataset, ITransformer trainedModel)
        {
            Console.WriteLine("Making predictions in bulk for evaluating model's quality...");

            // Measuring time
            var watch = Stopwatch.StartNew();

            var predictionsDataView = trainedModel.Transform(testDataset);

            var metrics = mlContext.MulticlassClassification.Evaluate(predictionsDataView, labelColumnName: "LabelAsKey", predictedLabelColumnName: "PredictedLabel");
            Console.WriteLine("TensorFlow DNN Transfer Learning", metrics);

            watch.Stop();
            var elapsed2Ms = watch.ElapsedMilliseconds;

            Console.WriteLine($"Predicting and Evaluation took: {elapsed2Ms / 1000} seconds");
        }
        #region Helpers
        public static string GetAbsolutePath(string relativePath)
            => FileUtils.GetAbsolutePath(typeof(AITrainer).Assembly, relativePath);
        public static IEnumerable<ImageData> LoadImagesFromDirectory(string folder, bool useFolderNameAsLabel = true)
    => FileUtils.LoadImagesFromDirectory(folder, useFolderNameAsLabel)
        .Select(x => new ImageData(x.imagePath, x.label));
        #endregion
    }
}
