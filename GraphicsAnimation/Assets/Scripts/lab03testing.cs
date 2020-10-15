using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class lab03testing : MonoBehaviour
{

    public enum Blend
    {identity, construct, copy, invert, merge, nearest, linear, cubic,
        split, scale, triangular, binear, bilinear, bicubic};
    public Blend blend;

    public enum Direction { play, reverse, pause };
    public Direction direction;

    public float t = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if(direction == Direction.play)
        {

            t += Time.deltaTime;

            if(t >= 1)
            {

                t = 0;
            }
        }

        if (direction == Direction.reverse)
        {

            t -= Time.deltaTime;

            if (t <= 0)
            {

                t = 1;
            }
        }

        if (direction == Direction.pause)
        {

            //nothing
        }
    }
}
