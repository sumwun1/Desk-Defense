using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Manager : MonoBehaviour
{
    public int tps;
    public int round;
    public GameObject workButton;
    public GameObject helpButton;
    public GameObject slowButton;
    public GameObject fastButton;
    public GameObject pauseButton;
    public GameObject cancelButton;
    public GameObject retakeButton;
    public GameObject centerPanel;
    public Text aText;
    public Text tpsText;
    public Text roundText;
    public Text centerText;
	public Pin pin;
    public GameObject[] supplies;
    public GameObject[] supplyButtons;
	bool moveHomework;
    int a;
    int unlocked;
    int spawning;
    int willSpawn;
	float period;
	float time;
    string state;
    GameObject selectedSupply;
    string[] descriptions;
	Homework[] homeworks;

    // Start is called before the first frame update
    void Start()
    {
        UpdateA(1);
        unlocked = 0;
        descriptions = new string[4];
        descriptions[0] = "Pencils do nearby homework. They're stronger against geometry homework.";
        descriptions[1] = "Erasers do all homework depending on distance. They're stronger against history homework.";
        descriptions[2] = "Bottles do all homework and then refill. They're stronger against chemistry homework.";
        descriptions[3] = "Folders postpone all homework. Only group projects get done when postponed.";
        Select();
    }

    // Update is called once per frame
    void Update()
    {
        if(state == "work"){
            time -= Time.deltaTime;

            if (time <= 0){
				if(moveHomework){
					homeworks = GameObject.FindObjectsOfType<Homework>();
                    /*Temporary[] temporaries = GameObject.FindObjectsOfType<Temporary>();
                    //Debug.Log(temporaries.Length);*/

                    for (int a = 0; a < homeworks.Length; a++){
					    homeworks[a].Turn();
				    }

                    if (willSpawn > 0)
                    {
                        bool stupidVariable = pin.Turn();
                        willSpawn--;
                        homeworks = GameObject.FindObjectsOfType<Homework>();
                    }
                }
                else{
                    Pencil[] pencils = GameObject.FindObjectsOfType<Pencil>();

                    for (int a = 0; a < pencils.Length; a++)
                    {
                        bool stupidVariable = pencils[a].Turn();
                    }

                    homeworks = GameObject.FindObjectsOfType<Homework>();

                    if (homeworks.Length < 1 && willSpawn < 1)
                    {
                        round++;
                        roundText.text = round + " assignments";
                        UpdateA(1);

                        if (round == 2)
                        {
                            unlocked++;
                        }

                        Select();
                    }
                }
				
                moveHomework = !moveHomework;				
				time = period;
			}
	    }
    }

    public void StartWork()
    {
        if(state == "help")
        {
            centerText.text = "This button starts the homework.";
        }
        else if(state == "select")
        {
            workButton.SetActive(false);
            helpButton.SetActive(false);
            fastButton.SetActive(false);
            slowButton.SetActive(false);
            pauseButton.SetActive(true);
            moveHomework = true;
            period = 1f / (float)tps;
            time = 0;
            spawning = 0;
            float factor = (float)round;

            for(int a = 0; a < unlocked; a++)
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

    public void SelectSupply(int id)
    {
        if (state == "help")
        {
            centerText.text = descriptions[id];
        }
        else if (state == "select")
        {
            if (a >= 2)
            {
                //change sprite of selected supply
                selectedSupply = supplies[id];
                TogglePlace();
            }
            else
            {
                centerText.text = "Your grades are too low!";
                centerPanel.SetActive(true);
                state = "help";
            }
        }
    }

    public void Select()
    {
        workButton.SetActive(true);
        helpButton.SetActive(true);
        centerPanel.SetActive(false);
        pauseButton.SetActive(false);
        retakeButton.SetActive(false);
        cancelButton.SetActive(false);
        fastButton.SetActive(tps < 32);
        slowButton.SetActive(tps > 1);
        state = "select";
        homeworks = GameObject.FindObjectsOfType<Homework>();

        for (int a = 0; a < homeworks.Length; a++)
        {
            Destroy(homeworks[a].gameObject);
        }

        for (int a = 0; a < unlocked; a++)
        {
            supplyButtons[a].SetActive(true);
        }
    }

    public void TogglePlace()
    {
        if(state == "place")
        {
            Select();
        }
        else if(state == "select")
        {
            workButton.SetActive(false);
            helpButton.SetActive(false);
            fastButton.SetActive(false);
            slowButton.SetActive(false);
            cancelButton.SetActive(true);
            state = "place";

            for (int a = 0; a < unlocked; a++)
            {
                supplyButtons[a].SetActive(false);
            }
        }
    }

    public void ToggleHelp()
    {
        if(state == "help")
        {
            Select();
        }
        else if(state == "select")
        {
            centerText.text = "To buy supplies, click the supply's button and click a desk. Each supply costs 2 A's." + 
                "\n\nClick an already-bought supply to sell it." +
                "\n\nTry clicking some other buttons.";
            centerPanel.SetActive(true);
            state = "help";
        }
    }

    public void ChangeTps(bool faster)
    {
        if(state == "select" || state == "pause")
        {
            if (faster)
            {
                tps *= 2;
            }
            else
            {
                tps /= 2;
            }

            fastButton.SetActive(tps < 32);
            slowButton.SetActive(tps > 1);
            tpsText.text = tps + " turns/s";
        }
        else if(state == "help")
        {
            if (faster)
            {
                centerText.text = "This button doubles turns/s.";
            }
            else
            {
                centerText.text = "This button halves turns/s.";
            }
        }
    }

    public void Pause()
    {
        if(state == "work")
        {
            fastButton.SetActive(tps < 32);
            slowButton.SetActive(tps > 1);
            state = "pause";
        }
        else if(state == "pause")
        {
            fastButton.SetActive(false);
            slowButton.SetActive(false);
            period = 1f / (float)tps;
            time = 0;
            state = "work";
        }
    }

    public void Fail()
    {
        centerText.text = "You failed.";
        retakeButton.SetActive(true);
        pauseButton.SetActive(false);
        centerPanel.SetActive(true);
        state = "fail";
    }

    public void UpdateA(int addition)
    {
        a += addition;
        aText.text = a + " A's";
    }

    public GameObject GetSupply()
    {
        return (selectedSupply);
    }

    public string GetState()
    {
        return (state);
    }

    public int GetSpawning()
    {
        return (spawning);
    }
}
