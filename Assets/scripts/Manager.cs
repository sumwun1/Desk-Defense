using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Manager : MonoBehaviour
{
    public int rateIndex;
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
    public GameObject selectedImage;
    public GameObject unlockImage;
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
    public Texture[] textures;
    public AudioSource[] workAudio;
	bool moveHomework;
    bool showOverwrite;
    bool triggerBottle;
    bool triggerFolder;
    int a;
    int unlocked;
    int supplyId;
    int bottleState;
    int folderState;
    /*int spawning;
    int willSpawn;*/
    int workAudioIndex;
	float period;
	float time;
    string state;
    //GameObject selectedSupply;
    float[] tempoFactors;
    string[] rateStrings;
    string[] descriptions;
	Homework[] homeworks;

    // Start is called before the first frame update
    void Start()
    {
        UpdateA(1);
        showOverwrite = false;
        unlocked = 0;
        supplyId = -1;
        workAudioIndex = 3;
        tempoFactors = new float[4];
        tempoFactors[0] = 12f / 13f;
        tempoFactors[1] = 1f;
        tempoFactors[2] = 4f / 3f;
        tempoFactors[3] = 1f;
        rateStrings = new string[6];
        rateStrings[0] = "hyperslow";
        rateStrings[1] = "very slow";
        rateStrings[2] = "slow";
        rateStrings[3] = "fast";
        rateStrings[4] = "very fast";
        rateStrings[5] = "hyperfast";
        descriptions = new string[4];
        descriptions[0] = "Pencils do nearby homework. They're stronger against geometry homework.";
        descriptions[1] = "Erasers do all homework depending on distance. They're stronger against history homework.";
        descriptions[2] = "Bottles do a constant amount divided over all homework, and then refill. They're stronger against chemistry homework.";
        descriptions[3] = "Folders delay all homework. Only group projects get done when delayed.";
        state = "title";
        //Select();
    }

    // Update is called once per frame
    void Update()
    {
        workButton.SetActive(state == "select" || state == "help");
        helpButton.SetActive(state == "select" || state == "help");
        slowButton.SetActive((state == "select" || state == "help" || state == "pause") && rateIndex > 0);
        fastButton.SetActive((state == "select" || state == "help" || state == "pause") && rateIndex < 5);
        pauseButton.SetActive(state == "work" || state == "pause");
        cancelButton.SetActive(state == "place");
        retakeButton.SetActive(state == "title" || state == "fail");
        centerPanel.SetActive(state == "title" || state == "help" || state == "fail");
        selectedImage.SetActive(state == "place");
        period = tempoFactors[workAudioIndex] / Mathf.Pow(2, rateIndex);

        for (int b = 0; b < unlocked; b++)
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

                    bool stupidVariable = pin.Turn();
                    homeworks = GameObject.FindObjectsOfType<Homework>();
                    /*if (willSpawn > 0)
                    {
                        willSpawn--;
                    }*/
                }
                else{
                    Supply[] supplies = GameObject.FindObjectsOfType<Supply>();

                    for (int b = 0; b < supplies.Length; b++)
                    {
                        bool stupidVariable = supplies[b].Turn();
                    }

                    if (triggerBottle && bottleState <= 0)
                    {
                        for(int b = 0; b < homeworks.Length; b++)
                        {
                            homeworks[b].TakeDamage(Mathf.FloorToInt(42f / homeworks.Length), 2);
                        }

                        bottleState = 5;
                    }

                    triggerBottle = false;
                    triggerFolder = false;

                    if (bottleState > 0)
                    {
                        bottleState--;
                    }

                    if (folderState > 0)
                    {
                        folderState--;
                    }

                    homeworks = GameObject.FindObjectsOfType<Homework>();

                    if (homeworks.Length < 1 && pin.GetRemaining() < 1)
                    {
                        current++;

                        if(current > record)
                        {
                            UpdateA(current - record);
                            record = current;
                            roundText.text = "Current: " + current + ", Record: " + record;

                            if (record == 2 || record == 3 || record == 5 || record == 7)
                            { 
                                for (int b = 0; b < 4; b++)
                                {
                                    workAudio[b].Stop();
                                }

                                unlockImage.GetComponent<RawImage>().texture = textures[unlocked];
                                unlocked++;
                                centerText.text = "Unlocked new supply!";
                                unlockText.SetActive(true);
                                unlockImage.SetActive(true);
                                unlockAudio.Play();
                                state = "fail";
                                return;
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
    }

    public void NextRound()
    {
        moveHomework = true;
        time = 0;
        bottleState = 0;
        folderState = 0;
        pin.StartRound();
        /*int[] homeworkIndexes = new int[4];
        int factor = current;

        /*while (factor > 1f)
        {
            spawning++;
            factor /= 2;
        }*/

        //willSpawn = spawning;
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
            showOverwrite = true;
            centerText.text = descriptions[id];
        }
        else if (state == "select")
        {
            if (a >= 2)
            {
                //change sprite of selected supply
                supplyId = id;
                selectedImage.GetComponent<RawImage>().texture = textures[id];
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
        unlockImage.SetActive(false);
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
                rateIndex++;
            }
            else
            {
                rateIndex--;
            }

            tpsText.text = rateStrings[rateIndex];
        }
        else if(state == "help")
        {
            if (faster)
            {
                centerText.text = "This button doubles turns/second.";
            }
            else
            {
                centerText.text = "This button halves turns/second.";
            }
        }
    }

    public void Pause()
    {
        if(state == "work")
        {
            workAudio[workAudioIndex].Pause();
            state = "pause";
        }
        else if(state == "pause")
        {
            //time = 0;
            workAudio[workAudioIndex].UnPause();
            state = "work";
        }
    }

    public void Fail()
    {
        for (int b = 0; b < 4; b++)
        {
            workAudio[b].Stop();
        }

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

    public void TriggerBottle()
    {
        triggerBottle = true;
    }

    public void TriggerFolder()
    {
        triggerFolder = true;
    }

    public int GetSupplyId()
    {
        return (supplyId);
    }

    public string GetState()
    {
        return (state);
    }

    public bool GetOverwrite()
    {
        return (showOverwrite);
    }

    /*public int GetSpawning()
    {
        return (spawning);
    }*/
}
