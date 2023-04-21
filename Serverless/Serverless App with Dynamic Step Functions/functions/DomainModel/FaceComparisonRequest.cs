namespace DomainModel;

public class FaceComparisonRequest
{
	public string SourceImage { get; }

	public string SourceBucket { get; }

	public string TargetImage { get; }

	public string TargetBucket { get; }

	public FaceComparisonRequest(string sourceImage, string targetImage, string targetBucket, string sourceBucket)
	{
		SourceImage = sourceImage;
		TargetImage = targetImage;
		TargetBucket = targetBucket;
		SourceBucket = sourceBucket;
	}
}
