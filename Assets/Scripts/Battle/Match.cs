public class Match
{
    private MatchData matchData;
    public MatchData MatchData { get { return matchData; } }

    private MatchBattlerStatus playerBattlerStatus;
    private MatchBattlerStatus enemyBattlerStatus;

    public MatchBattlerStatus PlayerBattlerStatus {  get { return playerBattlerStatus; } }
    public MatchBattlerStatus EnemyBattlerStatus {  get { return enemyBattlerStatus; } }

    public Match(MatchData matchData , DeckData playerDeck, DeckData enemyDeck)
    {
        playerBattlerStatus = new MatchBattlerStatus(matchData.lifePoints, matchData.handLimit, playerDeck);
        enemyBattlerStatus = new MatchBattlerStatus(matchData.lifePoints, matchData.handLimit, enemyDeck);
        this.matchData = matchData;
    }

}