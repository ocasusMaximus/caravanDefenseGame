﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentManager : MonoBehaviour
{
    #region Singleton
    public static EquipmentManager instance;

     void Awake()
    {
        instance = this;
    }

    #endregion
    CharacterSelector characterSelector;
    Character selectedCharacter;
    public SkinnedMeshRenderer targetMesh; // empty player body mesh
    public GameObject handBone;
    Item[] currentEquipment;   //currently equiped items
    SkinnedMeshRenderer[] currentMeshes;
    

    public delegate void OnEquipmentChanged(Item newItem, Item oldItem);
    public OnEquipmentChanged onEquipmentChanged;

    Inventory inventory;
    //for weapons
    public Vector3 pickPosition;
    public Vector3 pickRotation;
    public Vector3 pickScale;

    void Start()
    {
        inventory = Inventory.instance;
        characterSelector = CharacterSelector.instance;
        selectedCharacter = characterSelector.selectedCharacter;

        currentEquipment = selectedCharacter.currentEquipment;
        //currentMeshes = selectedCharacter.currentMeshes;

        int numSlots = System.Enum.GetNames(typeof(EquipmentSlot)).Length;
        currentMeshes = new SkinnedMeshRenderer[numSlots];

    }

    public void Equip (Item newItem)
    {
        selectedCharacter = characterSelector.selectedCharacter; //makes sure the right character is selected
        currentEquipment = selectedCharacter.currentEquipment;   //udělat OnCharacterSwich() nebo tak něco
        //currentMeshes = selectedCharacter.currentMeshes;

        int slotIndex = (int)newItem.equipSlot;

        Item oldItem = null;

        if (currentEquipment[slotIndex] != null) //swap items if something's already equiped
        {
            oldItem = currentEquipment[slotIndex];
            inventory.Add(oldItem);
        }

        if (onEquipmentChanged != null)
        {
            onEquipmentChanged.Invoke(newItem, oldItem);
        }

        currentEquipment[slotIndex] = newItem;
        SkinnedMeshRenderer newMesh = Instantiate<SkinnedMeshRenderer>(newItem.mesh);
        newMesh.transform.parent = targetMesh.transform; //parent the equipment mesh to player mesh
        

        newMesh.bones = targetMesh.bones;
        if (newItem.equipSlot != EquipmentSlot.Weapon)
        {
            newMesh.rootBone = targetMesh.rootBone;
        }
        else{
            //GameObject handBone = targetArmature.transform.Find("weapon_bone").gameObject; // make weapon a child of handbone         
            newMesh.transform.parent = handBone.transform;
           // newMesh.transform.position = handBone.transform.position;
           newMesh.transform.localPosition = pickPosition;
            newMesh.transform.localEulerAngles = pickRotation;
            newMesh.transform.localScale = pickScale;

        }

        currentMeshes[slotIndex] = newMesh;

       
    }

    public void Unequip (int slotIndex) 
    {
        if (currentEquipment[slotIndex] != null)
        {
            if (currentMeshes[slotIndex] !=null)
            {
                Destroy(currentMeshes[slotIndex].gameObject);
            }

            Item oldItem = currentEquipment[slotIndex];
            inventory.Add(oldItem);

            currentEquipment[slotIndex] = null;

            if (onEquipmentChanged != null)
            {
                onEquipmentChanged.Invoke(null, oldItem);
            }
        }
    }

    public void UpdateCharacter(Character newCharacter)
    {
        for (int i = 0; i < currentEquipment.Length; i++)
        {
            if(currentMeshes[i] != null)
            {
                
                Destroy(currentMeshes[i].gameObject);
                Debug.Log("removed old equip mesh!");

            }
        }

        for (int i = 0; i < newCharacter.currentEquipment.Length; i++) //problém je tady, instantiate nedělá co má
        {
            Item newItem = newCharacter.currentEquipment[i];           //itemy se zobrazují miniaturní v rohu Canvasu!??
            if (newCharacter.currentEquipment[i] != null)
            {
                //int slotIndex = (int)newItem.equipSlot;
                //currentEquipment[slotIndex] = newItem;
                                
                //int numSlots = System.Enum.GetNames(typeof(EquipmentSlot)).Length; //není potřeba?
                //currentMeshes[i] = newItem.mesh; //tohle je nesmysl
                SkinnedMeshRenderer newMesh = Instantiate<SkinnedMeshRenderer>(newItem.mesh);
                newMesh.transform.parent = targetMesh.transform; //parent the equipment mesh to player mesh
                Debug.Log("newItem = " + newItem.name); //správně vypisuje seznam itemů co má newCharacter nasobě

            }
        }
         
    }

  
    public void UnequipAll()
    {
        for (int i = 0; i < currentEquipment.Length; i++)
        {
            Unequip(i);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            UnequipAll();
        }
    }

}
