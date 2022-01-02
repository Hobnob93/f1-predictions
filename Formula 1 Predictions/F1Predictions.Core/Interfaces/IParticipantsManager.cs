using F1Predictions.Core.Models;

namespace F1Predictions.Core.Interfaces;

public interface IParticipantsManager
{
    IEnumerable<Participant> GetParticipants();
    Participant GetParticipantByIndex(int index);
}