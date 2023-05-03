using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsAttacked : MonoBehaviour
{
    public int AttackCheker = 0;
    public void Attacked()
    {
        AttackCheker = 1;
    }

    public void AttackReset()
    {
        AttackCheker = 0;
    }
}
