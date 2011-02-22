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
        internal static void EmitFloatingPointInstruction(TextWriter w, PtxInstruction instruction)
        {
            string text;
            var opCode = instruction.OpCode;
            switch (opCode)
            {
                // rcp
                case z.rcp_f32: text = "rcp.f32"; break;
                case z.rcp_f64: text = "rcp.f64"; break;

                // sqrt
                case z.sqrt_f32: text = "sqrt.f32"; break;
                case z.sqrt_f64: text = "sqrt.f64"; break;

                // rsqrt
                case z.rsqrt_f32: text = "rsqrt.f32"; break;
                case z.rsqrt_f64: text = "rsqrt.f64"; break;

                // sin
                case z.sin_f32: text = "sin.f32"; break;

                // cos
                case z.cos_f32: text = "cos.f32"; break;

                // lg2
                case z.lg2_f32: text = "lg2.f32"; break;

                // ex2
                case z.ex2_f32: text = "ex2.f32"; break;

                //
                default: throw new InvalidOperationException();
            }
            EmitBasicOpcode(w, instruction, text);
        }
    }
}
