using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IStatus : MonoBehaviour
{
    public float health, energy, hunger, thirst, balas,flechas;
    public float healthTick, energyTick, hungerTick, thirstTick;
    public float healthMax, energyMax, hungerMax, thirstMax, balasMax;
    public float thirstMin;
    public Text HealthText, HungerText, ThirstText, balasText, alertText,flechasText;
    string alert = " ";
    public float tiempo;
    public Image healthIm, hungerIm, thirstIm;

    private void Update()
    {
        Control();

        SeteoUI();

        Maximo();
    }

    private void Maximo()
    {
        if (health > healthMax) health = healthMax;
        if (thirst < thirstMin) thirst = thirstMin;
        if (balas > balasMax) balas = balasMax;
    }

    private void SeteoUI()
    {
        HealthText.text = "Vida: ";
        HungerText.text = "Hambre: ";
        ThirstText.text = "Sed: ";
        balasText.text = "balas: " + balas;
        flechasText.text = "Flechas: " + flechas;
        healthIm.fillAmount = health / healthMax;
        hungerIm.fillAmount = hunger / hungerMax;
        thirstIm.fillAmount = thirst / thirstMax;
        if (tiempo == 0f) alertText.text = " ";
        else alertText.text = alert;
        
    }

    private void Control()
    {
        if (health < healthMax&& thirst<20 && hunger < 20) health += Time.deltaTime / healthTick;
        if (energy > 0) energy -= Time.deltaTime / energyTick;
        if (hunger < hungerMax) hunger += Time.deltaTime / hungerTick;
        if (thirst < thirstMax) thirst += Time.deltaTime / thirstTick;
        if (tiempo > 0) tiempo -= Time.deltaTime;
        else tiempo = 0;
        if (thirst == 100) health-= Time.deltaTime/healthTick;
        if (hunger == 100) health -= Time.deltaTime / healthTick;
    }
    public void sumavida(float suma) 
    {
        if (health == healthMax) alerta("vida llena");
        else
        {
            health += suma;           
            Elim1("Botiquin");
        }
    }

    public void restarsed(float resta)
    {
        if (thirst <= 0.9f)  alerta("No tenes sed");
        else
        {
            thirst -= resta;           
            Elim1("Agua");
        }
    }
    public void restarhambre(float resta)
    {
        if (hunger <= 0.9f) alerta("No tenes Hambre");
        else
        {
            hunger -= resta;
            Elim1("Manzana");
        }
    }

    public void sumabalas(float suma)
    {
        if (balas == balasMax) alerta("CargadorLleno");
        else
        {
            balas += suma;           
            Elim1("Caja de balas");
        }
    }
    public void sumaflechas(float suma)
    {
            flechas += suma;
            Elim1("Flecha");        
    }

    public void Elim1(string object1)
    {
        FuncionObjeto objeto = FindObjectOfType<FuncionObjeto>();
        IInventory inventario = FindObjectOfType<IInventory>();
        foreach (var i in inventario.inventory) if (i.name == object1) inventario.Remove(i.name, 1, i.objeto);
    }
    
    public void alerta(string a)
    {
        tiempo = 2;
        alert = a;
    }

}
