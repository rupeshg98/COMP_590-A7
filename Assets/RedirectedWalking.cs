using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedirectedWalking : MonoBehaviour
{
    // Start is called before the first frame update

    public Transform headpose; 
    public Transform PE; 

    Vector3 prevForwardVector; 
    float prevYawRelativeToCenter; 

    //Updating parameters
    private float howMuchUserRotated; 
    private int directionUserRotated;
    private float deltaYawRelativeToCenter;
    private float distanceFromCenter;
    private const float longestDimensionOfPE = 5f; 
    private float howMuchToAccelerate;
    private static float d(Vector3 A,Vector3 B, Vector3 C){
        return (A.x-B.x)*(C.z-B.z)-(A.z-B.z)*(C.x-B.x); 
    }

    private static float angleBetweenVectors(Vector3 A, Vector3 B){

        Vector3 A1 = new Vector3(A.x, 0, A.z); 
        Vector3 B1 = new Vector3(B.x, 0, B.z); 

        float angle = Mathf.Acos(Vector3.Dot(Vector3.Normalize(A1),Vector3.Normalize(B1)));

        angle = angle*180/Mathf.PI; 

        return angle; 
    }

    void Start()
    {
        prevForwardVector = headpose.forward; 
        prevYawRelativeToCenter = angleBetweenVectors(headpose.forward,PE.position-headpose.position); 
    }

    // Update is called once per frame
    void Update()
    {
        howMuchUserRotated=angleBetweenVectors(prevForwardVector,headpose.forward); 
        directionUserRotated=(d(headpose.position+prevForwardVector, headpose.position, headpose.position + headpose.forward)<0)?1:-1;
        deltaYawRelativeToCenter=prevYawRelativeToCenter-angleBetweenVectors(headpose.forward,PE.position-headpose.position);
        distanceFromCenter=(headpose.transform.position-PE.transform.position).magnitude; 
        howMuchToAccelerate=((deltaYawRelativeToCenter<0)? - 0.13f: .30f) * howMuchUserRotated * directionUserRotated * Mathf.Clamp(distanceFromCenter/longestDimensionOfPE/2,0,1);
        if(Mathf.Abs(howMuchToAccelerate) > 0) PE.transform.RotateAround(headpose.position, new Vector3(0,1,0), howMuchToAccelerate);
        prevForwardVector=headpose.forward;
        prevYawRelativeToCenter=angleBetweenVectors(headpose.forward,PE.position-headpose.position);


        
    }
}
