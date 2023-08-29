using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MoneyText : MonoBehaviour
{
    public TMP_Text text;
    public string baseText = "Points: ";
    // Start is called before the first frame update
    public void UpdatePoints(int amount){
        text.text = baseText + amount;
    }
    
}
