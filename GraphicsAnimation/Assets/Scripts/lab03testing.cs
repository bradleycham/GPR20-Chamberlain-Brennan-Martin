/*
File name: lab03testing.cs
Purpose:  This program display the information need for lab 03
Contributors: Nick Brennan-Martin and Bradley Chamberlain
Collaborated on one PC
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class lab03testing : MonoBehaviour
{

    public Text controlText;
    public Text controlText2;
    public Text baseText;
    public Text outPutText;

    public bool displayControl1;
    public bool displayControl2;
    public bool displayControl3;
    public bool displayControl4;

    public GameObject control1;
    public GameObject control2;
    public GameObject control3;
    public GameObject control4;

    public enum Blend
    {identity, construct, copy, invert, merge, nearest, linear, linearbounus, cubic,
        split, scale, triangular, binear, bilinear, bicubic};
    public Blend blend;

    public enum Direction { play, reverse, pause };
    public Direction direction;

    public static float t = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //play forward
        if(direction == Direction.play)
        {

            t += Time.deltaTime/ 5;

            if(t >= 1)
            {

                t = 0;
            }

            if (Input.GetKeyDown(KeyCode.P))
            {

                direction = Direction.reverse;
            }

        //play backward
        if (direction == Direction.reverse)
        {

            t -= Time.deltaTime/ 5;

            if (t <= 0)
            {

                t = 1;
            }

            if (Input.GetKeyDown(KeyCode.O))
            {

                direction = Direction.pause;
            }
        }

        //pause
        if (direction == Direction.pause)
        {

            if (Input.GetKeyDown(KeyCode.I))
            {

                direction = Direction.play;
            }
        }

        //change blend
        if (Input.GetKeyUp(KeyCode.Space))
        {

            if (blend != Blend.bicubic)
            {
                blend++;

            }

            else
            {

                blend = Blend.identity;
            }
        }

        //set control active
        if (displayControl1)
        {

            control1.SetActive(true);
        }
        else
        {

            control1.SetActive(false);
        }

        if (displayControl2)
        {

            control2.SetActive(true);
        }
        else
        {

            control2.SetActive(false);
        }

        if (displayControl3)
        {

            control3.SetActive(true);
        }
        else
        {

            control3.SetActive(false);
        }

        if (displayControl4)
        {

            control4.SetActive(true);
        }
        else
        {

            control4.SetActive(false);
        }

        if (blend == Blend.identity)
        {

            control2.SetActive(false);
            control3.SetActive(false);
            control4.SetActive(false);
        }
        if (blend == Blend.construct)
        {

            control2.SetActive(false);
            control3.SetActive(false);
            control4.SetActive(false);
        }
        if (blend == Blend.copy)
        {

            control2.SetActive(false);
            control3.SetActive(false);
            control4.SetActive(false);
        }
        if (blend == Blend.invert)
        {

            control2.SetActive(false);
            control3.SetActive(false);
            control4.SetActive(false);
        }
        if (blend == Blend.merge)
        {

            control2.SetActive(true);
            control3.SetActive(false);
            control4.SetActive(false);
        }
        if (blend == Blend.nearest)
        {

            control2.SetActive(true);
            control3.SetActive(false);
            control4.SetActive(false);
        }
        if (blend == Blend.linear)
        {

            control2.SetActive(true);
            control3.SetActive(false);
            control4.SetActive(false);
        }
        if (blend == Blend.linearbounus)
        {

            control2.SetActive(true);
            control3.SetActive(false);
            control4.SetActive(false);
        }
        if (blend == Blend.cubic)
        {

            control2.SetActive(true);
            control3.SetActive(true);
            control4.SetActive(true);
        }
        if (blend == Blend.split)
        {

            control2.SetActive(true);
            control3.SetActive(false);
            control4.SetActive(false);
        }
        if (blend == Blend.scale)
        {

            control2.SetActive(false);
            control3.SetActive(false);
            control4.SetActive(false);
        }
        if (blend == Blend.triangular)
        {

            control2.SetActive(true);
            control3.SetActive(true);
            control4.SetActive(true);
        }
        if (blend == Blend.binear)
        {

            control2.SetActive(true);
            control3.SetActive(true);
            control4.SetActive(false);
        }
        if (blend == Blend.bilinear)
        {

            control2.SetActive(true);
            control3.SetActive(true);
            control4.SetActive(true);
        }
        if (blend == Blend.bicubic)
        {

            control2.SetActive(true);
            control3.SetActive(true);
            control4.SetActive(true);
        }
    }
}
