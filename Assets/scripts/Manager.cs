using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Manager : MonoBehaviour
{
    public int tps;
    public int current;
    public int record;
    public GameObject workButton;
    public GameObject helpButton;
    public GameObject slowButton;
    public GameObject fastButton;
    public GameObject pauseButton;
    public GameObject cancelButton;
    public GameObject retakeButton;
    public GameObject centerPanel;
    public GameObject unlockText;
    public GameObject retakeText;
    public Text aText;
    public Text tpsText;
    public Text roundText;
    public Text centerText;
    public AudioSource titleAudio;
    public AudioSource unlockAudio;
    public AudioSource failAudio;
	public Pin pin;
    public GameObject[] supplies;
    public GameObject[] supplyButtons;
    public AudioSource[] workAudio;
	bool moveHomework;
    int a;
    int unlocked;
    int spawning;
    int willSpawn;
    int workAudioIndex;
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
        workAudioIndex = 3;
        descriptions = new string[4];
        descriptions[0] = "Pencils do nearby homework. They're stronger against geometry homework.";
        descriptions[1] = "Erasers do all homework depending on distance. They're stronger against history homework.";
        descriptions[2] = "Bottles do all homework and then refill. They're stronger against chemistry homework.";
        descriptions[3] = "Folders postpone all homework. Only group projects get done when postponed.";
        state = "title";
        //Select();
    }

    // Update is called once per frame
    void Update()
    {
        workButton.SetActive(state == "select" || state == "help");
        helpButton.SetActive(state == "select" || state == "help");
        slowButton.SetActive((state == "select" || state == "help" || state == "pause") && tps > 1);
        fastButton.SetActive((state == "select" || state == "help" || state == "pause") && tps < 32);
        pauseButton.SetActive(state == "work" || state == "pause");
        cancelButton.SetActive(state == "place");
        retakeButton.SetActive(state == "title" || state == "fail");
        centerPanel.SetActive(state == "title" || state == "help" || state == "fail");

        for(int b = 0; b < unlocked; b++)
        {
            supplyButtons[b].SetActive(state == "select" || state == "help");
        }

        if (state == "work"){
            time -= Time.deltaTime;

            if (time <= 0){
				if(moveHomework){
					homeworks = GameObject.FindObjectsOfType<Homework>();
                    /*Temporary[] temporaries = GameObject.FindObjectsOfType<Temporary>();
                    //Debug.Log(temporaries.Length);*/

                    for (int b = 0; b < homeworks.Length; b++){
					    homeworks[b].Turn();
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

                    for (int b = 0; b < pencils.Length; b++)
                    {
                        bool stupidVariable = pencils[b].Turn();
                    }

                    homeworks = GameObject.FindObjectsOfType<Homework>();

                    if (homeworks.Length < 1 && willSpawn < 1)
                    {
                        current++;

                        if(current > record)
                        {
                            UpdateA(current - record);
                            record = current;
                            roundText.text = "Current: " + current + ", Record: " + record;

                            if (record == 2)
                            {
                                //Debug.Log("activating " + unlocked);
                                unlocked++;
                                centerText.text = "Unlocked new supply!";
                                unlockText.SetActive(true);
                                unlockAudio.Play();
                                state = "fail";
                            }
                        }

                        roundText.text = "Current: " + current + ", Record: " + record;
                        NextRound();
                    }
                }
				
                moveHomework = !moveHomework;				
				time = period;
			}
        }
        else
        {
            for (int b = 0; b < 4; b++)
            {
                workAudio[b].Stop();
            }
        }
    }

    public void NextRound()
    {
        moveHomework = true;
        period = 1f / (float)tps;
        time = 0;
        spawning = 0;
        float factor = (float)current;

        while (factor > 1f)
        {
            spawning++;
            factor /= 2;
        }

        willSpawn = spawning;
    }

    public void StartWork()
    {
        if(state == "help")
        {
            centerText.text = "This button starts the homework.";
        }
        else if(state == "select")
        {
            current = 1;
            state = "work";
            workAudioIndex = (workAudioIndex + 1) % 4;
            workAudio[workAudioIndex].Play();
            NextRound();
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
                state = "help";
            }
        }
    }

    public void Select()
    {
        unlockText.SetActive(false);
        retakeText.SetActive(false);
        titleAudio.Stop();
        failAudio.Stop();
        state = "select";
        homeworks = GameObject.FindObjectsOfType<Homework>();

        for (int b = 0; b < homeworks.Length; b++)
        {
            Destroy(homeworks[b].gameObject);
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
            state = "place";
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
            state = "pause";
        }
        else if(state == "pause")
        {
            period = 1f / (float)tps;
            time = 0;
            state = "work";
        }
    }

    public void Fail()
    {
        centerText.text = "You failed.";
        retakeText.SetActive(true);
        failAudio.Play();
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
