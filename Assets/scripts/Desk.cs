using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Desk : MonoBehaviour
{
	public int x;
	public int y;
	int index;
	Homework homework;
	
    // Start is called before the first frame update
    void Start()
    {
        
    }
	
	public int GetIndex(){
		return(index);
	}
	
	public void SetIndex(int input){
		index = input;
	}

    public Homework GetHomework()
	{
		return(homework);
	}
	
	public void SetHomework(Homework input)
	{
		homework = input;
	}
}
