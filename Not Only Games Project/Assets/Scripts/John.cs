using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class John : MonoBehaviour {

    public S_JohnState m_currentState;

    public Vector3 m_newDestination;

    [SerializeField] private bool m_speed;
    [SerializeField] private TheGrid I_grid;
    [SerializeField] private List<Actor> L_actors = new List<Actor>();


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
            case S_JohnState.Idle:
                break;
            case S_JohnState.MoveTowards:
                break;
            case S_JohnState.Patrol:
                break;
            case S_JohnState.RunAway:
                break;
            case S_JohnState.DefaultAction:
                break;
            case S_JohnState.BullyAction:
                break;
        }
    }

    private void ChangeState(S_ActorState currentStae, S_ActorState nextState)
    {
        switch (currentStae)
        {
            case S_ActorState.Idle:
                switch (nextState)
                {
                    case S_ActorState.MoveTowards:
                        break;
                }
                break;
            case S_ActorState.MoveTowards:
                switch (nextState)
                {
                    case S_ActorState.Idle:
                        break;
                    case S_ActorState.Patrol:
                        break;
                    case S_ActorState.RunAway:
                        break;
                    case S_ActorState.BullyActionIndividual:
                        break;
                }
                break;
            case S_ActorState.Patrol:
                switch (nextState)
                {
                    case S_ActorState.DefaultAction:
                        break;
                    case S_ActorState.MoveTowards:
                        break;
                    case S_ActorState.RunAway:
                        break;
                    case S_ActorState.BullyActionIndividual:
                        break;
                }
                break;
            case S_ActorState.RunAway:
                switch (nextState)
                {
                    case S_ActorState.MoveTowards:
                        break;
                    case S_ActorState.Patrol:
                        break;
                    case S_ActorState.DefaultAction:
                        break;
                    case S_ActorState.BullyActionIndividual:
                        break;
                }
                break;
            case S_ActorState.DefaultAction:
                switch (nextState)
                {
                    case S_ActorState.MoveTowards:
                        break;
                    case S_ActorState.RunAway:
                        break;
                    case S_ActorState.BullyActionIndividual:
                        break;
                }
                break;
            case S_ActorState.BullyActionIndividual:
                switch (nextState)
                {
                    case S_ActorState.RunAway:
                        break;
                    case S_ActorState.BullyActionIndividual:
                        break;
                }
                break;
        }
    }
}
