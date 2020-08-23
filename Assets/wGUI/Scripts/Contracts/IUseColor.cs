using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace wGUI
{
    public interface IUseColor
    {
        Color GetColor();
        void SetColor(Color color);
    }
}
