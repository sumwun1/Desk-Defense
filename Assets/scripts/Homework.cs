using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Homework : MonoBehaviour
{
    public GameObject damageObject;
	int deskIndex;
    int health;
    GameObject desks;
	GameObject b;
	Manager _manager;
	
	// Start is called before the first frame update
    void Start()
    {
		deskIndex = -1;
        desks = GameObject.Find("desks");
		b = GameObject.Find("b");
		_manager = GameObject.Find("_manager").GetComponent<Manager>();
        health = (int)Math.Ceiling((float)_manager.round / _manager.GetSpawning());
    }

    public void TakeDamage(int damage, int type)
    {
        Instantiate(damageObject, transform.position, transform.rotation);
        health -= damage;

        if(health <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void Turn()
    {
        if(deskIndex >= 0)
        {
            if (GetDesk().GetHomework() == this)
            {
                GetDesk().SetHomework(null);
            }
        }

        deskIndex++;

        if (deskIndex < 16){
			transform.position = desks.transform.GetChild(deskIndex).transform.position;
            GetDesk().SetHomework(this);
        }
        else{
			transform.position = b.transform.position;
			_manager.Fail();
		}
    }
	
	public Desk GetDesk()
	{
        if(deskIndex < 0)
        {
            return (null);
        }

		return(desks.transform.GetChild(deskIndex).GetComponent<Desk>());
	}
}
