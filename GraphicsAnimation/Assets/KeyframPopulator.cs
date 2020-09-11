using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyframPopulator : MonoBehaviour
{
    public KeyframePool pool;
    public int count;
    Keyframe[] newFrames;
    public void KeyframePopulator(KeyframePool newPool, int newCount)
    {
        pool = newPool;
        count = newCount;
    }

    private void Update()
    {
        if(pool.keyframePool.Length == 0)
        {
            newFrames = new Keyframe[count];
            for (int i = 0; i < count; i++)
            {
                newFrames[i] = new Keyframe(i, Random.Range(1.0f, 3.0f), Random.Range(1, 100));
            }
            pool.keyframePool = newFrames;
        } 
    }
}
