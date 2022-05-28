using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[Serializable]
public class UIManager : MonoBehaviour
{
    public string gameName;
    
    [Header("Units")] 
    public List<Unit> allUnits;
    public List<Unit> deadUnits;

    [Header("Base UI")] 
    public GameObject stageOne;
    public GameObject stageTwo;
    public GameObject stageThree;
    public GameObject stageFour;
    public TMP_InputField inputField;
    public Sprite emptyImage;
    
    [Header("Buttons")]
    public Button[] stageOneButtons;
    public Button[] stageTwoButtons;
    public Button[] stageThreeButtons;
    public Button[] stageFourButtons;


    private bool addedUnit;
    
    private void Start()
    {
        Load();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Return))
            OnTryAddUnit();
    }

    /// <summary>
    /// Tries adding a dead unit.
    /// </summary>
    public void OnTryAddUnit()
    {
        // Using the Input Field.
        var newUnit = inputField.text.ToLower(); 
        if(newUnit == "")
            return;

        // Two Commands.
        if (newUnit == "clear")
        {
            deadUnits.Clear();
            inputField.text = "";
        }
        else if (newUnit.Contains("add"))
        {
            newUnit = newUnit.Remove(0, 3);
            for (int i = 0; i < int.Parse(newUnit); i++)
            {
                deadUnits.Add(allUnits[0]);
            }
            
            inputField.text = "";
        }
        // Adding the Unit.
        else
        {
            foreach (var t in allUnits.Where(t => t.unitName.ToLower() == newUnit))
            { 
                // Adds Unit and ends the process. 
                deadUnits.Add(t);
                inputField.text = "";
                addedUnit = true;
                break; 
            }
        }

        if (deadUnits.Count == stageTwoButtons.Length + 1 && addedUnit)
        {
            Screen.SetResolution(1200, 500, false);
            var transform1 = transform;
            var position = transform1.position;
            position = new Vector3(position.x, position.y + 100, position.z);
            transform1.position = position;
        }
        
        UpdateImages();

        // Tries translating the string into a color and changes the background.
        if (!addedUnit)
        {
            if (!ColorUtility.TryParseHtmlString(inputField.text, out var newColor)) 
                return;
            
            Camera.main.backgroundColor = newColor;
            inputField.text = "";
            Save();
        }
        else
        {
            addedUnit = false;
        }
    }
    
    /// <summary>
    /// Tries adding a dead unit.
    /// </summary>
    public void OnTryAddUnit(string newUnit)
    {
        newUnit = newUnit.ToLower();
        
        if(newUnit == "")
            return;
        
        foreach (var t in allUnits.Where(t => t.unitName.ToLower() == newUnit))
        {
            // Adds Unit and ends the process. 
            deadUnits.Add(t);
            inputField.text = ""; 
            break;
        }

        if (deadUnits.Count == stageTwoButtons.Length + 1)
        {
            Screen.SetResolution(1200, 500, false);
            var transform1 = transform;
            var position = transform1.position;
            position = new Vector3(position.x, position.y + 100, position.z);
            transform1.position = position;
        }
        
        UpdateImages();
    }

    /// <summary>
    /// Tries removing a dead unit.
    /// </summary>
    /// <param name="btn"> The pressed button. </param>
    public void OnTryRemoveUnit(Button btn)
    {
        int index = int.Parse(btn.name);
        
        if(index >= deadUnits.Count)
            return;
        
        deadUnits.Remove(deadUnits[index]);
        
        if (deadUnits.Count <= stageTwoButtons.Length && !stageOne.activeSelf && !stageTwo.activeSelf)
        {
            Screen.SetResolution(800, 300, false);
            var transform1 = transform;
            var position = transform1.position;
            position = new Vector3(position.x, position.y - 100, position.z);
            transform1.position = position;
        }
        
        UpdateImages();
    }

    public void OnGoToMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    /// <summary>
    /// Updates all the Images.
    /// </summary>
    private void UpdateImages()
    {
        if (deadUnits.Count <= stageOneButtons.Length)
        {
            for (int i = 0; i < stageOneButtons.Length; i++)
            {
                if (i >= deadUnits.Count)
                {
                    stageOneButtons[i].image.sprite = emptyImage;
                    stageOneButtons[i].image.enabled = false;
                }
                else
                {
                    stageOneButtons[i].image.sprite = deadUnits[i] != null ? deadUnits[i].sprite : emptyImage;
                    stageOneButtons[i].image.enabled = true;
                }
            }
            
            stageOne.SetActive(true);
            stageTwo.SetActive(false);
            stageThree.SetActive(false);
            stageFour.SetActive(false);
        }
        else if(deadUnits.Count <= stageTwoButtons.Length)
        {
            for (int i = 0; i < stageTwoButtons.Length; i++)
            {
                if (i >= deadUnits.Count)
                {
                    stageTwoButtons[i].image.sprite = emptyImage;
                    stageTwoButtons[i].image.enabled = false;
                }
                else
                {
                    stageTwoButtons[i].image.sprite = deadUnits[i] != null ? deadUnits[i].sprite : emptyImage;
                    stageTwoButtons[i].image.enabled = true;
                }
            }

            stageOne.SetActive(false);
            stageTwo.SetActive(true);
            stageThree.SetActive(false);
            stageFour.SetActive(false);
        }
        else if(deadUnits.Count <= stageThreeButtons.Length)
        {
            for (int i = 0; i < stageThreeButtons.Length; i++)
            {
                if (i >= deadUnits.Count)
                {
                    stageThreeButtons[i].image.sprite = emptyImage;
                    stageThreeButtons[i].image.enabled = false;
                }
                else
                {
                    stageThreeButtons[i].image.sprite = deadUnits[i] != null ? deadUnits[i].sprite : emptyImage;
                    stageThreeButtons[i].image.enabled = true;
                }
            }

            stageOne.SetActive(false);
            stageTwo.SetActive(false);
            stageThree.SetActive(true);
            stageFour.SetActive(false);
        }
        else
        {
            for (int i = 0; i < stageFourButtons.Length; i++)
            {
                if (i >= deadUnits.Count)
                {
                    stageFourButtons[i].image.sprite = emptyImage;
                    stageFourButtons[i].image.enabled = false;
                }
                else
                {
                    stageFourButtons[i].image.sprite = deadUnits[i] != null ? deadUnits[i].sprite : emptyImage;
                    stageFourButtons[i].image.enabled = true;
                }
            }

            stageOne.SetActive(false);
            stageTwo.SetActive(false);
            stageThree.SetActive(false);
            stageFour.SetActive(true);
        }
        
        Save();
    }

    private void Save()
    {
        string actualDeadUnits = "";
        
        for (int i = 0; i < deadUnits.Count; i++)
        {
            actualDeadUnits += deadUnits[i].unitName + "-";
        }
        SaveSystem.SetString(gameName, actualDeadUnits);

        Color saveColor = Camera.main.backgroundColor;
        string colorRGBA = saveColor.r + "-" + saveColor.g + "-" + saveColor.b + "-" + saveColor.a;
        SaveSystem.SetString(string.Concat(gameName, "BackgroundColor"), colorRGBA);
    }

    private void Load()
    {
        string backgroundColor = SaveSystem.GetString(string.Concat(gameName, "BackgroundColor"));

        var aRGBA = backgroundColor.Split('-');
        float[] rgba = new float[4];
        bool hasColor = true;
        
        for (int i = 0; i < rgba.Length; i++)
        {
            if (!float.TryParse(aRGBA[i], out rgba[i]))
            {
                hasColor = false;
                break;
            }
        }
        
        if(hasColor)
            Camera.main.backgroundColor = new Color(rgba[0], rgba[1], rgba[2], rgba[3]);
        
        string actualDeadUnits = SaveSystem.GetString(gameName);
        var aDU = actualDeadUnits.Split('-');

        for (int i = 0; i < aDU.Length; i++)
        {
            for (int j = 0; j < allUnits.Count; j++)
            {
                if (aDU[i] == allUnits[j].unitName)
                {
                    OnTryAddUnit(aDU[i]);
                    break;
                }
            }
        }
    }
}