using System.Collections.Generic;

namespace CSharpBackend.API.ModeratorClasses
{

public class SpanScore
{
    public int begin { get; set; }
    public int end { get; set; }
    public Score score { get; set; }
}

public class Score
{
    public double value { get; set; }
    public string type { get; set; }
}

public class SummaryScore
{
    public double value { get; set; }
    public string type { get; set; }
}

public class AttributeScores
{
    public List<SpanScore> spanScores { get; set; }
    public SummaryScore summaryScore { get; set; }
}

public class ContentModerationResponse
{
    public AttributeScores attributeScores { get; set; }
    public List<string> languages { get; set; }
    public List<string> detectedLanguages { get; set; }
}

}