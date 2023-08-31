using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public int baseDamage;
    public float damageMultiplier = 0;

    private void Start()
    {
        damageMultiplier = 0;
    }
}
