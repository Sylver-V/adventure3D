using UnityEngine;
using Ebac.StateMachine;

public class PlayerStateIdle : PlayerStateBase
{
    public PlayerStateIdle(PlayerStateMachine Player) : base(Player) { }

    public override void OnStateEnter(object o = null)
    {
        Debug.Log("Entrou no estado IDLE");
    }

    public override void OnStateStay()
    {
        // Nada por enquanto
    }

    public override void OnStateExit()
    {
        Debug.Log("Saiu do estado IDLE");
    }
}
