using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGameState 
{
    public IDeckState GetDeckState(Owner owner);
    public IHandState GetHandState(Owner owner);

    public IBoardState GetBoardState();
}
