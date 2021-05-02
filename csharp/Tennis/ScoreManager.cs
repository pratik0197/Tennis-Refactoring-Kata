namespace Tennis
{
    class ScoreManager
    {
        public IScoreStrategy ScoreStrategy;

        public string Score => ScoreStrategy.GetScore();
    }
}
