﻿using Avalonia;
using Avalonia.Media;
using Avalonia.Platform;
using Avalonia.Rendering.SceneGraph;
using Avalonia.Skia;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aura.UI.Rendering
{
    public class AuraDrawOperationBase : ICustomDrawOperation
    {
        public AuraDrawOperationBase(Rect bounds, IFormattedTextImpl noSkia)
        {
            Bounds = bounds;
            NoSkia = noSkia;
        }

        public virtual Rect Bounds { get; private set; }
        public virtual IFormattedTextImpl NoSkia { get; private set; }

        public virtual void Dispose()
        {
            // do nothing
        }

        public virtual bool Equals(ICustomDrawOperation other) => false;

        public virtual bool HitTest(Point p) => false;

        public virtual void Render(IDrawingContextImpl context)
        {
            
        }

        protected virtual bool CheckSkia(IDrawingContextImpl context)
        {
            if(context is ISkiaDrawingContextImpl)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
