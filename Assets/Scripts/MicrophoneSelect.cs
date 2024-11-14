using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;


public class MicrophoneSelect : MonoBehaviour
{
    public TMP_Dropdown sourceDropdown;
    public int perangkatTerpilih = 0;

    public static UnityAction<int> OnMicrophoneChoiceChanged;
    void Start()
    {
        PopulateSourceDropdown();

    }

    private void PopulateSourceDropdown()
    {
        var options = new List<TMP_Dropdown.OptionData>();

        foreach (var microphone in Microphone.devices)
        {
            TMP_Dropdown.OptionData optionData = new TMP_Dropdown.OptionData(microphone, null);

            options.Add(optionData);
        }
        sourceDropdown.options = options;
    }
   
    public void PilihMicrophone(int optionIndex)
    {
        perangkatTerpilih = optionIndex;
        OnMicrophoneChoiceChanged?.Invoke(perangkatTerpilih);
    }
}
