using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClaseEnemigo : MonoBehaviour
{
    public string nombre = "Sephiroth";
    public int nivel = 120;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            Debug.Log("Cuidado! " + nombre + " es nivel " + nivel);
        }
    }
}
