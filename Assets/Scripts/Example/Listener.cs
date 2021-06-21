using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ES;

public class Listener : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //Receive any Event with type EventExample
        ES.EventSystem.Current.RegisterListener<EventExample>(TestListen);
        
        //Receive Event with type EventExample only with ID equal 2
        ES.EventSystem.Current.RegisterListener<EventExample>(TestListenWithFilter, o=>o.ID == 2);

        //Register this listener as first one to receive event. 
        //This shouldn't be used, because it is promoting bad design. All events should be equal and order of calling shouldn't matter...
        //However it solved my issue once, so I kept it.
        ES.EventSystem.Current.RegisterListenerLead<EventExample>(TestListenerLead);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            ES.EventSystem.Current.UnregisterListener<EventExample>(TestListen);
        }
    }

    private void TestListen(EventExample eventExample)
    {
        Debug.Log(string.Format("Event with ID {0} Received", eventExample.ID));
    }

    private void TestListenWithFilter(EventExample eventExample)
    {
        Debug.Log(string.Format("Filtered Event with ID {0} Received", eventExample.ID));
    }

    private void TestListenerLead(EventExample eventExample)
    {
        Debug.Log(string.Format("This is Fired First ID {0}", eventExample.ID));
    }
}
