using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscillator : MonoBehaviour
{
    [SerializeField] Vector3 movementVector;
    [SerializeField] [Range(0,1)] float movementFactor;
    [SerializeField] float period = 2f;
    private Vector3 startingPos;

    private void Start()
    {
        startingPos = transform.position;
    }

    void Update()
    {
        Oscillate();
    }

    void Oscillate()
    {
        if (period > Mathf.Epsilon)
        {
            float cycles = Time.time / period; //if time taken is 4 period is 2 that means there are 2 cycles
            const float tau = Mathf.PI * 2;
            float rawSineWave = Mathf.Sin(cycles * tau); //cycles*tau gives radian value like 2PI etc.

            movementFactor = (rawSineWave + 1f) / 2f; //instead of going from -1 to 1  now it goes from 0 to 1
            //and since with time cycle and sine wave is going to change movement factor is not gonna be stable
            Vector3 offset = movementVector * movementFactor;
            transform.position = startingPos + offset;
        }
    }
}
