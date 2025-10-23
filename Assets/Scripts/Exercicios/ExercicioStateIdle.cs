using UnityEngine;
using Ebac.StateMachine;

public class ExercicioStateIdle : ExercicioStateBase
{
    public ExercicioStateIdle(ExercicioStateMachine exercicio) : base(exercicio) { }

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
