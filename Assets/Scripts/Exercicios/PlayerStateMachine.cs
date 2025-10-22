using Ebac.StateMachine;
using UnityEngine;
using static PlayerEnums;

public class PlayerStateMachine : MonoBehaviour
{
    public StateMachine<PlayerStates> stateMachine;

    private void Awake()
    {
        stateMachine = new StateMachine<PlayerStates>();
        stateMachine.Init(); 

        stateMachine.RegisterStates(PlayerStates.IDLE, new PlayerStateIdle(this));
        stateMachine.RegisterStates(PlayerStates.WALK, new PlayerStateWalk(this));
        stateMachine.RegisterStates(PlayerStates.JUMP, new PlayerStateJump(this));

        stateMachine.SwitchState(PlayerStates.IDLE);
    }

    private void Update()
    {
        stateMachine.Update();

        if (Input.GetKeyDown(KeyCode.W))
            stateMachine.SwitchState(PlayerStates.WALK);
        else if (Input.GetKeyDown(KeyCode.Space))
            stateMachine.SwitchState(PlayerStates.JUMP);
        else if (Input.GetKeyDown(KeyCode.S))
            stateMachine.SwitchState(PlayerStates.IDLE);
    }
}
