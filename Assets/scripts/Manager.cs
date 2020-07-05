using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Manager : MonoBehaviour
{
    public int tps;
    public int round;
    public string state;
    public Text roundText;
    public Button workButton;
	public Pin pin;
	bool moveHomework;
    int spawning;
    int willSpawn;
	float period;
	float time;
	Homework[] homeworks;

    // Start is called before the first frame update
    void Start()
    {
		
    }

    // Update is called once per frame
    void Update()
    {
        if(state == "work"){
            time -= Time.deltaTime;

            if (time <= 0){
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

                    if (willSpawn > 0)
                    {
                        bool stupidVariable = pin.Turn();
                        willSpawn--;
                        homeworks = GameObject.FindObjectsOfType<Homework>();
                    }

                    if (homeworks.Length < 1 && willSpawn < 1)
                    {
                        round++;
                        roundText.text = round + " rounds";
                        workButton.gameObject.SetActive(true);
                        state = "build";
                    }
                }
                else{
                    Pencil[] pencils = GameObject.FindObjectsOfType<Pencil>();

                    for (int a = 0; a < pencils.Length; a++)
                    {
                        bool stupidVariable = pencils[a].Turn();
                    }
                }
				
                moveHomework = !moveHomework;				
				time = period;
			}
	    }
    }

    public int GetSpawning()
    {
        return (spawning);
    }

    public void StartWork()
    {
        workButton.gameObject.SetActive(false);
        moveHomework = true;
        period = 1f / (float)tps;
        time = 0;
        spawning = 0;
        float factor = (float)round;

        while(factor > 1f)
        {
            spawning++;
            factor /= 2;
        }

        willSpawn = spawning;
        state = "work";
    }
}
