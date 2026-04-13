using UnityEngine;
using UnityEngine.EventSystems;
using System;

public class HayMachineSwitcher : MonoBehaviour, IPointerClickHandler
{
    public GameObject blueHayMachine;
    public GameObject yellowHayMachine;
    public GameObject redHayMachine;

    private int selectedIndex;
    void Start()
    {
        // 1. Sync the index to the saved setting so the next click is correct
        selectedIndex = (int)GameSettings.hayMachineColor;

        // 2. Update the visuals to match the saved setting
        UpdateVisuals();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        selectedIndex++;
        selectedIndex %= Enum.GetValues(typeof(HayMachineColor)).Length;

        GameSettings.hayMachineColor = (HayMachineColor)selectedIndex;
        UpdateVisuals();
    }

    // Move the switch logic to its own method to call it from Start() and Click
    private void UpdateVisuals()
    {
        blueHayMachine.SetActive(GameSettings.hayMachineColor == HayMachineColor.Blue);
        yellowHayMachine.SetActive(GameSettings.hayMachineColor == HayMachineColor.Yellow);
        redHayMachine.SetActive(GameSettings.hayMachineColor == HayMachineColor.Red);
    }
}
