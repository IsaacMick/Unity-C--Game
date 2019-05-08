using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour {

    public Transform[] backgrounds; //Array of all the background/forgrounds.
    private float[] scales; //Scale of the camera movement.
    public float smoothing = 2; //smoothness of parallax.

    private Transform cam; //camera
    private Vector3 lastCamPosition; //the position of the camera before calculations

    void Awake ()
    {
        cam = Camera.main.transform;
    }

	// Use this for initialization
	void Start ()
    {
        lastCamPosition = cam.position;

        scales = new float[backgrounds.Length];
        //assigning scales
        for(int i = 0; i < backgrounds.Length; i++)
        {
            scales[i] = backgrounds[i].position.z * -1;
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
		for (int i = 0; i <backgrounds.Length; i++)
        {
            //the parallax is the opposite of the camera movement vecause the previous from multiplied by the scale
            float parallaxX = (lastCamPosition.x - cam.position.x) * scales[i];
            float parallaxY = (lastCamPosition.y - cam.position.y) * scales[i];

            float backgroundTargetX = backgrounds[i].position.x + parallaxX;
            float backgroundTargetY = backgrounds[i].position.y + parallaxY;

            Vector3 backgroundTargetPos = new Vector3(backgroundTargetX, backgroundTargetY, backgrounds[i].position.z);

            backgrounds[i].position = Vector3.Lerp(backgrounds[i].position, backgroundTargetPos, smoothing * Time.deltaTime);
        }

        lastCamPosition = cam.position;
	}
}
