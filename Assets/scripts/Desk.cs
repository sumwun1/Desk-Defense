using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Desk : MonoBehaviour
{
    public bool occupied;
	public int x;
	public int y;
	public int index;
    Manager _manager;
	Homework homework;

    private void OnMouseUp()
    {
        if (!occupied && _manager.GetState() == "place" && _manager.GetSupply() != null)
        {
            Instantiate(_manager.GetSupply(), transform.position, transform.rotation).GetComponent<Pencil>().Start0(this);
            _manager.UpdateA(-2);
            _manager.TogglePlace();
            occupied = true;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        _manager = GameObject.Find("_manager").GetComponent<Manager>();
    }
	
	/*public int GetIndex(){
		return(index);
	}
	
	public void SetIndex(int input){
		index = input;
	}*/

    public Homework GetHomework()
	{
		return(homework);
	}
	
	public void SetHomework(Homework input)
	{
		homework = input;
	}
}
