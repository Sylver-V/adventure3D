using UnityEngine;
using Ebac.StateMachine;

public abstract class PlayerStateBase : StateBase
{
    protected PlayerStateMachine _player;

    public PlayerStateBase(PlayerStateMachine player)
    {
        _player = player;
    }
}
