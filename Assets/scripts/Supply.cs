using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Supply : MonoBehaviour
{
    public int id;
    Desk desk;
    Manager _manager;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Start0(Desk input)
    {
        desk = input;
        _manager = GameObject.Find("_manager").GetComponent<Manager>();

        if(id == 0)
        {
            GetComponent<Pencil>().Start0();
        }else if(id == 1)
        {
            GetComponent<Eraser>().Start0();
        }else if(id == 2)
        {

        }else if(id == 3)
        {

        }else
        {
            Debug.Log("supply ID was " + _manager.GetSupplyId());
        }
    }

    public bool Turn()
    {
        if (id == 0)
        {
            GetComponent<Pencil>().Turn();
        }
        else if (id == 1)
        {
            GetComponent<Eraser>().Turn();
        }
        else if (id == 2)
        {

        }
        else if (id == 3)
        {

        }
        else
        {
            Debug.Log("supply ID was " + _manager.GetSupplyId());
        }

        return (true);
    }

    private void OnMouseUp()
    {
        if (_manager.GetState() == "select")
        {
            _manager.UpdateA(2);
            desk.occupied = false;
            Destroy(gameObject);
        }
    }

    public Manager GetManager()
    {
        return (_manager);
    }

    public Desk GetDesk()
    {
        return (desk);
    }
}
