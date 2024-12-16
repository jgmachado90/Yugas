using System;

public static class MatchEvents
{
    public static Action OnPauseGame;
    public static Action OnResumeGame;

    //StateEvents

    //InputEvents
    public static Action<CardData> onSelectCardForPlay;
    public static Action<CardData> onViewCardDetails;
    public static Action<CardData> onMarkForFusion;

    //GeneralMatchEvents

    public static Action<BattleData, int> onDrawCards;
    public static Action<CardData> onSelectCard;
}
