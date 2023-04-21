using System.Collections.Generic;

namespace CompareImages;

public class FaceComparisonReponse
{
	public List<FaceComparisonResult> Results { get; }

	public string SourceImage { get; }

	public bool HasResults => Results.Count > 0;

	public FaceComparisonReponse(string sourceImage)
	{
		SourceImage = sourceImage;
		Results = new List<FaceComparisonResult>();
	}
}
