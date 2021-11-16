using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ControlJugador : MonoBehaviour
{
    public GameObject camara;
    public Transform mano;
    public int fuerza;
    public GameObject palo;

    void Start()
    {
    }


    void Update()
    {

        if (Physics.Raycast(camara.transform.position, camara.transform.forward, out RaycastHit hit, 5f)) {
            if (Input.GetKey(KeyCode.E))
            {
                if (hit.collider.CompareTag("caja"))
                {
                    ControlCaja controlcaja = hit.collider.GetComponentInParent<ControlCaja>();
                    controlcaja.DestroyBox();
                }
                if (hit.collider.CompareTag("agarrable"))
                {
                    IObject objeto = hit.collider.GetComponentInParent<IObject>();
                    Agregar(objeto);
                }
                if (hit.collider.CompareTag("mesa"))
                {
                    Debug.Log("crafteo");
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
                }
    }

    public void Agregar(IObject objeto)
    {
        IInventory inventory = FindObjectOfType<IInventory>();
        inventory.Add(objeto);
        objeto.gameobject.SetActive(false);
        objeto.gameobject.transform.position = new Vector3(mano.position.x, mano.position.y, mano.position.z);
        objeto.gameobject.transform.rotation = mano.rotation;
        objeto.gameobject.transform.SetParent(mano);
        inventory.UpdateUI();
    }


}
