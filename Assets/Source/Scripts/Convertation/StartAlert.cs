using System;
using System.Collections;
using PixelCrushers.DialogueSystem;
using UnityEngine;

public class StartAlert : MonoBehaviour
{
    [SerializeField] private string _message;
    [SerializeField] private float _delay = 1;
    [SerializeField] private ChooseDialoguePlace _chooseDialoguePlace;
    [SerializeField]  private EndPlayConverstation _endPlayConverstation;
    [SerializeField]  private StartPlayConverstation _startPlayConverstation;
    
    private Coroutine _coroutine;

    public ChooseDialoguePlace ChooseDialoguePlaceCurrent => _chooseDialoguePlace;
    
    private void Start() =>
        _coroutine = StartCoroutine(StartMessage());

    public void StartEndDialogue() => 
        _endPlayConverstation.gameObject.SetActive(true);

    private IEnumerator StartMessage()
    {
        var delay = new WaitForSeconds(_delay);
        yield return delay;

        if (!string.IsNullOrEmpty(_message))
            DialogueManager.ShowAlert(_message);
        StopCoroutine(_coroutine);
        
        if (_chooseDialoguePlace != ChooseDialoguePlace.End) 
            _startPlayConverstation.gameObject.SetActive(true);
    }
    

    public enum ChooseDialoguePlace
    {
        Start = 0,
        End = 1,
        StartAndEnd = 3
    }
}