using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClipPool : MonoBehaviour
{
    public Clip[] clipPool;
    public int clipCount;

    public int GetClip(string clipName)
    {
        for(int i = 0; i < clipCount; i++)
        {
            if(clipPool[i].name == clipName)
            {
                return i;
            }
        }
        return -1;
    }

    public ClipPool()
    {
        clipCount = 0;
    }

    public ClipPool(Clip[] clipRef, int newClipCount)
    {
        clipPool = clipRef;
        clipCount = newClipCount;
    }
}
