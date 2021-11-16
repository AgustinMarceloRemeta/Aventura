using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlCaja : MonoBehaviour
{
    public GameObject[] armas;
    public MeshRenderer Render;
    public BoxCollider colider;
    public Transform ubi,spawn1,spawn2;
    public int vida = 100;
    bool paso;
    public GameObject ZombieWhite, ZombieBlack;
    ControlJugador jugador;
    void Start()
    {
        paso = false;
        jugador = FindObjectOfType<ControlJugador>();
        spawn();
    }

    
    void Update()
    {
        if (vida == 0&& !paso) DestroyBox();
       
    }

     public void DestroyBox() 
    {
        int cant = -1;
        for (int i = 0; i < armas.Length; i++)
        {
            cant++;
        }
        Render.enabled = false;
        colider.enabled = false;
        int number = Random.Range(0, cant);
        GameObject arma = armas[number];
        float x = ubi.position.x;
        float y = ubi.position.y;
        float z = ubi.position.z;
        Instantiate(arma, new Vector3(x, y, z), Quaternion.identity);
        paso = true;
      
    }
    private void spawn()
    {
        Instantiate(ZombieBlack, spawn1.position, Quaternion.identity);
        Instantiate(ZombieWhite, spawn2.position, Quaternion.identity);
    }
}

