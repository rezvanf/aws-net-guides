using System;
using System.Threading.Tasks;
using Amazon.Rekognition;
using Amazon.Rekognition.Model;

namespace CompareImages;

public class ImageCompare
{
	private IAmazonRekognition RekognitionClient { get; set; }

	public ImageCompare()
	{
		RekognitionClient = new AmazonRekognitionClient();
	}

	public ImageCompare(IAmazonRekognition client)
	{
		RekognitionClient = client;
	}

	public async Task<FaceComparisonResult> CompareFacesAsync(string sourceBucketName, string sourceImage, string targetBucketName, string targetImage)
	{
		float similarityThreshold = 70f;
		Image image = new Image();
		try
		{
			image.S3Object = new S3Object
			{
				Name = sourceImage,
				Bucket = sourceBucketName
			};
		}
		catch (Exception)
		{
			Console.WriteLine("Failed to load source image: " + sourceImage);
			throw;
		}
		Image image2 = new Image();
		try
		{
			image2.S3Object = new S3Object
			{
				Name = targetImage,
				Bucket = targetBucketName
			};
		}
		catch (Exception)
		{
			Console.WriteLine("Failed to load target image: " + targetImage);
			throw;
		}
		CompareFacesRequest request = new CompareFacesRequest
		{
			SourceImage = image,
			TargetImage = image2,
			SimilarityThreshold = similarityThreshold
		};
		Console.WriteLine("About to start comparison faces");
		FaceComparisonResult faceComparison = new FaceComparisonResult(targetImage);
		CompareFacesResponse compareFacesResponse;
		try
		{
			compareFacesResponse = await RekognitionClient.CompareFacesAsync(request);
		}
		catch (InvalidParameterException ex3)
		{
			Console.WriteLine("Failed to compare faces as the image does not contain a face : " + (object)ex3);
			return faceComparison;
		}
		foreach (CompareFacesMatch faceMatch in compareFacesResponse.FaceMatches)
		{
			BoundingBox boundingBox = faceMatch.Face.BoundingBox;
			Console.WriteLine("Face at " + boundingBox.Left + " " + boundingBox.Top + " matches with " + faceMatch.Similarity + "% confidence.");
			faceComparison.Results.Add(new ComparisonResult(boundingBox.Left, boundingBox.Top, faceMatch.Similarity));
		}
		return faceComparison;
	}
}
