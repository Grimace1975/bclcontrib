#region License
/*
The MIT License

Copyright (c) 2008 Sky Morey

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in
all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
THE SOFTWARE.
*/
#endregion
//using System.Collections.Generic;
//using System.Linq;
//using System.Reflection;
//using System.Text;
//namespace System.Interop.Cuda.Clr
//{
//    enum SpecialMethodCode
//    {
//        None,
//        Shared1DLoad,
//        Shared1DStore,
//    }

//    class SpecialMethodInfo
//    {
//        static readonly Dictionary<MethodBase, SpecialMethodInfo> s_dict;

//        public SpecialMethodInfo()
//        {
//            Type threadIndexType = typeof(ThreadIndex);
//            Type blockSizeType = typeof(BlockSize);
//            Type blockIndexType = typeof(BlockIndex);
//            Type gridSizeType = typeof(GridSize);
//            //
//            s_dict = new Dictionary<MethodBase, SpecialMethodInfo>
//        {
//            {threadIndexType.GetProperty("X").GetGetMethod(), new SpecialMethodInfo(GlobalVReg.FromSpecialRegister(StackType.I2, VRegType.SpecialRegister, "%tid.x"))},
//            {threadIndexType.GetProperty("Y").GetGetMethod(), new SpecialMethodInfo(GlobalVReg.FromSpecialRegister(StackType.I2, VRegType.SpecialRegister, "%tid.y"))},
//            {threadIndexType.GetProperty("Z").GetGetMethod(), new SpecialMethodInfo(GlobalVReg.FromSpecialRegister(StackType.I2, VRegType.SpecialRegister, "%tid.z"))},
//            {typeof(BlockSize).GetProperty("X").GetGetMethod(), new SpecialMethodInfo(GlobalVReg.FromSpecialRegister(StackType.I2, VRegType.SpecialRegister, "%ntid.x"))},
//            {typeof(BlockSize).GetProperty("Y").GetGetMethod(), new SpecialMethodInfo(GlobalVReg.FromSpecialRegister(StackType.I2, VRegType.SpecialRegister, "%ntid.y"))},
//            {typeof(BlockSize).GetProperty("Z").GetGetMethod(), new SpecialMethodInfo(GlobalVReg.FromSpecialRegister(StackType.I2, VRegType.SpecialRegister, "%ntid.z"))},
//            {typeof(BlockIndex).GetProperty("X").GetGetMethod(), new SpecialMethodInfo(GlobalVReg.FromSpecialRegister(StackType.I2, VRegType.SpecialRegister, "%ctaid.x"))},
//            {typeof(BlockIndex).GetProperty("Y").GetGetMethod(), new SpecialMethodInfo(GlobalVReg.FromSpecialRegister(StackType.I2, VRegType.SpecialRegister, "%ctaid.y"))},
//            {typeof(BlockIndex).GetProperty("Z").GetGetMethod(), new SpecialMethodInfo(GlobalVReg.FromSpecialRegister(StackType.I2, VRegType.SpecialRegister, "%ctaid.z"))},
//            {typeof(GridSize).GetProperty("X").GetGetMethod(), new SpecialMethodInfo(GlobalVReg.FromSpecialRegister(StackType.I2, VRegType.SpecialRegister, "%nctaid.x"))},
//            {typeof(GridSize).GetProperty("Y").GetGetMethod(), new SpecialMethodInfo(GlobalVReg.FromSpecialRegister(StackType.I2, VRegType.SpecialRegister, "%nctaid.y"))},
//            {typeof(GridSize).GetProperty("Z").GetGetMethod(), new SpecialMethodInfo(GlobalVReg.FromSpecialRegister(StackType.I2, VRegType.SpecialRegister, "%nctaid.z"))},

//            {new Action(CudaRuntime.SyncThreads).Method, new SpecialMethodInfo(PtxCode.Bar_Sync)},
//            {new Func<int>(CudaRuntime.GetClock).Method, new SpecialMethodInfo(GlobalVReg.FromSpecialRegister(StackType.I4, VRegType.SpecialRegister, "%clock"))},
//            {typeof(CudaRuntime).GetProperty("WarpSize").GetGetMethod(), new SpecialMethodInfo(GlobalVReg.FromSpecialRegister(StackType.I2, VRegType.Immediate, "WARP_SZ"))},

//            {typeof(Shared1D<int>).GetProperty("Item").GetGetMethod(), new SpecialMethodInfo(SpecialMethodCode.Shared1DLoad)},
//            {typeof(Shared1D<int>).GetProperty("Item").GetSetMethod(), new SpecialMethodInfo(SpecialMethodCode.Shared1DStore)},
//            {typeof(Shared1D<uint>).GetProperty("Item").GetGetMethod(), new SpecialMethodInfo(SpecialMethodCode.Shared1DLoad)},
//            {typeof(Shared1D<uint>).GetProperty("Item").GetSetMethod(), new SpecialMethodInfo(SpecialMethodCode.Shared1DStore)},
//            {typeof(Shared1D<float>).GetProperty("Item").GetGetMethod(), new SpecialMethodInfo(SpecialMethodCode.Shared1DLoad)},
//            {typeof(Shared1D<float>).GetProperty("Item").GetSetMethod(), new SpecialMethodInfo(SpecialMethodCode.Shared1DStore)},

//        };
//        }

//        public bool IsSinglePtxCode { get; private set; }
//        public bool IsSpecialMethodCode { get; private set; }
//        public bool IsGlobalVReg { get; private set; }

//        public PtxCode PtxCode { get; private set; }
//        public GlobalVReg HardcodedGlobalVReg { get; private set; }
//        public SpecialMethodCode SpecialMethodCode { get; private set; }

//        public SpecialMethodInfo(PtxCode ptxCode)
//        {
//            IsSinglePtxCode = true;
//            PtxCode = ptxCode;
//        }

//        internal SpecialMethodInfo(GlobalVReg globalVReg)
//        {
//            IsGlobalVReg = true;
//            HardcodedGlobalVReg = globalVReg;
//        }

//        public SpecialMethodInfo(SpecialMethodCode specialMethodCode)
//        {
//            IsSpecialMethodCode = true;
//            SpecialMethodCode = specialMethodCode;
//        }

//        public static bool TryGetMethodInfo(MethodBase method, out SpecialMethodInfo specialMethodInfo)
//        {
//            return dict.TryGetValue(method, out specialMethodInfo);
//        }

//        public static bool IsSpecialMethod(MethodBase method)
//        {
//            return dict.ContainsKey(method);
//        }
//    }
//}
