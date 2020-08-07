using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Temporary : MonoBehaviour
{
    //public bool turnBased;
    public float life;

    // Start is called before the first frame update
    void Start()
    {
        if(life <= 0)
        {
            life = 0.5f / Mathf.Pow(2, GameObject.Find("_manager").GetComponent<Manager>().rateIndex);
        }
    }

    // Update is called once per frame
    void Update()
    {
        life -= Time.deltaTime;

        if(life <= 0)
        {
            Destroy(gameObject);
        }
    }
}
