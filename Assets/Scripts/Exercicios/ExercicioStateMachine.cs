using Ebac.StateMachine;
using UnityEngine;
using static ExercicioEnums;

public class ExercicioStateMachine : MonoBehaviour
{
    public StateMachine<ExercicioStates> stateMachine;

    private void Awake()
    {
        stateMachine = new StateMachine<ExercicioStates>();
        stateMachine.Init(); 

        stateMachine.RegisterStates(ExercicioStates.IDLE, new ExercicioStateIdle(this));
        stateMachine.RegisterStates(ExercicioStates.WALK, new ExercicioStateWalk(this));
        stateMachine.RegisterStates(ExercicioStates.JUMP, new ExercicioStateJump(this));

        stateMachine.SwitchState(ExercicioStates.IDLE);
    }

    private void Update()
    {
        stateMachine.Update();

        if (Input.GetKeyDown(KeyCode.W))
            stateMachine.SwitchState(ExercicioStates.WALK);
        else if (Input.GetKeyDown(KeyCode.Space))
            stateMachine.SwitchState(ExercicioStates.JUMP);
        else if (Input.GetKeyDown(KeyCode.S))
            stateMachine.SwitchState(ExercicioStates.IDLE);
    }
}
