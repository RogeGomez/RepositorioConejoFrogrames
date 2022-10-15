using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClaseJugador : MonoBehaviour
{
    public string nombre = "Cloud";
    public int nivel = 65;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log(nombre + " ahora es nivel " + nivel);
        }
    }
}
