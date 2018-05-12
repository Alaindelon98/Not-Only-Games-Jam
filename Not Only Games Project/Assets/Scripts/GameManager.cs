using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public S_GameState m_currentState;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void StateMachine()
    {
        switch (m_currentState)
        {
            case S_GameState.ActorsMakeActions:
                break;
            case S_GameState.ExitPlayGround:
                break;
            case S_GameState.StartPlayGround:
                break;

        }
    }

    private void ChangeState(S_GameState currentState, S_GameState nextState)
    {
        switch(nextState)
        {
            case S_GameState.ActorsMakeActions:
                switch(currentState)
                {
                    case S_GameState.StartPlayGround:

                        //call manager and instance al actors to make the action

                        break;
                }
                break;
            case S_GameState.ExitPlayGround:

                break;
            case S_GameState.StartPlayGround:

                break;

        }
    }
}
