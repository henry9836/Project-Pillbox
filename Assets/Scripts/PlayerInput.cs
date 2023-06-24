using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] public LayerMask InteractLayerMask;
    [SerializeField] public LayerMask KillLayerMask;
    [SerializeField] public LayerMask RepairLayerMask;
    private PlayerCamera PlayerCamera;
    public PillboxController PillboxController;
    public int HealThreshold = 3;
    private int HealAmount = 0;

    private void Start()
    {
        PlayerCamera = GetComponent<PlayerCamera>();
    }

    void Interact(GameObject OtherObject)
    {
        if (OtherObject.CompareTag("Slit"))
        {
            OtherObject.GetComponent<SlitController>().Interact();
        }
        else
        {
            if (!OtherObject.GetComponent<InteractiveObject>())
            {
                return;
            }

            OtherObject.GetComponent<InteractiveObject>().OnStartInteract();
            if (OtherObject.GetComponent<InteractiveObject>().UIInterface)
            {   
                OtherObject.GetComponent<InteractiveObject>().UIInterface.SetActive(true);
            }

            PlayerCamera.UnEquip();
            PlayerCamera.EquippedItem = OtherObject;
        }
    }
    
    void Update()
    {
        RaycastHit Hit;
        Ray Origin = Camera.main.ScreenPointToRay(Input.mousePosition);
        
        if (Input.GetButtonDown("Interact"))
        {
            //If we do not have an item equipped then just interact otherwise we shouldn't interact
            if (PlayerCamera.EquippedItem)
            {
                InteractiveObjectsType Type = PlayerCamera.EquippedItem.GetComponent<InteractiveObject>().Type;
                if ((Type == InteractiveObjectsType.Rifle) || (Type == InteractiveObjectsType.WesternPewPew))
                {
                    if (Physics.Raycast(Origin, out Hit, Mathf.Infinity, KillLayerMask))
                    {
                        if (Type == InteractiveObjectsType.Rifle)
                        {
                            Camera.main.GetComponent<PlayerCamera>().EquippedItem.GetComponent<RifleState>().Fire(Hit);
                        }
                        else
                        {
                            Camera.main.GetComponent<PlayerCamera>().EquippedItem.GetComponent<WesternState>().Fire(Hit);
                        }
                    }
                }
                if (Type == InteractiveObjectsType.Hammer)
                {
                    HealAmount++;
                    if (HealAmount >= HealThreshold)
                    {
                        PillboxController.Heal();
                        HealAmount = 0;
                    }
                }
            }
            else
            {
                if (Physics.Raycast(Origin, out Hit, Mathf.Infinity, InteractLayerMask))
                {
                    Interact(Hit.collider.gameObject);
                }
            }
        }
    }
}
