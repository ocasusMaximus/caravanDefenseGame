﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatsUI : MonoBehaviour
{
    EquipmentManager equipmentManager;

    public GameObject selectedCharacter;
    Text characterName;
    Text levelUI;
    Text healthUI;
    Text armorUI;
    Text damageUI;
    Text strengthUI;
    Text agilityUI;
    Text wisdomUI;
    Text resourcesUI;
    

    private void Start()
    {
       // EquipmentManager.instance.onEquipmentChanged += OnEquipmentChanged;
    }

    private void Update()
    {
        UpdateStatsUI(selectedCharacter); 


    }


    public void UpdateStatsUI(GameObject selectedCharacter)
    {
        levelUI = GameObject.Find("TextName").GetComponent<Text>();
        levelUI.text = " " + selectedCharacter.name;

        levelUI = GameObject.Find("TextLvl").GetComponent<Text>();
        levelUI.text = "Level: " + selectedCharacter.GetComponent<PlayerStats>().characterLevel;

        healthUI = GameObject.Find("TextHealth").GetComponent<Text>();
        healthUI.text = "Health: " + selectedCharacter.GetComponent<PlayerStats>().maxHealth.getValue();

        armorUI = GameObject.Find("TextArmor").GetComponent<Text>();
        armorUI.text = "Armor: " + selectedCharacter.GetComponent<PlayerStats>().armor.getValue();

        damageUI = GameObject.Find("TextDamage").GetComponent<Text>();
        damageUI.text = "Damage: " + selectedCharacter.GetComponent<PlayerStats>().damage.getValue();

        damageUI = GameObject.Find("TextAttackSpeed").GetComponent<Text>();
        damageUI.text = "Attack Speed: " + selectedCharacter.GetComponent<PlayerStats>().attackSpeed.getValue();

        damageUI = GameObject.Find("TextMovementSpeed").GetComponent<Text>();
        damageUI.text = "Movement Speed: " + selectedCharacter.GetComponent<PlayerStats>().movementSpeed.getValue();

        damageUI = GameObject.Find("TextSpiritPower").GetComponent<Text>();
        damageUI.text = "Spirit Power: " + selectedCharacter.GetComponent<PlayerStats>().spiritPower.getValue();

        strengthUI = GameObject.Find("TextStatPoints").GetComponent<Text>();
        strengthUI.text = "Unused Stat Points: " + selectedCharacter.GetComponent<PlayerStats>().statPoints.getValue();

        strengthUI = GameObject.Find("TextStrength").GetComponent<Text>();
        strengthUI.text = "Strength: " + selectedCharacter.GetComponent<PlayerStats>().strength.getValue();

        agilityUI = GameObject.Find("TextAgility").GetComponent<Text>();
        agilityUI.text = "Agility: " + selectedCharacter.GetComponent<PlayerStats>().agility.getValue();

        wisdomUI = GameObject.Find("TextWisdom").GetComponent<Text>();
        wisdomUI.text = "Wisdom: " + selectedCharacter.GetComponent<PlayerStats>().wisdom.getValue();

        resourcesUI = GameObject.Find("TextResources").GetComponent<Text>();
        resourcesUI.text = "Resources: " + Resources.instance.currentResources;

        //dissable buttons
        /* if (PlayerStats.statPoints.getValue() > 0)
         {
             strengthButton.interactable = false;
             agilityButton.interactable = false;
             wisdomButton.interactable = false;
         }
         else
         {
             strengthButton.interactable = true;
             agilityButton.interactable = true;
             wisdomButton.interactable = true;
         }*/



    }

    
}
