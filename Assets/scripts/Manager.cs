using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Manager : MonoBehaviour
{
    public int tps;
    public int round;
    public string state;
    public GameObject workButton;
    public GameObject helpButton;
    public GameObject cancelButton;
    public GameObject centerPanel;
    public Text aText;
    public Text roundText;
    public Text centerText;
	public Pin pin;
    public GameObject[] supplies;
    public GameObject[] supplyButtons;
	bool moveHomework;
    int a;
    int spawning;
    int willSpawn;
	float period;
	float time;
    GameObject selectedSupply;
	Homework[] homeworks;

    // Start is called before the first frame update
    void Start()
    {
        UpdateA(1);
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
                        roundText.text = round + " assignments";
                        UpdateA(1);
                        workButton.SetActive(true);
                        helpButton.SetActive(true);
                        state = "select";

                        for (int a = 0; a < 1; a++)
                        {
                            supplyButtons[a].SetActive(true);
                        }
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

    public void SelectSupply(int id)
    {
        if(state == "help")
        {
            centerText.text = "Pencils do nearby homework. They're stronger against geometry homework.";
        }
        else if(state == "select")
        {
            if(a >= 2)
            {
                //change sprite of selected supply
                selectedSupply = supplies[id];
            }
            else
            {
                centerText.text = "Your grades are too low!";
                centerPanel.SetActive(true);
                state = "help";
            }
        }
    }

    public void StartWork()
    {
        if(state == "help")
        {
            centerText.text = "This button starts working on the next assignment.";
        }
        else if(state == "select")
        {
            workButton.SetActive(false);
            helpButton.SetActive(false);
            moveHomework = true;
            period = 1f / (float)tps;
            time = 0;
            spawning = 0;
            float factor = (float)round;

            for(int a = 0; a < 1; a++)
            {
                supplyButtons[a].SetActive(false);
            }

            while (factor > 1f)
            {
                spawning++;
                factor /= 2;
            }

            willSpawn = spawning;
            state = "work";
        }
    }

    public void TogglePlace()
    {
        if(state == "place")
        {
            workButton.SetActive(true);
            helpButton.SetActive(true);
            cancelButton.SetActive(false);
            state = "select";

            for (int a = 0; a < 1; a++)
            {
                supplyButtons[a].SetActive(true);
            }
        }
        else if(state == "select")
        {
            workButton.SetActive(false);
            helpButton.SetActive(false);
            cancelButton.SetActive(true);
            state = "place";

            for (int a = 0; a < 1; a++)
            {
                supplyButtons[a].SetActive(false);
            }
        }
    }

    public void ToggleHelp()
    {
        if(state == "help")
        {
            centerPanel.SetActive(false);
            state = "select";
        }
        else if(state == "select")
        {
            centerText.text = "To buy supplies, click the supply's button and click a desk. Each supply costs 2 A's." + 
                "\n\nClick an already - bought supply to sell it." +
                "\n\nPress alt + tab if you want to quit." +
                "\n\nTry clicking some other buttons.";
            centerPanel.SetActive(true);
            state = "help";
        }
    }

    public void UpdateA(int addition)
    {
        a += addition;
        aText.text = a + " A's";
    }

    public int GetSpawning()
    {
        return (spawning);
    }
}
