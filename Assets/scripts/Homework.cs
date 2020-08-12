using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Homework : MonoBehaviour
{
    public GameObject damageColor;
    public GameObject damageAudio;
    public GameObject overwrite;
    public Material[] materials;
    int id;
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
        health = (int)Math.Ceiling(11f * _manager.current / _manager.pin.GetTotal());
    }

    public void TakeDamage(int damage, int type)
    {
        Instantiate(damageColor, transform.position, transform.rotation);
        Instantiate(damageAudio, transform.position, transform.rotation).GetComponent<AudioSource>().Play();

        if(type == id)
        {
            if (_manager.GetOverwrite())
            {
                Instantiate(overwrite, transform.position, transform.rotation);
            }

            if (id >= 0 && id <= 2)
            {
                damage *= 2;
            }else if(id == 3)
            {
                damage = 35;
            }else
            {
                Debug.Log("id and type were " + id);
            }
        }

        health -= damage;

        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void Turn()
    {
        /*if(_manager.current == 41)
        {
            Debug.Log(health);
        }*/

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

    public void SetId(int input)
    {
        id = input;
        GetComponent<MeshRenderer>().material = materials[id];
    }
}
