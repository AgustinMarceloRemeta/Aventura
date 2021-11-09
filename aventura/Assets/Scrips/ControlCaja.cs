using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlCaja : MonoBehaviour
{
    public GameObject[] armas;
    public MeshRenderer Render;
    public BoxCollider colider;
    public Transform ubi;
    public int vida = 100;
    bool paso;
    ControlJugador jugador;
    void Start()
    {
        paso = false;
        jugador = FindObjectOfType<ControlJugador>();        
    }

    
    void Update()
    {
        if (vida == 0&& !paso) DestroyBox();
    }

     public void DestroyBox() 
    { 
        Render.enabled = false;
        colider.enabled = false;
        int number = Random.Range(0, 5);
        GameObject arma = armas[number];
        float x = ubi.position.x;
        float y = ubi.position.y;
        float z = ubi.position.z;
        Instantiate(arma, new Vector3(x, y, z), Quaternion.identity);
        paso = true;
    }
}

