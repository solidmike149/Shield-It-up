﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldMovement : MonoBehaviour
{
    public float shieldAngle;

    public enum Directions { N, NE, E, SE, S, SW, W, NW };

    public Directions shieldDirection;

    public PlayerPlatformer playerScript;

    Quaternion left = new Quaternion(0, 180, 0, 0);

    Quaternion up = new Quaternion(0, 0, 180, 0);

    public Transform arm;

    public bool x;
    
    private void Awake()
    {
        x = false;
        shieldDirection = Directions.E;

        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerPlatformer>();
        
        // Arm deve essere il primo child nell'inspector
        arm = transform.GetChild(0);
    }

    void Update()
    {
        MoveShield(DetectDirection(ComputeAngle()));
    }

    private float ComputeAngle()
    {
        Vector2 mousePosition = Input.mousePosition;

        Vector2 targetScreenPosition = Camera.main.WorldToScreenPoint(transform.position);

        Vector2 offset = new Vector2(mousePosition.x - targetScreenPosition.x, mousePosition.y - targetScreenPosition.y);

        shieldAngle = Mathf.Atan2(offset.y, offset.x) * Mathf.Rad2Deg;

        if (shieldAngle < 0)
        {
            shieldAngle += 360;
        }
        return shieldAngle;
    }

    private Directions DetectDirection(float angle)
    {
        if ((shieldAngle <= 15 && shieldAngle >= 0)|| (shieldAngle >= 345 && shieldAngle <= 360))
        {
            shieldDirection = Directions.E;
            x = true;
        }
        else if (shieldAngle < 344 && shieldAngle > 285)
        {
            if (!playerScript.grounded)
            {
                shieldDirection = Directions.SE;
                x = true;
            }
        }
        else if (shieldAngle <= 284 && shieldAngle >= 255)
        {
            if (!playerScript.grounded)
            {
                shieldDirection = Directions.S;
                x = true;
            }
           
        }
        else if (shieldAngle < 254 && shieldAngle > 195)
        {
            if (!playerScript.grounded)
            {
                shieldDirection = Directions.SW;
                x = true;
            }
        }
        else if (shieldAngle <= 194 && shieldAngle >= 165)
        {
            shieldDirection = Directions.W;
            x = true;
        }
        else if (shieldAngle < 164 && shieldAngle > 105)
        {
            if (shieldDirection != Directions.NW)
            {
                shieldDirection = Directions.NW;
                x = true;
            }

        }
        else if (shieldAngle <= 104 && shieldAngle >= 75)
        {
            shieldDirection = Directions.N;
            x = true;
        }
        else if (shieldAngle < 74 && shieldAngle > 16)
        {
            shieldDirection = Directions.NE;
            x = true;
        }
        return shieldDirection;
    }

    private void MoveShield(Directions directions)
    {
        switch (shieldDirection)
        {
            case Directions.N:
                transform.eulerAngles = new Vector3(0, 0, 90);
                break;

            case Directions.NE:
                transform.eulerAngles = new Vector3(0, 0, 45);
                //playerScript.gameObject.transform.rotation = Quaternion.identity;
                if(x)
                playerScript.transform.eulerAngles = Vector3.zero;

                x = false;
                break;

            case Directions.E:
                transform.eulerAngles = new Vector3(0, 0, 0);
                break;

            case Directions.SE:
                transform.eulerAngles = new Vector3(0, 0, 315);
                if(x)
                playerScript.transform.eulerAngles = Vector3.zero;
                // playerScript.gameObject.transform.rotation = Quaternion.identity;

                x = false;
                break;

            case Directions.S:
                transform.eulerAngles = new Vector3(0, 0, 270);
                break;

            case Directions.SW:
                transform.eulerAngles = new Vector3(0, 0, 225);
                playerScript.transform.eulerAngles = new Vector3(0,180,0);
                // playerScript.gameObject.transform.rotation = left;
                if(x)
                arm.Rotate(new Vector3(180,0,0));

                x = false;
                break;

            case Directions.W:
                transform.eulerAngles = new Vector3(0, 0, 180);
                break;

            case Directions.NW:
                transform.eulerAngles = new Vector3(0, 0, 125);
                playerScript.transform.eulerAngles = new Vector3(0,180, 0);
                if(x)
                arm.Rotate(new Vector3(180, 0, 0));
                // playerScript.gameObject.transform.rotation = left;

                x = false;
                break;

            default:
                Debug.Log(shieldDirection);
                break;
        }
    }
}