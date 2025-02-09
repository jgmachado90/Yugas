using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandlerFactory : MonoBehaviour, ISubsystem, IInputHandlerFactory
{
    public IInputHandler CreateBattlePhaseHandler()
    {
        return new BattlePhaseInputHandler();
    }

    public IInputHandler CreateMainPhaseHandler()
    {
        return new MainPhaseInputHandler();
    }

    public void Initialize()
    {

    }

    public void Shutdown()
    {

    }
}
