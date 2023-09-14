using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GAMEOVER : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI text;

    public void ShowRecord()
    {
        text.text = PlayerPrefs.GetString("Timer");
    }
}
