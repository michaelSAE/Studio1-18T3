// Version 1.7.4
// ©2016 Reindeer Games
// All rights reserved
// Redistribution of source code without permission not allowed

using Exploder.Utils;
using UnityEngine;

namespace Exploder.Demo
{
    public class CursorLocking : MonoBehaviour
    {
        /// <summary>
        /// lock mouse cursor
        /// </summary>
        public bool LockCursor = false;

        /// <summary>
        /// key for locking the mouse cursor
        /// </summary>
        public UnityEngine.KeyCode LockKey;

        /// <summary>
        /// key for unlocking the mouse cursor
        /// </summary>
        public UnityEngine.KeyCode UnlockKey;

        public static bool IsLocked;

        private static CursorLocking instance;

        private void Awake()
        {
            instance = this;
        }

        private void Update()
        {
            if (LockCursor)
            {
                Lock();
            }
            else
            {
                Unlock();
            }

            IsLocked = Compatibility.IsCursorLocked();

            if (UnityEngine.Input.GetKeyDown(LockKey))
            {
                Lock();
            }

            if (UnityEngine.Input.GetKeyDown(UnlockKey))
            {
                Unlock();
            }

            if (Compatibility.IsCursorLocked() == false)
            {
                Compatibility.SetCursorVisible(true);
            }
        }

        public static void Lock()
        {
            Compatibility.LockCursor(true);

            Compatibility.SetCursorVisible(false);
            instance.LockCursor = true;
        }

        public static void Unlock()
        {
            Compatibility.LockCursor(false);
            Compatibility.SetCursorVisible(true);
            instance.LockCursor = false;
        }
    }
}
