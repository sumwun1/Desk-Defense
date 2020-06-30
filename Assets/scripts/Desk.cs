using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Desk : MonoBehaviour
{
	public int x;
	public int y;
	Homework homework;
	
    // Start is called before the first frame update
    void Start()
    {
        
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
