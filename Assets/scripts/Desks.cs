using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Desks : MonoBehaviour
{
    bool started;
	Desk[,] deskArray;
	
    // Start is called before the first frame update
    void Start()
    {
        started = false;
		deskArray = new Desk[4, 4];
		
        for(int a = 0; a < 16; a++){
			Desk desk = transform.GetChild(a).GetComponent<Desk>();
			desk.SetIndex(a);
            //Debug.Log(desk.GetIndex());
			deskArray[desk.x, desk.y] = desk;
		}

        started = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
	
	public Desk[] StartPencil(Desk desk)
	{
        //Debug.Log(desk.x + " " + desk.y);
        Desk[] output = new Desk[5];
		output[0] = desk;
		
		if(desk.x < 3){
			output[1] = deskArray[desk.x + 1, desk.y];
		}
		
		if(desk.y < 3){
			output[2] = deskArray[desk.x, desk.y + 1];
		}
		
		if(desk.x > 0){
			output[3] = deskArray[desk.x - 1, desk.y];
		}
		
		if(desk.y > 0){
			output[4] = deskArray[desk.x, desk.y - 1];
		}
		
		return(output);
	}

    public bool GetStarted()
    {
        return (started);
    }
}
