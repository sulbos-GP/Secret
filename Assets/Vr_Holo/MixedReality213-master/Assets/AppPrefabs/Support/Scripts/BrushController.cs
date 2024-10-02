// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System.Collections;
using UnityEngine;
using UnityEngine.XR.WSA.Input;

namespace HoloToolkit.Unity.ControllerExamples
{
    public class BrushController : MonoBehaviour
    {
        public GameObject hit;
        public GameObject play;
    
        public enum DisplayModeEnum
        {
            InMenu,
            InHand,
            Hidden
        }

        public bool Draw
        {
            get { return draw; }
            set
            {
                if (draw != value)
                {
                    draw = value;
                    if (draw)
                    {
                        StartCoroutine(DrawOverTime());
                    }
                }
            }
        }

        public Vector3 TipPosition
        {
            get { return tip.position; }
        }

        [Header("Drawing Settings")]
        [SerializeField]
        private float minColorDelta = 0.01f;
        [SerializeField]
        private float minPositionDelta = 0.01f;
        [SerializeField]
        private float maxTimeDelta = 0.25f;
        [SerializeField]
        private Transform tip;
        [SerializeField]
        private GameObject strokePrefab;
        [SerializeField]
        private Transform brushObjectTransform;
        [SerializeField]
        private Renderer brushRenderer;

        private ColorPickerWheel colorPicker;
        private Color currentStrokeColor = Color.white;
        private bool draw = false;

        // Default storke width is defined in BrushThinStroke.prefab
        private float width = 0f;
        private float lastPointAddedTime = 0f;

        private void OnEnable()
        {
            // Subscribe to press and release events for drawing
            InteractionManager.InteractionSourcePressed += InteractionSourcePressed;
            InteractionManager.InteractionSourceReleased += InteractionSourceReleased;
        }

        private void OnDisable()
        {
            InteractionManager.InteractionSourcePressed -= InteractionSourcePressed;
            InteractionManager.InteractionSourceReleased -= InteractionSourceReleased;
        }

        private void Update()
        {
            if (!FindColorPickerWheel())
            {
                return;
            }

            brushRenderer.material.color = colorPicker.SelectedColor;
        }

        private void InteractionSourcePressed(InteractionSourcePressedEventArgs obj)
        {
            if (obj.state.source.handedness == InteractionSourceHandedness.Right && obj.pressType == InteractionSourcePressType.Select)
            {
                Draw = true;
                width = 0f;
            }
        }

        private void InteractionSourceReleased(InteractionSourceReleasedEventArgs obj)
        {
            if (obj.state.source.handedness == InteractionSourceHandedness.Right && obj.pressType == InteractionSourcePressType.Select)
            {
                Draw = false;
                width = 0f;
            }
        }

        private bool FindColorPickerWheel()
        {
            if (colorPicker == null)
            {
                colorPicker = FindObjectOfType<ColorPickerWheel>();
            }

            return colorPicker != null;
        }

        private IEnumerator DrawOverTime()
        {
            // Get the position of the tip
            Vector3 lastPointPosition = tip.position;
            // Then wait one frame and get the position again
            yield return null;

            // If we're not still drawing after one frame
            if (!draw)
            {
                // Abort drawing
                yield break;
            }

            Vector3 startPosition = tip.position;
            // Create a new brush stroke by instantiating stokePrefab


            // Generate points in an instantiated Unity LineRenderer
            while (draw)
            {
                RaycastHit seen;
                Ray raydirection = new Ray(transform.position, transform.forward);
                if(Physics.Raycast(raydirection,out seen, 10))
                {
                    hit = GameObject.Find("hit1");
                    if (seen.collider.tag == "Object")
                    {
                        hit.transform.parent = gameObject.transform;
                    }
                    if (seen.collider.tag == "knob")
                    {
                        hit.transform.parent = null;
                    }
                }
                yield return null;
            }
        }
    }
}