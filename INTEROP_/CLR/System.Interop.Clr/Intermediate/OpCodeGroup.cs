//using System.Collections.Generic;
//using System.Text;
//namespace System.Interop.Intermediate
//{
//    /// <summary>
//    /// Groups of instructions; derived from CLI partition III; 
//    /// thought to be useful for deriving types, but it seems it's not?
//    /// Note that it currently doesn't contain all instructions; might be a problem ;).
//    /// </summary>
//    [Obsolete("Do we need this?")]
//    enum OpCodeGroup
//    {
//        None,
//        /// <summary>
//        /// add, div, mul, rem, and sub
//        /// 
//        /// and
//        /// 
//        /// add.ovf, add.ovf.un, mul.ovf, mul.ovf.un, sub.ovf, sub.ovf.un
//        /// </summary>
//        BinaryNumeric,
//        /// <summary>
//        /// neg
//        /// </summary>
//        UnaryNumeric,
//        /// <summary>
//        /// ceq, cgt, cgt.un, clt, clt.un
//        /// </summary>
//        BinaryComparison,
//        /// <summary>
//        /// and, div.un, not, or, rem.un, xor
//        /// </summary>
//        Integer,
//        /// <summary>
//        /// shl, shr, shr_un
//        /// </summary>
//        Shift,

//        /// beq, beq.s, bge, bge.s, bge.un, bge.un.s, bgt, bgt.s, bgt.un, 
//        /// bgt.un.s, ble, ble.s, ble.un, ble.un.s, blt, blt.s, blt.un, blt.un.s, bne.un, bne.un.s
//        BinaryBranch
//    }
//}
