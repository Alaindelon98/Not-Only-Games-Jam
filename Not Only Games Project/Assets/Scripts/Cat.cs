using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cat : Actor {

    private Transform catDestination;
    private Vector3 catOrigin;
    [SerializeField] float speed;
    [SerializeField] PhoneManager I_phoneManager;
    

	// Use this for initialization
	void Start () {
        catOrigin = this.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        this.transform.position = Vector3.MoveTowards(this.transform.position, catDestination.position, speed * Time.deltaTime);

        if (this.transform.position == catDestination.position)
        {
            //animacion de cat idle
        }
        else if (I_phoneManager.m_currentState == s_PhoneState.PicturePosted)
        {
            //animacion corriendo
            this.transform.position = Vector3.MoveTowards(this.transform.position, catDestination.position, speed * Time.deltaTime);
        }


    }
}
