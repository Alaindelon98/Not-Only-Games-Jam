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
}
