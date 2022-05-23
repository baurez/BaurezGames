namespace BaurezGames.Shared.AdditionGame;

public interface IAdditionGameService
{
    string NewGame(int minValue, int maxValue, int numberOfElements);
}