using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrencyGiverTool : MonoBehaviour
{
    [SerializeField] private SoftCurrencyHolder _currencyHolder;
    [SerializeField] private int _valueToAdd = 5000;
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
            _currencyHolder.AddSoft(_valueToAdd);
    }
}
