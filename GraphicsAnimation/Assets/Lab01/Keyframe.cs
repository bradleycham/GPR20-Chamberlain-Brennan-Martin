using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Keyframe : MonoBehaviour
{
    public int index;
    public float duration;
    public float durationInv;
    public int data;

    public Keyframe()
    {
        index = 0;
        duration = 0.01f;
        durationInv = 1 / duration;
        data = 1;
    }

    public Keyframe(int newIndex, float newDuration, int newData)
    {
        index = newIndex;
        duration = newDuration;
        durationInv = 1 / duration;
        data = newData;
    }

    public void SetIndex(int i)
    {
        index = i;
    }

    public void SetData(int i)
    {
        data = i;
    }

    public void SetDuration(float newDuration)
    {
        duration = newDuration;
        durationInv = 1 / duration;
    }


}
