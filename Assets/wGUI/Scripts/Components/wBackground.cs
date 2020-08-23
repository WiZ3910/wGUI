using Shapes;
using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

namespace wGUI
{
    [ExecuteAlways]
    [RequireComponent(typeof(RectTransform))]
    public class wBackground : wRenderElement, IUseColor
    {
        public UnityEngine.Color Color = new UnityEngine.Color(0.25f, 0.25f, 0.25f);
        [Range(0, 1)]
        public float corner = 0;
        public override void Render()
        {
            var fixedCorner = corner * Mathf.Max(rectTransform.rect.width, rectTransform.rect.height) * 3 / 8;
            Draw.Rectangle(pos: rectTransform.position, rect: rectTransform.rect, color: Color, normal: rectTransform.forward, cornerRadii: new Vector4(fixedCorner, fixedCorner, fixedCorner, fixedCorner));
        }

        Color IUseColor.GetColor()
        {
            return Color;
        }

        void IUseColor.SetColor(Color color)
        {
            this.Color = color;
        }
    }
}