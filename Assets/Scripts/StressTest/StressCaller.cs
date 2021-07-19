using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StressCaller : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(FireEvent());
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ES.EventSystem.Current.FireEvent(new EventStressTest());
        }
    }

    IEnumerator FireEvent()
    {
        while (true)
        {
            ES.EventSystem.Current.FireEvent(new EventStressTest());
            yield return new WaitForSeconds(0.1f);
        }
    }
}
