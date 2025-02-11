using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGameState 
{
    public IDeckState GetDeckState(bool player);
    public IHandState GetHandState(bool player);

    public IBoardState GetBoardState();
}
