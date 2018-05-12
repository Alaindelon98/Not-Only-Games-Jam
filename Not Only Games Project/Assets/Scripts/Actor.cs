using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Actor : MonoBehaviour {

    public S_ActorState m_currentState;

    private Vector3 m_newDestination;

    [SerializeField] private float m_speed;
    [SerializeField] private TheGrid I_grid;
    [SerializeField] private List<Actor> L_actors = new List<Actor>();
    [SerializeField] private John I_john;
    [SerializeField] private GameManager I_gameManager;




    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetMouseButtonUp(0))
        {
            m_currentState = S_ActorState.MoveTowards;
        }

        StateMachine();

	}

    private void StateMachine()
    {
        switch(m_currentState)
        {
            case S_ActorState.Idle:
                break;
            case S_ActorState.MoveTowards:

                if(m_newDestination != null)
                {
                    this.transform.position = Vector3.MoveTowards(this.transform.position, m_newDestination, m_speed * Time.deltaTime);
                }

                break;
            case S_ActorState.Patrol:
                break;
            case S_ActorState.RunAway:
                break;
            case S_ActorState.DefaultAction:
                break;
            case S_ActorState.BullyActionIndividual:
                break;
            case S_ActorState.BullyActionGroupal:
                break;
            case S_ActorState.LookAtSmartphone:
                break;
        }
    }

    private void ChangeState(S_ActorState currentStae, S_ActorState nextState)
    {
        switch(currentStae)
        {
            case S_ActorState.Idle:
                switch(nextState)
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

                        GetRandomDestination();

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

                        GetRandomDestination();

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

                        GetRandomDestination();

                        break;
                    case S_ActorState.RunAway:
                        break;
                    case S_ActorState.BullyActionIndividual:
                        break;
                }
                break;
            case S_ActorState.BullyActionIndividual:
                switch(nextState)
                {
                    case S_ActorState.RunAway:
                        break;
                    case S_ActorState.MoveTowards:
                        break;
                }
                break;
            case S_ActorState.BullyActionGroupal:
                switch (nextState)
                {
                    case S_ActorState.RunAway:
                        break;
                    case S_ActorState.MoveTowards:
                        break;
                }
                break;

            case S_ActorState.LookAtSmartphone:
                switch(nextState)
                {
                    case S_ActorState.RunAway:
                        break;
                    case S_ActorState.MoveTowards:
                        break;
                }
                break;
        }
    }


    private void GetRandomDestination() // gets random position and iterates again if its the same new destination of another actor or any actor is doing an action there
    {
        CNode l_node;
        l_node = I_grid.GetRandomNode();

        foreach (Actor actor in L_actors)
        {
            if (actor != null)
            {


                if (actor.m_newDestination == l_node.position)
                {
                    GetRandomDestination();
                }
                else if (actor.transform.position == m_newDestination)
                {
                    if (actor.m_currentState == S_ActorState.DefaultAction || actor.m_currentState == S_ActorState.BullyActionIndividual)
                    {
                        GetRandomDestination();
                    }
                }
            }
        }

        if (I_john.m_newDestination == l_node.position)
        {
            GetRandomDestination();
        }
        else if (I_john.transform.position == m_newDestination)
        {
            if (I_john.m_currentState == S_JohnState.DefaultAction || I_john.m_currentState == S_JohnState.BullyAction)
            {
                GetRandomDestination();
            }
        }

        m_newDestination = l_node.position;
    }


}
