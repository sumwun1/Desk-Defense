using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Temporary : MonoBehaviour
{
    public bool moveBack;
    public float life;
    public Material[] colors;
    int id;

    // Start is called before the first frame update
    void Start()
    {
        if(life <= 0)
        {
            life = GameObject.Find("_manager").GetComponent<Manager>().GetPeriod();
        }
    }

    // Update is called once per frame
    void Update()
    {
        life -= Time.deltaTime;
        
        if(colors.Length >= 4)
        {
            //Debug.Log(life + " " + GameObject.Find("_manager").GetComponent<Manager>().GetPeriod());
            /*Debug.Log(life + " " + GameObject.Find("_manager").GetComponent<Manager>().GetPeriod() * (4f - id / 4f)
            + " " + GameObject.Find("_manager").GetComponent<Manager>().GetPeriod() * (3f - id / 4f));*/
            GetComponent<MeshRenderer>().enabled = life <= GameObject.Find("_manager").GetComponent<Manager>().GetPeriod() * ((4f - id) / 4f)
            && life >= GameObject.Find("_manager").GetComponent<Manager>().GetPeriod() * ((3f - id) / 4f);
        }

        if(life <= 0)
        {
            Destroy(gameObject);
        }

        if (moveBack)
        {
            transform.Translate(64 * Vector3.back.normalized * Time.deltaTime, Space.World);
        }
    }

    public void SetId(int input)
    {
        id = input;
        GetComponent<MeshRenderer>().material = colors[id];
    }
}
