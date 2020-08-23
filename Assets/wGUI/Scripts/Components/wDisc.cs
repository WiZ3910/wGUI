using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Shapes;
using UnityEditor;
using TMPro;
using static Shapes.Disc;
using Sirenix.OdinInspector;
using System;

namespace wGUI
{
    public class wDisc : wRenderElement
    {
        //Color
        [Title("Color")]
        [EnumToggleButtons]
        public DiscColorMode ColorMode = DiscColorMode.Single;

        string FirstColorLabel { get
            {
                switch (ColorMode)
                {
                    case DiscColorMode.Single:
                        return "Color";
                    case DiscColorMode.Radial:
                        return "Inner";
                    case DiscColorMode.Angular:
                        return "Start";
                    case DiscColorMode.Bilinear:
                        return "InnerStart";
                }
                return "Color";
            }
        }
        [LabelText("$FirstColorLabel")]
        public Color FirstColor = Color.white;


        string SecondColorLabel
        {
            get
            {
                switch (ColorMode)
                {
                    case DiscColorMode.Radial:
                        return "Outer";
                    case DiscColorMode.Angular:
                        return "End";
                    case DiscColorMode.Bilinear:
                        return "OuterStart";
                }
                return "Color";
            }
        }
        private bool showSecondColor => (int)ColorMode > 0;
        [ShowIf(nameof(showSecondColor))]
        [LabelText(nameof(SecondColorLabel))]
        public Color SecondColor = Color.white;

        private bool isBilinear => ColorMode == DiscColorMode.Bilinear;
        [ShowIf(nameof(isBilinear))]
        public Color InnerEnd = Color.white;
        [ShowIf(nameof(isBilinear))]
        public Color OuterEnd = Color.white;

        [Title("Shape")]
        [EnumToggleButtons]
        public DiscType Type = DiscType.Disc;
        public float Radius;

        bool showThickness => (int)Type > 1;
        [ShowIf(nameof(showThickness))]
        public float Thickness = 0.5f;

        bool isArc => Type == DiscType.Arc;
        [ShowIf(nameof(isArc))]
        [EnumToggleButtons]
        public ArcEndCap EndCap = ArcEndCap.None;

        private bool useAngle => (int)Type > 0;
        [ShowIf(nameof(useAngle))]
        public float AngleStart;
        [ShowIf(nameof(useAngle))]
        public float AngleEnd = 3.14159f/4;

        //Dashはまた今度。
        //[BoxGroup("Dash")]
        //public bool Dashed = true;
        //[BoxGroup("Dash/Dashed")]
        //DashStyle style = DashStyle;
        public override void Render()
        {
            
            switch (Type)
            {
                case DiscType.Disc:
                    switch (ColorMode)
                    {
                        case DiscColorMode.Single:
                            Draw.Disc(pos: transform.position, radius: Radius, color: FirstColor, rot: transform.rotation);
                            break;
                        case DiscColorMode.Radial:
                            Draw.DiscGradientRadial(pos: transform.position, radius: Radius, colorInner: FirstColor, colorOuter: SecondColor,rot:transform.rotation);
                            break;
                        case DiscColorMode.Angular:
                            Draw.DiscGradientAngular(pos: transform.position, colorStart: FirstColor, colorEnd: SecondColor, rot: transform.rotation,radius:Radius);
                            break;
                        case DiscColorMode.Bilinear:
                            Draw.DiscGradientBilinear(pos: transform.position, colorInnerStart: FirstColor, colorOuterStart: SecondColor, colorInnerEnd: InnerEnd, colorOuterEnd: OuterEnd
                                , rot: transform.rotation, radius: Radius);
                            break;
                    }
                    break;
                case DiscType.Pie:
                    switch (ColorMode)
                    {
                        case DiscColorMode.Single:
                            Draw.Pie(pos:transform.position,angleRadStart:AngleStart,angleRadEnd:AngleEnd,color:FirstColor,rot:transform.rotation,radius:Radius);
                            break;
                        case DiscColorMode.Radial:
                            Draw.PieGradientRadial(transform.position, transform.rotation, Radius, AngleStart, AngleEnd, FirstColor, SecondColor);
                            break;
                        case DiscColorMode.Angular:
                            Draw.PieGradientAngular(pos: transform.position, rot: transform.rotation, radius: Radius, angleRadStart: AngleStart, angleRadEnd: AngleEnd,
                                colorStart: FirstColor, colorEnd: SecondColor);
                            break;
                        case DiscColorMode.Bilinear:
                            Draw.PieGradientBilinear(pos: transform.position, rot: transform.rotation, radius: Radius, angleRadStart: AngleStart, angleRadEnd: AngleEnd,
                                colorInnerStart: FirstColor, colorOuterStart: SecondColor, colorInnerEnd: InnerEnd, colorOuterEnd: OuterEnd);
                            break;
                    }
                    break;
                case DiscType.Ring:
                    switch (ColorMode)
                    {
                        case DiscColorMode.Single:
                            Draw.Ring(pos: transform.position, rot: transform.rotation, color: FirstColor, radius: Radius, thickness: Thickness);
                            break;
                        case DiscColorMode.Radial:
                            Draw.RingGradientRadial(pos: transform.position, rot: transform.rotation, colorInner: FirstColor, colorOuter: SecondColor, thickness: Thickness,radius:Radius);
                            break;
                        case DiscColorMode.Angular:
                            Draw.RingGradientAngular(pos: transform.position, rot: transform.rotation, colorStart: FirstColor, colorEnd: SecondColor, thickness: Thickness, radius: Radius);
                            break;
                        case DiscColorMode.Bilinear:
                            Draw.RingGradientBilinear(pos: transform.position, rot: transform.rotation, colorInnerStart: FirstColor, colorOuterStart: SecondColor, colorInnerEnd: InnerEnd, colorOuterEnd: OuterEnd,
                                thickness: Thickness, radius: Radius);
                            break;
                    }
                    break;
                case DiscType.Arc:
                    switch (ColorMode)
                    {
                        case DiscColorMode.Single:
                            Draw.Arc(pos: transform.position, rot: transform.rotation, angleRadStart: AngleStart, angleRadEnd: AngleEnd, endCaps:EndCap,color:FirstColor,thickness:Thickness,radius:Radius);
                            break;
                        case DiscColorMode.Radial:
                            Draw.ArcGradientRadial(pos: transform.position, rot: transform.rotation, angleRadStart: AngleStart, angleRadEnd: AngleEnd, endCaps: EndCap, colorInner:FirstColor,colorOuter:SecondColor, thickness: Thickness,radius:Radius);
                            break;
                        case DiscColorMode.Angular:
                            Draw.ArcGradientAngular(pos: transform.position, rot: transform.rotation, angleRadStart: AngleStart, angleRadEnd: AngleEnd, endCaps: EndCap, colorStart:FirstColor,colorEnd:SecondColor, thickness: Thickness,radius:Radius);
                            break;
                        case DiscColorMode.Bilinear:
                            Draw.ArcGradientBilinear(pos: transform.position, rot: transform.rotation, angleRadStart: AngleStart, angleRadEnd: AngleEnd, endCaps: EndCap,
                                colorInnerStart:FirstColor,colorOuterStart:SecondColor,colorInnerEnd:InnerEnd,colorOuterEnd:OuterEnd, radius: Radius,thickness:Thickness);
                            break;
                    }
                    break;
            }
        }

    }
}
