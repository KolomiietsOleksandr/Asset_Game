using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEditor;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class Sail : MonoBehaviour
{
    [SerializeField] GameObject SailButtons;
    [SerializeField] GameObject MerchantButtons;
    [SerializeField] GameObject BottleButtons;
    [SerializeField] GameObject ShipsFightButtons;
    [SerializeField] GameObject PiratesFightButtons;

    [SerializeField] private TMP_Text NameText;
    [SerializeField] private string[] PassivePhrases;
    [SerializeField] private string[] InteravtiveEvents;

    [SerializeField] GameObject Merchant_Ship;
    [SerializeField] GameObject Bottle;
    [SerializeField] GameObject Islands;
    GameObject MerchantShip_var;
    GameObject Bottle_var;
    GameObject Islands_var;

    [SerializeField] GameObject[] Enemies;
    GameObject Enemy;

    [SerializeField] GameObject AttackBut;
    [SerializeField] GameObject ShopBut;
    [SerializeField] GameObject CannonBut;
    [SerializeField] GameObject EscapeBut;
    [SerializeField] GameObject BoardingBut;

    int Events = 1;

    public void GetEvent()
    {   
        int EventsTypeRandomiser = Random.Range(0, 11);

        if (EventsTypeRandomiser >= 0 && EventsTypeRandomiser <= 4)
        {
            NameText.text += PassivePhrases[0] + '\n' + '\n';
            Events = 0;
        }

        else if (EventsTypeRandomiser == 5 || EventsTypeRandomiser == 6)
        {
            NameText.text += PassivePhrases[Random.Range(1, PassivePhrases.Length)] + '\n' + '\n';
            Events = 0;
        }

        else if (EventsTypeRandomiser == 7 || EventsTypeRandomiser == 8)
        {
            NameText.text += "Captain, on the course <#bf2424>enemy vessel</color>" + '\n' + '\n';
            Events = 1;
        }

        else if (EventsTypeRandomiser == 9) 
        {
            NameText.text += "Captain, on the course <#dbf229>merchant ship</color>" + '\n' + '\n';
            Events = 2;
        }

        else if (EventsTypeRandomiser == 10)
        {
            NameText.text += "<#218009>I see a bottle, maybe there's a map in it?</color>" + '\n' + '\n';
            Events = 3;
        }
    }

    public void SailEvent()
    {
        if (Events == 0) 
        {
            SailButtons.transform.SetPositionAndRotation(new Vector3(0, 0, 0), Quaternion.identity);
        }
        else if (Events != 0)
        { 
            SailButtons.transform.SetPositionAndRotation(new Vector3(1000, 1000, 0), Quaternion.identity); 
        }
    }

    public void MerchantEvent()
    {
        if (Events == 2)
        {
            Destroy(MerchantShip_var);
            MerchantShip_var = Instantiate(Merchant_Ship, new Vector3(1.95f, 0.25f, 0), Quaternion.identity);
            MerchantButtons.transform.SetPositionAndRotation(new Vector3(0, 0, 0), Quaternion.identity);
        }
        else if (Events != 2) 
        {
            Destroy(MerchantShip_var);
            MerchantButtons.transform.SetPositionAndRotation(new Vector3(0, 1000, 0), Quaternion.identity);
        }
    }

    public void BottleEvent()
    {
        if (Events == 3)
        {
            Destroy(Bottle_var);
            Destroy(Islands_var);
            Islands_var = Instantiate(Islands, new Vector3(-2.5f, -2.9f, 211), Quaternion.identity);
            Bottle_var = Instantiate(Bottle, new Vector3(0.3f, -3, 0), Quaternion.identity);
            BottleButtons.transform.SetPositionAndRotation(new Vector3(0, 0, 0), Quaternion.identity);
        }
        else if (Events != 3)
        {
            Destroy(Bottle_var);
            Destroy(Islands_var);
            BottleButtons.transform.SetPositionAndRotation(new Vector3(0, 1000, 0), Quaternion.identity);

        }
    }

    public void EnemyEvent()
    {
        if (Events == 1)
        {
            Destroy(Enemy);
            Enemy = Instantiate(Enemies[Random.Range(0,Enemies.Length)], new Vector3(1.2f, -2.2f, 100), Quaternion.identity);
            ShipsFightButtons.transform.SetPositionAndRotation(new Vector3(0, 0, 0), Quaternion.identity);
        }
        else if (Events != 1)
        {
            Destroy(Enemy);
            ShipsFightButtons.transform.SetPositionAndRotation(new Vector3(0, 1000, 0), Quaternion.identity);
        }
    }
}