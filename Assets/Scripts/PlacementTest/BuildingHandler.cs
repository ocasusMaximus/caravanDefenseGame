﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingHandler : MonoBehaviour
{
    public BuildableObject[] buildables;
    [SerializeField] private int selectionIndex;
    public Camera mainCamera;
    public LayerMask buidlableAreaMask;
    public Material ghostMaterial;
    public Material collisionMaterial;
    public Grid grid;
    private GameObject ghost;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void Select(int index)
    {
        if(index >= 0 || index < buildables.Length)
        {
            selectionIndex = index;
        }
    }
    public void Deselect()
    {
        selectionIndex = -1;
    }

    // Update is called once per frame
    void Update()
    {
        if (selectionIndex >= 0)
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            Physics.Raycast(ray, out hit, Mathf.Infinity, buidlableAreaMask);
           
            if (hit.collider != null)
            {
                Vector3 position = grid.GetNearestPointOnGrid(hit.point);
                BuildableObject buildable = buildables[selectionIndex];
                if (ghost == null)
                {
                   
                    ghost = Instantiate(buildable.prefab, position + buildable.offset, Quaternion.identity);
                  
                    ghost.GetComponent<Renderer>().material = ghostMaterial;
                    
                }
                else
                {
                    ghost.transform.position = position + buildable.offset;
                    if (!CanPlaceObject())
                    {
                        //ghost.GetComponent<Renderer>().material = collisionMaterial; 
                      } else {
                        //ghost.GetComponent<Renderer>().material = ghostMaterial;
                    }

                    Debug.Log(ghost.GetComponent<Ghost>().colliding);

                    //Physics.Raycast(ray, out hit, Mathf.Infinity, collisionAreaMask);
                    //if(hit.collider != null)
                    //{
                    //    ghost.GetComponent<Renderer>().material = collisionMaterial;
                    //}

                    ////kdyz ghost koliduje s necim co neni ground v tehlech podminkach tak zmen barvu a neumozni polozeni objektu
                    //ghost.GetComponent<Collider>().
                    ////if(hit.collider.laye)

                    //Debug.Log(hit.collider.name);
                }

                if (Input.GetMouseButtonDown(0))
                {
                
                 if(CanPlaceObject())
                   Instantiate(buildable.prefab, position + buildable.offset, Quaternion.identity);

                }
            }
            else
            {
                Destroy(ghost);
              
            }
        }
    }


    bool CanPlaceObject()
    {
        return ghost.GetComponent<Ghost>().colliding != true;
    }



}