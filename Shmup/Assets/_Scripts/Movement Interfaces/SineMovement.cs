using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SineMovement : IMoveable {


    // TODO refactor sine wave
    public float frequency = 2.0f; // speed of the sine wave
    public float amplitude = 2.0f; // height of the sine wave
    public float waveOffset = 0.0f; // offset in the wave

    public Vector3 Move(Vector3 position, Vector3 direction, float speed)
    {
        return WaveMove(position, direction, speed);
    }

    // use transform right or transform left for direction
    public Vector3 WaveMove(Vector3 position, Vector3 direction, float speed)
    {
        float randomAmplitude = UnityEngine.Random.Range(0.5f, 2.0f); // was 2
        amplitude = randomAmplitude;

        Vector3 dir = direction * -speed * Time.deltaTime;
        Vector3 wave = Vector3.zero;
        float sineWave = Mathf.Sin(Time.time * frequency + waveOffset);
        wave.y = (sineWave) * amplitude;

       return position += dir + wave * Time.deltaTime;
    }

}
