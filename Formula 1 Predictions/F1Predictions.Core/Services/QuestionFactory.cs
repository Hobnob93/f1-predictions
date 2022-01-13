using F1Predictions.Core.Enums;
using F1Predictions.Core.Interfaces;
using F1Predictions.Core.Models;

namespace F1Predictions.Core.Services;

public class QuestionFactory : IQuestionFactory
{
    private readonly IGoogleSheets sheets;
    private readonly ISectionManager sectionManager;
    private readonly IProgressService progress;

    public QuestionFactory(IGoogleSheets sheets, ISectionManager sectionManager, IProgressService progress)
    {
        this.sheets = sheets;
        this.sectionManager = sectionManager;
        this.progress = progress;
    }
    
    
    public BaseQuestion GetQuestion()
    {
        var scoringType = sectionManager.GetCurrentQuestionScoringType();
        var sectionIndex = progress.CurrentSectionIndex;
        var questionIndex = progress.CurrentQuestionIndex;
        
        var question = (BaseQuestion) (scoringType switch
        {
            ScoringTypes.Top => sheets.FetchTopQuestion(sectionIndex, questionIndex),
            ScoringTypes.Numerical => sheets.FetchNumericalQuestion(sectionIndex, questionIndex),
            ScoringTypes.HeadToHead => sheets.FetchHeadToHeadQuestion(sectionIndex, questionIndex),
            ScoringTypes.TopMisc => sheets.FetchTopMiscQuestion(sectionIndex, questionIndex),
            _ => null
        });
        
        return question! with
        {
            Section = sheets.FetchSectionTitle(sectionIndex)
        };
    }
}