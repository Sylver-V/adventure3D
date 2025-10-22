using UnityEngine;

public class PlayerStateWalk : PlayerStateBase
{
    public PlayerStateWalk(PlayerStateMachine Player) : base(Player) { }
    public float speedWalk = 2f;

    public override void OnStateEnter(object o = null)
    {
        Debug.Log("Entrou no estado WALK");
    }

    public override void OnStateStay()
    {
        _player.transform.Translate(Vector3.forward * Time.deltaTime * speedWalk);
    }

    public override void OnStateExit()
    {
        Debug.Log("Saiu do estado WALK");
    }
}
