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
    public partial class PtxEmitter
    {
        internal static void EmitLogicalShiftInstruction(TextWriter w, PtxInstruction instruction)
        {
            string text;
            var opCode = instruction.OpCode;
            switch (opCode)
            {
                // and
                case z.and_pred: text = "and.pred"; break;
                case z.and_b16: text = "and.b16"; break;
                case z.and_b32: text = "and.b32"; break;
                case z.and_b64: text = "and.b64"; break;

                // or
                case z.or_pred: text = "or.pred"; break;
                case z.or_b16: text = "or.b16"; break;
                case z.or_b32: text = "or.b32"; break;
                case z.or_b64: text = "or.b64"; break;

                // xor
                case z.xor_pred: text = "xor.pred"; break;
                case z.xor_b16: text = "xor.b16"; break;
                case z.xor_b32: text = "xor.b32"; break;
                case z.xor_b64: text = "xor.b64"; break;

                // not
                case z.not_pred: text = "not.pred"; break;
                case z.not_b16: text = "not.b16"; break;
                case z.not_b32: text = "not.b32"; break;
                case z.not_b64: text = "not.b64"; break;

                // cnot
                case z.cnot_b16: text = "cnot.b16"; break;
                case z.cnot_b32: text = "cnot.b32"; break;
                case z.cnot_b64: text = "cnot.b64"; break;

                // shl
                case z.shl_b16: text = "shl.b16"; break;
                case z.shl_b32: text = "shl.b32"; break;
                case z.shl_b64: text = "shl.b64"; break;

                // shr
                case z.shr_b16: text = "shr.b16"; break;
                case z.shr_b32: text = "shr.b32"; break;
                case z.shr_b64: text = "shr.b64"; break;
                case z.shr_u16: text = "shr.u16"; break;
                case z.shr_u32: text = "shr.u32"; break;
                case z.shr_u64: text = "shr.u64"; break;
                case z.shr_s16: text = "shr.s16"; break;
                case z.shr_s32: text = "shr.s32"; break;
                case z.shr_s64: text = "shr.s64"; break;

                //
                default: throw new InvalidOperationException();
            }
            EmitBasicOpcode(w, instruction, text);
        }
    }
}
