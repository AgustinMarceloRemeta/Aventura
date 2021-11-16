using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public string Color;
    public NavMeshAgent agent;
    public Transform Destino;
    public Transform salida;
    public float Cooldown= 2;
    public int vida = 100;
    public GameObject Object;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        Destino = GameObject.FindGameObjectWithTag("Player").transform;
        InvokeRepeating("seguir",0,.1f);
    }

    // Update is called once per frame
    void Update()
    {
        if (Cooldown > 0)  Cooldown -= Time.deltaTime; 
        if (Cooldown<= 0 && Vector3.Distance(salida.position, Destino.position) < 6f)
            switch (Color)
            {
                case "Black": { atacar(10); Cooldown = 7f;}
                    break;
                case "White": { atacar(20); Cooldown = 5f;}
                    break;
                default: 
                    break;
            }
        if (vida <= 0) Destroy(Object); 
    }
    void seguir() 
    {
        if (Vector3.Distance(salida.position, Destino.position) < 15f)
        {
            if (Vector3.Distance(salida.position, Destino.position) < 5f) agent.SetDestination(salida.position);
            else agent.SetDestination(Destino.position);
        }
    }
    void atacar(int Daño) 
    {
        GameObject statuso = GameObject.FindGameObjectWithTag("Status");
        IStatus status = statuso.GetComponent<IStatus>();
        status.health = status.health - Daño;
    }
}

