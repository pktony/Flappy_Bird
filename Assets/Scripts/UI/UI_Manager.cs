using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Manager : MonoBehaviour
{
    private Image img = null;
    private void Awake()
    {
        img = GetComponent<Image>();
    }

    void ShowNewUI()
    {
        img.color = Color.white;
    }
}
