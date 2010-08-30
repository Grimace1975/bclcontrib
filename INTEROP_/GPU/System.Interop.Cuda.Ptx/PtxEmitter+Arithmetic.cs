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
    using a = PtxEmitterExtentions.AdjustPack;
    public partial class PtxEmitter
    {
        internal static void EmitArithmeticInstruction(TextWriter w, PtxInstruction instruction)
        {
            string text;
            var opCode = instruction.OpCode;
            switch (opCode)
            {
                // add
                case z.add_b16: text = "add.b16"; break;
                case z.add_b32: text = "add.b32"; break;
                case z.add_b64: text = "add.b64"; break;
                case z.add_u16: text = "add.u16"; break;
                case z.add_u32: text = "add.u32"; break;
                case z.add_u64: text = "add.u64"; break;
                case z.add_s16: text = "add.s16"; break;
                case z.add_s32: text = "add.s32"; break;
                case z.add_sat_s32: text = "add.sat.s32"; break;
                case z.add_s64: text = "add.s64"; break;
                case z.add_f32: text = "add.f32"; opCode.AdjustText(a.rnd, ref text); break;
                case z.add_sat_f32: text = "add.sat.f32"; opCode.AdjustText(a.rnd, ref text); break;
                case z.add_f64: text = "add.f64"; opCode.AdjustText(a.rnd, ref text); break;
                //
                case z.add_cc_b32: text = "add.cc.b32"; break;
                case z.add_cc_u32: text = "add.cc.u32"; break;
                case z.add_cc_s32: text = "add.cc.s32"; break;
    
                // adc
                case z.addc_b32: text = "addc.b32"; break;
                case z.addc_cc_b32: text = "addc.cc.b32"; break;
                case z.addc_u32: text = "addc.u32"; break;
                case z.addc_cc_u32: text = "addc.cc.u32"; break;
                case z.addc_s32: text = "addc.s32"; break;
                case z.addc_cc_s32: text = "addc.cc.s32"; break;
                
                // sub
                case z.sub_b16: text = "sub.b16"; break;
                case z.sub_b32: text = "sub.b32"; break;
                case z.sub_b64: text = "sub.b64"; break;
                case z.sub_u16: text = "sub.u16"; break;
                case z.sub_u32: text = "sub.u32"; break;
                case z.sub_u64: text = "sub.u64"; break;
                case z.sub_s16: text = "sub.s16"; break;
                case z.sub_s32: text = "sub.s32"; break;
                case z.sub_sat_s32: text = "sub.sat.s32"; break;
                case z.sub_s64: text = "sub.s64"; break;
                case z.sub_f32: text = "sub.f32"; opCode.AdjustText(a.rnd, ref text); break;
                case z.sub_sat_f32: text = "sub.sat.f32"; opCode.AdjustText(a.rnd, ref text); break;
                case z.sub_f64: text = "sub.f64"; opCode.AdjustText(a.rnd, ref text); break;
                
                // mul
                case z.mul_b16: text = "mul.b16"; opCode.AdjustText(a.hilowide, ref text); break;
                case z.mul_b32: text = "mul.b32"; opCode.AdjustText(a.hilowide, ref text); break;
                case z.mul_b64: text = "mul.b64"; opCode.AdjustText(a.hilowide, ref text); break;
                case z.mul_u16: text = "mul.u16"; opCode.AdjustText(a.hilowide, ref text); break;
                case z.mul_u32: text = "mul.u32"; opCode.AdjustText(a.hilowide, ref text); break;
                case z.mul_u64: text = "mul.u64"; opCode.AdjustText(a.hilowide, ref text); break;
                case z.mul_s16: text = "mul.s16"; opCode.AdjustText(a.hilowide, ref text); break;
                case z.mul_s32: text = "mul.s32"; opCode.AdjustText(a.hilowide, ref text); break;
                case z.mul_s64: text = "mul.s64"; opCode.AdjustText(a.hilowide, ref text); break;
                case z.mul_f32: text = "mul.f32"; opCode.AdjustText(a.rnd, ref text); break;
                case z.mul_sat_f32: text = "mul.sat.f32"; opCode.AdjustText(a.rnd, ref text); break;
                case z.mul_f64: text = "mul.f64"; opCode.AdjustText(a.rnd, ref text); break;
                
                // mad
                case z.mad_b16: text = "mad.b16"; opCode.AdjustText(a.hilowide, ref text); break;
                case z.mad_b32: text = "mad.b32"; opCode.AdjustText(a.hilowide, ref text); break;
                case z.mad_b64: text = "mad.b64"; opCode.AdjustText(a.hilowide, ref text); break;
                case z.mad_u16: text = "mad.u16"; opCode.AdjustText(a.hilowide, ref text); break;
                case z.mad_u32: text = "mad.u32"; opCode.AdjustText(a.hilowide, ref text); break;
                case z.mad_u64: text = "mad.u64"; opCode.AdjustText(a.hilowide, ref text); break;
                case z.mad_s16: text = "mad.s16"; opCode.AdjustText(a.hilowide, ref text); break;
                case z.mad_s32: text = "mad.s32"; opCode.AdjustText(a.hilowide, ref text); break;
                case z.mad_sat_s32: text = "mad.sat.s32"; opCode.AdjustText(a.hilowide, ref text); break;
                case z.mad_s64: text = "mad.s64"; opCode.AdjustText(a.hilowide, ref text); break;
                case z.mad_f32: text = "mad.f32"; opCode.AdjustText(a.rnd, ref text); break;
                case z.mad_sat_f32: text = "mad.sat.f32"; opCode.AdjustText(a.rnd, ref text); break;
                case z.mad_f64: text = "mad.f64"; opCode.AdjustText(a.rnd, ref text); break;
                
                // mul24
                case z.mul24_b32: text = "mul24.b32"; opCode.AdjustText(a.hilo, ref text); break;
                case z.mul24_u32: text = "mul24.u32"; opCode.AdjustText(a.hilo, ref text); break;
                case z.mul24_s32: text = "mul24.s32"; opCode.AdjustText(a.hilo, ref text); break;
                
                // mad24
                case z.mad24_b32: text = "mad24.b32"; opCode.AdjustText(a.hilo, ref text); break;
                case z.mad24_u32: text = "mad24.u32"; opCode.AdjustText(a.hilo, ref text); break;
                case z.mad24_s32: text = "mad24.s32"; opCode.AdjustText(a.hilo, ref text); break;
                case z.mad24_sat_s32: text = "mad24.sat.s32"; opCode.AdjustText(a.hilo, ref text); break;
                // sad
                case z.sad_b16: text = "sad.b16"; break;
                case z.sad_b32: text = "sad.b32"; break;
                case z.sad_b64: text = "sad.b64"; break;
                case z.sad_u16: text = "sad.u16"; break;
                case z.sad_u32: text = "sad.u32"; break;
                case z.sad_u64: text = "sad.u64"; break;
                case z.sad_s16: text = "sad.s16"; break;
                case z.sad_s32: text = "sad.s32"; break;
                case z.sad_s64: text = "sad.s64"; break;
                
                // div
                case z.div_b16: text = "div.b16"; opCode.AdjustText(a.wide, ref text); break;
                case z.div_b32: text = "div.b32"; opCode.AdjustText(a.wide, ref text); break;
                case z.div_b64: text = "div.b64"; break;
                case z.div_u16: text = "div.u16"; opCode.AdjustText(a.wide, ref text); break;
                case z.div_u32: text = "div.u32"; opCode.AdjustText(a.wide, ref text); break;
                case z.div_u64: text = "div.u64"; break;
                case z.div_s16: text = "div.s16"; opCode.AdjustText(a.wide, ref text); break;
                case z.div_s32: text = "div.s32"; opCode.AdjustText(a.wide, ref text); break;
                case z.div_s64: text = "div.s64"; break;
                case z.div_f32: text = "div.f32"; break;
                case z.div_sat_f32: text = "div.sat.f32"; break;
                case z.div_f64: text = "div.f64"; break;
                
                // rem
                case z.rem_b16: text = "rem.b16"; opCode.AdjustText(a.wide, ref text); break;
                case z.rem_b32: text = "rem.b32"; opCode.AdjustText(a.wide, ref text); break;
                case z.rem_b64: text = "rem.b64"; break;
                case z.rem_u16: text = "rem.u16"; opCode.AdjustText(a.wide, ref text); break;
                case z.rem_u32: text = "rem.u32"; opCode.AdjustText(a.wide, ref text); break;
                case z.rem_u64: text = "rem.u64"; break;
                case z.rem_s16: text = "rem.s16"; opCode.AdjustText(a.wide, ref text); break;
                case z.rem_s32: text = "rem.s32"; opCode.AdjustText(a.wide, ref text); break;
                case z.rem_s64: text = "rem.s64"; break;
                
                // abs
                case z.abs_b16: text = "abs.b16"; break;
                case z.abs_b32: text = "abs.b32"; break;
                case z.abs_b64: text = "abs.b64"; break;
                case z.abs_s16: text = "abs.s16"; break;
                case z.abs_s32: text = "abs.s32"; break;
                case z.abs_s64: text = "abs.s64"; break;
                case z.abs_f32: text = "abs.f32"; break;
                case z.abs_f64: text = "abs.f64"; break;
                
                // neg
                case z.neg_b16: text = "neg.b16"; break;
                case z.neg_b32: text = "neg.b32"; break;
                case z.neg_b64: text = "neg.b64"; break;
                case z.neg_s16: text = "neg.s16"; break;
                case z.neg_s32: text = "neg.s32"; break;
                case z.neg_s64: text = "neg.s64"; break;
                case z.neg_f32: text = "neg.f32"; break;
                case z.neg_f64: text = "neg.f64"; break;
                
                // min
                case z.min_b16: text = "min.b16"; break;
                case z.min_b32: text = "min.b32"; break;
                case z.min_b64: text = "min.b64"; break;
                case z.min_u16: text = "min.u16"; break;
                case z.min_u32: text = "min.u32"; break;
                case z.min_u64: text = "min.u64"; break;
                case z.min_s16: text = "min.s16"; break;
                case z.min_s32: text = "min.s32"; break;
                case z.min_s64: text = "min.s64"; break;
                case z.min_f32: text = "min.f32"; break;
                case z.min_f64: text = "min.f64"; break;
                
                // max
                case z.max_b16: text = "max.b16"; break;
                case z.max_b32: text = "max.b32"; break;
                case z.max_b64: text = "max.b64"; break;
                case z.max_u16: text = "max.u16"; break;
                case z.max_u32: text = "max.u32"; break;
                case z.max_u64: text = "max.u64"; break;
                case z.max_s16: text = "max.s16"; break;
                case z.max_s32: text = "max.s32"; break;
                case z.max_s64: text = "max.s64"; break;
                case z.max_f32: text = "max.f32"; break;
                case z.max_f64: text = "max.f64"; break;
                //
                default: throw new InvalidOperationException();
            }
            EmitBasicOpcode(w, instruction, text);
        }
    }
}
