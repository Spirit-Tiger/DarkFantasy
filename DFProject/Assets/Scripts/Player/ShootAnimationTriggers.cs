using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootAnimationTriggers : MonoBehaviour
{

    public SpriteRenderer ShootSpriteRenderer;
    public SpriteRenderer TopBodySpriteRenderer;
    public PlayerInputHandler InputHandler;

    public void SwichToTopAnimation()
    {
        TopBodySpriteRenderer.enabled = true;
        ShootSpriteRenderer.enabled = false;
    }

    public void SwichToShootAnimation()
    {
        ShootSpriteRenderer.enabled = true;
        TopBodySpriteRenderer.enabled = false;
        InputHandler.ShootInput = false;
    }
}
