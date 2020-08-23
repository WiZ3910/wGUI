using Shapes;
using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace wGUI
{
    [RequireComponent(typeof(ResponsiveCollider))]
    [RequireComponent(typeof(IUseColor))]
    [RequireComponent(typeof(wClickable))]
    public class wButton : wElement
    {
        public IObservable<Unit> onClick => Clickable.onClick;
        public IObservable<Unit> onClickUp => Clickable.onClickUp;
        public IObservable<Unit> onClickDown => Clickable.onClickDown;
        public IObservable<Unit> onClicking => Clickable.onClicking;
        public IObservable<Unit> onFocusEnter => Forcusable.onFocusEnter;
        public IObservable<Unit> onFocusLeave => Forcusable.onFocusLeave;
        public IObservable<Unit> onFocussing => Forcusable.onFocussing;

        TMPro.TMP_FontAsset font;
        public Color Color { get; set; }
        public string Text
        {
            get => GetComponent<wText>()?.Content;
            set
            {
                var textUI = GetComponent<wText>();
                if (textUI) textUI.Content = value;
            }
        }

        //background
        private IUseColor _color;
        public IUseColor color { get { if (_color == null) _color = GetComponent<IUseColor>(); return _color; } }

        private wClickable _clickable;
        public wClickable Clickable { get { if (_clickable == null) _clickable = GetComponent<wClickable>(); return _clickable; } }

        private wForcusable _forcusable;
        public wForcusable Forcusable { get { if (_forcusable == null) _forcusable = GetComponent<wForcusable>(); return _forcusable; } }


        public Color BaseColor = Color.white;
        public Color FocusColor = new Color(0.8f, 0.8f, 0.8f);
        public Color PressedColor = new Color(0.6f, 0.6f, 0.6f);

        protected override void Awake()
        {
            base.Awake();
            color?.SetColor(BaseColor);
        }

        private void Start()
        {
            onClickUp.Subscribe(_ => color.SetColor(FocusColor));
            onFocusEnter.Subscribe(_ => color.SetColor(FocusColor));
            onFocusLeave.Subscribe(_ => color.SetColor(BaseColor));
            onClickDown.Subscribe(_ => color.SetColor(PressedColor));
        }
        private void OnClickedDebug() => Debug.Log("Clicked");

    }
}