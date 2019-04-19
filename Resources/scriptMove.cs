using UnityEngine;
using System.Collections;

public class scriptMove : MonoBehaviour {

    public GameObject otherObject;
   // public GameObject currentObject;

    //private RectTransform rt;
    Vector3 temp;
    float moveAreaX;
    void Start(){

       
    }

    void Update(){

       // otherObject.transform.position = transform.TransformPoint (0 , (float)-0.5 , (float)-0.2 );
        //rt = (RectTransform)transform;
       //x = rt.rect.height;
       temp = new Vector3(0,0,0);
       otherObject.transform.position = transform.position + temp;

        //Fetch the size of the Collider volume
        //Vector3 objectSize = Vector3.Scale(transform.localScale, GetComponent().mesh.bounds.size);
        
        
        
        //currentObject.transform.position = currentObject.transform.position - Vector3.forward * 10f;
        
        
        //moveAreaX = currentObject.GetComponent().bounds.size.x / 2;
        //Output to the console the size of the Collider volume
        //Debug.Log("Inaltimea : " + moveAreaX);

    }
}