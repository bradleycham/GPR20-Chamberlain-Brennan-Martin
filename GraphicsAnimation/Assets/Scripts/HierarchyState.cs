/*
File name: HierarchyState.cs
Purpose:  This is a controller-type data stucture that allows for manipulating
Hierarchical Poses
Contributors: Nick Brennan-Martin and Bradley Chamberlain
Collaborated on one PC
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HierarchyState : MonoBehaviour
{

    //elements of a hierarchyState
    public Hierarchy hierarchy;

    public HierarchicalPose samplePose;
    public Matrix4x4[] objectTransformList;
    public Matrix4x4[] localTransformList;
    //public HierarchicalPose localSpacePose;
    //public HierarchicalPose objectSpacePose;

    public HierarchicalPose basePose;
    public HierarchicalPose newPose;
    public HierarchicalPose previousPose;
    public HierarchicalPose nextNextPose;

    //check which kinematic is used
    public bool isKinematic = false;
    //set time and what pose is coming from
    [Range(0,1)]
    public float t;
    public HierarchicalPose towardPose;

    //set which interpolation is used
    public enum Interp {step, nearest, linear, smoothstep };
    public Interp interp;

    public enum Blend
    {
        identity, construct, copy, invert, merge, nearest, linear, linearbounus, cubic,
        split, scale, triangular, binear, bilinear, bicubic
    };
    public Blend blend;

    //constructor
    HierarchyState(Hierarchy h, HierarchicalPose sp, Matrix4x4[] lsp, Matrix4x4[] osp)
    {

        hierarchy = h;
        samplePose = sp;
        localTransformList = lsp;
        objectTransformList = osp;
    }

    // Start is called before the first frame update
    void Start()
    {

        //towardPose = basePose;
    }

    // Update is called once per frame
    void Update()
    {
        if(isKinematic)
        {

            t = lab03testing.t;

            //Interpolation(newPose);

            //if(interp == Interp.nearest)
            //{

            //    Nearest(towardPose, newPose, t);
            //}

            //if (interp == Interp.smoothstep)
            //{

            //    Smoothstep(newPose, towardPose, t);
            //}

            //if (interp == Interp.step)
            //{

            //    Step(towardPose, newPose, t); 
            //}

            //if (interp == Interp.linear)
            //{

            //    Linear(towardPose, newPose, t);
            //}


            if (blend == Blend.identity)
            {

                Identity();
            }
            if (blend == Blend.construct)
            {

                Construct(towardPose, newPose);
            }
            if (blend == Blend.copy)
            {

                Copy(newPose);
            }
            if (blend == Blend.invert)
            {

                InvertPose(towardPose);
            }
            if (blend == Blend.merge)
            {

                Interpolation(newPose);
                Concat(basePose, samplePose);
            }
            if (blend == Blend.nearest)
            {

                Near(towardPose, newPose, t);
            }
            if (blend == Blend.linear)
            {

                Lerp(towardPose, newPose, t);
            }
            if (blend == Blend.linearbounus)
            {

                Lerp2(towardPose, newPose, t);
            }
            if (blend == Blend.cubic)
            {

                Cubic(previousPose, towardPose, newPose, nextNextPose, t);
            }
            if (blend == Blend.split)
            {

                Split(samplePose, towardPose, newPose);
            }
            if (blend == Blend.scale)
            {

                Scale(samplePose, newPose, t);
            }
            if (blend == Blend.triangular)
            {

                Trianglular(samplePose, towardPose, newPose, nextNextPose, t, t);
            }
            if (blend == Blend.binear)
            {

                BiNearest(towardPose, newPose, previousPose, nextNextPose, t, t);
            }
            if (blend == Blend.bilinear)
            {

                BiLinear(previousPose, towardPose, newPose, nextNextPose, t, t);
            }
            if (blend == Blend.bicubic)
            {

                BiCubic(previousPose, towardPose, newPose, nextNextPose, t);
            }


            ConcatenationConversion();//2
            Kinematic();
        }

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

    }

    //lerp interpolation
    public void Linear(HierarchicalPose hp, HierarchicalPose hp2, float t)
    {

        for (int i = 0; i < samplePose.currentPose.Length; i++)
        {

            samplePose.currentPose[i].translation = (1 - t) * hp.currentPose[i].translation + hp2.currentPose[i].translation * t;
            samplePose.currentPose[i].orientation = (1 - t) * hp.currentPose[i].orientation + hp2.currentPose[i].orientation * t;
            samplePose.currentPose[i].scale = (1 - t) * hp.currentPose[i].scale + hp2.currentPose[i].scale * t;
        }
    }

    //nearest interpolation
    public void Nearest(HierarchicalPose hp, HierarchicalPose hp2, float t)
    {

        for (int i = 0; i < samplePose.currentPose.Length; i++)
        {

            if (t < .5f)
            {

                samplePose.currentPose[i].translation = hp.currentPose[i].translation;
                samplePose.currentPose[i].orientation = hp.currentPose[i].orientation;
                samplePose.currentPose[i].scale = hp.currentPose[i].scale;
            }

            else
            {

                samplePose.currentPose[i].translation = hp2.currentPose[i].translation;
                samplePose.currentPose[i].orientation = hp2.currentPose[i].orientation;
                samplePose.currentPose[i].scale = hp2.currentPose[i].scale;
            }
        }
    }

    //smoothstep interpolation
    public void Smoothstep(HierarchicalPose hp, HierarchicalPose hp2, float t)
    {

        for (int i = 0; i < samplePose.currentPose.Length; i++)
        {

            float alpha = t * t * (3 - 2 * t);

            samplePose.currentPose[i].translation = hp.currentPose[i].translation * alpha + (hp2.currentPose[i].translation * (1 - alpha));
            samplePose.currentPose[i].orientation = hp.currentPose[i].orientation * alpha + (hp2.currentPose[i].orientation * (1 - alpha));
            samplePose.currentPose[i].scale = hp.currentPose[i].scale * alpha + (hp2.currentPose[i].scale * (1 - alpha));
        }
    }

    //step interpolation
    public void Step(HierarchicalPose hp, HierarchicalPose hp2, float t)
    {

        for (int i = 0; i < samplePose.currentPose.Length; i++)
        {

            if (t < 1)
            {
                samplePose.currentPose[i].translation = hp.currentPose[i].translation;
                samplePose.currentPose[i].orientation = hp.currentPose[i].orientation;
                samplePose.currentPose[i].scale = hp.currentPose[i].scale;
            }

            else
            {

                samplePose.currentPose[i].translation = hp2.currentPose[i].translation;
                samplePose.currentPose[i].orientation = hp2.currentPose[i].orientation;
                samplePose.currentPose[i].scale = hp2.currentPose[i].scale;
            }

        }
    }

    //interpolation
    public void Interpolation(HierarchicalPose hp)
    {
        // step function, no dt involved
        for(int i = 0; i < samplePose.currentPose.Length; i++)
        {
            samplePose.currentPose[i].translation = hp.currentPose[i].translation;
            samplePose.currentPose[i].orientation = hp.currentPose[i].orientation;
            samplePose.currentPose[i].scale = hp.currentPose[i].scale;

        }
    }

    //concatentation and conversion of the matrices
    public void ConcatenationConversion()
    {

        if (basePose.currentPose.Length == samplePose.currentPose.Length)
            for(int i = 0; i < samplePose.currentPose.Length; i++)
            {
                localTransformList[i] = Matrix4x4.TRS(basePose.currentPose[i].translation + samplePose.currentPose[i].translation,
                    Quaternion.Euler(basePose.currentPose[i].orientation.x + samplePose.currentPose[i].orientation.x,
                    basePose.currentPose[i].orientation.y + samplePose.currentPose[i].orientation.y,
                    basePose.currentPose[i].orientation.z + samplePose.currentPose[i].orientation.z),
                    new Vector3
                    (samplePose.currentPose[i].scale.x * basePose.currentPose[i].scale.x,
                     samplePose.currentPose[i].scale.y * basePose.currentPose[i].scale.y,
                     samplePose.currentPose[i].scale.z * basePose.currentPose[i].scale.z));
            }
        else
            Debug.Log("ERROR: Imbalanced hierarchy lengths"); 
    }


    //forward kinematic
    public void Kinematic()
    {
        for (int i = 0; i < samplePose.currentPose.Length; i++)
        {
            if (hierarchy.treeDepth[i].parentIndex == -1)
            {
                Debug.Log("0");
                objectTransformList[i] = localTransformList[i];
            }
            else // forward kinematics
            {
                Debug.Log("1");
                objectTransformList[i] = objectTransformList[hierarchy.treeDepth[i].parentIndex] * localTransformList[i].transpose;
            }
        }
        //Debug.Log("hello");
        //set the new position
        for (int i = 0; i < samplePose.currentPose.Length; i++)
        {
            samplePose.currentPose[i].transform.position = samplePose.currentPose[i].translation + samplePose.currentPose[hierarchy.treeDepth[i].parentIndex].translation;
            Debug.Log(hierarchy.treeDepth[i].parentIndex);
        }
    }

    public HierarchicalPose Concat(HierarchicalPose lhs, HierarchicalPose rhs)
    {

        for (int i = 0; i < samplePose.currentPose.Length; i++)
        {
            samplePose.currentPose[i].translation = lhs.currentPose[i].translation + rhs.currentPose[i].translation;
            samplePose.currentPose[i].orientation = lhs.currentPose[i].orientation + rhs.currentPose[i].orientation;
            samplePose.currentPose[i].scale = new Vector3
                (lhs.currentPose[i].scale.x * rhs.currentPose[i].scale.x,
                 lhs.currentPose[i].scale.y * rhs.currentPose[i].scale.y,
                 lhs.currentPose[i].scale.z * rhs.currentPose[i].scale.z);
        }

        return samplePose;
    }

    //near blend
    public HierarchicalPose Near(HierarchicalPose currentPose, HierarchicalPose nextPose, float t)
    {

        for (int i = 0; i < samplePose.currentPose.Length; i++)
        {

            if (t < .5f)
            {

                samplePose.currentPose[i].translation = currentPose.currentPose[i].translation;
                samplePose.currentPose[i].orientation = currentPose.currentPose[i].orientation;
                samplePose.currentPose[i].scale = currentPose.currentPose[i].scale;
            }

            else
            {

                samplePose.currentPose[i].translation = nextPose.currentPose[i].translation;
                samplePose.currentPose[i].orientation = nextPose.currentPose[i].orientation;
                samplePose.currentPose[i].scale = nextPose.currentPose[i].scale;
            }
        }

        return samplePose;
    }


    //binearest blend
    public HierarchicalPose BiNearest(HierarchicalPose hI0, HierarchicalPose hT0, HierarchicalPose hI1, HierarchicalPose hT1, float t, float t2)
    {
        HierarchicalPose hPose = new HierarchicalPose(hT0.currentPose.Length);
        for (int i = 0; i < hI1.currentPose.Length; i++)
        {
            SpatialPose pose0 = new SpatialPose();
            if (t < .5f)
            {
                pose0.orientation = hI0.currentPose[i].orientation;
                pose0.translation = hI0.currentPose[i].translation;
                pose0.scale = hI0.currentPose[i].scale;

            }

            else
            {

                pose0.orientation = hT0.currentPose[i].orientation;
                pose0.translation = hT0.currentPose[i].translation;
                pose0.scale = hT0.currentPose[i].scale;
            }


            SpatialPose pose1 = new SpatialPose();

            if (t < .5f)
            {

                pose1.orientation = hI1.currentPose[i].orientation;
                pose1.translation = hI1.currentPose[i].translation;
                pose1.scale = hI1.currentPose[i].scale;
            }
            else
            {

                pose1.orientation = hT1.currentPose[i].orientation;
                pose1.translation = hT1.currentPose[i].translation;
                pose1.scale = hT1.currentPose[i].scale;
            }

            if (t2 < 0.5f)
            {
                hPose.currentPose[i] = pose0;

            }
            else
            {
                hPose.currentPose[i] = pose1;
            }
        }

        return hPose;
    }

    // Composite Linear Interpolation
    public HierarchicalPose Lerp2(HierarchicalPose pose0, HierarchicalPose pose1, float u)
    {
        HierarchicalPose newPose = new HierarchicalPose(pose1.currentPose.Length);
        for (int i = 0; i < samplePose.currentPose.Length; i++)
        {
            newPose.currentPose[i].translation = (1 - u) * pose0.currentPose[i].translation + pose1.currentPose[i].translation * u;
            newPose.currentPose[i].orientation = (1 - u) * pose0.currentPose[i].orientation + pose1.currentPose[i].orientation * u;
            float x = (1 - u) * pose0.currentPose[i].scale.x * pose1.currentPose[i].scale.x * u;
            float y = (1 - u) * pose0.currentPose[i].scale.y * pose1.currentPose[i].scale.y * u;
            float z = (1 - u) * pose0.currentPose[i].scale.z * pose1.currentPose[i].scale.z * u;
            newPose.currentPose[i].scale = new Vector3(x, y, z);
        }

        return newPose;
    }
    // Traditional Linear Interpolation
    public HierarchicalPose Lerp(HierarchicalPose hp, HierarchicalPose hp2, float u)
    {
        HierarchicalPose newPose = new HierarchicalPose(hp.currentPose.Length);

        for (int i = 0; i < samplePose.currentPose.Length; i++)
        {

            newPose.currentPose[i].translation = hp.currentPose[i].translation + (hp2.currentPose[i].translation - hp.currentPose[i].translation) * u;
            newPose.currentPose[i].orientation = hp.currentPose[i].orientation + (hp2.currentPose[i].orientation - hp.currentPose[i].orientation) * u;
            float x = hp.currentPose[i].scale.x * (hp2.currentPose[i].scale.x - hp.currentPose[i].scale.x) * u;
            float y = hp.currentPose[i].scale.y * (hp2.currentPose[i].scale.y - hp.currentPose[i].scale.y) * u;
            float z = hp.currentPose[i].scale.z * (hp2.currentPose[i].scale.z - hp.currentPose[i].scale.z) * u;
            newPose.currentPose[i].scale = new Vector3(x, y, z);
        }
        return newPose;
    }

    //bilinear blend
    public HierarchicalPose BiLinear(HierarchicalPose pose0, HierarchicalPose pose1, HierarchicalPose pose2, HierarchicalPose pose3, float u, float u2)
    {
        return Lerp(Lerp(pose0, pose1, u), Lerp(pose2, pose3, u), u2);
    }

    //split blend
    public HierarchicalPose Split(HierarchicalPose samplePose, HierarchicalPose lhs, HierarchicalPose rhs)
    {

        for (int i = 0; i < samplePose.currentPose.Length; i++)
        {
            samplePose.currentPose[i].translation = lhs.currentPose[i].translation - rhs.currentPose[i].translation;
            samplePose.currentPose[i].orientation = lhs.currentPose[i].orientation - rhs.currentPose[i].orientation;
            samplePose.currentPose[i].scale = new Vector3
                (lhs.currentPose[i].scale.x / rhs.currentPose[i].scale.x,
                 lhs.currentPose[i].scale.y / rhs.currentPose[i].scale.y,
                 lhs.currentPose[i].scale.z / rhs.currentPose[i].scale.z);
        }

        return samplePose;
    }

    //scale blend
    public HierarchicalPose Scale(HierarchicalPose samplePose, HierarchicalPose nextPose, float t)
    {

        for (int i = 0; i < samplePose.currentPose.Length; i++)
        {

            samplePose.currentPose[i].scale = samplePose.currentPose[i].scale * (1 - t) + nextPose.currentPose[i].scale * t;
        }

        return samplePose;
    }

    //triangular blend
    public HierarchicalPose Trianglular(HierarchicalPose samplePose, HierarchicalPose currentPose, HierarchicalPose nextPose, HierarchicalPose nextNextPose, float t1, float t2)
    {

        float t0 = 1 - t1 - t2;

        for (int i = 0; i < samplePose.currentPose.Length; i++)
        {

            samplePose = Concat(Concat(Scale(samplePose, currentPose, t0), Scale(currentPose, nextPose, t1)), Scale(nextPose, nextNextPose, t2));

            //samplePose.currentPose[i].translation = t0 * currentPose.currentPose[i].translation + nextPose.currentPose[i].translation * t1 + nextNextPose.currentPose[i].translation * t2;
            //samplePose.currentPose[i].orientation = t0 * currentPose.currentPose[i].orientation + nextPose.currentPose[i].orientation * t1 + nextNextPose.currentPose[i].orientation * t2;
            //samplePose.currentPose[i].scale = t0 * currentPose.currentPose[i].scale + nextPose.currentPose[i].scale * t1 + nextNextPose.currentPose[i].scale * t2;
        }

        return samplePose;
    }

    //cubic blend
    public HierarchicalPose Cubic(HierarchicalPose preInit, HierarchicalPose init, HierarchicalPose final, HierarchicalPose postFinal, float u)
    {
        // ((-1/2)preInit + (3/2)init - (3/2)final + (1/2)postFinal)x^3 
        //  + (preInit - (5/2)init + 2final - 1/2postFinal)x^2
        //  + (-1/2 preInit + 1/2 final)x + init
        HierarchicalPose hPose = new HierarchicalPose(init.currentPose.Length);

        for (int i = 0; i < init.currentPose.Length; i++)
        {
            Vector3 translation = (1 / 2 * preInit.currentPose[i].translation + 3 / 2 * init.currentPose[i].translation + 1 / 2 * postFinal.currentPose[i].translation) * Mathf.Pow(u, 3)
                                + (preInit.currentPose[i].translation - 2 * final.currentPose[i].translation - 1 / 2 * final.currentPose[i].translation) * Mathf.Pow(u, 2)
                                + (-1 / 2 * preInit.currentPose[i].translation + 1 / 2 * final.currentPose[i].translation) * u + init.currentPose[i].translation;

            Vector3 orientation = (1 / 2 * preInit.currentPose[i].orientation + 3 / 2 * init.currentPose[i].orientation + 1 / 2 * postFinal.currentPose[i].orientation) * Mathf.Pow(u, 3)
                                + (preInit.currentPose[i].orientation - 2 * final.currentPose[i].orientation - 1 / 2 * final.currentPose[i].orientation) * Mathf.Pow(u, 2)
                                + (-1 / 2 * preInit.currentPose[i].orientation + 1 / 2 * final.currentPose[i].orientation) * u + init.currentPose[i].orientation;

            float scaleX = (1 / 2 * preInit.currentPose[i].scale.x * 3 / 2 * init.currentPose[i].scale.x * 1 / 2 * postFinal.currentPose[i].scale.x) * Mathf.Pow(u, 3)
                                * (preInit.currentPose[i].scale.x - 2 * final.currentPose[i].scale.x / 1 / 2 * final.currentPose[i].scale.x) * Mathf.Pow(u, 2)
                                * (-1 / 2 * preInit.currentPose[i].scale.x * 1 / 2 * final.currentPose[i].scale.x) * u + init.currentPose[i].scale.x;

            float scaleY = (1 / 2 * preInit.currentPose[i].scale.y * 3 / 2 * init.currentPose[i].scale.y * 1 / 2 * postFinal.currentPose[i].scale.y) * Mathf.Pow(u, 3)
                                * (preInit.currentPose[i].scale.y - 2 * final.currentPose[i].scale.y / 1 / 2 * final.currentPose[i].scale.y) * Mathf.Pow(u, 2)
                                * (-1 / 2 * preInit.currentPose[i].scale.y * 1 / 2 * final.currentPose[i].scale.y) * u + init.currentPose[i].scale.y;

            float scaleZ = (1 / 2 * preInit.currentPose[i].scale.z * 3 / 2 * init.currentPose[i].scale.z * 1 / 2 * postFinal.currentPose[i].scale.z) * Mathf.Pow(u, 3)
                                * (preInit.currentPose[i].scale.z - 2 * final.currentPose[i].scale.z / 1 / 2 * final.currentPose[i].scale.z) * Mathf.Pow(u, 2)
                                * (-1 / 2 * preInit.currentPose[i].scale.z * 1 / 2 * final.currentPose[i].scale.z) * u + init.currentPose[i].scale.z;

            Vector3 scale = new Vector3(scaleX, scaleY, scaleZ);
            SpatialPose newPose = new SpatialPose();

            newPose.translation = translation;
            newPose.orientation = orientation;
            newPose.scale = scale;
            hPose.currentPose[i] = newPose;
        }
        Debug.Log("cubic");
        return hPose;
    }

    //bicubic blend
    public HierarchicalPose BiCubic(HierarchicalPose preInit, HierarchicalPose init, HierarchicalPose final, HierarchicalPose postFinal, float u)
    {
        // ((-1/2)preInit + (3/2)init - (3/2)final + (1/2)postFinal)x^3 
        //  + (preInit - (5/2)init + 2final - 1/2postFinal)x^2
        //  + (-1/2 preInit + 1/2 final)x + init
        HierarchicalPose hPose = new HierarchicalPose(init.currentPose.Length);

        for (int i = 0; i < init.currentPose.Length; i++)
        {
            Vector3 translation = (1 / 2 * preInit.currentPose[i].translation + 3 / 2 * init.currentPose[i].translation + 1 / 2 * postFinal.currentPose[i].translation) * Mathf.Pow(u, 3)
                                + (preInit.currentPose[i].translation - 2 * final.currentPose[i].translation - 1 / 2 * final.currentPose[i].translation) * Mathf.Pow(u, 2)
                                + (-1 / 2 * preInit.currentPose[i].translation + 1 / 2 * final.currentPose[i].translation) * u + init.currentPose[i].translation;
            Vector3 orientation = (1 / 2 * preInit.currentPose[i].orientation + 3 / 2 * init.currentPose[i].orientation + 1 / 2 * postFinal.currentPose[i].orientation) * Mathf.Pow(u, 3)
                                + (preInit.currentPose[i].orientation - 2 * final.currentPose[i].orientation - 1 / 2 * final.currentPose[i].orientation) * Mathf.Pow(u, 2)
                                + (-1 / 2 * preInit.currentPose[i].orientation + 1 / 2 * final.currentPose[i].orientation) * u + init.currentPose[i].orientation;
            float scaleX = (1 / 2 * preInit.currentPose[i].scale.x * 3 / 2 * init.currentPose[i].scale.x * 1 / 2 * postFinal.currentPose[i].scale.x) * Mathf.Pow(u, 3)
                                * (preInit.currentPose[i].scale.x - 2 * final.currentPose[i].scale.x / 1 / 2 * final.currentPose[i].scale.x) * Mathf.Pow(u, 2)
                                * (-1 / 2 * preInit.currentPose[i].scale.x * 1 / 2 * final.currentPose[i].scale.x) * u + init.currentPose[i].scale.x;
            float scaleY = (1 / 2 * preInit.currentPose[i].scale.y * 3 / 2 * init.currentPose[i].scale.y * 1 / 2 * postFinal.currentPose[i].scale.y) * Mathf.Pow(u, 3)
                                * (preInit.currentPose[i].scale.y - 2 * final.currentPose[i].scale.y / 1 / 2 * final.currentPose[i].scale.y) * Mathf.Pow(u, 2)
                                * (-1 / 2 * preInit.currentPose[i].scale.y * 1 / 2 * final.currentPose[i].scale.y) * u + init.currentPose[i].scale.y;
            float scaleZ = (1 / 2 * preInit.currentPose[i].scale.z * 3 / 2 * init.currentPose[i].scale.z * 1 / 2 * postFinal.currentPose[i].scale.z) * Mathf.Pow(u, 3)
                                * (preInit.currentPose[i].scale.z - 2 * final.currentPose[i].scale.z / 1 / 2 * final.currentPose[i].scale.z) * Mathf.Pow(u, 2)
                                * (-1 / 2 * preInit.currentPose[i].scale.z * 1 / 2 * final.currentPose[i].scale.z) * u + init.currentPose[i].scale.z;
            Vector3 scale = new Vector3(scaleX, scaleY, scaleZ);
            SpatialPose newPose = new SpatialPose();
            newPose.translation = translation;
            newPose.orientation = orientation;
            newPose.scale = scale;
            hPose.currentPose[i] = newPose;
        }
        return hPose;
    }

    //identity blend
    public HierarchicalPose Identity()
    {

        for (int i = 0; i < samplePose.currentPose.Length; i++)
        {
            SpatialPose temp = new SpatialPose(Matrix4x4.identity, Vector3.one, Vector3.one, Vector3.zero);
            samplePose.currentPose[i] = temp;
        }

        return samplePose;
    }

    //construct blend
    public HierarchicalPose Construct(HierarchicalPose controlPose, HierarchicalPose plusPose)
    {

        for (int i = 0; i < samplePose.currentPose.Length; i++)
        {
            samplePose.currentPose[i].translation = controlPose.currentPose[i].translation + plusPose.currentPose[i].translation;
            samplePose.currentPose[i].orientation = controlPose.currentPose[i].orientation + plusPose.currentPose[i].orientation;
            samplePose.currentPose[i].scale = controlPose.currentPose[i].scale + plusPose.currentPose[i].scale;
        }

        //controlPose.worldPose += plusPose.worldPose;
        return samplePose;
    }

    //copy blend
    public HierarchicalPose Copy(HierarchicalPose copy)
    {

        for (int i = 0; i < samplePose.currentPose.Length; i++)
        {
            samplePose.currentPose[i] = copy.currentPose[i];
        }

        return samplePose;
    }

    //invert blend
    public HierarchicalPose InvertPose(HierarchicalPose inPose)
    {

        for (int i = 0; i < samplePose.currentPose.Length; i++)
        {
            samplePose.currentPose[i].translation = inPose.currentPose[i].translation * -1;
            samplePose.currentPose[i].orientation = inPose.currentPose[i].orientation * -1;
            //samplePose.currentPose[i].scale = inPose.currentPose[i].scale * -1;
        }

        return samplePose;
    }
}
