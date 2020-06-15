using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Homework : MonoBehaviour
{
	int deskIndex;
    GameObject desks;
	GameObject b;
	Manager _manager;
	
	// Start is called before the first frame update
    void Start()
    {
		deskIndex = 0;
        desks = GameObject.Find("desks");
		b = GameObject.Find("b");
		_manager = GameObject.Find("_manager").GetComponent<Manager>();
    }

    public void Turn()
    {
        if(deskIndex < 1){
			transform.position = desks.transform.GetChild(deskIndex).transform.position;
		}else{
			transform.position = b.transform.position;
			_manager.state = "failed";
		}
		
		deskIndex++;
    }
}
