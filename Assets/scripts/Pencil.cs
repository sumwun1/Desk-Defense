using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pencil : MonoBehaviour
{
    public Desk desk;
	Desk[] desks;
	
	// Start is called before the first frame update
    void Start()
    {
        //add desk-setting line
		desks = new Desk[5];
    }

    void Turn()
    {
        
    }
}
