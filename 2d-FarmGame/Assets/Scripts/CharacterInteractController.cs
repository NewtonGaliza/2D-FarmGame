using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterInteractController : MonoBehaviour
{
    private CharacterController2D characterController;
    private Character character;
    private Rigidbody2D rgbd2d;
    [SerializeField] private float offsetDistance = 1f;
    [SerializeField] private float sizeOfInteractableArea = 1.2f;
    [SerializeReference] private HighlightController highlightController;


    private void Awake()
    {
        characterController = GetComponent<CharacterController2D>();
        character = GetComponent<Character>();
        rgbd2d = GetComponent<Rigidbody2D>();
    }


    private void Update() 
    {
        Check();

        if(Input.GetMouseButtonDown(1))
        {
            Interact();
        }
    }


    private void Check()
    {
         Vector2 position = rgbd2d.position + characterController.lastMotionVector * offsetDistance;  
   
        Collider2D[] colliders = Physics2D.OverlapCircleAll(position, sizeOfInteractableArea);

        foreach(Collider2D c in colliders)
        {
            Interactable hit = c.GetComponent<Interactable>();
            if(hit != null)
            {
                highlightController.Highlight(hit.gameObject);
                return;
            }
        }

        highlightController.Hide();
    }


    private void Interact()
    {
        Vector2 position = rgbd2d.position + characterController.lastMotionVector * offsetDistance;  
   
        Collider2D[] colliders = Physics2D.OverlapCircleAll(position, sizeOfInteractableArea);

        foreach(Collider2D c in colliders)
        {
            Interactable hit = c.GetComponent<Interactable>();
            if(hit != null)
            {
                hit.Interact(character);
                break;
            }
        }
    }
}
