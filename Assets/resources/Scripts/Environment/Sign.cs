using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sign : MonoBehaviour
{
    public string text;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.gameObject.CompareTag("Player")) return;

        UIController uc = (UIController)UIController.instance;
        uc.showHelpPanel(this.text);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.gameObject.CompareTag("Player")) return;

        UIController uc = (UIController)UIController.instance;
        uc.hideHelpPanel();
    }
}
