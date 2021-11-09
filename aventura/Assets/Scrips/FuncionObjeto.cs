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
                case "Pistola": if(objeto.activo) if (status.balas > 0) { arma(50); status.balas--; }
                    break;
                case "Bate": if (objeto.activo)  arma(25);
                    break;
                case "Botiquin": if (objeto.activo)status.sumavida(20);
                    break;
                case "Caja de balas": if (objeto.activo) status.sumabalas(20);
                    break;
                case "Agua": if (objeto.activo) status.restarsed(20);
                    break;
                default:
                    break;
            }
        }
    void arma(int suma)
        {
            jugador.fuerza = suma;
            jugador.disparar();
        }
    
    }
}
