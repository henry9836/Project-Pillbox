using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCamera : MonoBehaviour
{
    public bool Dead = false;
    public GameObject EquippedItem;
    public Texture2D DefaultCursor;
    public Texture2D RifleCursor;
    public Texture2D AimCursor;
    public Texture2D HammerCursor;
    public List<GameObject> EquippableItems;
    public float CameraRotationSpeed = 20.0f;
    public int Ammo;
    private Vector3 TargetAngle;
    private bool CameraPanning;
    private bool LookButtonReleased;
    private bool LookingDown;
    private bool HackFix;
    private Vector3 OldAngle;

    public void EffectAmmo(int Value)
    {
        Ammo += Value;
        Ammo = Mathf.Clamp(Ammo, 0, 6);
    }

    public void UnEquip()
    {
        if (!EquippedItem)
        {
            return;
        }

        EquippedItem.GetComponent<InteractiveObject>().OnEndInteract();
        EquippedItem = null;
    }
    
    void Start()
    {
        TargetAngle = new Vector3(0.0f, 0.0f, 0.0f);
        Cursor.visible = true;
    }

    public void UpdateCursor()
    {
        Texture2D NewCursor = DefaultCursor;
        if (EquippedItem)
        {
            InteractiveObjectsType Type = EquippedItem.GetComponent<InteractiveObject>().Type;
            if (Type == InteractiveObjectsType.Hammer)
            {
                NewCursor = HammerCursor;
            }
            else if (Type == InteractiveObjectsType.Rifle)
            {
                NewCursor = RifleCursor;
            }
            else if (Type == InteractiveObjectsType.WesternPewPew)
            {
                NewCursor = AimCursor;
            }
        }
        Cursor.SetCursor(NewCursor, new Vector2(0.5f, 0.5f), CursorMode.Auto);
    }

    void Update()
    {
        if (!HackFix)
        {
            return;
        }
        
        // Determine which direction to rotate towards
        Vector3 targetDirection = TargetAngle - transform.position;

        // The step size is equal to speed times frame time.
        float singleStep = CameraRotationSpeed * Time.deltaTime;

        // Rotate the forward vector towards the target direction by one step
        Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, singleStep, 0.0f);

        transform.rotation = Quaternion.LookRotation(newDirection);

        //Check if angle is same if so we have stopped
        if (OldAngle == newDirection)
        {
            CameraPanning = false;
        }

        OldAngle = newDirection;
    }

    void FixedUpdate()
    {
        UpdateCursor();

        if (Input.GetButtonDown("Unequip"))
        {
            UnEquip();
        }
        
        float Horizontal = Input.GetAxis("LookHorizontal");
        float Vertical = Input.GetAxis("LookVertical");

        if ((Horizontal != 0.0f) || (Vertical != 0.0f))
        {
            HackFix = true;
            // Do not allow more camera pan till we are done panning
            if (CameraPanning || !LookButtonReleased)
            {
                return;
            }

            CameraPanning = true;
            LookButtonReleased = false;

            if (Horizontal != 0.0f)
            {
                if (LookingDown)
                {
                    return;
                }
                TargetAngle = transform.position + ((transform.right * Horizontal) * 10.0f);
            }
            else
            {
                if (Vertical > 0.0f)
                {
                    if (!LookingDown)
                    {
                        return;
                    }
                    LookingDown = false;
                    TargetAngle = transform.position + (transform.up * 10.0f);
                }
                else
                {
                    if (LookingDown)
                    {
                        return;
                    }

                    LookingDown = true;
                    TargetAngle = transform.position + (transform.up * -10.0f);
                }
            }
        }
        else
        {
            LookButtonReleased = true;
        }
    }
}
