using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pencil : MonoBehaviour
{
    public Desk desk;
    bool started;
	Desk[] desks;
	
	// Start is called before the first frame update
    void Start()
    {
        started = false;
    }

    void Update()
    {
        if(GameObject.Find("desks").GetComponent<Desks>().GetStarted() && !started)
        {
            //add desk-setting line
            //Debug.Log(desk.GetIndex());
            desks = GameObject.Find("desks").GetComponent<Desks>().StartPencil(desk);

            for (int a = 1; a < 5; a++)
            {
                for (int b = 1; b < 5; b++)
                {
                    if (desks[b - 1] == null && desks[b] != null)
                    {
                        Swap(b - 1, b);
                    }

                    if (desks[b - 1] != null && desks[b] != null)
                    {
                        if (desks[b - 1].GetIndex() < desks[b].GetIndex())
                        {
                            Swap(b - 1, b);
                        }
                    }
                }
            }

            /*for(int a = 0; a < 5; a++)
            {
                Debug.Log(desks[a].GetIndex());
            }*/

            started = true;
        }
    }

    public void Turn()
    {
        for(int a = 0; a < 5; a++)
        {
            if(desks[a] != null)
            {
                if(desks[a].GetHomework() != null)
                {
                    desks[a].GetHomework().TakeDamage(1, 0);
                    break;
                }
            }
        }
    }
	
	public void Swap(int a, int b){
		Desk desk = desks[a];
		desks[a] = desks[b];
		desks[b] = desk;
	}
}
