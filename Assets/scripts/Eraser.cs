using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eraser : MonoBehaviour
{
    Desks desks;
    int[,] damages;
    Desk[] deskArray;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Start0()
    {
        desks = GameObject
            .Find("desks")
            .GetComponent<Desks>();
        //Debug.Log(desks.deskArray[0, 0].occupied);
        damages = new int[4, 4];
        deskArray = GameObject.Find("desks").GetComponent<Desks>().StartPencilEraser(GetComponent<Supply>().GetDesk());
        SetDamage(GetComponent<Supply>().GetDesk().x, GetComponent<Supply>().GetDesk().y, 5);
    }

    public void Turn()
    {
        bool doHomework = false;

        for (int a = 0; a < 5; a++)
        {
            if (deskArray[a] != null)
            {
                if (deskArray[a].GetHomework() != null)
                {
                    doHomework = true;
                    break;
                }
            }
        }

        if (doHomework)
        {
            for(int x = 0; x < 4; x++)
            {
                for(int y = 0; y < 4; y++)
                {
                    //if (desks.deskArray[x, y].GetHomework() != null) {
                        desks
                        .deskArray[x, y]
                        .GetHomework()
                        .TakeDamage(damages[x, y], 1);
                    //}
                }
            }
        }
    }

    public void SetDamage(int x, int y, int damage)
    {
        //Debug.Log(damages[0, 0]);

        if (damages[x, y] >= damage)
        {
            return;
        }

        if (x > 0)
        {
            SetDamage(x - 1, y, damage - 1);
        }

        if (x < 3)
        {
            SetDamage(x + 1, y, damage - 1);
        }

        if (y > 0)
        {
            SetDamage(x, y - 1, damage - 1);
        }

        if (y < 3)
        {
            SetDamage(x, y + 1, damage - 1);
        }
    }
}
