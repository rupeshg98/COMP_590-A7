using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collector : MonoBehaviour
{
    // Start is called before the first frame update
    
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.tag);
        if(other.transform.gameObject.CompareTag("Treasure")){
            Destroy(other.transform.gameObject);
        }
    }
}
