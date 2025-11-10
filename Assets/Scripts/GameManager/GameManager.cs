using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ebac.Core.Singleton;
using Ebac.StateMachine;
using System.Reflection;

public class GameManager : Singleton<GameManager>
{
    public enum GameStates
    {
        INTRO,
        GAMEPLAY,
        PAUSE,
        WIN,
        LOSE
    }

    public StateMachine<GameStates> stateMachine;

    private void Start()
    {
        Init();

        if (CheckpointManager.Instance.HasCheckpoint())
        {
            Vector3 pos = CheckpointManager.Instance.GetPositionFromLastCheckpoint();

            // Certifique-se de que o jogador já foi instanciado
            if (Player.Instance != null)
            {
                Player.Instance.transform.position = pos;
            }
            else
            {
                Debug.LogWarning("Player.Instance não está disponível no momento do checkpoint.");
            }
        }
    }


    public void Init()
    {
        stateMachine = new StateMachine<GameStates>();

        stateMachine.Init();
        stateMachine.RegisterStates(GameStates.INTRO, new GMStateIntro());
        stateMachine.RegisterStates(GameStates.GAMEPLAY, new StateBase());
        stateMachine.RegisterStates(GameStates.PAUSE, new StateBase());
        stateMachine.RegisterStates(GameStates.WIN, new StateBase());
        stateMachine.RegisterStates(GameStates.LOSE, new StateBase());

        stateMachine.SwitchState(GameStates.INTRO);
    }

}
