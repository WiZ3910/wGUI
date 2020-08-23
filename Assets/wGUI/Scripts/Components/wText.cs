using Shapes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace wGUI
{
    public class wText : wRenderElement
    {
        public Color Color = Color.black;
        public string Content = "Text";
        public float Size = 1f;
        public TextAlign Align = TextAlign.Center;
        public TMPro.TMP_FontAsset TMPFont;
        public override void Render()
        {
            if (!TMPFont)
            {
                Draw.Text(
                    pos: rectTransform.position,
                    rot: rectTransform.rotation,
                    content: Content,
                    color: Color,
                    align: Align,
                    fontSize: Size
                    );
            }
            else
            {

                Draw.Text(
                        rectTransform.position,
                        rectTransform.rotation,
                        Content,
                        Align,
                        Size,
                        TMPFont,
                        Color
                    );
            }
        }
    }
}