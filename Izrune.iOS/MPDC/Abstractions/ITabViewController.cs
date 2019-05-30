using System;
using CoreGraphics;

namespace MPDC.Abstractions
{
    public interface ITabViewController
    {
        void SetContentOffset(CGPoint ContentOffset, bool animated = false);
        Action<float> ScrollChanged { get; set; }
        CGPoint ContentOffset { get; }
    }
}
