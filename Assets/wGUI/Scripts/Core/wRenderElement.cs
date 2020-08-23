using Shapes;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace wGUI
{
    [ExecuteAlways]
    public abstract class wRenderElement : wElement
    {
        protected override void OnEnable()
        {
            base.OnEnable();
            if(GetCanvas() == null)
                Camera.onPostRender += RenderImmediate;
        }
        protected override void OnDisable()
        {
            base.OnDisable();
            if (GetCanvas() == null)
                Camera.onPostRender -= RenderImmediate;
        }
        protected override void OnDestroy()
        {
            base.OnDestroy();
            Camera.onPostRender -= RenderImmediate;
        }

        public abstract void Render();
        private void RenderImmediate(Camera cam) => Render();
    }
}
