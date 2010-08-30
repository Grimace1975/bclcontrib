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
        internal static void EmitControlFlowInstruction(TextWriter w, PtxInstruction instruction)
        {
            string text;
            var opCode = instruction.OpCode;
            switch (opCode)
            {
                // bra
                case z.bra:
                    w.WriteLine("\t" + GetBasicOpcodePredicate(instruction) + " bra " + ((PtxBlock)instruction.Operand).Name + ";");
                    return;
                case z.bra_uni:
                    w.WriteLine("\t" + GetBasicOpcodePredicate(instruction) + " bra.uni " + ((PtxBlock)instruction.Operand).Name + ";");
                    return;

                // call
                case z.call: text = "call"; break;
                case z.call_uni: text = "call.uni"; break;

                // ret
                case z.ret: text = "ret"; break;
                case z.ret_uni: text = "ret.uni"; break;

                // exit
                case z.exit: text = "exit"; break;

                //
                default: throw new InvalidOperationException();
            }
            EmitBasicOpcode(w, instruction, text);
        }
    }
}
