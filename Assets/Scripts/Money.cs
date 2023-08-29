using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Money : MonoBehaviour
{
    [SerializeField] private int value = 1;
    // Start is called before the first frame update
    public int GetValue(){
        return value;
    }
}
