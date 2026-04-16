using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

public class Player : MonoBehaviour
{
    //[Header("General parameters")]
    //[Tooltip("The player's health value"), Range(0f, 100f)]
    public class EventCheck
    {
        bool condition = false;

        GameObject checkedObject = null;
        string operative = "";
        int value = 0;

        public bool CheckEvent()
        {
            if (checkedObject == null)
            {
                condition = false;
                return false;
            }    

            if (operative == "=")
            {
                return true;
            }

            return false;
        }
    }

    /*[Header("Jump parameters")]
    [Tooltip("Whether player can jump")]
    [SerializeField] private bool canJump = false;

    [Tooltip("Whether player takes fall damage")]
    [SerializeField] private bool hasFallDamage = false;

    [Tooltip("Player jump height, in game units")]
    [SerializeField] private float jumpHeight = 10.0f;

    [Tooltip("The delay in milliseconds from input to jump")]
    [SerializeField] private float jumpDelayMS = 15.0f;

    [Tooltip("The coyote time, in milliseconds")]
    [SerializeField] private float coyoteTimeMS = 100.0f;

    [Header("Move parameters")]
    [Tooltip("Whether the player is currently sprinting or not")]
    [SerializeField] private bool isSprinting = false;

    [Tooltip("Player move speed")]
    [SerializeField] private float moveSpeed = 10.0f;

    [Tooltip("How much faster the player sprint is"), Range(1f, 5f)]
    [SerializeField] private float sprintMultiplier = 1.5f;*/
}
