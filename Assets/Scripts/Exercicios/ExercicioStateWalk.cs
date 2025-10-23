using UnityEngine;
using Ebac.StateMachine;

public class ExercicioStateWalk : ExercicioStateBase
{
    public ExercicioStateWalk(ExercicioStateMachine exercicio) : base(exercicio) { }
    public float speedWalk = 2f;

    public override void OnStateEnter(object o = null)
    {
        Debug.Log("Entrou no estado WALK");
    }

    public override void OnStateStay()
    {
        _exercicio.transform.Translate(Vector3.forward * Time.deltaTime * speedWalk);
    }

    public override void OnStateExit()
    {
        Debug.Log("Saiu do estado WALK");
    }
}
