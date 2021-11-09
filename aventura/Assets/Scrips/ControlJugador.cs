using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlJugador : MonoBehaviour
{
    public GameObject camara;
    public Transform mano;
    public int fuerza;


    void Start()
    {
    }


    void Update()
    {
        if (Input.GetKey(KeyCode.E))
            if (Physics.Raycast(camara.transform.position, camara.transform.forward, out RaycastHit hit, 5f))
            {
                if (hit.collider.CompareTag("caja"))
                {
                    ControlCaja controlcaja = hit.collider.GetComponentInParent<ControlCaja>();
                    controlcaja.DestroyBox();
                }
                if (hit.collider.CompareTag("agarrable"))
                {
                    IObject objeto = hit.collider.GetComponentInParent<IObject>();
                    IInventory inventory = FindObjectOfType<IInventory>();
                    inventory.Add(objeto);
                    objeto.gameobject.SetActive(false);
                    objeto.gameobject.transform.position = new Vector3(mano.position.x, mano.position.y, mano.position.z);
                    objeto.gameobject.transform.rotation = mano.rotation;
                    objeto.gameobject.transform.SetParent(mano);
                }
            }

    }
        public void disparar()
        {
            Debug.Log("Disparo con " + fuerza + "de fuerza");
        }
    

}
