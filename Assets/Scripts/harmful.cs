using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class harmful : MonoBehaviour
{
    [SerializeField] private int damage = 1;
    public int GetDamage(){
        return damage;
    }
}
