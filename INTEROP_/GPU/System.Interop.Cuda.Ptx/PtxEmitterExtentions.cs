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
using System.IO;
namespace System.Interop.Cuda
{
    using z = PtxOpCode;
    internal static class PtxEmitterExtentions
    {
        internal enum AdjustPack : long
        {
            hilowide = z._hi | z._lo | z._wide,
            hilo = z._hi | z._lo,
            wide = z._wide,
            rnd = z._rn | z._rz | z._rm | z._rp,
        }

        public static void AdjustText(this PtxOpCode opCode, AdjustPack pack, ref string text)
        {
        }

        public static void EmitInstruction(this PtxInstruction instruction, TextWriter w)
        {
            var opCode = instruction.OpCode;
            if ((opCode >= z.add_b16) && (opCode <= z.max_f64))
                PtxEmitter.EmitArithmeticInstruction(w, instruction);
            else if ((opCode >= z.set_eq_b32_b16) && (opCode <= z.slct_f64_f32))
                PtxEmitter.EmitComparisonSelectInstruction(w, instruction);
            else if ((opCode >= z.and_pred) && (opCode <= z.shr_s64))
                PtxEmitter.EmitLogicalShiftInstruction(w, instruction);
            else if ((opCode >= z.mov_pred) && (opCode <= z.cvt_sat_f64_f64))
                PtxEmitter.EmitDataMovementAndConversionInstruction(w, instruction);
            else if ((opCode >= z.tex_1d_v4_u32_s32) && (opCode <= z.tex_3d_v4_f32_f32))
                PtxEmitter.EmitTextureInstruction(w, instruction);
            else if ((opCode >= z.bra) && (opCode <= z.exit))
                PtxEmitter.EmitControlFlowInstruction(w, instruction);
            else if ((opCode >= z.bar_sync) && (opCode <= z.vote_uni_pred))
                PtxEmitter.EmitParallelSyncInstruction(w, instruction);
            else if ((opCode >= z.rcp_f32) && (opCode <= z.ex2_f32))
                PtxEmitter.EmitFloatingPointInstruction(w, instruction);
            else if ((opCode >= z.trap) && (opCode <= z.brkpt))
                PtxEmitter.EmitMiscellaneousInstruction(w, instruction);
        }

        public static string GetEmitString(this PtxComputeCapability computeCapability)
        {
            switch (computeCapability)
            {
                case PtxComputeCapability.Compute10: return "sm_10";
                case PtxComputeCapability.Compute11: return "sm_11";
                case PtxComputeCapability.Compute12: return "sm_12";
                case PtxComputeCapability.Compute13: return "sm_13";
                default:
                    throw new NotSupportedException("Bad compute capability: " + computeCapability);
            }
        }

        public static string GetPrefix(this PtxOperand.PtxStackType stackType)
        {
            switch (stackType)
            {
                case PtxOperand.PtxStackType.Object: return "%o";
                case PtxOperand.PtxStackType.ManagedPointer: return "%p";
                case PtxOperand.PtxStackType.I4: return "%i";
                case PtxOperand.PtxStackType.I8: return "%l";
                case PtxOperand.PtxStackType.R4: return "%f";
                case PtxOperand.PtxStackType.R8: return "%d";
                //? Later we'll check that it's really predicates.
                case PtxOperand.PtxStackType.ValueType: return "%pp";
                default:
                    throw new NotSupportedException("Bad vreg stacktype: " + stackType);
            }
        }

        public static string GetEmitString(this PtxOperand.PtxStateSpace stateSpace)
        {
            switch (stateSpace)
            {
                case PtxOperand.PtxStateSpace.Constant: return ".constant";
                case PtxOperand.PtxStateSpace.Parameter: return ".param";
                case PtxOperand.PtxStateSpace.Texture: return ".tex";
                case PtxOperand.PtxStateSpace.Global: return ".global";
                case PtxOperand.PtxStateSpace.Local: return ".local";
                case PtxOperand.PtxStateSpace.Register: return ".reg";
                case PtxOperand.PtxStateSpace.Shared: return ".shared";
                default:
                    throw new NotSupportedException("Bad vreg type: " + stateSpace);
            }
        }

        public static string GetEmitString(this PtxOperand.PtxStackType stackType, bool knownToBePredicate)
        {
            switch (stackType)
            {
                case PtxOperand.PtxStackType.pred: return ".pred";
                case PtxOperand.PtxStackType.b8: return ".b8";
                case PtxOperand.PtxStackType.b16: return ".b16";
                case PtxOperand.PtxStackType.b32: return ".b32";
                case PtxOperand.PtxStackType.b64: return ".b64";
                case PtxOperand.PtxStackType.u8: return ".u8";
                case PtxOperand.PtxStackType.u16: return ".u16";
                case PtxOperand.PtxStackType.u32: return ".u32";
                case PtxOperand.PtxStackType.u64: return ".u64";
                case PtxOperand.PtxStackType.s8: return ".s8";
                case PtxOperand.PtxStackType.s16: return ".s16";
                case PtxOperand.PtxStackType.s32: return ".s32";
                case PtxOperand.PtxStackType.s64: return ".s64";
                case PtxOperand.PtxStackType.f16: return ".f16";
                case PtxOperand.PtxStackType.f32: return ".f32";
                case PtxOperand.PtxStackType.f64: return ".f64";

                case PtxOperand.PtxStackType.Object: return ".s32";
                case PtxOperand.PtxStackType.ManagedPointer: return ".s32";
                case PtxOperand.PtxStackType.ValueType:
                    if (knownToBePredicate)
                        return ".pred";
                    throw new NotSupportedException("Cannot emit PTX type for non-predicate value type.");
                case PtxOperand.PtxStackType.I4: return ".s32";
                case PtxOperand.PtxStackType.I8: return ".s64";
                case PtxOperand.PtxStackType.R4: return ".f32";
                case PtxOperand.PtxStackType.R8: return ".f64";
                default:
                    throw new NotSupportedException("Cannot emit PTX type for '" + stackType + "'.");
            }
        }
    }
}
