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
        internal static void EmitTextureInstruction(TextWriter w, PtxInstruction instruction)
        {
            string text;
            var opCode = instruction.OpCode;
            switch (opCode)
            {
                // tex
                case z.tex_1d_v4_u32_s32: text = "tex.1d.v4.u32.s32"; break;
                case z.tex_1d_v4_u32_f32: text = "tex.1d.v4.u32.f32"; break;
                case z.tex_1d_v4_s32_s32: text = "tex.1d.v4.s32.s32"; break;
                case z.tex_1d_v4_s32_f32: text = "tex.1d.v4.s32.f32"; break;
                case z.tex_1d_v4_f32_s32: text = "tex.1d.v4.f32.s32"; break;
                case z.tex_1d_v4_f32_f32: text = "tex.1d.v4.f32.f32"; break;
                //
                case z.tex_2d_v4_u32_s32: text = "tex.2d.v4.u32.s32"; break;
                case z.tex_2d_v4_u32_f32: text = "tex.2d.v4.u32.f32"; break;
                case z.tex_2d_v4_s32_s32: text = "tex.2d.v4.s32.s32"; break;
                case z.tex_2d_v4_s32_f32: text = "tex.2d.v4.s32.f32"; break;
                case z.tex_2d_v4_f32_s32: text = "tex.2d.v4.f32.s32"; break;
                case z.tex_2d_v4_f32_f32: text = "tex.2d.v4.f32.f32"; break;
                //
                case z.tex_3d_v4_u32_s32: text = "tex.3d.v4.u32.s32"; break;
                case z.tex_3d_v4_u32_f32: text = "tex.3d.v4.u32.f32"; break;
                case z.tex_3d_v4_s32_s32: text = "tex.3d.v4.s32.s32"; break;
                case z.tex_3d_v4_s32_f32: text = "tex.3d.v4.s32.f32"; break;
                case z.tex_3d_v4_f32_s32: text = "tex.3d.v4.f32.s32"; break;
                case z.tex_3d_v4_f32_f32: text = "tex.3d.v4.f32.f32"; break;
                //
                default: throw new InvalidOperationException();
            }
            EmitBasicOpcode(w, instruction, text);
        }
    }
}
