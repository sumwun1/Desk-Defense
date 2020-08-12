using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pin : MonoBehaviour
{
    public GameObject homeworkPrefab;
    public Manager _manager;
    int total;
    int currentIndex = 0;
    int[] indexes;
    List<int> primes;

    // Start is called before the first frame update
    void Start()
    {
        indexes = new int[4];
        primes = new List<int>();
        primes.Add(2);
        primes.Add(3);
        primes.Add(5);
    }

    public bool Turn()
    {
        if(GetRemaining() > 0)
        {
            while(indexes[currentIndex] <= 0)
            {
                currentIndex = (currentIndex + 1) % 4;
            }

            Instantiate(homeworkPrefab, transform.position, transform.rotation).GetComponent<Homework>().SetId(currentIndex);
            indexes[currentIndex]--;
            currentIndex = (currentIndex + 1) % 4;
        }
        
        return (true);
    }

    public int GetRemaining()
    {
        int output = 0;

        for(int a = 0; a < 4; a++)
        {
            output += indexes[a];
        }

        return (output);
    }

    public void StartRound()
    {
        currentIndex = 0;
        total = 0;
        int factor = _manager.current;

        for (int b = 0; b < primes.Count; b++)
        {
            while (factor % primes[b] == 0)
            {
                indexes[Mathf.Min(b, 3)]++;
                //Debug.Log(_manager.record + " " + _manager.current + " " + factor + " " + primes[b] + " " + b);
                factor /= primes[b];
                //Debug.Log(_manager.record + " " + _manager.current + " " + factor + " " + primes[b]);
            }
        }

        for (int a = 0; a < 4; a++)
        {
            //Debug.Log(_manager.current + " " + a + " " + indexes[a]);
            total += indexes[a];
        }

        if (total <= 0 && factor > 5)
        {
            indexes[3] = 1;
            total = 1;
            primes.Add(factor);
        }
    }

    public int GetTotal()
    {
        return (total);
    }
}
