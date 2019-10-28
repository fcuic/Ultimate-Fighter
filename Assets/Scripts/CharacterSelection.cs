using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class CharacterSelection : MonoBehaviour
{
    private int selectedCharacterIndex;

    [Header("List of Characters")]
    [SerializeField] private List<CharacterSelectObject> characterList = new List<CharacterSelectObject>();

    [Header("UI References")]
    [SerializeField] private TextMeshProUGUI characterName;
    [SerializeField] private Image characterSplash;
    [SerializeField] private Image backgroundColor;


    [Header("Sounds")]
    [SerializeField] private AudioClip arrowClickSFX;
    [SerializeField] private AudioClip characterSelectMusic;


    
    private void Start()
    {
        UpdateCharacterSelectionUI();
        AudioManager.Instance.PlayMusicWithFade(characterSelectMusic);
        AudioManager.Instance.SetMusicVolume(0.2f);
        
    }
    public void Confirm() 
    {
        Debug.Log(string.Format("Character {0}:{1} has been chosen", selectedCharacterIndex, characterList[selectedCharacterIndex].CharacterName));


    }
    public void LeftArrow() 
    {
        selectedCharacterIndex--;
        if (selectedCharacterIndex < 0) 
        {
            selectedCharacterIndex = characterList.Count - 1;
        }
        UpdateCharacterSelectionUI();
        AudioManager.Instance.PlaySFX(arrowClickSFX);
    }
    public void RightArrow()
    {
        selectedCharacterIndex++;
        if (selectedCharacterIndex == characterList.Count)
        {
            selectedCharacterIndex = 0;
        }
        UpdateCharacterSelectionUI();
        AudioManager.Instance.PlaySFX(arrowClickSFX);
    }


    private void UpdateCharacterSelectionUI()
    {
        //Changing Spash, name and desired color
        characterSplash.sprite = characterList[selectedCharacterIndex].Splash;
        characterName.text = characterList[selectedCharacterIndex].CharacterName;
        backgroundColor.color = characterList[selectedCharacterIndex].characterColor;
    }

    [System.Serializable]
    public class CharacterSelectObject
    {
        public Sprite Splash;
        public string CharacterName;
        public Color characterColor;
    }
       
}

