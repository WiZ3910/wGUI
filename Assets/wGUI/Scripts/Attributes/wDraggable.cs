using Sirenix.OdinInspector;
using Sirenix.Utilities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using UniRx;
using UnityEngine;
using wGUI;

[RequireComponent(typeof(wClickable))]
public class wDraggable : wAttribute
{
    internal Subject<Unit> dragStart;
    internal Subject<Unit> dragEnd;
    internal Subject<Vector3> dragging;
    public IObservable<Unit> onDragStart => dragStart;
    public IObservable<Unit> onDragEnd => dragEnd;
    public IObservable<Vector3> onDragging => dragging;

    public bool isDragging = false;

    [ShowInInspector]
    public Constraint Constraint = new Constraint
    {
        enabled = false,
        From = new Vector3(-1, 0, 0),
        To = new Vector3(1, 0, 0)
    };

    private wClickable _clickable;
    protected wClickable Clickable { get { if (_clickable == null) _clickable = GetComponent<wClickable>(); return _clickable; } }

    public override void Initialize()
    {
        dragStart = new Subject<Unit>().AddTo(this);
        dragEnd = new Subject<Unit>().AddTo(this);
        dragging = new Subject<Vector3>().AddTo(this);

        dragStart.Subscribe(_ => isDragging = true);
        dragEnd.Subscribe(_ => isDragging = false);

        onDragging.Subscribe(v => Move(v));
    }


    private void Move(Vector3 tryMoveTo)
    {
        var position = transform.parent.InverseTransformPoint(tryMoveTo);
        if (Constraint.enabled)
        {
            position.x = Mathf.Clamp(position.x,Constraint.From.x,Constraint.To.x);
            position.y = Mathf.Clamp(position.y,Constraint.From.y,Constraint.To.y);
            position.z = Mathf.Clamp(position.z,Constraint.From.z,Constraint.To.z);
        }
        transform.localPosition = position;
        return;

        Vector3 p = transform.InverseTransformPoint(tryMoveTo);
        transform.localPosition = position;
    }
}

public struct Constraint
{
    public bool enabled;
    public Vector3 From;
    public Vector3 To;
}
