using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Temporary : MonoBehaviour
{
    float life;

    // Start is called before the first frame update
    void Start()
    {
        life = 1f / 32f;
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
