using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    public GameObject TestListener;
    public int NumberOfListeners = 1000;
    public bool SelfMove = false;

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < NumberOfListeners; i++)
        {
            StressListener stressListener = GameObject.Instantiate(TestListener,transform).GetComponent<StressListener>();
            stressListener.SelfMove = SelfMove;
            stressListener.DirectionToMove = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
        }
    }
}
