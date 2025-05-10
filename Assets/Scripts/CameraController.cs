using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Transform target; // ska påverka en transformer är private ska inte användas utanför. 
    // Start is called before the first frame update
    void Start()
    {
        target = FindObjectOfType<PlayerController>().transform; //hitta object av playerController, dens transformer tildelar vi som target
    }

    // LateUpdate is called once per frame after all regular updates is done first
    void LateUpdate()
    {
        transform.position = new Vector3(target.position.x, target.position.y, transform.position.z);
    }
}
