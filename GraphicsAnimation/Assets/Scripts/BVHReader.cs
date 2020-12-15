/*
File name: BVHReader.cs
Purpose:  This is a file reader that takes BVH file asset paths as an input
and delivers a skeltal hierarchy in return

Contributors: Nick Brennan-Martin and Bradley Chamberlain
Collaborated on one PC
*/
using UnityEngine;
using UnityEditor;
using System.IO;

public class BVHReader : MonoBehaviour
{
    public static StreamReader reader;
    public Hierarchy skeletonH;
    public HierarchicalPose skeletonPose;
    public string assetPath = "Assets/Resources/test.txt";
    public bool activate = false;
    bool activated = false;
    private void Start()
    {
        
        
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
             
            activate = true;
        }
        if(activate && !activated)
        {
            // open file read in and out
            ReadString(assetPath);

            string read = CreateString(ReturnNextBlock());// hierarchy
            ReadJoint(0, -1);
            ReadJoint(1, 0);
            ReadJoint(2, 1);
            ReadJoint(3, 2);
            activated = true;
        }
    }
    // read through a joint and create thhe appropriate nodes and poses
    void ReadJoint(int index, int parentIndex)
    {
        //bool isEnd;
        string Name = CreateString(ReturnNextBlock());
        string jointType = CreateString(ReturnNextBlock());
        string brackets = CreateString(ReturnNextBlock());

        string offset = CreateString(ReturnNextBlock());
        Vector3 offsetVec = ReadOffset();
        //Channels 
        string channels = CreateString(ReturnNextBlock());
        float i = ReturnInt(ReturnNextBlock());
        Debug.Log(i);
        for (float j = 0f; j < i; ++j)
        {
            //collect channels
            CreateString(ReturnNextBlock());
        }

        GameObject node = new GameObject();
        node.AddComponent<HierarchyNode>();
        node.name = Name;
        node.GetComponent<HierarchyNode>().index = index;
        node.GetComponent<HierarchyNode>().index = parentIndex;
        node.transform.position = offsetVec;

        GameObject poseNode = new GameObject();
        poseNode.AddComponent<SpatialPose>();
        poseNode.name = Name;
        poseNode.GetComponent<SpatialPose>().translation = offsetVec;
        poseNode.transform.position = offsetVec;

        skeletonH.treeDepth[index] = node.GetComponent<HierarchyNode>();
        skeletonPose.AddNode(poseNode.GetComponent<SpatialPose>(), index);
    }

    // read the 
    static void ReadString(string assetPath) 
    {

        string path = assetPath;
        //Read the text from directly from the test.txt file
        reader = new StreamReader(path);         
    }

    //return the next group of character that arte not space, returns, or tabs
    private char[] ReturnNextBlock()
    {
        
        char[] charHolder = new char[16];
        char letter = ' ';
        int count = 0;
        
        while (letter == ' ' || letter == '\r' || letter == '\n' || letter == '\t' || letter == '\b') // consume spaces
        {
            letter = (char)reader.Read();
            //Debug.Log(";" + letter + ";");
        }           
        while(letter != ' ' && letter != '\r' && letter != '\n' && letter != '\t' && letter != '\b')
        {
            charHolder[count] = letter;
            ++count;
            //Debug.Log(":" + letter + ":");
            letter = (char)reader.Read();
        }
        
        if(count == 0)
        {
            //Debug.Log("nowords");
            return new char[0];
        }

        char[] returnChars = new char[count];
        for (int i = 0; i < count; i++)
        {
            //Debug.Log(returnChars[i]);
            returnChars[i] = charHolder[i];
        }
        return returnChars;
    }

    //return a group of characters as a proper float variable
    private float ReturnFloat(char[] chars)
    {
        int periodPos = 0;
        int length = chars.Length;
        float returnFloat = 0f;
        //bool negative = false;
        //if (chars[0] == '-')
            //negative = true;
        
        for(int i = 0; i < length; i ++)
        {
            
            if(chars[i] == '.')
            {
                periodPos = i;
            }
        }
        for (int j = 0; j < length; j++)
        {
            if (j == periodPos)
            {
                //Debug.Log("period");
            }
            else
            {
                //Debug.Log((float)chars[j] - 48f);
                returnFloat += ((float)chars[j]-48f) * Mathf.Pow(10f, periodPos - j - 1);
            }           
        }
        //Debug.Log(returnFloat);
        return returnFloat;

    }

    // return a proper interger form a string of numbers
    int ReturnInt(char[] chars)
    {
        int returnInt = 0;
        for(int i = 0; i < chars.Length; i ++)
        {
            returnInt += ((int)chars[i] - 48) * (int)Mathf.Pow(10, chars.Length - i - 1);
        }
        return returnInt;
    }
    
    // read the vector 3 offset from the BVH File
    private Vector3 ReadOffset()
    {
        Vector3 returnVec;        
        returnVec = new Vector3(ReturnFloat(ReturnNextBlock()), ReturnFloat(ReturnNextBlock()), ReturnFloat(ReturnNextBlock()));
        return returnVec;      
    }

    // Create a proper string form an array of chars
    private string CreateString(char[] chars)
    {

        string str = "";
        for (int i = 0; i < chars.Length; i++)
        {
            str += chars[i];
        }
        Debug.Log(str);

        return str;
    }
    

}