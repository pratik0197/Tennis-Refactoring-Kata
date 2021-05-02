namespace Tennis
{
    class ZeroScore : IScoreStrategy
    {
        public string GetScore()
        {
            return "Love";
        }
    }
}
