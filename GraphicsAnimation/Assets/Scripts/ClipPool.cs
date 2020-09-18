/*
File name: ClipPool.cs
Purpose: Holds an unsorted and unordered collection of clips 
Contributors: Nick Brennan-Martin and Bradley Chamberlain
Collaborated on one PC
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClipPool : MonoBehaviour
{
    public Clip[] clipPool;
    public int clipCount;

    // return the index of the indended clip
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
    
    // Constructors
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
