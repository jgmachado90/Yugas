using System;
using System.Collections.Generic;

public static class MatchEvents
{
    public static Action OnPauseGame;
    public static Action OnResumeGame;

    //StateEvents

    //InputEvents
    public static Action<int> onSelectCardForPlay;
    public static Action<CardData> onViewCardDetails;

    //Fusion
    public static Action<CardData, int> onMarkForFusion;
    public static Action<CardData, int> onUnMarkForFusion;
    public static Action onCancelFusion;
    public static Action onFusionStart;

    //GeneralMatchEvents

    public static Action<BattleData, int> onDrawCards;
    public static Action<CardData> onSelectCard;
}
