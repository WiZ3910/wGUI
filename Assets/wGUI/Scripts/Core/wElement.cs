using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace wGUI
{
    public class wElement : MonoBehaviour
    {
        protected RectTransform _rect;
        public RectTransform rectTransform { get { if (_rect == null) { _rect = GetComponent<RectTransform>(); } return _rect; } }

        protected wCanvas _canvas;
        //RootのCanvasを取得する。
        protected wCanvas Canvas;

        protected virtual void Awake()
        {
            if (FindObjectOfType<wEventSystem>() == null)
            {
                var go = new GameObject("wEventSystem");
                go.AddComponent<wEventSystem>();
            }
            GetCanvas()?.DOMUpdate();
        }
        protected virtual void OnEnable()
        {
            GetCanvas()?.DOMUpdate();
        }

        protected virtual void OnDisable()
        {
            GetCanvas()?.DOMUpdate();
        }
        protected virtual void OnDestroy() => OnDisable();

        public wCanvas GetCanvas()
        {
            return GetCanvasRec(transform);
            //Recursive inner function
            wCanvas GetCanvasRec(Transform current)
            {
                if (current.GetComponent<wCanvas>() != null) return current.GetComponent<wCanvas>();
                if (current.parent == null) return null;
                return GetCanvasRec(current.parent);
            }
        }


    }
}
