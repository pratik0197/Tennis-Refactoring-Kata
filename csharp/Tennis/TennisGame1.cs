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
            var tempScore = 0;
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
            if (scoreDifference == 1) score = "Advantage player1";
            else if (scoreDifference == -1) score = "Advantage player2";
            else if (scoreDifference >= 2) score = "Win for player1";
            else score = "Win for player2";
            return score;
        }

        private string EqualScoreResult()
        {
            string score;
            switch (firstPlayerScore)
            {
                case 0:
                    score = "Love-All";
                    break;
                case 1:
                    score = "Fifteen-All";
                    break;
                case 2:
                    score = "Thirty-All";
                    break;
                default:
                    score = "Deuce";
                    break;

            }

            return score;
        }
    }
}

