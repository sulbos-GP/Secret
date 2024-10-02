// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using HoloToolkit.Unity.InputModule;
using System;
using System.Collections;
using UnityEngine;

#if UNITY_WSA && UNITY_2017_2_OR_NEWER
using UnityEngine.XR.WSA.Input;
#endif

namespace HoloToolkit.Unity.ControllerExamples
{
    public class LeftHand : AttachToController
    {
        public Light li;
        private bool onHand=false;
        public GameObject play;
        public GameObject hit;
        public GameObject gameManager;
        public GameObject Cursor;
        public enum StateEnum
        {
            Uninitialized,
            Idle,
            Switching,
            Spawning
        }

        public int MeshIndex
        {
            get { return meshIndex; }
            set
            {
                if (state != StateEnum.Idle)
                {
                    return;
                }

                if (meshIndex != value)
                {
                    meshIndex = value;
                    state = StateEnum.Switching;
                    StartCoroutine(SwitchOverTime());
                }
            }
        }



        [Header("Animation")]
        [SerializeField]
        private Animator animator;

        private int meshIndex = 0;
        private StateEnum state = StateEnum.Uninitialized;
        private Material instantiatedMaterial;

        private void Update()
        {
            if (state != StateEnum.Idle)
            { 
                return;
            }


        }





        protected override void OnAttachToController()
        {

#if UNITY_WSA && UNITY_2017_2_OR_NEWER
            // Subscribe to input now that we're parented under the controller
            InteractionManager.InteractionSourcePressed += InteractionSourcePressed;
            InteractionManager.InteractionSourceReleased += InteractionSourceReleased;
#endif

            state = StateEnum.Idle;
            Cursor.SetActive(false);
        }

        protected override void OnDetachFromController()
        {
#if UNITY_WSA && UNITY_2017_2_OR_NEWER
            // Unsubscribe from input now that we've detached from the controller
            InteractionManager.InteractionSourcePressed -= InteractionSourcePressed;
            InteractionManager.InteractionSourceReleased -= InteractionSourceReleased;
#endif

            state = StateEnum.Uninitialized;
            Cursor.SetActive(false);
        }

        private IEnumerator SwitchOverTime()
        {
            animator.SetTrigger("Switch");

            // Wait for the animation to play out
            while (!animator.GetCurrentAnimatorStateInfo(0).IsName("SwitchStart"))
            {
                yield return null;
            }

            while (animator.GetCurrentAnimatorStateInfo(0).IsName("SwitchStart"))
            {
                yield return null;
            }

            // Now switch the mesh on the display object
            // Then wait for the reverse to play out

            while (animator.GetCurrentAnimatorStateInfo(0).IsName("SwitchFinish"))
            {
                yield return null;
            }

            state = StateEnum.Idle;
            yield break;
        }

#if UNITY_WSA && UNITY_2017_2_OR_NEWER
        private void InteractionSourcePressed(InteractionSourcePressedEventArgs obj)
        {
            // Check handedness, see if it is left controller
            if (obj.state.source.handedness == Handedness)
            {
                switch (obj.pressType)
                {
                    // If it is Select button event, spawn object
                    case InteractionSourcePressType.Select:
                        if (state == StateEnum.Idle)
                        {
                            // We've pressed select - enter spawning state
                            state = StateEnum.Spawning;
                            Cursor.SetActive(true);
                        }
                        break;
                    default:
                        break;
                }
            }
        }

        private void InteractionSourceReleased(InteractionSourceReleasedEventArgs obj)
        {
            if (obj.state.source.handedness == Handedness)
            {
                switch (obj.pressType)
                {
                    case InteractionSourcePressType.Select:
                        if (state == StateEnum.Spawning)
                        {
                            // We've released select - return to idle state
                            state = StateEnum.Idle;
                            Cursor.SetActive(false);
                            //SpawnObject();
                            play = gameObject;
                            RaycastHit seen;
                            Ray raydirection = new Ray(transform.position, transform.forward);
                            Debug.DrawRay(raydirection.origin, raydirection.direction);
                            if (Physics.Raycast(raydirection, out seen, 2f))
                            {
                                Debug.Log(seen.collider.name);
                                if (seen.collider.tag == "Keys")
                                {
                                    gameManager.SendMessage("GetKey", seen.transform.name);
                                    seen.transform.gameObject.SendMessage("Gotten");
                                }
                                else if (seen.collider.tag == "Evident")
                                {
                                    gameManager.SendMessage("GetEvident", seen.transform.name);
                                    seen.transform.gameObject.SendMessage("Gotten");
                                }
                                else if (seen.collider.tag == "Door")
                                {
                                    seen.transform.gameObject.SendMessage("Open");
                                }
                            }
                        }
                        break;

                    default:
                        break;
                }
            }
        }
#endif
    }
}
