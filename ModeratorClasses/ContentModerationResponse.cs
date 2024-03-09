using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;


namespace CSharpBackend.API.ModeratorClasses
{


public class ContentModerationResponse
{
    
    [JsonPropertyName("attributeScores")]
    public AttributeScores attributeScores { get; set; }
    
    [JsonPropertyName("languages")]


    public List<string> languages { get; set; }

    [JsonPropertyName("detectedLanguages")]

    public List<string> detectedLanguages { get; set; }
}


public class AttributeScores
{
    [JsonPropertyName("TOXICITY")]
    public Toxicity Toxicity {get;set;}

}


public class Toxicity {

    [JsonPropertyName("spanScores")]

    public List<SpanScore> spanScores { get; set; }

    [JsonPropertyName("summaryScore")]

    public SummaryScore summaryScore { get; set; }

}



public class SpanScore
{
    [JsonPropertyName("begin")]

    public int begin { get; set; }

    [JsonPropertyName("end")]

    public int end { get; set; }

    [JsonPropertyName("score")]

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




}