using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class Text_InputField : MonoBehaviour
{
    public TMP_InputField field = null;

    private void Start()
    {
        field.onSubmit.AddListener(onSubmit);
        field.onValueChanged.AddListener(onValueChange);
        field.onEndEdit.AddListener(onEndEdit);
    }

    private void onEndEdit(string arg0)
    {
        Debug.Log("onEndEdit");
    }

    private void onSubmit(string arg0)
    {
        Debug.Log("onsubmit");
    }

    private void onValueChange(string arg0)
    {
        Debug.Log("onvaluechange");
    }
}
