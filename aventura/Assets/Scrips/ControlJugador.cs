using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ControlJugador : MonoBehaviour
{
    public GameObject camara;
    public Transform mano;
    public int fuerza;
    public GameObject palo, inventario, crafter,building;

    void Start()
    {

    }


    void Update()
    {

        if (Physics.Raycast(camara.transform.position, camara.transform.forward, out RaycastHit hit, 5f)) {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (hit.collider.CompareTag("caja"))
                {
                    ControlCaja controlcaja = hit.collider.GetComponentInParent<ControlCaja>();
                    controlcaja.DestroyBox();
                }
           }
            if (Input.GetMouseButtonDown(0))
            {
                if (hit.collider.CompareTag("Arbol"))
                {
                    ControlCaja controlcaja = hit.collider.GetComponentInParent<ControlCaja>();
                    controlcaja.vida  -= 50;
                }
                if(hit.collider.CompareTag("Zombie"))
                {
                    Enemy enemigo = hit.collider.GetComponentInParent<Enemy>();
                    enemigo.vida -= fuerza;
                }
            }
            if(Input.GetKeyDown(KeyCode.C))
            if (hit.collider.CompareTag("mesa"))
            {                              
                crafter.SetActive(!crafter.activeSelf);
                    if (crafter.activeSelf == true)
                    {
                        Cursor.lockState = CursorLockMode.Confined;
                        Time.timeScale = 0;
                    }
                    else
                    {
                        Cursor.lockState = CursorLockMode.Locked;
                        Time.timeScale = 1;
                    }
            }
            if(Input.GetKeyDown(KeyCode.X))
            {
                building.SetActive(!building.activeSelf);
                if (crafter.activeSelf == true)

                    Cursor.lockState = CursorLockMode.Confined;

                else
                   Cursor.lockState = CursorLockMode.Locked;
          
                
            }
        }
        if(Input.GetKeyDown(KeyCode.I))
        {
            inventario.SetActive(!inventario.activeSelf);
            if(inventario.activeSelf== true) Cursor.lockState = CursorLockMode.Confined;
            else Cursor.lockState = CursorLockMode.Locked;
        }
    }
}
