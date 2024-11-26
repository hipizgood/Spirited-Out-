using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Melee_VFX : MonoBehaviour
{
    public GameObject vfx;

   
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Spirit_Vire"))
        {
           
            Instantiate(vfx, transform.position, transform.rotation);
        }
    }
}
