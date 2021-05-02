namespace Tennis
{
    class TennisGame1 : ITennisGame
    {
        private int firstPlayerScore = 0;
        private int secondPlayerScore = 0;
        private readonly string player1Name;
        private readonly string player2Name;

        public TennisGame1(string player1Name, string player2Name)
        {
            this.player1Name = player1Name;
            this.player2Name = player2Name;
        }

        public void WonPoint(string playerName)
        {
            if (playerName == "player1")
                firstPlayerScore += 1;
            else
                secondPlayerScore += 1;
        }

        public string GetScore()
        {
            if (EqualScores())
            {
                return EqualScoreResult();
            }
            else if (ThresholdScores())
            {
                return ThresholdScoreResult();
            }

            return NonEqualNonThresholdResults();
        }

        private string NonEqualNonThresholdResults()
        {
            var score = "";
            ScoreManager manager = new ScoreManager();
            manager.ScoreStrategy = GetStrategy(firstPlayerScore);
            score = manager.Score;
            score += "-";
            manager.ScoreStrategy = GetStrategy(secondPlayerScore);
            score += manager.Score;
            return score;
        }

        private IScoreStrategy? GetStrategy(int firstPlayerScore)
        {
            switch (firstPlayerScore)
            {
                case 0:
                    return new ZeroScore();
                case 1:
                    return new OneScore();
                case 2:
                    return new TwoScore();
                case 3:
                    return new ThreeScore();
                default:
                    return null;
            }
        }

        private bool ThresholdScores()
        {
            return firstPlayerScore >= 4 || secondPlayerScore >= 4;
        }

        private bool EqualScores()
        {
            return firstPlayerScore == secondPlayerScore;
        }

        private string ThresholdScoreResult()
        {
            string score;
            var scoreDifference = firstPlayerScore - secondPlayerScore;

            if (scoreDifference == 1) return GetResult(Results.Advantage,player1Name);
            else if (scoreDifference == -1) return GetResult(Results.Advantage,player2Name);
            else if (scoreDifference >= 2) return GetResult(Results.Win,player1Name);
            return GetResult(Results.Win, player2Name);
        }

        private string GetResult(string result, string playerName)
        {
            return $"{result} {playerName}";
        }

        private static class Results
        {
            public static string Advantage = "Advantage";
            public static string Win = "Win for";
        }

            
        private string EqualScoreResult()
        {
            string score;
            ScoreManager manager = new ScoreManager();
            manager.ScoreStrategy = GetStrategy(firstPlayerScore);
            if (manager.ScoreStrategy == null)
                return "Deuce";
            return $"{manager.Score}-All";
        }
    }
}

