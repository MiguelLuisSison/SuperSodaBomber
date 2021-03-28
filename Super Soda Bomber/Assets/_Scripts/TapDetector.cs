using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// Detects the Tap Activity of the UI made by the player
/// </summary>
public class TapDetector : MonoBehaviour, IPointerClickHandler
{
    /// <summary>
    /// Double Click state of the UI
    /// </summary>
    public bool isDoubleClick { get; private set; }
    public void OnPointerClick(PointerEventData eventData){
        if (eventData.clickCount == 2){
            isDoubleClick = true;
        }
        else
            isDoubleClick = false;
    }
}
