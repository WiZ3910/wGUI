using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniRx;
using UnityEngine;

namespace wGUI
{
    public class wForcusable : wAttribute
    {

        internal Subject<Unit> focusEnter;
        internal Subject<Unit> focusLeave;
        internal Subject<Unit> focussing;

        public IObservable<Unit> onFocusEnter => focusEnter;
        public IObservable<Unit> onFocusLeave => focusLeave;
        public IObservable<Unit> onFocussing => focusLeave;

        public override void Initialize()
        {
            focusEnter = new Subject<Unit>().AddTo(this);
            focusLeave = new Subject<Unit>().AddTo(this);
            focussing = new Subject<Unit>().AddTo(this);
        }
    }
}
