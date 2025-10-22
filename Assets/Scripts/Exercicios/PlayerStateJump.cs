using UnityEngine;
using Ebac.StateMachine;

public class PlayerStateJump : PlayerStateBase
{
    private float _jumpForce = 5f;
    private bool _hasJumped = false;
    private Rigidbody _rb;

    public PlayerStateJump(PlayerStateMachine Player) : base(Player)
    {
        _rb = Player.GetComponent<Rigidbody>();
    }

    public override void OnStateEnter(object o = null)
    {
        Debug.Log("Entrou no estado JUMP");

        if (_rb != null && !_hasJumped)
        {
            _rb.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
            Debug.Log("Pulando com força: " + _jumpForce);
            _hasJumped = true;
        }
        else
        {
            Debug.LogWarning("Rigidbody não encontrado ou já pulou");
        }
    }

    public override void OnStateStay()
    {
        Debug.Log("Estado JUMP ativo");
    }

    public override void OnStateExit()
    {
        Debug.Log("Saiu do estado JUMP");
        _hasJumped = false;
    }
}
