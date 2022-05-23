namespace BaurezGames.Shared.AdditionGame
{


    public class AdditionGameService : IAdditionGameService
    {
        public AdditionGameService()
        {
        }
        public string NewGame(int minValue, int maxValue, int numberOfElements)
        {
            var result = new List<int>();
            for (int i = 0; i < numberOfElements; i++)
            {
                result.Add(GenerateRandomValue(minValue, maxValue));
            }
            return String.Join(" + ", result);
        }

        private static int GenerateRandomValue(int minValue, int maxValue)
        {
            Random random = new Random();
            int randomBetweenMinValueAndMaxValue = random.Next(minValue, maxValue);
            return randomBetweenMinValueAndMaxValue;
        }
   
    }
}
