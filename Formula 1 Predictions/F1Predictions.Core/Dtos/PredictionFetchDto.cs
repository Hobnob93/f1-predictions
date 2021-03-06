namespace F1Predictions.Core.Dtos;

public record PredictionFetchDto
{
    public string Section { get; set; }
    public string Question { get; set; }
    public string[] Predictions { get; set; }
    public string Note { get; set; }
}