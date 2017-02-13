using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LevelManager : MonoBehaviour {
    
    [SerializeField]
    private GameObject brickTypeThree, brickTypeTwo,brickTypeOne,brickTypeX;
    [SerializeField]
    private GameObject pointZero,pointEnd;
    private string[] lineData;
    private string[] mapData;
    private string[] lines;
    private int offset = 2;
    public static LevelManager instance = null;
    [HideInInspector]
    public int numberOfBricks;


    private void Awake()
    {
        numberOfBricks = 0;
        CreateLevel();
        if (instance == null)
        {
            instance = this;
        }
    }



    private void CreateLevel()
    {
        // Temp instanatiation of the brick map
        mapData =ReadCSV() ;

        //// calculates the y map size
        int mapY = mapData.Length;


        //This gets the start point of the brick map

        Vector3 worldStart = pointZero.transform.position;
  
        for (int y = 0; y < mapY; y++)
        {
            char[] newBricks = mapData[y].ToCharArray();        //saves the chars of every row to an array
            
            for (int x = 0; x < mapData[y].Length; x++)
            {
               
                //calls the function to place the bricks
                PlaceBrick(newBricks[x], x, y, worldStart);
                if (newBricks[x] != ' ')
                {
                    numberOfBricks++; //increases the amount of  bricks if its not empty
                }
                
               
            }

        }

    }

    private void PlaceBrick(char brickType,int x, int y, Vector3 worldStart)
    {
        //creates a new gameobject of one of the 4 types of bricks
        GameObject newBrick = PickBrick(brickType);
        // gets the actual brick size x and y
        float brickSizeX = BrickSizeX(newBrick);
        float brickSizeY = BrickSizeY(newBrick);

        // rescales the brick depending on the map (amount of bricks and brick area)
        newBrick.transform.localScale = CalculateBrickScale(newBrick,brickSizeX,brickSizeY);

        float xBrickScale = CalculateBrickScale(newBrick, brickSizeX, brickSizeY).x;
        float yBrickScale = CalculateBrickScale(newBrick, brickSizeX, brickSizeY).y;

        if (brickType == ' ')
        {
            return;
        }
        else
        {
           
            //Uses the newBrick var to change the position of a brick and place them in a grid kindof way
            newBrick.transform.position = new Vector3(worldStart.x +(xBrickScale* brickSizeX/2) + (xBrickScale*brickSizeX * x) , (worldStart.y - ((brickSizeY / 2) * yBrickScale)) - ((brickSizeY * yBrickScale) * y), 0);
            Instantiate(newBrick);

           
        }
    }
    //depending on the type it returns the appropriate bricktype
    private GameObject PickBrick(char brickType)
    {
        if (brickType == '1')
        {
            return brickTypeOne;
        }
        else if (brickType == '2')
        {
            return brickTypeTwo;
        }
        else if (brickType == '3')
        {
            return brickTypeThree;
        }
        else
        {
            return brickTypeX;
        }
    }

    private float BrickSizeX(GameObject newBrick)
    {
        return newBrick.GetComponent<SpriteRenderer>().sprite.bounds.size.x;

    }

    private float BrickSizeY(GameObject newBrick)
    {

        return newBrick.GetComponent<SpriteRenderer>().sprite.bounds.size.y;

    }

    //Given the limits of 2 points, calculates how much the bricks should be scaled in order to fit in the specified area.
    private Vector3 CalculateBrickScale(GameObject newBrick, float brickSizeX, float brickSizeY)
    {
        int max = 0;
        float brickAreaX = pointZero.transform.position.x - pointEnd.transform.position.x;
        float brickAreaY = pointZero.transform.position.y - pointEnd.transform.position.y;
       
        brickAreaX = Mathf.Abs(brickAreaX);
        brickAreaY = Mathf.Abs(brickAreaY);

        for (int i = 0; i < mapData.Length; i++)
        {
            int curMax = Mathf.Max(mapData[i].Length);
            if (curMax > max)
            {
                max = curMax;
            } 
        }
         
        float brickScaleX = brickAreaX / ((max) * (brickSizeX));
        float brickScaleY = brickAreaY / ((mapData.Length) * (brickSizeY));
        return new Vector3(brickScaleX,brickScaleY);
     
    }

    // reading the csv file
    private string[] ReadCSV()
    {
        TextAsset bindData = Resources.Load("level") as TextAsset; //loads it

        string tempdata = bindData.text.Replace(Environment.NewLine, "-"); //puts everything in one line, replacing new lines with "-"
        string data = tempdata.Replace(",","");                            //deleting the commas

        return  data.Split('-');                                           //splits when "-" to an array

    }


}
