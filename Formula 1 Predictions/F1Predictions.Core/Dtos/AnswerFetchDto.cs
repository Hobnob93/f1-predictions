namespace F1Predictions.Core.Dtos;

public record AnswerFetchDto
{
    public string Question { get; set; }
    public string[] Answers { get; set; }
    public string[] Notes { get; set; }
}