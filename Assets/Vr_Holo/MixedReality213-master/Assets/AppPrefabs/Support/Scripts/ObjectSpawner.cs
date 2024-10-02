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
    public class ObjectSpawner : AttachToController
    {
        public Light li;
        public Light handLight;
        public bool origin = false;
        public GameObject gameManager;
        public enum StateEnum
        {
            Uninitialized,
            Idle,
            Switching,
            Spawning
        }

        public int MeshIndex {
            get { return meshIndex; }
            set {
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

        float battime = 200;

        private void Update()
        {
            if (state != StateEnum.Idle)
            {
                battime -= Time.deltaTime;
                gameManager.SendMessage("BatterySet", (int)battime);
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
            transform.localEulerAngles = new Vector3(0, 270, 0);
        }

        protected override void OnDetachFromController()
        {
#if UNITY_WSA && UNITY_2017_2_OR_NEWER
            // Unsubscribe from input now that we've detached from the controller
            InteractionManager.InteractionSourcePressed -= InteractionSourcePressed;
            InteractionManager.InteractionSourceReleased -= InteractionSourceReleased;
#endif

            state = StateEnum.Uninitialized;
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
                        if (state == StateEnum.Idle && battime > 0)
                        {
                            if (origin == false)
                            {
                                Transform t = gameObject.transform;
                                handLight = Instantiate(li, t);
                            }
                            state = StateEnum.Spawning;
                            //SpawnObject();
                            handLight.spotAngle = 50;

                            if (battime > 80)
                                handLight.intensity = 3;
                            else if (battime > 0)
                                handLight.intensity = 3 - ((80 - battime) / 30);
                            else
                                handLight.intensity = 0;
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
                            handLight.intensity = 0;
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
