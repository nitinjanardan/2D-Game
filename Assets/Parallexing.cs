using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallexing : MonoBehaviour {
    public Transform[] backgrounds;     // Array of list of all back-ground and fore ground  to be parallaxed
    private float[] parallexScales;     //The proportion of the camera's movement to move the background by
    public float smoothing = 1f;

    private Transform cam;              // reference to camera reference
    private Vector3 previousCamPos; //the posiion  of the camera in previous frame 

    // Is called brfore start().call all the lgic before the start function but after game object are setup ,Just assigning reference between object and scripts
    void Awake()
    {
        // set up thecamera reference
        cam = Camera.main.transform;
    }


    // Use this for initialization
    void Start ()
    {
        // The previous  frame had  the current frame's camera  position
        previousCamPos = cam.position;

        //assigning corresponding parallexScales 
        parallexScales = new float[backgrounds.Length];
        for (int i =0;i<backgrounds.Length; i++)
        {
            parallexScales[i] = backgrounds[i].position.z * -1;

        }

	}
	
	// Update is called once per frame
	void Update ()
    {
        // for each background
        for (int i = 0; i < backgrounds.Length; i++)
        {
            // the parallex is the opposite of the camera movement  because  the previous  frame multiplied  by the scale
            float parallex = (previousCamPos.x - cam.position.x) * parallexScales[i];

            // set a target whic is current positoin plus the parallex 
            float backgroundTargetPosX = backgrounds[i].position.x + parallex;

            // create  a target position which is the backhgrounds's current position with its target position 
            Vector3 backgroundTargetPos = new Vector3(backgroundTargetPosX, backgrounds[i].position.y, backgrounds[i].position.z);

            // fade  between  current position  and  the target position using  lerp 
            backgrounds[i].position = Vector3.Lerp(backgrounds[i].position, backgroundTargetPos, smoothing * Time.deltaTime);

        }
        // set the previousCamPos to the camera 's position at the end of the frame 
        previousCamPos = cam.position;

	}
}
