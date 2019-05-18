using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent (typeof(SpriteRenderer)) ]

public class Tiling : MonoBehaviour {

    //The offset so we do not get any errors
    public int offsetX = 2;

    // these are used for checking  if we need  to instantiate  stuff
    public bool hasARightBuddy = false;
    public bool hasALeftBuddy = false;

    //used the objecg if it is not tilable
    public bool reverseScale = false;

    private float spriteWidth = 0f;
    private Camera cam;
    private Transform myTransform;

    void Awake()
    {
        cam = Camera.main;
        myTransform = transform;

    }


	// Use this for initialization
	void Start ()
    {
        SpriteRenderer sRenderer = GetComponent<SpriteRenderer>();
        spriteWidth = sRenderer.sprite.bounds.size.x;

	}
	
	// Update is called once per frame
	void Update ()
    {
		//does it still need buddies ? if not do nothing
        if(hasALeftBuddy == false || hasARightBuddy == false)
        {
            float camHorizontalExtend = cam.orthographicSize * Screen.width / Screen.height;

            // calculate  the x position  where the camera  can see the edge of the sprite 
            float edgeVisiblePositionLeft = (myTransform.position.x + spriteWidth/2) + camHorizontalExtend;
            float edgeVisiblePositionRight = (myTransform.position.x - spriteWidth/2) - camHorizontalExtend;

            //checking if w can see the edge of the element and then calling the Make new buddy if we can
            if (cam.transform.position.x >= edgeVisiblePositionRight - offsetX && hasARightBuddy == false)
            {
                MakeNewBuddy(1);
                hasARightBuddy = true;
            }
            else if (cam.transform.position.x <= edgeVisiblePositionLeft + offsetX && hasALeftBuddy == false)
            {
                MakeNewBuddy(-1);
                hasALeftBuddy = true;
            }
        }
	}

    // a function that creates  a buddy on the side required
    void MakeNewBuddy (int rightorLeft)
    {
        // calculating  the new position for our new buddy
        Vector3 newPosition = new Vector3(myTransform.position.x + spriteWidth * rightorLeft, myTransform.position.y, myTransform.position.z);

        // intantating our new body and storing him in a variable
        Transform newBuddy = Instantiate(myTransform, newPosition, myTransform.rotation) as Transform;

        // if not tilable  let's reverse  the x size  of our  object to get rid of  ugly seams

        if(reverseScale == true)
        {
            newBuddy.localScale = new Vector3(newBuddy.localScale.x * -1, newBuddy.localScale.y, newBuddy.localScale.z);

        }
        newBuddy.parent = myTransform.parent;
        if(rightorLeft >0)
        {
            newBuddy.GetComponent<Tiling>().hasALeftBuddy = true;
        }
        else
        {
            newBuddy.GetComponent<Tiling>().hasARightBuddy = true;
        }
    }
}
