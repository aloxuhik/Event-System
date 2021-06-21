using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ES;

public class Caller : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //Create new Event Info
            EventExample eventExample = new EventExample();

            //Add Info to Event
            eventExample.ID = 1;

            //Fire Event
            ES.EventSystem.Current.FireEvent(eventExample);
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            //Create new Event Info
            EventExample eventExample = new EventExample();

            //Add Info to Event
            eventExample.ID = 2;

            //Fire Event
            ES.EventSystem.Current.FireEvent(eventExample);
        }
    }
}
