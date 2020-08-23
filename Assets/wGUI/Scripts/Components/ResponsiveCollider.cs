using Sirenix.OdinInspector;
using System;
using UniRx;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

[ExecuteAlways]
[RequireComponent(typeof(RectTransform))]
public class ResponsiveCollider : MonoBehaviour
{
    public Collider collider;
    private ColliderType _type;

    IDisposable disposable;

    [ShowInInspector]
    [EnumToggleButtons]
    public ColliderType Type
    {
        get { return _type; }
        set
        {
            _type = value;
            disposable?.Dispose();

            //Reset Collider
            if (collider)
            {
                DestroyImmediate(collider);
            }
;            switch (Type)
            {
                case ColliderType.Box:
                    collider = gameObject.AddComponent<BoxCollider>();
                    disposable  = this.ObserveEveryValueChanged(x => (x.transform as RectTransform).rect).Subscribe(rect =>
                    {
                        (collider as BoxCollider).size = rect.size;
                    }).AddTo(collider);
                    break;

                case ColliderType.Sphere:
                    collider = gameObject.AddComponent<SphereCollider>();
                    disposable = this.ObserveEveryValueChanged(x => (x.transform as RectTransform).rect).Subscribe(rect =>
                    (collider as SphereCollider).radius = radiusBy == RadiusBy.Width ? (rect.width / 2) : (rect.height / 2)).AddTo(collider);
                    break;
                case ColliderType.Capsure:
                    collider = gameObject.AddComponent<CapsuleCollider>();
                    disposable = this.ObserveEveryValueChanged(x => (x.transform as RectTransform).rect).Subscribe(rect =>
                    {
                        if (radiusBy == RadiusBy.Width)
                        {
                            (collider as CapsuleCollider).height = rect.size.y;
                            (collider as CapsuleCollider).radius = rect.size.x / 2;
                            (collider as CapsuleCollider).direction = 1;
                        } else
                        {
                            (collider as CapsuleCollider).radius = rect.size.y / 2;
                            (collider as CapsuleCollider).height = rect.size.x;
                            (collider as CapsuleCollider).direction = 0;
                        }
                    }).AddTo(collider);
                    break;
            }
        }
    }
    public enum RadiusBy{
        Width,Height
    }
    private RadiusBy _radiusBy = RadiusBy.Width;
    bool showRadiusBy => Type == ColliderType.Sphere || Type == ColliderType.Capsure;
    [EnumToggleButtons]
    [ShowInInspector]
    [ShowIf(nameof(showRadiusBy))]
    public RadiusBy radiusBy
    {
        get { return _radiusBy; }
        set
        {
            _radiusBy = value;
            Type = _type;
        }
    }

    bool initialized { get; set; } = false;
    
    void Awake()
    {
        if(!initialized)
        {
            Type = _type;
            initialized = true;
        }
    }

    public enum ColliderType
    {
        Box,
        Sphere,
        Capsure
    }
}
