﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldMovement : MonoBehaviour
{
    public bool canCompute;

    public float shieldAngle;

    public enum Directions { N, NE, E, SE, S, SW, W, NW};

    public Directions shieldDirection;

    public PlayerPlatformer playerScript;
    
    private void Awake()
    {
        shieldDirection = Directions.E;

        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerPlatformer>();

        canCompute = true;
    }

    void Update()
    {
        MoveShield(DetectDirection(ComputeAngle(canCompute)));
    }

    private float ComputeAngle(bool x)
    {
        if (x)
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
        else
        {
            return 0;
        }
    }

    private Directions DetectDirection(float angle)
    {
        if (canCompute)
        {
            if ((shieldAngle <= 15 && shieldAngle >= 0) || (shieldAngle >= 345 && shieldAngle <= 360))
            {
                shieldDirection = Directions.E;
            }
            else if (shieldAngle < 344 && shieldAngle > 285)
            {
                if (!playerScript.grounded)
                {
                    shieldDirection = Directions.SE;
                }
            }
            else if (shieldAngle <= 284 && shieldAngle >= 255)
            {
                if (!playerScript.grounded)
                {
                    shieldDirection = Directions.S;
                }
            }
            else if (shieldAngle < 254 && shieldAngle > 195)
            {
                if (!playerScript.grounded)
                {
                    shieldDirection = Directions.SW;
                }
            }
            else if (shieldAngle <= 194 && shieldAngle >= 165)
            {
                shieldDirection = Directions.W;
            }
            else if (shieldAngle < 164 && shieldAngle > 105)
            {
                shieldDirection = Directions.NW;
            }
            else if (shieldAngle <= 104 && shieldAngle >= 75)
            {
                shieldDirection = Directions.N;
            }
            else if (shieldAngle < 74 && shieldAngle > 16)
            {
                shieldDirection = Directions.NE;
            }
            return shieldDirection;
        }
        else return Directions.E;
    }

    private void MoveShield(Directions directions)
    {
        if(!playerScript.grabbing)
        switch (shieldDirection)
        {
            case Directions.N:
                transform.eulerAngles = new Vector3(0, 0, 90);
                break;

            case Directions.NE:
                transform.eulerAngles = new Vector3(0, 0, 45);
                playerScript.transform.rotation = Quaternion.identity;
                break;

            case Directions.E:
                transform.eulerAngles = new Vector3(0, 0, 0);
                break;

            case Directions.SE:
                transform.eulerAngles = new Vector3(0, 0, 315);
                playerScript.transform.rotation = Quaternion.identity;
                break;

            case Directions.S:
                transform.eulerAngles = new Vector3(0, 0, 270);
                break;

            case Directions.SW:
                transform.eulerAngles = new Vector3(0, 0, 225);
                playerScript.transform.eulerAngles = new Vector3(0, 180, 0);
                break;

            case Directions.W:
                transform.eulerAngles = new Vector3(0, 0, 180);
                break;

            case Directions.NW:
                transform.eulerAngles = new Vector3(0, 0, 135);
                playerScript.transform.eulerAngles = new Vector3(0, 180, 0);
                break;

            default:
                Debug.Log(shieldDirection);
                break;
        }
    }
}
