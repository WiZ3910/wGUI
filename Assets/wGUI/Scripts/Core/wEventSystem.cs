using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniRx;
using UnityEngine;

namespace wGUI
{
    public class wEventSystem : MonoBehaviour
    {
        wForcusable lastFocussed;
        wClickable lastClicked;
        wDraggable lastDragged;
        private void Update()
        {
            if (!Settings.XRMode)
            {
                RaycastHit hit;
                var hitted = Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, float.MaxValue, 1 << LayerMask.NameToLayer("UI"));

                //Focus処理
                var focusable = hit.collider?.GetComponent<wForcusable>();
                if (focusable != lastFocussed)
                {
                    lastFocussed?.focusLeave.OnNext(Unit.Default);
                    focusable?.focusEnter.OnNext(Unit.Default);
                }
                focusable?.focussing.OnNext(Unit.Default);
                lastFocussed = focusable;

                //Click処理
                var clickable = hit.collider?.GetComponent<wClickable>();

                if (Input.GetMouseButtonDown(0))
                {
                    clickable?.clickDown?.OnNext(Unit.Default);
                    lastClicked = clickable;
                } 
                else if (Input.GetMouseButtonUp(0))
                {
                    lastClicked?.clickUp?.OnNext(Unit.Default);
                    if (lastClicked == clickable) lastClicked?.click?.OnNext(Unit.Default);
                    lastClicked = null;
                } 
                else if (Input.GetMouseButton(0))
                {
                    clickable?.clicking?.OnNext(Unit.Default);
                }


                //Drag処理
                var dragable = hit.collider?.GetComponent<wDraggable>();
                if (Input.GetMouseButtonDown(0))
                {
                    dragable?.dragStart?.OnNext(Unit.Default);
                    lastDragged = dragable;
                }
                else if (Input.GetMouseButtonUp(0))
                {
                    lastDragged?.dragEnd?.OnNext(Unit.Default);
                    lastDragged = null;
                }
                else if (Input.GetMouseButton(0))
                {
                    if (lastDragged)
                    {
                        var distance = Camera.main.transform.InverseTransformPoint(lastDragged.transform.position).z * Camera.main.transform.lossyScale.z;
                        lastDragged.dragging?.OnNext(Camera.main.ScreenToWorldPoint(Input.mousePosition + Vector3.forward * distance));
                    }
                }
            }
        }

    } // End of Non XRMode
}
