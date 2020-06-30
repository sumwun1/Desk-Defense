using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Desks : MonoBehaviour
{
	Desk[][] deskArray;
	
    // Start is called before the first frame update
    void Start()
    {
		deskArray = new Desk[4][4];
		
        for(int a = 0; a < 16; a++){
			Desk desk = transform.GetChild(a).GetComponent<Desk>();
			deskArray[desk.x][desk.y] = desk;
		}
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
