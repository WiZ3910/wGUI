using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UniRx;
using UnityEngine;

namespace wGUI
{
    [ExecuteAlways]
    [RequireComponent(typeof(RectTransform))]
    public class wCanvas : MonoBehaviour
    {
        //public SortedSet<wElement> elements { get; } = new SortedSet<wElement>();
        public wRenderElement[] elements = new wRenderElement[] { };
        public bool VRMode { get; set; } = false;

        public void OnEnable() =>
            Camera.onPostRender += OnPostRender;
        public void OnDisable() =>
            Camera.onPostRender -= OnPostRender;
        private void OnPostRender(Camera cam)
        {
            foreach (var element in elements)
            {
                if (element != null && element.enabled)
                    element.Render();
            }
        }
        [ContextMenu("DOMUpdate")]
        public void DOMUpdate()
        {
            Debug.Log("DOMUpdated");
            elements = Traverse(transform).ToArray();
        }

        protected IEnumerable<wRenderElement> Traverse(Transform current)
        {
            return GetComponentsInChildren<wRenderElement>().Where(x => x.enabled);
        }
    }
}