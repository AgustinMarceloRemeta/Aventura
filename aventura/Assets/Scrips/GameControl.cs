using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControl : MonoBehaviour
{
    public GameObject caja, jugador,mesa;




    void Start()
    {
        Spawn();
    }

  
    void Update()
    {
        
    }

    void Spawn()
    {
        jugador.transform.position= new Vector3(-5, 1, 8);
        for (int i = 0; i < 14; i++)
        {
            float z = Random.Range(-490, -156);
            float x = Random.Range(-490, -156);
            Instantiate(caja, new Vector3(x, 1f, z), Quaternion.identity);
           }
        for (int i = 0; i < 7; i++)
        {
            float z = Random.Range(-166, -11);
            float x = Random.Range(-166, -11);
            Instantiate(caja, new Vector3(x, 1f, z), Quaternion.identity);
        }
        for (int i = 0; i < 7; i++)
        {
            float z = Random.Range(1, 157);
            float x = Random.Range(1, 157);
            Instantiate(caja, new Vector3(x, 1f, z), Quaternion.identity);
        }
        for (int i = 0; i < 14; i++)
        {
            float z = Random.Range(167, 470);
            float x = Random.Range(167, 470);
            Instantiate(caja, new Vector3(x, 1f, z), Quaternion.identity);
        }
        
            Instantiate(mesa, new Vector3(-160, 0f, -160), Quaternion.identity);
        
      
            Instantiate(mesa, new Vector3(-5, 0f, -5), Quaternion.identity);
        
      
            Instantiate(mesa, new Vector3(162, 0f, 162), Quaternion.identity);

            Instantiate(mesa, new Vector3(480, 0f, 480), Quaternion.identity);

    }

}
