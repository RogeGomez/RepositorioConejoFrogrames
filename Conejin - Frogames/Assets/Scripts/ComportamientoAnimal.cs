using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComportamientoAnimal : MonoBehaviour
{
    public string animalSound = "Desconocido";

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            Debug.Log("El " + gameObject.name + " hace " + animalSound);
        }
    }
}
