using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    public int frequency;
    public string state;
	public Pin pin;
	float period;
	float time;
	Homework[] homeworks;

    // Start is called before the first frame update
    void Start()
    {
        period = 1f / (float)frequency;
		time = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(state == "working"){
			time -= Time.deltaTime;
			
			if(time <= 0){
				homeworks = GameObject.FindObjectsOfType<Homework>();
				pin.Turn();
                				
				for(int a = 0; a < homeworks.Length; a++){
					homeworks[a].Turn();
				}
								
				time = period;
			}
	    }
    }
}
