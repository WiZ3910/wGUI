using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UIElements;

namespace wGUI
{
    [ExecuteAlways]
    public class wSlider : wElement
    {
        public wDraggable Handle;

        private SliderMode _sliderMode = SliderMode.One;
        [EnumToggleButtons]
        [ShowInInspector]
        public SliderMode SliderMode
        {
            get
            {
                return _sliderMode;
            }
            set
            {
                _sliderMode = value;
                if (value == SliderMode.One) Value3D = new Vector3(Value3D.x, 0, 0);
                if (value == SliderMode.Two) Value3D = new Vector3(Value3D.x, Value3D.y, 0);
                Handle.Constraint = new Constraint
                {
                    enabled = true,
                    From = new Vector3(-0.5f, 0, 0),
                    To = new Vector3(0.5f, useY?1:0, useZ?1:0)
                };
            }
        }

        public Vector3 Value3D { get
            {
                if(Handle)
                {
                    return Handle.transform.localPosition + new Vector3(0.5f,0,0);
                }
                return Vector3.zero;
            }
            set
            {
                Handle.transform.localPosition = value - new Vector3(0.5f,0,0);
            }
        }
        public Vector2 Value2D { get
            {
                return new Vector2(Value3D.x,Value3D.y);
            }
            set
            {
                var pos = Handle.transform.localPosition;
                Value3D = new Vector3(value.x, value.y, Value3D.z);
            }
        }

        [ShowInInspector, PropertyRange(0, 1)]
        public float X
        {
            get
            {
                return Value3D.x;
            }
            set
            {
                var pos = Handle.transform.localPosition;
                Value3D = new Vector3(value, Value3D.y, Value3D.z);
            }
        }

        bool useY => (int)SliderMode >= 2;
        [ShowIf(nameof(useY))]
        [ShowInInspector,PropertyRange(0,1)]
        public float Y { 
            get { return Value2D.y; }
            set {
                Value2D = new Vector2(Value2D.x, value);
            }
        }
        bool useZ => (int)SliderMode >= 3;
        [ShowIf(nameof(useZ))]
        [ShowInInspector,PropertyRange(0,1)]
        public float Z
        {
            get { return Value3D.z; }
            set
            {
                Value3D = new Vector3(Value3D.x, Value3D.y,value);
            }
        }

        protected override void Awake()
        {
            base.Awake();
            SliderMode = SliderMode;
        }


    }
    
    public enum SliderMode
    {
        One = 1,
        Two = 2,
        Three = 3,
    }
}