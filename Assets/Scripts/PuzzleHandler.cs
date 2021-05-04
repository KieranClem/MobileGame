using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PuzzleHandler : MonoBehaviour
{
    public Button UseButton;
    public Text UseButtonText;
    
    [Header("Key Panel Variables")]
    public GameObject KeyPadHolder;
    public string KeyPadAnswer = "12345";
    public Text EnteredCodeDisplay;
    private string PlayerEnteredCode;
    public bool KeyPanelSolved = false;


    
    // Start is called before the first frame update
    void Start()
    {
        KeyPadHolder.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddToPlayerEnteredCode()
    {
        string numberadded = EventSystem.current.currentSelectedGameObject.GetComponent<ButtonID>().ButtonNumber;
        PlayerEnteredCode += numberadded;
        EnteredCodeDisplay.text = PlayerEnteredCode;
    }

    public void CheckKeyPadCorrect()
    {
        if(KeyPadAnswer == PlayerEnteredCode)
        {
            //responce
            KeyPanelSolved = true;
            CloseKeyPanel();
        }
        else
        {
            PlayerEnteredCode = "";
            EnteredCodeDisplay.text = "Incorrect code";
        }
    }

    public void OpenKeyPanel()
    {
        Time.timeScale = 0;
        KeyPadHolder.SetActive(true);
        UseButtonText.text = "Close Panel";
        UseButton.onClick.AddListener(delegate { CloseKeyPanel(); });
    }

    public void CloseKeyPanel()
    {
        Time.timeScale = 1;
        KeyPadHolder.SetActive(false);
        UseButton.onClick.RemoveAllListeners();
        UseButtonText.text = "";
        PlayerEnteredCode = "";
    }
}
