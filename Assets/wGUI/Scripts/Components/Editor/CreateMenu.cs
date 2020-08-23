using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Experimental.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using Object = UnityEngine.Object;
namespace wGUI
{
    public static class CreateMenu
    {

        const string MENU_ROOT = "wGUI/";
        const string CREATE_ROOT = MENU_ROOT + "Create/";
        const string GO_CREATE_ROOT = "GameObject/" + MENU_ROOT;
        const int GO_MENU_SORT_OFFSET = 2;
        const int MENU_ITEM_SORT_CREATE_LINES = 0;
        const int MENU_ITEM_SORT_CREATE_DISCS = 20;
        const int MENU_ITEM_SORT_CREATE_FLATS = 40;
        const int MENU_ITEM_SORT_CREATE_3D = 60;



        [MenuItem(GO_CREATE_ROOT + "Background", false, GO_MENU_SORT_OFFSET + MENU_ITEM_SORT_CREATE_FLATS)]
        [MenuItem(CREATE_ROOT + "Background", false, MENU_ITEM_SORT_CREATE_FLATS)]
        public static void CreateBackground(MenuCommand menuCommand) => CreateUI<wBackground>(menuCommand, "Background", postModifications:x => {
            if(x.transform.parent != null)
            {
                var parentRect = x.transform.parent.GetComponent<RectTransform>();
                x.rectTransform.anchorMin = Vector2.zero;
                x.rectTransform.anchorMax = Vector2.one;
                x.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, parentRect.rect.width);
                x.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, parentRect.rect.height);
                // x.rectTransform.anchoredPosition = RectTransformUtility.a
            }
        });

        [MenuItem(GO_CREATE_ROOT + "Button", false, GO_MENU_SORT_OFFSET + MENU_ITEM_SORT_CREATE_FLATS)]
        [MenuItem(CREATE_ROOT + "Button", false, MENU_ITEM_SORT_CREATE_FLATS)]
        public static void CreateButton(MenuCommand menuCommand) => CreateUI<wButton>(menuCommand, "Button",premodification:x =>
            {
                x.AddComponent<ResponsiveCollider>().Type = ResponsiveCollider.ColliderType.Box;
            },
            postModifications: x => {
            if (x.transform.parent != null)
            {
                x.rectTransform.sizeDelta = new Vector2(0.6f, 0.2f);

                var bg = x.gameObject.AddComponent<wBackground>();
                bg.Color = Color.white;
                bg.corner = 0.27f;

                var text = x.gameObject.AddComponent<wText>();
                    text.Content = "Button";
            }
        });

        [MenuItem(GO_CREATE_ROOT + "Canvas", false, GO_MENU_SORT_OFFSET + MENU_ITEM_SORT_CREATE_FLATS)]
        [MenuItem(CREATE_ROOT + "Canvas", false, MENU_ITEM_SORT_CREATE_FLATS)]
        public static void CreateCanvas(MenuCommand menuCommand) => CreateUI<wCanvas>(menuCommand, "Canvas", x => {
            var rect = (x.transform as RectTransform);
            rect.sizeDelta = new Vector2(1.92f, 1.08f);
        });


        public static T CreateUI<T>(MenuCommand menuCommand, string name,Action<GameObject> premodification = null, Action<T> postModifications = null) where T : UnityEngine.Component
        {
            GameObject go = new GameObject(name);
            premodification?.Invoke(go);
            Vector3 createPos = default;
            if (SceneView.lastActiveSceneView != null)
                createPos = SceneView.lastActiveSceneView.pivot;
            go.transform.position = createPos;
            T component = go.AddComponent<T>();
            Undo.RegisterCreatedObjectUndo(go, $"create {name}");
            Place(go, menuCommand.context);
            postModifications?.Invoke(component);
            go.layer = LayerMask.NameToLayer("UI");
            return component;
        }
        private static void Place(GameObject go, Object context, Transform parent = null)
        {
            Transform parentTransform;
            if (context != null && context is GameObject goCtx)
                parentTransform = goCtx.transform;
            else
                parentTransform = PrefabStageUtility.GetCurrentPrefabStage()?.prefabContentsRoot.transform;
            Transform transform = go.transform;
            Undo.SetTransformParent(transform, parentTransform, "Reparenting");
            transform.localPosition = Vector3.zero;
            transform.localRotation = Quaternion.identity;
            transform.localScale = Vector3.one;
            if (parentTransform != null)
            {
                go.layer = parentTransform.gameObject.layer;
              //  if (parentTransform.GetComponent<RectTransform>())
              //      ObjectFactory.AddComponent<RectTransform>(go);
            }
        }
    }
}
