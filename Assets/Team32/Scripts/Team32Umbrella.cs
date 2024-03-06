using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Team32Umbrella : MicrogameInputEvents
{
    public PhysicsMaterial2D material1; // PhysicsMaterial2D specified in the Unity Editor
    private bool materialApplied = false; // Tracks whether the physics material has been applied

    protected override void OnGameStart()
    {
        Collider2D collider = GetComponent<Collider2D>();
        collider.sharedMaterial = material1;
        materialApplied = true; // Update the flag state
    }
    
    protected override void OnButton1Pressed(InputAction.CallbackContext context)
    {
        Collider2D collider = GetComponent<Collider2D>();


                // If the physics material has already been applied, remove it (i.e., set to null)
                if (materialApplied)
                {
                    collider.sharedMaterial = null;
                    materialApplied = false; // Update the flag state
                    transform.localScale = new Vector3(.5f, 1, 0);
                }
                // If the physics material has not been applied, apply the new physics material
                else
                {
                    collider.sharedMaterial = material1;
                    materialApplied = true; // Update the flag state
                    transform.localScale = new Vector3(1, 1, 0);
                }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Collider2D collider = GetComponent<Collider2D>();

        if (materialApplied)
        {
            collider.sharedMaterial = null;
            materialApplied = false; // Update the flag state
            transform.localScale = new Vector3(.5f, 1, 0);
        }
    }

}
