using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstructionPanel : MonoBehaviour
{
    [SerializeField]
    private GameObject popupWindow;

    private void Start()
    {
       // popupWindow.SetActive(false);
    }

    public void ShowPopUp()
    {
        popupWindow.SetActive(true);
    }

    public void ClosePopUp()
    {
        popupWindow.SetActive(false);
    }

}
