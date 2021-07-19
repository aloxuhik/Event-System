using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StressListener : MonoBehaviour
{
    public Vector3 DirectionToMove;
    public bool SelfMove;
    // Start is called before the first frame update
    void Start()
    {
        if (SelfMove)
            StartCoroutine(Move());
        else
            ES.EventSystem.Current.RegisterListener<EventStressTest>(Stress);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void Stress(EventStressTest eventStress)
    {
        transform.position += DirectionToMove;
    }

    IEnumerator Move()
    {
        while (true)
        {
            transform.position += DirectionToMove;
            yield return new WaitForSeconds(0.1f);
        }
    }
}
