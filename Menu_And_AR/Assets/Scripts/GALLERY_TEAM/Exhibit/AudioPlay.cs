using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlay : MonoBehaviour
{
 
    public AudioSource clipSource;
    public AudioClip click;

    public void clickSound()
    {
        clipSource.PlayOneShot(click);
    }

    // Start is called before the first frame update
    /* void Start()
     {
         clipSource = GetComponent<AudioSource>();
     }

     // Update is called once per frame
     void Update()
     {
         if (Input.GetMouseButtonDown(0))
         {
             clipSource.PlayDelayed(0);
         }
     } */
}
