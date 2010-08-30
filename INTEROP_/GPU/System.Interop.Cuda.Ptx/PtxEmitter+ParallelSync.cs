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
        internal static void EmitParallelSyncInstruction(TextWriter w, PtxInstruction instruction)
        {
            string text;
            var opCode = instruction.OpCode;
            switch (opCode)
            {
                // bar
                case z.bar_sync: text = "bar.sync"; break;

                // atom
                case z.atom_global_and_b32: text = "atom.global.and.b32"; break;
                case z.atom_global_or_b32: text = "atom.global.or.b32"; break;
                case z.atom_global_xor_b32: text = "atom.global.xor.b32"; break;
                case z.atom_global_cas_b32: text = "atom.global.cas.b32"; break;
                case z.atom_global_cas_b64: text = "atom.global.cas.b64"; break;
                case z.atom_global_exch_b32: text = "atom.global.exch.b32"; break;
                case z.atom_global_exch_b64: text = "atom.global.exch.b64"; break;
                case z.atom_global_add_u32: text = "atom.global.add.u32"; break;
                case z.atom_global_add_u64: text = "atom.global.add.u64"; break;
                case z.atom_global_add_s32: text = "atom.global.add.s32"; break;
                case z.atom_global_add_f32: text = "atom.global.add.f32"; break;
                case z.atom_global_inc_u32: text = "atom.global.inc.u32"; break;
                case z.atom_global_dec_u32: text = "atom.global.dec.u32"; break;
                case z.atom_global_min_u32: text = "atom.global.min.u32"; break;
                case z.atom_global_min_s32: text = "atom.global.min.s32"; break;
                case z.atom_global_min_f32: text = "atom.global.min.f32"; break;
                case z.atom_global_max_u32: text = "atom.global.max.u32"; break;
                case z.atom_global_max_s32: text = "atom.global.max.s32"; break;
                case z.atom_global_max_f32: text = "atom.global.max.f32"; break;
                case z.atom_shared_and_b32: text = "atom.shared.and.b32"; break;
                case z.atom_shared_or_b32: text = "atom.shared.or.b32"; break;
                case z.atom_shared_xor_b32: text = "atom.shared.xor.b32"; break;
                case z.atom_shared_cas_b32: text = "atom.shared.cas.b32"; break;
                case z.atom_shared_cas_b64: text = "atom.shared.cas.b64"; break;
                case z.atom_shared_exch_b32: text = "atom.shared.exch.b32"; break;
                case z.atom_shared_exch_b64: text = "atom.shared.exch.b64"; break;
                case z.atom_shared_add_u32: text = "atom.shared.add.u32"; break;
                case z.atom_shared_add_u64: text = "atom.shared.add.u64"; break;
                case z.atom_shared_add_s32: text = "atom.shared.add.s32"; break;
                case z.atom_shared_add_f32: text = "atom.shared.add.f32"; break;
                case z.atom_shared_inc_u32: text = "atom.shared.inc.u32"; break;
                case z.atom_shared_dec_u32: text = "atom.shared.dec.u32"; break;
                case z.atom_shared_min_u32: text = "atom.shared.min.u32"; break;
                case z.atom_shared_min_s32: text = "atom.shared.min.s32"; break;
                case z.atom_shared_min_f32: text = "atom.shared.min.f32"; break;
                case z.atom_shared_max_u32: text = "atom.shared.max.u32"; break;
                case z.atom_shared_max_s32: text = "atom.shared.max.s32"; break;
                case z.atom_shared_max_f32: text = "atom.shared.max.f32"; break;

                // red
                case z.red_global_and_b32: text = "red.global.and.b32"; break;
                case z.red_global_or_b32: text = "red.global.or.b32"; break;
                case z.red_global_xor_b32: text = "red.global.xor.b32"; break;
                case z.red_global_add_u32: text = "red.global.add.u32"; break;
                case z.red_global_add_u64: text = "red.global.add.u64"; break;
                case z.red_global_add_s32: text = "red.global.add.s32"; break;
                case z.red_global_add_f32: text = "red.global.add.f32"; break;
                case z.red_global_inc_u32: text = "red.global.inc.u32"; break;
                case z.red_global_dec_u32: text = "red.global.dec.u32"; break;
                case z.red_global_min_u32: text = "red.global.min.u32"; break;
                case z.red_global_min_s32: text = "red.global.min.s32"; break;
                case z.red_global_min_f32: text = "red.global.min.f32"; break;
                case z.red_global_max_u32: text = "red.global.max.u32"; break;
                case z.red_global_max_s32: text = "red.global.max.s32"; break;
                case z.red_global_max_f32: text = "red.global.max.f32"; break;
                case z.red_shared_and_b32: text = "red.shared.and.b32"; break;
                case z.red_shared_or_b32: text = "red.shared.or.b32"; break;
                case z.red_shared_xor_b32: text = "red.shared.xor.b32"; break;
                case z.red_shared_add_u32: text = "red.shared.add.u32"; break;
                case z.red_shared_add_u64: text = "red.shared.add.u64"; break;
                case z.red_shared_add_s32: text = "red.shared.add.s32"; break;
                case z.red_shared_add_f32: text = "red.shared.add.f32"; break;
                case z.red_shared_inc_u32: text = "red.shared.inc.u32"; break;
                case z.red_shared_dec_u32: text = "red.shared.dec.u32"; break;
                case z.red_shared_min_u32: text = "red.shared.min.u32"; break;
                case z.red_shared_min_s32: text = "red.shared.min.s32"; break;
                case z.red_shared_min_f32: text = "red.shared.min.f32"; break;
                case z.red_shared_max_u32: text = "red.shared.max.u32"; break;
                case z.red_shared_max_s32: text = "red.shared.max.s32"; break;
                case z.red_shared_max_f32: text = "red.shared.max.f32"; break;

                // vote
                case z.vote_all_pred: text = "vote.all.pred"; break;
                case z.vote_any_pred: text = "vote.any.pred"; break;
                case z.vote_uni_pred: text = "vote.uni.pred"; break;

                //
                default: throw new InvalidOperationException();
            }
            EmitBasicOpcode(w, instruction, text);
        }
    }
}
