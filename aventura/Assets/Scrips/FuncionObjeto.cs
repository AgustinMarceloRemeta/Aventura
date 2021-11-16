using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuncionObjeto : MonoBehaviour
{
    public IObject objeto;
    ControlJugador jugador;
    IStatus status;
    IInventory inventory;
    void Start()
    {
        jugador = FindObjectOfType<ControlJugador>();
        status = FindObjectOfType<IStatus>();
        inventory = FindObjectOfType<IInventory>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) 
        {
            switch (objeto.name)
            {
                case "Pistola": if(objeto.activo) if (status.balas > 0) { arma(100); status.balas--; }
                    break;
                case "Bate": if (objeto.activo)  arma(20);
                    break;
                case "Espada":
                    if (objeto.activo) if (status.balas > 0) { arma(25); status.balas--; }
                    break;
                case "Arco":
                    if (objeto.activo) if (status.balas > 0) { arma(50); status.flechas--; }
                    break;
                case "Botiquin": if (objeto.activo)status.sumavida(20);
                    break;
                case "Caja de balas": if (objeto.activo) status.sumabalas(20);
                    break;
                case "Flecha":
                    if (objeto.activo) status.sumaflechas(10);
                    break;
                case "Agua": if (objeto.activo) status.restarsed(20);
                    break;
                case "Manzana":
                    if (objeto.activo) status.restarhambre(20);
                    break;
                case "Barricada":
                    if (objeto.activo) building();
                    break;
                default:
                    break;
            }
        }
    void arma(int suma)
        {
            jugador.fuerza = suma;
        }
    
    }
    void building()
    {
        ControlJugador controlJugador = FindObjectOfType<ControlJugador>();
        if(Physics.Raycast(controlJugador.camara.transform.position, controlJugador.camara.transform.forward,out RaycastHit hit,5f))
            if (!hit.collider.CompareTag("agarrable"))
          {  Instantiate(objeto, inventory.aparicion.position, Quaternion.Euler(-90,0,0));
            objeto.quantity--;
                objeto.transform.localScale+= new Vector3(2, 2, 2);
        }
    }
}
