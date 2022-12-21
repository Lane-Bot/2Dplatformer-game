using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxEffect : MonoBehaviour
{

    public Camera cam;
    public Transform followTraget;

    // start Position for the parallax game object
    Vector2 startingPosition;

    //start 2 value of  the parallax game object
    float startingZ;

    //Distance that the camera has moved from  the starting position of the  parallax object
    Vector2 camMoveSinceStart => (Vector2)cam.transform.position - startingPosition;

    float zDistanceFromtarget => transform.position.z - followTraget.transform.position.z;

    // if object is in front of target , use near clip plane. if behide target, use farClipPlane
    float clippingPlane => (cam.transform.position.z + (zDistanceFromtarget > 0 ? cam.farClipPlane : cam.nearClipPlane));

    //the futher the object from the player the faster the paralaxeffect object will move . Drag it's z value closer to the target to make it move slower
    float parallaxFactor => Mathf.Abs(zDistanceFromtarget) / clippingPlane;

    // Start is called before the first frame update
    void Start()
    {
        startingPosition = transform.position;
        startingZ = transform.position.z;
    }

    // Update is called once per frame
    void Update()
    {
        //When the target moves , move the parallax object the samne distance time a multiplier
        Vector2 newPosition = startingPosition + camMoveSinceStart * parallaxFactor;
        //the x/y position change base on target travel speed time the  parallax factor buy z stays consistent
        transform.position = new Vector3(newPosition.x, newPosition.y, startingZ);
    }
}
