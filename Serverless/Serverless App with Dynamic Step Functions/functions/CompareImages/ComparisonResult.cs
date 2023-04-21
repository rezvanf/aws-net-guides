namespace CompareImages;

public struct ComparisonResult
{
	public float Left { get; }

	public float Top { get; }

	public float Similarity { get; }

	public ComparisonResult(float left, float top, float similarity)
	{
		Left = left;
		Top = top;
		Similarity = similarity;
	}
}
