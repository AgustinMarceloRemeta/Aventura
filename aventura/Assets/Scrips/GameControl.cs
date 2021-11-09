using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControl : MonoBehaviour
{
    public GameObject caja, jugador;

    void Start()
    {
        Spawn();
    }

  
    void Update()
    {
        
    }

    void Spawn()
    {
        jugador.transform.position= new Vector3(0, 1, 0);
        for (int i = 0; i < 14; i++)
        {
            float z = Random.Range(-490, -166);
            float x = Random.Range(-490, -166);
            Instantiate(caja, new Vector3(x, 1f, z), Quaternion.identity);
           }
        for (int i = 0; i < 7; i++)
        {
            float z = Random.Range(-166, -1);
            float x = Random.Range(-166, -1);
            Instantiate(caja, new Vector3(x, 1f, z), Quaternion.identity);
        }
        for (int i = 0; i < 7; i++)
        {
            float z = Random.Range(1, 167);
            float x = Random.Range(1, 167);
            Instantiate(caja, new Vector3(x, 1f, z), Quaternion.identity);
        }
        for (int i = 0; i < 14; i++)
        {
            float z = Random.Range(167, 490);
            float x = Random.Range(167, 490);
            Instantiate(caja, new Vector3(x, 1f, z), Quaternion.identity);
        }
    }


}
