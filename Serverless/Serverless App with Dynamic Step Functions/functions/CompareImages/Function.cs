// <copyright file="Function.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using Amazon.Lambda.Core;
using Amazon.Rekognition;
using DomainModel;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.CamelCaseLambdaJsonSerializer))]

namespace CompareImages
{
    public class Function
    {
        public Function() => this.ImageCompare = new ImageCompare();

        public Function(IAmazonRekognition client) => this.ImageCompare = new ImageCompare(client);

        private ImageCompare ImageCompare { get; set; }

        public async Task<FaceComparisonReponse> FunctionHandler(Root items, ILambdaContext context)
        {
            Console.WriteLine("Request count is {0}", items.Items.Count);
            try
            {
                FaceComparisonReponse response = new FaceComparisonReponse(items.Items.First().SourceImage);
                foreach (FaceComparisonRequest item in items.Items)
                {
                    Console.WriteLine("Target Image is {0}", item.TargetImage);
                    FaceComparisonResult faceComparisonResult = await ImageCompare.CompareFacesAsync(item.SourceBucket, item.SourceImage, item.TargetBucket, item.TargetImage);
                    if (faceComparisonResult.Results.Count > 0)
                    {
                        response.Results.Add(faceComparisonResult);
                    }
                }
                return response;
            }
            catch (Exception arg)
            {
                Console.Error.WriteLine("Exception occured while comparing faces {0}", arg);
                throw;
            }
        }
    }
}
