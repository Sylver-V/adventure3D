using UnityEngine;
using Ebac.StateMachine;

public abstract class ExercicioStateBase : StateBase
{
    protected ExercicioStateMachine _exercicio;

    public ExercicioStateBase(ExercicioStateMachine exercicio)
    {
        _exercicio = exercicio;
    }
}
