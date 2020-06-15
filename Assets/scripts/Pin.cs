using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pin : MonoBehaviour
{
    public GameObject[] homeworkPrefabs;
	
	// Start is called before the first frame update
    void Start()
    {
        
    }

    public void Turn()
    {
        Instantiate(homeworkPrefabs[0], transform.position, transform.rotation);
    }
}
