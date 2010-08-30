using System.Collections.Generic;
namespace System.Interop.Cuda
{
    public class PtxOperand
    {
        public static readonly PtxOperand tid = CreateSpecialRegister(PtxStackType.V4, PtxOperandType.SpecialRegister, "%tid");
        public static readonly PtxOperand tid_x = CreateSpecialRegister(PtxStackType.I2, PtxOperandType.SpecialRegister, "%tid.x");
        public static readonly PtxOperand tid_y = CreateSpecialRegister(PtxStackType.I2, PtxOperandType.SpecialRegister, "%tid.y");
        public static readonly PtxOperand tid_z = CreateSpecialRegister(PtxStackType.I2, PtxOperandType.SpecialRegister, "%tid.z");
        public static readonly PtxOperand ntid = CreateSpecialRegister(PtxStackType.V4, PtxOperandType.SpecialRegister, "%ntid");
        public static readonly PtxOperand ntid_x = CreateSpecialRegister(PtxStackType.I2, PtxOperandType.SpecialRegister, "%ntid.x");
        public static readonly PtxOperand ntid_y = CreateSpecialRegister(PtxStackType.I2, PtxOperandType.SpecialRegister, "%ntid.y");
        public static readonly PtxOperand ntid_z = CreateSpecialRegister(PtxStackType.I2, PtxOperandType.SpecialRegister, "%ntid.z");
        public static readonly PtxOperand ctaid = CreateSpecialRegister(PtxStackType.V4, PtxOperandType.SpecialRegister, "%ctaid");
        public static readonly PtxOperand ctaid_x = CreateSpecialRegister(PtxStackType.I2, PtxOperandType.SpecialRegister, "%ctaid.x");
        public static readonly PtxOperand ctaid_y = CreateSpecialRegister(PtxStackType.I2, PtxOperandType.SpecialRegister, "%ctaid.y");
        public static readonly PtxOperand ctaid_z = CreateSpecialRegister(PtxStackType.I2, PtxOperandType.SpecialRegister, "%ctaid.z");
        public static readonly PtxOperand nctaid = CreateSpecialRegister(PtxStackType.V4, PtxOperandType.SpecialRegister, "%nctaid");
        public static readonly PtxOperand nctaid_x = CreateSpecialRegister(PtxStackType.I2, PtxOperandType.SpecialRegister, "%nctaid.x");
        public static readonly PtxOperand nctaid_y = CreateSpecialRegister(PtxStackType.I2, PtxOperandType.SpecialRegister, "%nctaid.y");
        public static readonly PtxOperand nctaid_z = CreateSpecialRegister(PtxStackType.I2, PtxOperandType.SpecialRegister, "%nctaid.z");
        public static readonly PtxOperand gridid = CreateSpecialRegister(PtxStackType.V4, PtxOperandType.SpecialRegister, "%gridid");
        public static readonly PtxOperand gridid_x = CreateSpecialRegister(PtxStackType.I2, PtxOperandType.SpecialRegister, "%gridid.x");
        public static readonly PtxOperand gridid_y = CreateSpecialRegister(PtxStackType.I2, PtxOperandType.SpecialRegister, "%gridid.y");
        public static readonly PtxOperand gridid_z = CreateSpecialRegister(PtxStackType.I2, PtxOperandType.SpecialRegister, "%gridid.z");
        public static readonly PtxOperand clock = CreateSpecialRegister(PtxStackType.I4, PtxOperandType.SpecialRegister, "%clock");
        public static readonly PtxOperand WARP_SZ = CreateSpecialRegister(PtxStackType.I2, PtxOperandType.Immediate, "WARP_SZ");

        internal class EqualityComparer : IEqualityComparer<PtxOperand>
        {
            public bool Equals(PtxOperand x, PtxOperand y)
            {
                //Utilities.DebugAssert(x.Type == VRegType.Address && y.Type == VRegType.Address);
                return (x.Name == y.Name);
            }

            public int GetHashCode(PtxOperand obj)
            {
                //Utilities.DebugAssert(obj.Type == VRegType.Address);
                return obj.Name.GetHashCode();
            }
        }

        public enum PtxStackType
        {
            pred,
            b8, b16, b32, b64,
            u8, u16, u32, u64,
            s8, s16, s32, s64,
            f16, f32, f64,

            // refactor below
            None,
            I2,
            I4,
            I8,
            R4,
            R8,
            ValueType,
            Object,
            ManagedPointer,
            UnmanangedPointer,
            //
            V4,
        }

        public enum PtxOperandType
        {
            None,
            Register,
            SpecialRegister,
            /// <summary>
            /// An address value as defined by a ptx array.
            /// </summary>
            Address,
            Immediate,
        }

        public enum PtxStateSpace
        {
            None,
            Register,
            Global,
            Local,
            Parameter,
            Shared,
            Texture,
            Constant
        }

        //private PtxOperand() { }

        public PtxOperandType Type { get; private set; }
        public PtxStateSpace StateSpace { get; private set; }
        public PtxStackType StackType { get; private set; }
        public PtxOperandPointerInfo PointerInfo { get; private set; }
        public object ImmediateValue { get; set; }
        public string Name { get; set; }

        private static PtxOperand CreateSpecialRegister(PtxStackType stackType, PtxOperandType type, string name)
        {
            return new PtxOperand { Name = name, StackType = stackType, Type = type };
        }

        public string GetAssemblyText()
        {
            object immediateValue = ImmediateValue;
            if (immediateValue != null)
            {
                string valueAsText = immediateValue.ToString();
                bool isReal = ((immediateValue is float) || (immediateValue is double));
                bool isUnsigned = ((immediateValue is byte) || (immediateValue is ushort) || (immediateValue is uint) || (immediateValue is ulong));
                return (isReal && (valueAsText.IndexOf('.') == -1) ? ImmediateValue + ".0" : valueAsText) + (isUnsigned ? "U" : string.Empty);
            }
            else
                return Name;
        }

    }
}
