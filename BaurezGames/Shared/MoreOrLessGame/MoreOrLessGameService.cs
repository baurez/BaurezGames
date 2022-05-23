namespace BaurezGames.Shared.MoreOrLessGame
{
    public class MoreOrLessGameService : IMoreOrLessGameService
    {
        private readonly string _basePath;

        public MoreOrLessGameService(string basePath)
        {
            _basePath = basePath;
        }
        public string NewGame(int minValue, int maxValue)
        {
            var gameId = Guid.NewGuid().ToString("N");
            File.WriteAllText(GetPathToFile(gameId), GenerateRandomValue(minValue, maxValue).ToString());
            return gameId;
        }

        private static int GenerateRandomValue(int minValue, int maxValue)
        {
            Random random = new Random();
            int randomBetweenMinValueAndMaxValue = random.Next(minValue, maxValue);
            return randomBetweenMinValueAndMaxValue;
        }

        public string GetPathToFile(string gameId)
        {
            var pathFile = $"{_basePath}\\Data\\GameOrLess\\";

            if (!Directory.Exists(pathFile))
                Directory.CreateDirectory(pathFile);

            return pathFile + gameId;
        }

        public MoreOrLessGameResult? SubmitResponse(MoreOrLessGameResponse? response)
        {
            if (response is null || response.GameId == string.Empty)
                return null;

            var pathFile = GetPathToFile(response.GameId!);
            if (!File.Exists(pathFile))
                return null;

            var valueInTextFile = File.ReadAllText(pathFile);
            var value = int.Parse(valueInTextFile);
            

            var score = 0;

            if (response.Value < value)
                score = 1;
            if (response.Value > value)
                score = -1;


            return new MoreOrLessGameResult()
            {
                SubmitedResponse = response.Value,
                Score = score
            };
        }

    }
}
