namespace BaurezGames.Shared.MoreOrLessGame;

public interface IMoreOrLessGameService
{
    string NewGame(int minValue, int maxValue);
    string GetPathToFile(string gameId);
    MoreOrLessGameResult? SubmitResponse(MoreOrLessGameResponse? response);
}