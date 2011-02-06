#if !CODE_ANALYSIS
namespace System
#else
using System;
namespace SystemEx
#endif
{
    public interface IAsyncAssetLoader
    {
        void GetAsset(string path, JSAsyncCallback callback);
        bool Pump();
        void Reset();
    }
}