
using UnityEngine;
using System;

[RequireComponent(typeof(SpriteRenderer))]
//BIG THANKS TO BRACKEYS 
//Source is Brackeys on youtube.
public class tiling : MonoBehaviour {

    public int offset = 2;
    public bool rightSide = false;
    public bool leftSide = false;

    public bool reverse = false;

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
        SpriteRenderer render = GetComponent<SpriteRenderer>();
        spriteWidth = render.sprite.bounds.size.x * Math.Abs(myTransform.localScale.x);
	}
	
	// Update is called once per frame
	void Update () {
        //Checks to see if the sides need an initiated background.
		if(leftSide == false || rightSide == false)
        {
            //calc the cam exten (half the width) of what the camera can see in world coordinates.
            float camHorizontalExtend = cam.orthographicSize * Screen.width / Screen.height;

            //calculate the x pos where the camera can see teh edge of the sprite
            float edgeVisibleRight = (myTransform.position.x + spriteWidth / 2) - camHorizontalExtend;
            float edgeVisibleLeft = (myTransform.position.x - spriteWidth / 2) + camHorizontalExtend;

            //Checks to see the edge of the elements.
            if (cam.transform.position.x >= edgeVisibleRight - offset && rightSide == false)
            {
                MakeNewSide(1);
                rightSide = true;
            }
            else if (cam.transform.position.x <= edgeVisibleLeft + offset && leftSide == false)
            {
                MakeNewSide(-1);
                leftSide = true;
            }
        }
	}

    void MakeNewSide(int direction)
    {
        Vector3 newPos = new Vector3 (myTransform.position.x + spriteWidth * direction, myTransform.position.y, myTransform.position.z);
        Transform newInstance = Instantiate(myTransform, newPos, myTransform.rotation) as Transform;

        if (reverse == true)
        {
            newInstance.localScale = new Vector3(newInstance.localScale.x * -1, newInstance.localScale.y, newInstance.localScale.z);
        }

        newInstance.parent = myTransform.parent;
        if(direction > 0)
        {
            newInstance.GetComponent<tiling>().leftSide = true;
        }

        else
        {
            newInstance.GetComponent<tiling>().rightSide = true;
        }
    }
}
