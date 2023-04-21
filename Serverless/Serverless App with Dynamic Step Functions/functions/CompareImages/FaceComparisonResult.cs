using System.Collections.Generic;

namespace CompareImages;

public class FaceComparisonResult
{
	public List<ComparisonResult> Results { get; }

	public string TargetImage { get; }

	public FaceComparisonResult(string targetImage)
	{
		Results = new List<ComparisonResult>();
		TargetImage = targetImage;
	}
}
