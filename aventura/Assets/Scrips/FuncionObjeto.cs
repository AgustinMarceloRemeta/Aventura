using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuncionObjeto : MonoBehaviour
{
    public IObject objeto;
    ControlJugador jugador;
    IStatus status;
    IInventory inventory;
    public Transform aparicion;
    public GameObject obj;
    void Start()
    {
        jugador = FindObjectOfType<ControlJugador>();
        status = FindObjectOfType<IStatus>();
        inventory = FindObjectOfType<IInventory>();
    }

    // Update is called once per frame
    void Update()
    {
    }
    void arma(int suma)
    {
        jugador.fuerza = suma;
    }

    public void funcion()
    {
        if (Input.GetMouseButtonDown(0))
        {
            switch (objeto.name)
            {
                case "Pistola":
                    if (status.balas > 0) { arma(100); status.balas--; }
                    break;
                case "Bate":
                    arma(20);
                    break;
                case "Espada":
                    if (objeto.activo) if (status.balas > 0) { arma(25); status.balas--; }
                    break;
                case "Arco":
                    if (status.flechas > 0) { arma(50); status.flechas--; }
                    break;
                case "Botiquin":
                    status.sumavida(20);
                    break;
                case "Caja de balas":
                    status.sumabalas(20);
                    break;
                case "Flecha":
                    status.sumaflechas(10);
                    break;
                case "Agua":
                    status.restarsed(20);
                    break;
                case "Manzana":
                    status.restarhambre(20);
                    break;
                case "Barricada":
                    building();
                    break;
                case "Hierro":
                    UpgradeBuilding();
                    break;
                default:
                    break;
            }
        }
    }

    void building()
    {
        ControlJugador controlJugador = FindObjectOfType<ControlJugador>();
        if (Physics.Raycast(controlJugador.camara.transform.position, controlJugador.camara.transform.forward, out RaycastHit hit, 5f))
            if (!hit.collider.CompareTag("agarrable") || !hit.collider.CompareTag("Building"))
            {
                Instantiate(objeto, aparicion.position, Quaternion.Euler(-90, 0, 0));
                foreach (var i in inventory.inventory)
                    if (i.name == "Barricada") inventory.Remove(i.name, 1, i.objeto);
            }
    }
    void UpgradeBuilding()
    {
        ControlJugador controlJugador = FindObjectOfType<ControlJugador>();
        if (Physics.Raycast(controlJugador.camara.transform.position, controlJugador.camara.transform.forward, out RaycastHit hit, 5f))
            if (hit.collider.CompareTag("Building") && !(hit.collider.transform.localScale == new Vector3(1, 1, 1)))
            {
                hit.collider.transform.localScale += new Vector3(0.25f, 0.25f, 0.25f);
                foreach (var i in inventory.inventory) if (i.name == "Hierro") inventory.Remove(i.name, 1, i.objeto);
            }


    }
}
