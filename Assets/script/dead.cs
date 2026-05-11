using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class dead : MonoBehaviour

{
    private GameObject personaje;
    
        // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        personaje = GameObject.Find("Personaje");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if(coll.name == "personaje")
        {
            Debug.Log("Has muerto");
            personaje = GameObject.Find("Personaje");
            personaje.transform.position = new Vector3(1, 2, 0);
        }
        
    }
}
