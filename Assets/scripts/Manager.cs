using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    public int frequency;
    public string state;
	public Pin pin;
	bool moveHomework;
	float period;
	float time;
	Homework[] homeworks;

    // Start is called before the first frame update
    void Start()
    {
		moveHomework = true;
        period = 1f / (float)frequency;
		time = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(state == "work"){
			time -= Time.deltaTime;
			
			if(time <= 0){
				if(moveHomework){
					homeworks = GameObject.FindObjectsOfType<Homework>();
                    Temporary[] temporaries = GameObject.FindObjectsOfType<Temporary>();
                    //Debug.Log(temporaries.Length);

                    for (int a = 0; a < homeworks.Length; a++){
					    homeworks[a].Turn();
				    }

                    for(int a = 0; a < temporaries.Length; a++)
                    {
                        Destroy(temporaries[a].gameObject);
                    }
					
					pin.Turn();
				}else{
                    Pencil[] pencils = GameObject.FindObjectsOfType<Pencil>();

                    for (int a = 0; a < pencils.Length; a++)
                    {
                        pencils[a].Turn();
                    }
                }
				
                moveHomework = !moveHomework;				
				time = period;
			}
	    }
    }
}
