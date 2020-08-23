using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.UIElements;

namespace wGUI
{
    [RequireComponent(typeof(wForcusable))]
    public class wClickable : wAttribute
    {
        internal Subject<Unit> click;
        internal Subject<Unit> clicking;
        internal Subject<Unit> clickUp;
        internal Subject<Unit> clickDown;
        internal bool hasPressed = false;

        public IObservable<Unit> onClicking => clicking;
        public IObservable<Unit> onClickDown => clickDown;
        public IObservable<Unit> onClickUp => clickUp;
        public IObservable<Unit> onClick => click;

        public override void Initialize()
        {
            click = new Subject<Unit>().AddTo(this);
            clicking = new Subject<Unit>().AddTo(this);
            clickUp = new Subject<Unit>().AddTo(this);
            clickDown = new Subject<Unit>().AddTo(this);
            onClickDown.Subscribe(_ => hasPressed = true);
            onClickUp.Subscribe(_ => hasPressed = false);
        }
    }
}
