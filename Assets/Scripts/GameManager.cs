using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public PlayerState playerState;
    public StateMachineManager stateMachineManager;
    public BattleManager battleManager;
    public SelectorManager selectorManager;
    public FusionManager fusionManager;

    private void Awake()
    {
        Instance = this;
    }
}
