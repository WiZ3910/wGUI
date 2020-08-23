using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace wGUI
{
    public abstract class wAttribute : MonoBehaviour
    {
        internal bool Initialized;
        public abstract void Initialize();
        protected virtual void Awake()
        {
            if (!Initialized) Initialize();
            Initialized = true;
        }

    }
}
