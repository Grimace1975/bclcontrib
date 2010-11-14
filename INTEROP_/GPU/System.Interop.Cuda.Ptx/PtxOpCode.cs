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
namespace System.Interop.Cuda
{
    [Flags]
    public enum PtxOpCode : long
    {
        _hi, _lo, _wide,
        _rn, _rz, _rm, _rp,

        #region Arithmetic Instructions

        // add
        add_b16, add_b32, add_b64,
        add_u16, add_u32, add_u64,
        add_s16, add_s32, add_sat_s32, add_s64,
        add_f32, add_sat_f32, add_f64,
        //
        add_cc_b32, add_cc_u32, add_cc_s32,

        // addc
        addc_b32, addc_cc_b32, addc_u32, addc_cc_u32, addc_s32, addc_cc_s32,

        // sub
        sub_b16, sub_b32, sub_b64,
        sub_u16, sub_u32, sub_u64,
        sub_s16, sub_s32, sub_sat_s32, sub_s64,
        sub_f32, sub_sat_f32, sub_f64,

        // mul
        mul_b16, mul_b32, mul_b64,
        mul_u16, mul_u32, mul_u64,
        mul_s16, mul_s32, mul_s64,
        mul_f32, mul_sat_f32, mul_f64,

        // mad
        mad_b16, mad_b32, mad_b64,
        mad_u16, mad_u32, mad_u64,
        mad_s16, mad_s32, mad_sat_s32, mad_s64,
        mad_f32, mad_sat_f32, mad_f64,

        // mul24
        mul24_b32,
        mul24_u32,
        mul24_s32,

        // mad24
        mad24_b32,
        mad24_u32,
        mad24_s32, mad24_sat_s32,

        // sad
        sad_b16, sad_b32, sad_b64,
        sad_u16, sad_u32, sad_u64,
        sad_s16, sad_s32, sad_s64,

        // div
        div_b16, div_b32, div_b64,
        div_u16, div_u32, div_u64,
        div_s16, div_s32, div_s64,
        div_f32, div_sat_f32, div_f64,

        // rem
        rem_b16, rem_b32, rem_b64,
        rem_u16, rem_u32, rem_u64,
        rem_s16, rem_s32, rem_s64,

        // abs
        abs_b16, abs_b32, abs_b64, //?
        abs_s16, abs_s32, abs_s64,
        abs_f32, abs_f64,

        // neg
        neg_b16, neg_b32, neg_b64, //?
        neg_s16, neg_s32, neg_s64,
        neg_f32, neg_f64,

        // min
        min_b16, min_b32, min_b64,
        min_u16, min_u32, min_u64,
        min_s16, min_s32, min_s64,
        min_f32, min_f64,

        // max
        max_b16, max_b32, max_b64,
        max_u16, max_u32, max_u64,
        max_s16, max_s32, max_s64,
        max_f32, max_f64,

        #endregion

        #region Comparison Select Instructions

        // set
        set_eq_b32_b16, set_eq_and_b32_b16, set_eq_or_b32_b16, set_eq_xor_b32_b16, set_eq_b32_b32, set_eq_and_b32_b32, set_eq_or_b32_b32, set_eq_xor_b32_b32, set_eq_b32_b64, set_eq_and_b32_b64, set_eq_or_b32_b64, set_eq_xor_b32_b64,
        set_eq_b32_u16, set_eq_and_b32_u16, set_eq_or_b32_u16, set_eq_xor_b32_u16, set_eq_b32_u32, set_eq_and_b32_u32, set_eq_or_b32_u32, set_eq_xor_b32_u32, set_eq_b32_u64, set_eq_and_b32_u64, set_eq_or_b32_u64, set_eq_xor_b32_u64,
        set_eq_b32_s16, set_eq_and_b32_s16, set_eq_or_b32_s16, set_eq_xor_b32_s16, set_eq_b32_s32, set_eq_and_b32_s32, set_eq_or_b32_s32, set_eq_xor_b32_s32, set_eq_b32_s64, set_eq_and_b32_s64, set_eq_or_b32_s64, set_eq_xor_b32_s64,
        set_eq_b32_f32, set_eq_and_b32_f32, set_eq_or_b32_f32, set_eq_xor_b32_f32, set_eq_b32_f64, set_eq_and_b32_f64, set_eq_or_b32_f64, set_eq_xor_b32_f64,
        set_ne_b32_b16, set_ne_and_b32_b16, set_ne_or_b32_b16, set_ne_xor_b32_b16, set_ne_b32_b32, set_ne_and_b32_b32, set_ne_or_b32_b32, set_ne_xor_b32_b32, set_ne_b32_b64, set_ne_and_b32_b64, set_ne_or_b32_b64, set_ne_xor_b32_b64,
        set_ne_b32_u16, set_ne_and_b32_u16, set_ne_or_b32_u16, set_ne_xor_b32_u16, set_ne_b32_u32, set_ne_and_b32_u32, set_ne_or_b32_u32, set_ne_xor_b32_u32, set_ne_b32_u64, set_ne_and_b32_u64, set_ne_or_b32_u64, set_ne_xor_b32_u64,
        set_ne_b32_s16, set_ne_and_b32_s16, set_ne_or_b32_s16, set_ne_xor_b32_s16, set_ne_b32_s32, set_ne_and_b32_s32, set_ne_or_b32_s32, set_ne_xor_b32_s32, set_ne_b32_s64, set_ne_and_b32_s64, set_ne_or_b32_s64, set_ne_xor_b32_s64,
        set_ne_b32_f32, set_ne_and_b32_f32, set_ne_or_b32_f32, set_ne_xor_b32_f32, set_ne_b32_f64, set_ne_and_b32_f64, set_ne_or_b32_f64, set_ne_xor_b32_f64,
        set_lt_b32_u16, set_lt_and_b32_u16, set_lt_or_b32_u16, set_lt_xor_b32_u16, set_lt_b32_u32, set_lt_and_b32_u32, set_lt_or_b32_u32, set_lt_xor_b32_u32, set_lt_b32_u64, set_lt_and_b32_u64, set_lt_or_b32_u64, set_lt_xor_b32_u64,
        set_lt_b32_s16, set_lt_and_b32_s16, set_lt_or_b32_s16, set_lt_xor_b32_s16, set_lt_b32_s32, set_lt_and_b32_s32, set_lt_or_b32_s32, set_lt_xor_b32_s32, set_lt_b32_s64, set_lt_and_b32_s64, set_lt_or_b32_s64, set_lt_xor_b32_s64,
        set_lt_b32_f32, set_lt_and_b32_f32, set_lt_or_b32_f32, set_lt_xor_b32_f32, set_lt_b32_f64, set_lt_and_b32_f64, set_lt_or_b32_f64, set_lt_xor_b32_f64,
        set_le_b32_u16, set_le_and_b32_u16, set_le_or_b32_u16, set_le_xor_b32_u16, set_le_b32_u32, set_le_and_b32_u32, set_le_or_b32_u32, set_le_xor_b32_u32, set_le_b32_u64, set_le_and_b32_u64, set_le_or_b32_u64, set_le_xor_b32_u64,
        set_le_b32_s16, set_le_and_b32_s16, set_le_or_b32_s16, set_le_xor_b32_s16, set_le_b32_s32, set_le_and_b32_s32, set_le_or_b32_s32, set_le_xor_b32_s32, set_le_b32_s64, set_le_and_b32_s64, set_le_or_b32_s64, set_le_xor_b32_s64,
        set_le_b32_f32, set_le_and_b32_f32, set_le_or_b32_f32, set_le_xor_b32_f32, set_le_b32_f64, set_le_and_b32_f64, set_le_or_b32_f64, set_le_xor_b32_f64,
        set_gt_b32_u16, set_gt_and_b32_u16, set_gt_or_b32_u16, set_gt_xor_b32_u16, set_gt_b32_u32, set_gt_and_b32_u32, set_gt_or_b32_u32, set_gt_xor_b32_u32, set_gt_b32_u64, set_gt_and_b32_u64, set_gt_or_b32_u64, set_gt_xor_b32_u64,
        set_gt_b32_s16, set_gt_and_b32_s16, set_gt_or_b32_s16, set_gt_xor_b32_s16, set_gt_b32_s32, set_gt_and_b32_s32, set_gt_or_b32_s32, set_gt_xor_b32_s32, set_gt_b32_s64, set_gt_and_b32_s64, set_gt_or_b32_s64, set_gt_xor_b32_s64,
        set_gt_b32_f32, set_gt_and_b32_f32, set_gt_or_b32_f32, set_gt_xor_b32_f32, set_gt_b32_f64, set_gt_and_b32_f64, set_gt_or_b32_f64, set_gt_xor_b32_f64,
        set_ge_b32_u16, set_ge_and_b32_u16, set_ge_or_b32_u16, set_ge_xor_b32_u16, set_ge_b32_u32, set_ge_and_b32_u32, set_ge_or_b32_u32, set_ge_xor_b32_u32, set_ge_b32_u64, set_ge_and_b32_u64, set_ge_or_b32_u64, set_ge_xor_b32_u64,
        set_ge_b32_s16, set_ge_and_b32_s16, set_ge_or_b32_s16, set_ge_xor_b32_s16, set_ge_b32_s32, set_ge_and_b32_s32, set_ge_or_b32_s32, set_ge_xor_b32_s32, set_ge_b32_s64, set_ge_and_b32_s64, set_ge_or_b32_s64, set_ge_xor_b32_s64,
        set_ge_b32_f32, set_ge_and_b32_f32, set_ge_or_b32_f32, set_ge_xor_b32_f32, set_ge_b32_f64, set_ge_and_b32_f64, set_ge_or_b32_f64, set_ge_xor_b32_f64,
        //
        set_eq_u32_b16, set_eq_and_u32_b16, set_eq_or_u32_b16, set_eq_xor_u32_b16, set_eq_u32_b32, set_eq_and_u32_b32, set_eq_or_u32_b32, set_eq_xor_u32_b32, set_eq_u32_b64, set_eq_and_u32_b64, set_eq_or_u32_b64, set_eq_xor_u32_b64,
        set_eq_u32_u16, set_eq_and_u32_u16, set_eq_or_u32_u16, set_eq_xor_u32_u16, set_eq_u32_u32, set_eq_and_u32_u32, set_eq_or_u32_u32, set_eq_xor_u32_u32, set_eq_u32_u64, set_eq_and_u32_u64, set_eq_or_u32_u64, set_eq_xor_u32_u64,
        set_eq_u32_s16, set_eq_and_u32_s16, set_eq_or_u32_s16, set_eq_xor_u32_s16, set_eq_u32_s32, set_eq_and_u32_s32, set_eq_or_u32_s32, set_eq_xor_u32_s32, set_eq_u32_s64, set_eq_and_u32_s64, set_eq_or_u32_s64, set_eq_xor_u32_s64,
        set_eq_u32_f32, set_eq_and_u32_f32, set_eq_or_u32_f32, set_eq_xor_u32_f32, set_eq_u32_f64, set_eq_and_u32_f64, set_eq_or_u32_f64, set_eq_xor_u32_f64,
        set_ne_u32_b16, set_ne_and_u32_b16, set_ne_or_u32_b16, set_ne_xor_u32_b16, set_ne_u32_b32, set_ne_and_u32_b32, set_ne_or_u32_b32, set_ne_xor_u32_b32, set_ne_u32_b64, set_ne_and_u32_b64, set_ne_or_u32_b64, set_ne_xor_u32_b64,
        set_ne_u32_u16, set_ne_and_u32_u16, set_ne_or_u32_u16, set_ne_xor_u32_u16, set_ne_u32_u32, set_ne_and_u32_u32, set_ne_or_u32_u32, set_ne_xor_u32_u32, set_ne_u32_u64, set_ne_and_u32_u64, set_ne_or_u32_u64, set_ne_xor_u32_u64,
        set_ne_u32_s16, set_ne_and_u32_s16, set_ne_or_u32_s16, set_ne_xor_u32_s16, set_ne_u32_s32, set_ne_and_u32_s32, set_ne_or_u32_s32, set_ne_xor_u32_s32, set_ne_u32_s64, set_ne_and_u32_s64, set_ne_or_u32_s64, set_ne_xor_u32_s64,
        set_ne_u32_f32, set_ne_and_u32_f32, set_ne_or_u32_f32, set_ne_xor_u32_f32, set_ne_u32_f64, set_ne_and_u32_f64, set_ne_or_u32_f64, set_ne_xor_u32_f64,
        set_lt_u32_u16, set_lt_and_u32_u16, set_lt_or_u32_u16, set_lt_xor_u32_u16, set_lt_u32_u32, set_lt_and_u32_u32, set_lt_or_u32_u32, set_lt_xor_u32_u32, set_lt_u32_u64, set_lt_and_u32_u64, set_lt_or_u32_u64, set_lt_xor_u32_u64,
        set_lt_u32_s16, set_lt_and_u32_s16, set_lt_or_u32_s16, set_lt_xor_u32_s16, set_lt_u32_s32, set_lt_and_u32_s32, set_lt_or_u32_s32, set_lt_xor_u32_s32, set_lt_u32_s64, set_lt_and_u32_s64, set_lt_or_u32_s64, set_lt_xor_u32_s64,
        set_lt_u32_f32, set_lt_and_u32_f32, set_lt_or_u32_f32, set_lt_xor_u32_f32, set_lt_u32_f64, set_lt_and_u32_f64, set_lt_or_u32_f64, set_lt_xor_u32_f64,
        set_le_u32_u16, set_le_and_u32_u16, set_le_or_u32_u16, set_le_xor_u32_u16, set_le_u32_u32, set_le_and_u32_u32, set_le_or_u32_u32, set_le_xor_u32_u32, set_le_u32_u64, set_le_and_u32_u64, set_le_or_u32_u64, set_le_xor_u32_u64,
        set_le_u32_s16, set_le_and_u32_s16, set_le_or_u32_s16, set_le_xor_u32_s16, set_le_u32_s32, set_le_and_u32_s32, set_le_or_u32_s32, set_le_xor_u32_s32, set_le_u32_s64, set_le_and_u32_s64, set_le_or_u32_s64, set_le_xor_u32_s64,
        set_le_u32_f32, set_le_and_u32_f32, set_le_or_u32_f32, set_le_xor_u32_f32, set_le_u32_f64, set_le_and_u32_f64, set_le_or_u32_f64, set_le_xor_u32_f64,
        set_gt_u32_u16, set_gt_and_u32_u16, set_gt_or_u32_u16, set_gt_xor_u32_u16, set_gt_u32_u32, set_gt_and_u32_u32, set_gt_or_u32_u32, set_gt_xor_u32_u32, set_gt_u32_u64, set_gt_and_u32_u64, set_gt_or_u32_u64, set_gt_xor_u32_u64,
        set_gt_u32_s16, set_gt_and_u32_s16, set_gt_or_u32_s16, set_gt_xor_u32_s16, set_gt_u32_s32, set_gt_and_u32_s32, set_gt_or_u32_s32, set_gt_xor_u32_s32, set_gt_u32_s64, set_gt_and_u32_s64, set_gt_or_u32_s64, set_gt_xor_u32_s64,
        set_gt_u32_f32, set_gt_and_u32_f32, set_gt_or_u32_f32, set_gt_xor_u32_f32, set_gt_u32_f64, set_gt_and_u32_f64, set_gt_or_u32_f64, set_gt_xor_u32_f64,
        set_ge_u32_u16, set_ge_and_u32_u16, set_ge_or_u32_u16, set_ge_xor_u32_u16, set_ge_u32_u32, set_ge_and_u32_u32, set_ge_or_u32_u32, set_ge_xor_u32_u32, set_ge_u32_u64, set_ge_and_u32_u64, set_ge_or_u32_u64, set_ge_xor_u32_u64,
        set_ge_u32_s16, set_ge_and_u32_s16, set_ge_or_u32_s16, set_ge_xor_u32_s16, set_ge_u32_s32, set_ge_and_u32_s32, set_ge_or_u32_s32, set_ge_xor_u32_s32, set_ge_u32_s64, set_ge_and_u32_s64, set_ge_or_u32_s64, set_ge_xor_u32_s64,
        set_ge_u32_f32, set_ge_and_u32_f32, set_ge_or_u32_f32, set_ge_xor_u32_f32, set_ge_u32_f64, set_ge_and_u32_f64, set_ge_or_u32_f64, set_ge_xor_u32_f64,
        //
        set_eq_s32_b16, set_eq_and_s32_b16, set_eq_or_s32_b16, set_eq_xor_s32_b16, set_eq_s32_b32, set_eq_and_s32_b32, set_eq_or_s32_b32, set_eq_xor_s32_b32, set_eq_s32_b64, set_eq_and_s32_b64, set_eq_or_s32_b64, set_eq_xor_s32_b64,
        set_eq_s32_u16, set_eq_and_s32_u16, set_eq_or_s32_u16, set_eq_xor_s32_u16, set_eq_s32_u32, set_eq_and_s32_u32, set_eq_or_s32_u32, set_eq_xor_s32_u32, set_eq_s32_u64, set_eq_and_s32_u64, set_eq_or_s32_u64, set_eq_xor_s32_u64,
        set_eq_s32_s16, set_eq_and_s32_s16, set_eq_or_s32_s16, set_eq_xor_s32_s16, set_eq_s32_s32, set_eq_and_s32_s32, set_eq_or_s32_s32, set_eq_xor_s32_s32, set_eq_s32_s64, set_eq_and_s32_s64, set_eq_or_s32_s64, set_eq_xor_s32_s64,
        set_eq_s32_f32, set_eq_and_s32_f32, set_eq_or_s32_f32, set_eq_xor_s32_f32, set_eq_s32_f64, set_eq_and_s32_f64, set_eq_or_s32_f64, set_eq_xor_s32_f64,
        set_ne_s32_b16, set_ne_and_s32_b16, set_ne_or_s32_b16, set_ne_xor_s32_b16, set_ne_s32_b32, set_ne_and_s32_b32, set_ne_or_s32_b32, set_ne_xor_s32_b32, set_ne_s32_b64, set_ne_and_s32_b64, set_ne_or_s32_b64, set_ne_xor_s32_b64,
        set_ne_s32_u16, set_ne_and_s32_u16, set_ne_or_s32_u16, set_ne_xor_s32_u16, set_ne_s32_u32, set_ne_and_s32_u32, set_ne_or_s32_u32, set_ne_xor_s32_u32, set_ne_s32_u64, set_ne_and_s32_u64, set_ne_or_s32_u64, set_ne_xor_s32_u64,
        set_ne_s32_s16, set_ne_and_s32_s16, set_ne_or_s32_s16, set_ne_xor_s32_s16, set_ne_s32_s32, set_ne_and_s32_s32, set_ne_or_s32_s32, set_ne_xor_s32_s32, set_ne_s32_s64, set_ne_and_s32_s64, set_ne_or_s32_s64, set_ne_xor_s32_s64,
        set_ne_s32_f32, set_ne_and_s32_f32, set_ne_or_s32_f32, set_ne_xor_s32_f32, set_ne_s32_f64, set_ne_and_s32_f64, set_ne_or_s32_f64, set_ne_xor_s32_f64,
        set_lt_s32_u16, set_lt_and_s32_u16, set_lt_or_s32_u16, set_lt_xor_s32_u16, set_lt_s32_u32, set_lt_and_s32_u32, set_lt_or_s32_u32, set_lt_xor_s32_u32, set_lt_s32_u64, set_lt_and_s32_u64, set_lt_or_s32_u64, set_lt_xor_s32_u64,
        set_lt_s32_s16, set_lt_and_s32_s16, set_lt_or_s32_s16, set_lt_xor_s32_s16, set_lt_s32_s32, set_lt_and_s32_s32, set_lt_or_s32_s32, set_lt_xor_s32_s32, set_lt_s32_s64, set_lt_and_s32_s64, set_lt_or_s32_s64, set_lt_xor_s32_s64,
        set_lt_s32_f32, set_lt_and_s32_f32, set_lt_or_s32_f32, set_lt_xor_s32_f32, set_lt_s32_f64, set_lt_and_s32_f64, set_lt_or_s32_f64, set_lt_xor_s32_f64,
        set_le_s32_u16, set_le_and_s32_u16, set_le_or_s32_u16, set_le_xor_s32_u16, set_le_s32_u32, set_le_and_s32_u32, set_le_or_s32_u32, set_le_xor_s32_u32, set_le_s32_u64, set_le_and_s32_u64, set_le_or_s32_u64, set_le_xor_s32_u64,
        set_le_s32_s16, set_le_and_s32_s16, set_le_or_s32_s16, set_le_xor_s32_s16, set_le_s32_s32, set_le_and_s32_s32, set_le_or_s32_s32, set_le_xor_s32_s32, set_le_s32_s64, set_le_and_s32_s64, set_le_or_s32_s64, set_le_xor_s32_s64,
        set_le_s32_f32, set_le_and_s32_f32, set_le_or_s32_f32, set_le_xor_s32_f32, set_le_s32_f64, set_le_and_s32_f64, set_le_or_s32_f64, set_le_xor_s32_f64,
        set_gt_s32_u16, set_gt_and_s32_u16, set_gt_or_s32_u16, set_gt_xor_s32_u16, set_gt_s32_u32, set_gt_and_s32_u32, set_gt_or_s32_u32, set_gt_xor_s32_u32, set_gt_s32_u64, set_gt_and_s32_u64, set_gt_or_s32_u64, set_gt_xor_s32_u64,
        set_gt_s32_s16, set_gt_and_s32_s16, set_gt_or_s32_s16, set_gt_xor_s32_s16, set_gt_s32_s32, set_gt_and_s32_s32, set_gt_or_s32_s32, set_gt_xor_s32_s32, set_gt_s32_s64, set_gt_and_s32_s64, set_gt_or_s32_s64, set_gt_xor_s32_s64,
        set_gt_s32_f32, set_gt_and_s32_f32, set_gt_or_s32_f32, set_gt_xor_s32_f32, set_gt_s32_f64, set_gt_and_s32_f64, set_gt_or_s32_f64, set_gt_xor_s32_f64,
        set_ge_s32_u16, set_ge_and_s32_u16, set_ge_or_s32_u16, set_ge_xor_s32_u16, set_ge_s32_u32, set_ge_and_s32_u32, set_ge_or_s32_u32, set_ge_xor_s32_u32, set_ge_s32_u64, set_ge_and_s32_u64, set_ge_or_s32_u64, set_ge_xor_s32_u64,
        set_ge_s32_s16, set_ge_and_s32_s16, set_ge_or_s32_s16, set_ge_xor_s32_s16, set_ge_s32_s32, set_ge_and_s32_s32, set_ge_or_s32_s32, set_ge_xor_s32_s32, set_ge_s32_s64, set_ge_and_s32_s64, set_ge_or_s32_s64, set_ge_xor_s32_s64,
        set_ge_s32_f32, set_ge_and_s32_f32, set_ge_or_s32_f32, set_ge_xor_s32_f32, set_ge_s32_f64, set_ge_and_s32_f64, set_ge_or_s32_f64, set_ge_xor_s32_f64,
        //
        set_eq_f32_b16, set_eq_and_f32_b16, set_eq_or_f32_b16, set_eq_xor_f32_b16, set_eq_f32_b32, set_eq_and_f32_b32, set_eq_or_f32_b32, set_eq_xor_f32_b32, set_eq_f32_b64, set_eq_and_f32_b64, set_eq_or_f32_b64, set_eq_xor_f32_b64,
        set_eq_f32_u16, set_eq_and_f32_u16, set_eq_or_f32_u16, set_eq_xor_f32_u16, set_eq_f32_u32, set_eq_and_f32_u32, set_eq_or_f32_u32, set_eq_xor_f32_u32, set_eq_f32_u64, set_eq_and_f32_u64, set_eq_or_f32_u64, set_eq_xor_f32_u64,
        set_eq_f32_s16, set_eq_and_f32_s16, set_eq_or_f32_s16, set_eq_xor_f32_s16, set_eq_f32_s32, set_eq_and_f32_s32, set_eq_or_f32_s32, set_eq_xor_f32_s32, set_eq_f32_s64, set_eq_and_f32_s64, set_eq_or_f32_s64, set_eq_xor_f32_s64,
        set_eq_f32_f32, set_eq_and_f32_f32, set_eq_or_f32_f32, set_eq_xor_f32_f32, set_eq_f32_f64, set_eq_and_f32_f64, set_eq_or_f32_f64, set_eq_xor_f32_f64,
        set_ne_f32_b16, set_ne_and_f32_b16, set_ne_or_f32_b16, set_ne_xor_f32_b16, set_ne_f32_b32, set_ne_and_f32_b32, set_ne_or_f32_b32, set_ne_xor_f32_b32, set_ne_f32_b64, set_ne_and_f32_b64, set_ne_or_f32_b64, set_ne_xor_f32_b64,
        set_ne_f32_u16, set_ne_and_f32_u16, set_ne_or_f32_u16, set_ne_xor_f32_u16, set_ne_f32_u32, set_ne_and_f32_u32, set_ne_or_f32_u32, set_ne_xor_f32_u32, set_ne_f32_u64, set_ne_and_f32_u64, set_ne_or_f32_u64, set_ne_xor_f32_u64,
        set_ne_f32_s16, set_ne_and_f32_s16, set_ne_or_f32_s16, set_ne_xor_f32_s16, set_ne_f32_s32, set_ne_and_f32_s32, set_ne_or_f32_s32, set_ne_xor_f32_s32, set_ne_f32_s64, set_ne_and_f32_s64, set_ne_or_f32_s64, set_ne_xor_f32_s64,
        set_ne_f32_f32, set_ne_and_f32_f32, set_ne_or_f32_f32, set_ne_xor_f32_f32, set_ne_f32_f64, set_ne_and_f32_f64, set_ne_or_f32_f64, set_ne_xor_f32_f64,
        set_lt_f32_u16, set_lt_and_f32_u16, set_lt_or_f32_u16, set_lt_xor_f32_u16, set_lt_f32_u32, set_lt_and_f32_u32, set_lt_or_f32_u32, set_lt_xor_f32_u32, set_lt_f32_u64, set_lt_and_f32_u64, set_lt_or_f32_u64, set_lt_xor_f32_u64,
        set_lt_f32_s16, set_lt_and_f32_s16, set_lt_or_f32_s16, set_lt_xor_f32_s16, set_lt_f32_s32, set_lt_and_f32_s32, set_lt_or_f32_s32, set_lt_xor_f32_s32, set_lt_f32_s64, set_lt_and_f32_s64, set_lt_or_f32_s64, set_lt_xor_f32_s64,
        set_lt_f32_f32, set_lt_and_f32_f32, set_lt_or_f32_f32, set_lt_xor_f32_f32, set_lt_f32_f64, set_lt_and_f32_f64, set_lt_or_f32_f64, set_lt_xor_f32_f64,
        set_le_f32_u16, set_le_and_f32_u16, set_le_or_f32_u16, set_le_xor_f32_u16, set_le_f32_u32, set_le_and_f32_u32, set_le_or_f32_u32, set_le_xor_f32_u32, set_le_f32_u64, set_le_and_f32_u64, set_le_or_f32_u64, set_le_xor_f32_u64,
        set_le_f32_s16, set_le_and_f32_s16, set_le_or_f32_s16, set_le_xor_f32_s16, set_le_f32_s32, set_le_and_f32_s32, set_le_or_f32_s32, set_le_xor_f32_s32, set_le_f32_s64, set_le_and_f32_s64, set_le_or_f32_s64, set_le_xor_f32_s64,
        set_le_f32_f32, set_le_and_f32_f32, set_le_or_f32_f32, set_le_xor_f32_f32, set_le_f32_f64, set_le_and_f32_f64, set_le_or_f32_f64, set_le_xor_f32_f64,
        set_gt_f32_u16, set_gt_and_f32_u16, set_gt_or_f32_u16, set_gt_xor_f32_u16, set_gt_f32_u32, set_gt_and_f32_u32, set_gt_or_f32_u32, set_gt_xor_f32_u32, set_gt_f32_u64, set_gt_and_f32_u64, set_gt_or_f32_u64, set_gt_xor_f32_u64,
        set_gt_f32_s16, set_gt_and_f32_s16, set_gt_or_f32_s16, set_gt_xor_f32_s16, set_gt_f32_s32, set_gt_and_f32_s32, set_gt_or_f32_s32, set_gt_xor_f32_s32, set_gt_f32_s64, set_gt_and_f32_s64, set_gt_or_f32_s64, set_gt_xor_f32_s64,
        set_gt_f32_f32, set_gt_and_f32_f32, set_gt_or_f32_f32, set_gt_xor_f32_f32, set_gt_f32_f64, set_gt_and_f32_f64, set_gt_or_f32_f64, set_gt_xor_f32_f64,
        set_ge_f32_u16, set_ge_and_f32_u16, set_ge_or_f32_u16, set_ge_xor_f32_u16, set_ge_f32_u32, set_ge_and_f32_u32, set_ge_or_f32_u32, set_ge_xor_f32_u32, set_ge_f32_u64, set_ge_and_f32_u64, set_ge_or_f32_u64, set_ge_xor_f32_u64,
        set_ge_f32_s16, set_ge_and_f32_s16, set_ge_or_f32_s16, set_ge_xor_f32_s16, set_ge_f32_s32, set_ge_and_f32_s32, set_ge_or_f32_s32, set_ge_xor_f32_s32, set_ge_f32_s64, set_ge_and_f32_s64, set_ge_or_f32_s64, set_ge_xor_f32_s64,
        set_ge_f32_f32, set_ge_and_f32_f32, set_ge_or_f32_f32, set_ge_xor_f32_f32, set_ge_f32_f64, set_ge_and_f32_f64, set_ge_or_f32_f64, set_ge_xor_f32_f64,
        // {i}
        set_lo_b32_u16, set_lo_and_b32_u16, set_lo_or_b32_u16, set_lo_xor_b32_u16, set_lo_b32_u32, set_lo_and_b32_u32, set_lo_or_b32_u32, set_lo_xor_b32_u32, set_lo_b32_u64, set_lo_and_b32_u64, set_lo_or_b32_u64, set_lo_xor_b32_u64,
        set_lo_b32_s16, set_lo_and_b32_s16, set_lo_or_b32_s16, set_lo_xor_b32_s16, set_lo_b32_s32, set_lo_and_b32_s32, set_lo_or_b32_s32, set_lo_xor_b32_s32, set_lo_b32_s64, set_lo_and_b32_s64, set_lo_or_b32_s64, set_lo_xor_b32_s64,
        set_ls_b32_u16, set_ls_and_b32_u16, set_ls_or_b32_u16, set_ls_xor_b32_u16, set_ls_b32_u32, set_ls_and_b32_u32, set_ls_or_b32_u32, set_ls_xor_b32_u32, set_ls_b32_u64, set_ls_and_b32_u64, set_ls_or_b32_u64, set_ls_xor_b32_u64,
        set_ls_b32_s16, set_ls_and_b32_s16, set_ls_or_b32_s16, set_ls_xor_b32_s16, set_ls_b32_s32, set_ls_and_b32_s32, set_ls_or_b32_s32, set_ls_xor_b32_s32, set_ls_b32_s64, set_ls_and_b32_s64, set_ls_or_b32_s64, set_ls_xor_b32_s64,
        set_hi_b32_u16, set_hi_and_b32_u16, set_hi_or_b32_u16, set_hi_xor_b32_u16, set_hi_b32_u32, set_hi_and_b32_u32, set_hi_or_b32_u32, set_hi_xor_b32_u32, set_hi_b32_u64, set_hi_and_b32_u64, set_hi_or_b32_u64, set_hi_xor_b32_u64,
        set_hi_b32_s16, set_hi_and_b32_s16, set_hi_or_b32_s16, set_hi_xor_b32_s16, set_hi_b32_s32, set_hi_and_b32_s32, set_hi_or_b32_s32, set_hi_xor_b32_s32, set_hi_b32_s64, set_hi_and_b32_s64, set_hi_or_b32_s64, set_hi_xor_b32_s64,
        set_hs_b32_u16, set_hs_and_b32_u16, set_hs_or_b32_u16, set_hs_xor_b32_u16, set_hs_b32_u32, set_hs_and_b32_u32, set_hs_or_b32_u32, set_hs_xor_b32_u32, set_hs_b32_u64, set_hs_and_b32_u64, set_hs_or_b32_u64, set_hs_xor_b32_u64,
        set_hs_b32_s16, set_hs_and_b32_s16, set_hs_or_b32_s16, set_hs_xor_b32_s16, set_hs_b32_s32, set_hs_and_b32_s32, set_hs_or_b32_s32, set_hs_xor_b32_s32, set_hs_b32_s64, set_hs_and_b32_s64, set_hs_or_b32_s64, set_hs_xor_b32_s64,
        //
        set_lo_u32_u16, set_lo_and_u32_u16, set_lo_or_u32_u16, set_lo_xor_u32_u16, set_lo_u32_u32, set_lo_and_u32_u32, set_lo_or_u32_u32, set_lo_xor_u32_u32, set_lo_u32_u64, set_lo_and_u32_u64, set_lo_or_u32_u64, set_lo_xor_u32_u64,
        set_lo_u32_s16, set_lo_and_u32_s16, set_lo_or_u32_s16, set_lo_xor_u32_s16, set_lo_u32_s32, set_lo_and_u32_s32, set_lo_or_u32_s32, set_lo_xor_u32_s32, set_lo_u32_s64, set_lo_and_u32_s64, set_lo_or_u32_s64, set_lo_xor_u32_s64,
        set_ls_u32_u16, set_ls_and_u32_u16, set_ls_or_u32_u16, set_ls_xor_u32_u16, set_ls_u32_u32, set_ls_and_u32_u32, set_ls_or_u32_u32, set_ls_xor_u32_u32, set_ls_u32_u64, set_ls_and_u32_u64, set_ls_or_u32_u64, set_ls_xor_u32_u64,
        set_ls_u32_s16, set_ls_and_u32_s16, set_ls_or_u32_s16, set_ls_xor_u32_s16, set_ls_u32_s32, set_ls_and_u32_s32, set_ls_or_u32_s32, set_ls_xor_u32_s32, set_ls_u32_s64, set_ls_and_u32_s64, set_ls_or_u32_s64, set_ls_xor_u32_s64,
        set_hi_u32_u16, set_hi_and_u32_u16, set_hi_or_u32_u16, set_hi_xor_u32_u16, set_hi_u32_u32, set_hi_and_u32_u32, set_hi_or_u32_u32, set_hi_xor_u32_u32, set_hi_u32_u64, set_hi_and_u32_u64, set_hi_or_u32_u64, set_hi_xor_u32_u64,
        set_hi_u32_s16, set_hi_and_u32_s16, set_hi_or_u32_s16, set_hi_xor_u32_s16, set_hi_u32_s32, set_hi_and_u32_s32, set_hi_or_u32_s32, set_hi_xor_u32_s32, set_hi_u32_s64, set_hi_and_u32_s64, set_hi_or_u32_s64, set_hi_xor_u32_s64,
        set_hs_u32_u16, set_hs_and_u32_u16, set_hs_or_u32_u16, set_hs_xor_u32_u16, set_hs_u32_u32, set_hs_and_u32_u32, set_hs_or_u32_u32, set_hs_xor_u32_u32, set_hs_u32_u64, set_hs_and_u32_u64, set_hs_or_u32_u64, set_hs_xor_u32_u64,
        set_hs_u32_s16, set_hs_and_u32_s16, set_hs_or_u32_s16, set_hs_xor_u32_s16, set_hs_u32_s32, set_hs_and_u32_s32, set_hs_or_u32_s32, set_hs_xor_u32_s32, set_hs_u32_s64, set_hs_and_u32_s64, set_hs_or_u32_s64, set_hs_xor_u32_s64,
        //
        set_lo_s32_u16, set_lo_and_s32_u16, set_lo_or_s32_u16, set_lo_xor_s32_u16, set_lo_s32_u32, set_lo_and_s32_u32, set_lo_or_s32_u32, set_lo_xor_s32_u32, set_lo_s32_u64, set_lo_and_s32_u64, set_lo_or_s32_u64, set_lo_xor_s32_u64,
        set_lo_s32_s16, set_lo_and_s32_s16, set_lo_or_s32_s16, set_lo_xor_s32_s16, set_lo_s32_s32, set_lo_and_s32_s32, set_lo_or_s32_s32, set_lo_xor_s32_s32, set_lo_s32_s64, set_lo_and_s32_s64, set_lo_or_s32_s64, set_lo_xor_s32_s64,
        set_ls_s32_u16, set_ls_and_s32_u16, set_ls_or_s32_u16, set_ls_xor_s32_u16, set_ls_s32_u32, set_ls_and_s32_u32, set_ls_or_s32_u32, set_ls_xor_s32_u32, set_ls_s32_u64, set_ls_and_s32_u64, set_ls_or_s32_u64, set_ls_xor_s32_u64,
        set_ls_s32_s16, set_ls_and_s32_s16, set_ls_or_s32_s16, set_ls_xor_s32_s16, set_ls_s32_s32, set_ls_and_s32_s32, set_ls_or_s32_s32, set_ls_xor_s32_s32, set_ls_s32_s64, set_ls_and_s32_s64, set_ls_or_s32_s64, set_ls_xor_s32_s64,
        set_hi_s32_u16, set_hi_and_s32_u16, set_hi_or_s32_u16, set_hi_xor_s32_u16, set_hi_s32_u32, set_hi_and_s32_u32, set_hi_or_s32_u32, set_hi_xor_s32_u32, set_hi_s32_u64, set_hi_and_s32_u64, set_hi_or_s32_u64, set_hi_xor_s32_u64,
        set_hi_s32_s16, set_hi_and_s32_s16, set_hi_or_s32_s16, set_hi_xor_s32_s16, set_hi_s32_s32, set_hi_and_s32_s32, set_hi_or_s32_s32, set_hi_xor_s32_s32, set_hi_s32_s64, set_hi_and_s32_s64, set_hi_or_s32_s64, set_hi_xor_s32_s64,
        set_hs_s32_u16, set_hs_and_s32_u16, set_hs_or_s32_u16, set_hs_xor_s32_u16, set_hs_s32_u32, set_hs_and_s32_u32, set_hs_or_s32_u32, set_hs_xor_s32_u32, set_hs_s32_u64, set_hs_and_s32_u64, set_hs_or_s32_u64, set_hs_xor_s32_u64,
        set_hs_s32_s16, set_hs_and_s32_s16, set_hs_or_s32_s16, set_hs_xor_s32_s16, set_hs_s32_s32, set_hs_and_s32_s32, set_hs_or_s32_s32, set_hs_xor_s32_s32, set_hs_s32_s64, set_hs_and_s32_s64, set_hs_or_s32_s64, set_hs_xor_s32_s64,
        //
        set_lo_f32_u16, set_lo_and_f32_u16, set_lo_or_f32_u16, set_lo_xor_f32_u16, set_lo_f32_u32, set_lo_and_f32_u32, set_lo_or_f32_u32, set_lo_xor_f32_u32, set_lo_f32_u64, set_lo_and_f32_u64, set_lo_or_f32_u64, set_lo_xor_f32_u64,
        set_lo_f32_s16, set_lo_and_f32_s16, set_lo_or_f32_s16, set_lo_xor_f32_s16, set_lo_f32_s32, set_lo_and_f32_s32, set_lo_or_f32_s32, set_lo_xor_f32_s32, set_lo_f32_s64, set_lo_and_f32_s64, set_lo_or_f32_s64, set_lo_xor_f32_s64,
        set_ls_f32_u16, set_ls_and_f32_u16, set_ls_or_f32_u16, set_ls_xor_f32_u16, set_ls_f32_u32, set_ls_and_f32_u32, set_ls_or_f32_u32, set_ls_xor_f32_u32, set_ls_f32_u64, set_ls_and_f32_u64, set_ls_or_f32_u64, set_ls_xor_f32_u64,
        set_ls_f32_s16, set_ls_and_f32_s16, set_ls_or_f32_s16, set_ls_xor_f32_s16, set_ls_f32_s32, set_ls_and_f32_s32, set_ls_or_f32_s32, set_ls_xor_f32_s32, set_ls_f32_s64, set_ls_and_f32_s64, set_ls_or_f32_s64, set_ls_xor_f32_s64,
        set_hi_f32_u16, set_hi_and_f32_u16, set_hi_or_f32_u16, set_hi_xor_f32_u16, set_hi_f32_u32, set_hi_and_f32_u32, set_hi_or_f32_u32, set_hi_xor_f32_u32, set_hi_f32_u64, set_hi_and_f32_u64, set_hi_or_f32_u64, set_hi_xor_f32_u64,
        set_hi_f32_s16, set_hi_and_f32_s16, set_hi_or_f32_s16, set_hi_xor_f32_s16, set_hi_f32_s32, set_hi_and_f32_s32, set_hi_or_f32_s32, set_hi_xor_f32_s32, set_hi_f32_s64, set_hi_and_f32_s64, set_hi_or_f32_s64, set_hi_xor_f32_s64,
        set_hs_f32_u16, set_hs_and_f32_u16, set_hs_or_f32_u16, set_hs_xor_f32_u16, set_hs_f32_u32, set_hs_and_f32_u32, set_hs_or_f32_u32, set_hs_xor_f32_u32, set_hs_f32_u64, set_hs_and_f32_u64, set_hs_or_f32_u64, set_hs_xor_f32_u64,
        set_hs_f32_s16, set_hs_and_f32_s16, set_hs_or_f32_s16, set_hs_xor_f32_s16, set_hs_f32_s32, set_hs_and_f32_s32, set_hs_or_f32_s32, set_hs_xor_f32_s32, set_hs_f32_s64, set_hs_and_f32_s64, set_hs_or_f32_s64, set_hs_xor_f32_s64,
        // {f}
        set_equ_b32_f32, set_equ_and_b32_f32, set_equ_or_b32_f32, set_equ_xor_b32_f32, set_equ_b32_f64, set_equ_and_b32_f64, set_equ_or_b32_f64, set_equ_xor_b32_f64,
        set_neu_b32_f32, set_neu_and_b32_f32, set_neu_or_b32_f32, set_neu_xor_b32_f32, set_neu_b32_f64, set_neu_and_b32_f64, set_neu_or_b32_f64, set_neu_xor_b32_f64,
        set_ltu_b32_f32, set_ltu_and_b32_f32, set_ltu_or_b32_f32, set_ltu_xor_b32_f32, set_ltu_b32_f64, set_ltu_and_b32_f64, set_ltu_or_b32_f64, set_ltu_xor_b32_f64,
        set_hsu_b32_f32, set_hsu_and_b32_f32, set_hsu_or_b32_f32, set_hsu_xor_b32_f32, set_hsu_b32_f64, set_hsu_and_b32_f64, set_hsu_or_b32_f64, set_hsu_xor_b32_f64,
        set_gtu_b32_f32, set_gtu_and_b32_f32, set_gtu_or_b32_f32, set_gtu_xor_b32_f32, set_gtu_b32_f64, set_gtu_and_b32_f64, set_gtu_or_b32_f64, set_gtu_xor_b32_f64,
        set_geu_b32_f32, set_geu_and_b32_f32, set_geu_or_b32_f32, set_geu_xor_b32_f32, set_geu_b32_f64, set_geu_and_b32_f64, set_geu_or_b32_f64, set_geu_xor_b32_f64,
        set_num_b32_f32, set_num_and_b32_f32, set_num_or_b32_f32, set_num_xor_b32_f32, set_num_b32_f64, set_num_and_b32_f64, set_num_or_b32_f64, set_num_xor_b32_f64,
        set_nan_b32_f32, set_nan_and_b32_f32, set_nan_or_b32_f32, set_nan_xor_b32_f32, set_nan_b32_f64, set_nan_and_b32_f64, set_nan_or_b32_f64, set_nan_xor_b32_f64,
        //
        set_equ_u32_f32, set_equ_and_u32_f32, set_equ_or_u32_f32, set_equ_xor_u32_f32, set_equ_u32_f64, set_equ_and_u32_f64, set_equ_or_u32_f64, set_equ_xor_u32_f64,
        set_neu_u32_f32, set_neu_and_u32_f32, set_neu_or_u32_f32, set_neu_xor_u32_f32, set_neu_u32_f64, set_neu_and_u32_f64, set_neu_or_u32_f64, set_neu_xor_u32_f64,
        set_ltu_u32_f32, set_ltu_and_u32_f32, set_ltu_or_u32_f32, set_ltu_xor_u32_f32, set_ltu_u32_f64, set_ltu_and_u32_f64, set_ltu_or_u32_f64, set_ltu_xor_u32_f64,
        set_hsu_u32_f32, set_hsu_and_u32_f32, set_hsu_or_u32_f32, set_hsu_xor_u32_f32, set_hsu_u32_f64, set_hsu_and_u32_f64, set_hsu_or_u32_f64, set_hsu_xor_u32_f64,
        set_gtu_u32_f32, set_gtu_and_u32_f32, set_gtu_or_u32_f32, set_gtu_xor_u32_f32, set_gtu_u32_f64, set_gtu_and_u32_f64, set_gtu_or_u32_f64, set_gtu_xor_u32_f64,
        set_geu_u32_f32, set_geu_and_u32_f32, set_geu_or_u32_f32, set_geu_xor_u32_f32, set_geu_u32_f64, set_geu_and_u32_f64, set_geu_or_u32_f64, set_geu_xor_u32_f64,
        set_num_u32_f32, set_num_and_u32_f32, set_num_or_u32_f32, set_num_xor_u32_f32, set_num_u32_f64, set_num_and_u32_f64, set_num_or_u32_f64, set_num_xor_u32_f64,
        set_nan_u32_f32, set_nan_and_u32_f32, set_nan_or_u32_f32, set_nan_xor_u32_f32, set_nan_u32_f64, set_nan_and_u32_f64, set_nan_or_u32_f64, set_nan_xor_u32_f64,
        //
        set_equ_s32_f32, set_equ_and_s32_f32, set_equ_or_s32_f32, set_equ_xor_s32_f32, set_equ_s32_f64, set_equ_and_s32_f64, set_equ_or_s32_f64, set_equ_xor_s32_f64,
        set_neu_s32_f32, set_neu_and_s32_f32, set_neu_or_s32_f32, set_neu_xor_s32_f32, set_neu_s32_f64, set_neu_and_s32_f64, set_neu_or_s32_f64, set_neu_xor_s32_f64,
        set_ltu_s32_f32, set_ltu_and_s32_f32, set_ltu_or_s32_f32, set_ltu_xor_s32_f32, set_ltu_s32_f64, set_ltu_and_s32_f64, set_ltu_or_s32_f64, set_ltu_xor_s32_f64,
        set_hsu_s32_f32, set_hsu_and_s32_f32, set_hsu_or_s32_f32, set_hsu_xor_s32_f32, set_hsu_s32_f64, set_hsu_and_s32_f64, set_hsu_or_s32_f64, set_hsu_xor_s32_f64,
        set_gtu_s32_f32, set_gtu_and_s32_f32, set_gtu_or_s32_f32, set_gtu_xor_s32_f32, set_gtu_s32_f64, set_gtu_and_s32_f64, set_gtu_or_s32_f64, set_gtu_xor_s32_f64,
        set_geu_s32_f32, set_geu_and_s32_f32, set_geu_or_s32_f32, set_geu_xor_s32_f32, set_geu_s32_f64, set_geu_and_s32_f64, set_geu_or_s32_f64, set_geu_xor_s32_f64,
        set_num_s32_f32, set_num_and_s32_f32, set_num_or_s32_f32, set_num_xor_s32_f32, set_num_s32_f64, set_num_and_s32_f64, set_num_or_s32_f64, set_num_xor_s32_f64,
        set_nan_s32_f32, set_nan_and_s32_f32, set_nan_or_s32_f32, set_nan_xor_s32_f32, set_nan_s32_f64, set_nan_and_s32_f64, set_nan_or_s32_f64, set_nan_xor_s32_f64,
        //
        set_equ_f32_f32, set_equ_and_f32_f32, set_equ_or_f32_f32, set_equ_xor_f32_f32, set_equ_f32_f64, set_equ_and_f32_f64, set_equ_or_f32_f64, set_equ_xor_f32_f64,
        set_neu_f32_f32, set_neu_and_f32_f32, set_neu_or_f32_f32, set_neu_xor_f32_f32, set_neu_f32_f64, set_neu_and_f32_f64, set_neu_or_f32_f64, set_neu_xor_f32_f64,
        set_ltu_f32_f32, set_ltu_and_f32_f32, set_ltu_or_f32_f32, set_ltu_xor_f32_f32, set_ltu_f32_f64, set_ltu_and_f32_f64, set_ltu_or_f32_f64, set_ltu_xor_f32_f64,
        set_hsu_f32_f32, set_hsu_and_f32_f32, set_hsu_or_f32_f32, set_hsu_xor_f32_f32, set_hsu_f32_f64, set_hsu_and_f32_f64, set_hsu_or_f32_f64, set_hsu_xor_f32_f64,
        set_gtu_f32_f32, set_gtu_and_f32_f32, set_gtu_or_f32_f32, set_gtu_xor_f32_f32, set_gtu_f32_f64, set_gtu_and_f32_f64, set_gtu_or_f32_f64, set_gtu_xor_f32_f64,
        set_geu_f32_f32, set_geu_and_f32_f32, set_geu_or_f32_f32, set_geu_xor_f32_f32, set_geu_f32_f64, set_geu_and_f32_f64, set_geu_or_f32_f64, set_geu_xor_f32_f64,
        set_num_f32_f32, set_num_and_f32_f32, set_num_or_f32_f32, set_num_xor_f32_f32, set_num_f32_f64, set_num_and_f32_f64, set_num_or_f32_f64, set_num_xor_f32_f64,
        set_nan_f32_f32, set_nan_and_f32_f32, set_nan_or_f32_f32, set_nan_xor_f32_f32, set_nan_f32_f64, set_nan_and_f32_f64, set_nan_or_f32_f64, set_nan_xor_f32_f64,

        // setp
        setp_eq_b16, setp_eq_and_b16, setp_eq_or_b16, setp_eq_xor_b16, setp_eq_b32, setp_eq_and_b32, setp_eq_or_b32, setp_eq_xor_b32, setp_eq_b64, setp_eq_and_b64, setp_eq_or_b64, setp_eq_xor_b64,
        setp_eq_u16, setp_eq_and_u16, setp_eq_or_u16, setp_eq_xor_u16, setp_eq_u32, setp_eq_and_u32, setp_eq_or_u32, setp_eq_xor_u32, setp_eq_u64, setp_eq_and_u64, setp_eq_or_u64, setp_eq_xor_u64,
        setp_eq_s16, setp_eq_and_s16, setp_eq_or_s16, setp_eq_xor_s16, setp_eq_s32, setp_eq_and_s32, setp_eq_or_s32, setp_eq_xor_s32, setp_eq_s64, setp_eq_and_s64, setp_eq_or_s64, setp_eq_xor_s64,
        setp_eq_f32, setp_eq_and_f32, setp_eq_or_f32, setp_eq_xor_f32, setp_eq_f64, setp_eq_and_f64, setp_eq_or_f64, setp_eq_xor_f64,
        setp_ne_b16, setp_ne_and_b16, setp_ne_or_b16, setp_ne_xor_b16, setp_ne_b32, setp_ne_and_b32, setp_ne_or_b32, setp_ne_xor_b32, setp_ne_b64, setp_ne_and_b64, setp_ne_or_b64, setp_ne_xor_b64,
        setp_ne_u16, setp_ne_and_u16, setp_ne_or_u16, setp_ne_xor_u16, setp_ne_u32, setp_ne_and_u32, setp_ne_or_u32, setp_ne_xor_u32, setp_ne_u64, setp_ne_and_u64, setp_ne_or_u64, setp_ne_xor_u64,
        setp_ne_s16, setp_ne_and_s16, setp_ne_or_s16, setp_ne_xor_s16, setp_ne_s32, setp_ne_and_s32, setp_ne_or_s32, setp_ne_xor_s32, setp_ne_s64, setp_ne_and_s64, setp_ne_or_s64, setp_ne_xor_s64,
        setp_ne_f32, setp_ne_and_f32, setp_ne_or_f32, setp_ne_xor_f32, setp_ne_f64, setp_ne_and_f64, setp_ne_or_f64, setp_ne_xor_f64,
        setp_lt_u16, setp_lt_and_u16, setp_lt_or_u16, setp_lt_xor_u16, setp_lt_u32, setp_lt_and_u32, setp_lt_or_u32, setp_lt_xor_u32, setp_lt_u64, setp_lt_and_u64, setp_lt_or_u64, setp_lt_xor_u64,
        setp_lt_s16, setp_lt_and_s16, setp_lt_or_s16, setp_lt_xor_s16, setp_lt_s32, setp_lt_and_s32, setp_lt_or_s32, setp_lt_xor_s32, setp_lt_s64, setp_lt_and_s64, setp_lt_or_s64, setp_lt_xor_s64,
        setp_lt_f32, setp_lt_and_f32, setp_lt_or_f32, setp_lt_xor_f32, setp_lt_f64, setp_lt_and_f64, setp_lt_or_f64, setp_lt_xor_f64,
        setp_le_u16, setp_le_and_u16, setp_le_or_u16, setp_le_xor_u16, setp_le_u32, setp_le_and_u32, setp_le_or_u32, setp_le_xor_u32, setp_le_u64, setp_le_and_u64, setp_le_or_u64, setp_le_xor_u64,
        setp_le_s16, setp_le_and_s16, setp_le_or_s16, setp_le_xor_s16, setp_le_s32, setp_le_and_s32, setp_le_or_s32, setp_le_xor_s32, setp_le_s64, setp_le_and_s64, setp_le_or_s64, setp_le_xor_s64,
        setp_le_f32, setp_le_and_f32, setp_le_or_f32, setp_le_xor_f32, setp_le_f64, setp_le_and_f64, setp_le_or_f64, setp_le_xor_f64,
        setp_gt_u16, setp_gt_and_u16, setp_gt_or_u16, setp_gt_xor_u16, setp_gt_u32, setp_gt_and_u32, setp_gt_or_u32, setp_gt_xor_u32, setp_gt_u64, setp_gt_and_u64, setp_gt_or_u64, setp_gt_xor_u64,
        setp_gt_s16, setp_gt_and_s16, setp_gt_or_s16, setp_gt_xor_s16, setp_gt_s32, setp_gt_and_s32, setp_gt_or_s32, setp_gt_xor_s32, setp_gt_s64, setp_gt_and_s64, setp_gt_or_s64, setp_gt_xor_s64,
        setp_gt_f32, setp_gt_and_f32, setp_gt_or_f32, setp_gt_xor_f32, setp_gt_f64, setp_gt_and_f64, setp_gt_or_f64, setp_gt_xor_f64,
        setp_ge_u16, setp_ge_and_u16, setp_ge_or_u16, setp_ge_xor_u16, setp_ge_u32, setp_ge_and_u32, setp_ge_or_u32, setp_ge_xor_u32, setp_ge_u64, setp_ge_and_u64, setp_ge_or_u64, setp_ge_xor_u64,
        setp_ge_s16, setp_ge_and_s16, setp_ge_or_s16, setp_ge_xor_s16, setp_ge_s32, setp_ge_and_s32, setp_ge_or_s32, setp_ge_xor_s32, setp_ge_s64, setp_ge_and_s64, setp_ge_or_s64, setp_ge_xor_s64,
        setp_ge_f32, setp_ge_and_f32, setp_ge_or_f32, setp_ge_xor_f32, setp_ge_f64, setp_ge_and_f64, setp_ge_or_f64, setp_ge_xor_f64,
        // {i}
        setp_lo_u16, setp_lo_and_u16, setp_lo_or_u16, setp_lo_xor_u16, setp_lo_u32, setp_lo_and_u32, setp_lo_or_u32, setp_lo_xor_u32, setp_lo_u64, setp_lo_and_u64, setp_lo_or_u64, setp_lo_xor_u64,
        setp_lo_s16, setp_lo_and_s16, setp_lo_or_s16, setp_lo_xor_s16, setp_lo_s32, setp_lo_and_s32, setp_lo_or_s32, setp_lo_xor_s32, setp_lo_s64, setp_lo_and_s64, setp_lo_or_s64, setp_lo_xor_s64,
        setp_ls_u16, setp_ls_and_u16, setp_ls_or_u16, setp_ls_xor_u16, setp_ls_u32, setp_ls_and_u32, setp_ls_or_u32, setp_ls_xor_u32, setp_ls_u64, setp_ls_and_u64, setp_ls_or_u64, setp_ls_xor_u64,
        setp_ls_s16, setp_ls_and_s16, setp_ls_or_s16, setp_ls_xor_s16, setp_ls_s32, setp_ls_and_s32, setp_ls_or_s32, setp_ls_xor_s32, setp_ls_s64, setp_ls_and_s64, setp_ls_or_s64, setp_ls_xor_s64,
        setp_hi_u16, setp_hi_and_u16, setp_hi_or_u16, setp_hi_xor_u16, setp_hi_u32, setp_hi_and_u32, setp_hi_or_u32, setp_hi_xor_u32, setp_hi_u64, setp_hi_and_u64, setp_hi_or_u64, setp_hi_xor_u64,
        setp_hi_s16, setp_hi_and_s16, setp_hi_or_s16, setp_hi_xor_s16, setp_hi_s32, setp_hi_and_s32, setp_hi_or_s32, setp_hi_xor_s32, setp_hi_s64, setp_hi_and_s64, setp_hi_or_s64, setp_hi_xor_s64,
        setp_hs_u16, setp_hs_and_u16, setp_hs_or_u16, setp_hs_xor_u16, setp_hs_u32, setp_hs_and_u32, setp_hs_or_u32, setp_hs_xor_u32, setp_hs_u64, setp_hs_and_u64, setp_hs_or_u64, setp_hs_xor_u64,
        setp_hs_s16, setp_hs_and_s16, setp_hs_or_s16, setp_hs_xor_s16, setp_hs_s32, setp_hs_and_s32, setp_hs_or_s32, setp_hs_xor_s32, setp_hs_s64, setp_hs_and_s64, setp_hs_or_s64, setp_hs_xor_s64,
        // {f}
        setp_equ_f32, setp_equ_and_f32, setp_equ_or_f32, setp_equ_xor_f32, setp_equ_f64, setp_equ_and_f64, setp_equ_or_f64, setp_equ_xor_f64,
        setp_neu_f32, setp_neu_and_f32, setp_neu_or_f32, setp_neu_xor_f32, setp_neu_f64, setp_neu_and_f64, setp_neu_or_f64, setp_neu_xor_f64,
        setp_ltu_f32, setp_ltu_and_f32, setp_ltu_or_f32, setp_ltu_xor_f32, setp_ltu_f64, setp_ltu_and_f64, setp_ltu_or_f64, setp_ltu_xor_f64,
        setp_leu_f32, setp_leu_and_f32, setp_leu_or_f32, setp_leu_xor_f32, setp_leu_f64, setp_leu_and_f64, setp_leu_or_f64, setp_leu_xor_f64,
        setp_gtu_f32, setp_gtu_and_f32, setp_gtu_or_f32, setp_gtu_xor_f32, setp_gtu_f64, setp_gtu_and_f64, setp_gtu_or_f64, setp_gtu_xor_f64,
        setp_geu_f32, setp_geu_and_f32, setp_geu_or_f32, setp_geu_xor_f32, setp_geu_f64, setp_geu_and_f64, setp_geu_or_f64, setp_geu_xor_f64,
        setp_num_f32, setp_num_and_f32, setp_num_or_f32, setp_num_xor_f32, setp_num_f64, setp_num_and_f64, setp_num_or_f64, setp_num_xor_f64,
        setp_nan_f32, setp_nan_and_f32, setp_nan_or_f32, setp_nan_xor_f32, setp_nan_f64, setp_nan_and_f64, setp_nan_or_f64, setp_nan_xor_f64,

        // selp
        selp_b16, selp_b32, selp_b64,
        selp_u16, selp_u32, selp_u64,
        selp_s16, selp_s32, selp_s64,
        selp_f32, selp_f64,

        // slct
        slct_b16_s32, slct_b32_s32, slct_b64_s32,
        slct_u16_s32, slct_u32_s32, slct_u64_s32,
        slct_s16_s32, slct_s32_s32, slct_s64_s32,
        slct_f32_s32, slct_f64_s32,
        //
        slct_b16_f32, slct_b32_f32, slct_b64_f32,
        slct_u16_f32, slct_u32_f32, slct_u64_f32,
        slct_s16_f32, slct_s32_f32, slct_s64_f32,
        slct_f32_f32, slct_f64_f32,

        #endregion

        #region Logical Shift Instructions

        // and
        and_pred, and_b16, and_b32, and_b64,

        // or
        or_pred, or_b16, or_b32, or_b64,

        // xor
        xor_pred, xor_b16, xor_b32, xor_b64,

        // not
        not_pred, not_b16, not_b32, not_b64,

        // cnot
        cnot_b16, cnot_b32, cnot_b64,

        // shl
        shl_b16, shl_b32, shl_b64,

        // shr
        shr_b16, shr_b32, shr_b64,
        shr_u16, shr_u32, shr_u64,
        shr_s16, shr_s32, shr_s64,

        #endregion

        #region Data Movement and Conversion Instructions

        // mov
        mov_pred,
        mov_b16, mov_b32, mov_b64,
        mov_u16, mov_u32, mov_u64,
        mov_s16, mov_s32, mov_s64,
        mov_f32, mov_f64,

        // ld
        ld_const_b8, ld_const_b16, ld_const_b32, ld_const_b64,
        ld_const_u8, ld_const_u16, ld_const_u32, ld_const_u64,
        ld_const_s8, ld_const_s16, ld_const_s32, ld_const_s64,
        ld_const_f32, ld_const_f64,
        ld_const_v2_b8, ld_const_v2_b16, ld_const_v2_b32, ld_const_v2_b64,
        ld_const_v2_u8, ld_const_v2_u16, ld_const_v2_u32, ld_const_v2_u64,
        ld_const_v2_s8, ld_const_v2_s16, ld_const_v2_s32, ld_const_v2_s64,
        ld_const_v2_f32, ld_const_v2_f64,
        ld_const_v4_b8, ld_const_v4_b16, ld_const_v4_b32, ld_const_v4_b64,
        ld_const_v4_u8, ld_const_v4_u16, ld_const_v4_u32, ld_const_v4_u64,
        ld_const_v4_s8, ld_const_v4_s16, ld_const_v4_s32, ld_const_v4_s64,
        ld_const_v4_f32, ld_const_v4_f64,
        ld_global_b8, ld_global_b16, ld_global_b32, ld_global_b64,
        ld_global_u8, ld_global_u16, ld_global_u32, ld_global_u64,
        ld_global_s8, ld_global_s16, ld_global_s32, ld_global_s64,
        ld_global_f32, ld_global_f64,
        ld_global_v2_b8, ld_global_v2_b16, ld_global_v2_b32, ld_global_v2_b64,
        ld_global_v2_u8, ld_global_v2_u16, ld_global_v2_u32, ld_global_v2_u64,
        ld_global_v2_s8, ld_global_v2_s16, ld_global_v2_s32, ld_global_v2_s64,
        ld_global_v2_f32, ld_global_v2_f64,
        ld_global_v4_b8, ld_global_v4_b16, ld_global_v4_b32, ld_global_v4_b64,
        ld_global_v4_u8, ld_global_v4_u16, ld_global_v4_u32, ld_global_v4_u64,
        ld_global_v4_s8, ld_global_v4_s16, ld_global_v4_s32, ld_global_v4_s64,
        ld_global_v4_f32, ld_global_v4_f64,
        ld_local_b8, ld_local_b16, ld_local_b32, ld_local_b64,
        ld_local_u8, ld_local_u16, ld_local_u32, ld_local_u64,
        ld_local_s8, ld_local_s16, ld_local_s32, ld_local_s64,
        ld_local_f32, ld_local_f64,
        ld_local_v2_b8, ld_local_v2_b16, ld_local_v2_b32, ld_local_v2_b64,
        ld_local_v2_u8, ld_local_v2_u16, ld_local_v2_u32, ld_local_v2_u64,
        ld_local_v2_s8, ld_local_v2_s16, ld_local_v2_s32, ld_local_v2_s64,
        ld_local_v2_f32, ld_local_v2_f64,
        ld_local_v4_b8, ld_local_v4_b16, ld_local_v4_b32, ld_local_v4_b64,
        ld_local_v4_u8, ld_local_v4_u16, ld_local_v4_u32, ld_local_v4_u64,
        ld_local_v4_s8, ld_local_v4_s16, ld_local_v4_s32, ld_local_v4_s64,
        ld_local_v4_f32, ld_local_v4_f64,
        ld_param_b8, ld_param_b16, ld_param_b32, ld_param_b64,
        ld_param_u8, ld_param_u16, ld_param_u32, ld_param_u64,
        ld_param_s8, ld_param_s16, ld_param_s32, ld_param_s64,
        ld_param_f32, ld_param_f64,
        ld_param_v2_b8, ld_param_v2_b16, ld_param_v2_b32, ld_param_v2_b64,
        ld_param_v2_u8, ld_param_v2_u16, ld_param_v2_u32, ld_param_v2_u64,
        ld_param_v2_s8, ld_param_v2_s16, ld_param_v2_s32, ld_param_v2_s64,
        ld_param_v2_f32, ld_param_v2_f64,
        ld_param_v4_b8, ld_param_v4_b16, ld_param_v4_b32, ld_param_v4_b64,
        ld_param_v4_u8, ld_param_v4_u16, ld_param_v4_u32, ld_param_v4_u64,
        ld_param_v4_s8, ld_param_v4_s16, ld_param_v4_s32, ld_param_v4_s64,
        ld_param_v4_f32, ld_param_v4_f64,
        ld_shared_b8, ld_shared_b16, ld_shared_b32, ld_shared_b64,
        ld_shared_u8, ld_shared_u16, ld_shared_u32, ld_shared_u64,
        ld_shared_s8, ld_shared_s16, ld_shared_s32, ld_shared_s64,
        ld_shared_f32, ld_shared_f64,
        ld_shared_v2_b8, ld_shared_v2_b16, ld_shared_v2_b32, ld_shared_v2_b64,
        ld_shared_v2_u8, ld_shared_v2_u16, ld_shared_v2_u32, ld_shared_v2_u64,
        ld_shared_v2_s8, ld_shared_v2_s16, ld_shared_v2_s32, ld_shared_v2_s64,
        ld_shared_v2_f32, ld_shared_v2_f64,
        ld_shared_v4_b8, ld_shared_v4_b16, ld_shared_v4_b32, ld_shared_v4_b64,
        ld_shared_v4_u8, ld_shared_v4_u16, ld_shared_v4_u32, ld_shared_v4_u64,
        ld_shared_v4_s8, ld_shared_v4_s16, ld_shared_v4_s32, ld_shared_v4_s64,
        ld_shared_v4_f32, ld_shared_v4_f64,
        // {volatile}
        ld_volatile_const_b8, ld_volatile_const_b16, ld_volatile_const_b32, ld_volatile_const_b64,
        ld_volatile_const_u8, ld_volatile_const_u16, ld_volatile_const_u32, ld_volatile_const_u64,
        ld_volatile_const_s8, ld_volatile_const_s16, ld_volatile_const_s32, ld_volatile_const_s64,
        ld_volatile_const_f32, ld_volatile_const_f64,
        ld_volatile_const_v2_b8, ld_volatile_const_v2_b16, ld_volatile_const_v2_b32, ld_volatile_const_v2_b64,
        ld_volatile_const_v2_u8, ld_volatile_const_v2_u16, ld_volatile_const_v2_u32, ld_volatile_const_v2_u64,
        ld_volatile_const_v2_s8, ld_volatile_const_v2_s16, ld_volatile_const_v2_s32, ld_volatile_const_v2_s64,
        ld_volatile_const_v2_f32, ld_volatile_const_v2_f64,
        ld_volatile_const_v4_b8, ld_volatile_const_v4_b16, ld_volatile_const_v4_b32, ld_volatile_const_v4_b64,
        ld_volatile_const_v4_u8, ld_volatile_const_v4_u16, ld_volatile_const_v4_u32, ld_volatile_const_v4_u64,
        ld_volatile_const_v4_s8, ld_volatile_const_v4_s16, ld_volatile_const_v4_s32, ld_volatile_const_v4_s64,
        ld_volatile_const_v4_f32, ld_volatile_const_v4_f64,
        ld_volatile_global_b8, ld_volatile_global_b16, ld_volatile_global_b32, ld_volatile_global_b64,
        ld_volatile_global_u8, ld_volatile_global_u16, ld_volatile_global_u32, ld_volatile_global_u64,
        ld_volatile_global_s8, ld_volatile_global_s16, ld_volatile_global_s32, ld_volatile_global_s64,
        ld_volatile_global_f32, ld_volatile_global_f64,
        ld_volatile_global_v2_b8, ld_volatile_global_v2_b16, ld_volatile_global_v2_b32, ld_volatile_global_v2_b64,
        ld_volatile_global_v2_u8, ld_volatile_global_v2_u16, ld_volatile_global_v2_u32, ld_volatile_global_v2_u64,
        ld_volatile_global_v2_s8, ld_volatile_global_v2_s16, ld_volatile_global_v2_s32, ld_volatile_global_v2_s64,
        ld_volatile_global_v2_f32, ld_volatile_global_v2_f64,
        ld_volatile_global_v4_b8, ld_volatile_global_v4_b16, ld_volatile_global_v4_b32, ld_volatile_global_v4_b64,
        ld_volatile_global_v4_u8, ld_volatile_global_v4_u16, ld_volatile_global_v4_u32, ld_volatile_global_v4_u64,
        ld_volatile_global_v4_s8, ld_volatile_global_v4_s16, ld_volatile_global_v4_s32, ld_volatile_global_v4_s64,
        ld_volatile_global_v4_f32, ld_volatile_global_v4_f64,
        ld_volatile_local_b8, ld_volatile_local_b16, ld_volatile_local_b32, ld_volatile_local_b64,
        ld_volatile_local_u8, ld_volatile_local_u16, ld_volatile_local_u32, ld_volatile_local_u64,
        ld_volatile_local_s8, ld_volatile_local_s16, ld_volatile_local_s32, ld_volatile_local_s64,
        ld_volatile_local_f32, ld_volatile_local_f64,
        ld_volatile_local_v2_b8, ld_volatile_local_v2_b16, ld_volatile_local_v2_b32, ld_volatile_local_v2_b64,
        ld_volatile_local_v2_u8, ld_volatile_local_v2_u16, ld_volatile_local_v2_u32, ld_volatile_local_v2_u64,
        ld_volatile_local_v2_s8, ld_volatile_local_v2_s16, ld_volatile_local_v2_s32, ld_volatile_local_v2_s64,
        ld_volatile_local_v2_f32, ld_volatile_local_v2_f64,
        ld_volatile_local_v4_b8, ld_volatile_local_v4_b16, ld_volatile_local_v4_b32, ld_volatile_local_v4_b64,
        ld_volatile_local_v4_u8, ld_volatile_local_v4_u16, ld_volatile_local_v4_u32, ld_volatile_local_v4_u64,
        ld_volatile_local_v4_s8, ld_volatile_local_v4_s16, ld_volatile_local_v4_s32, ld_volatile_local_v4_s64,
        ld_volatile_local_v4_f32, ld_volatile_local_v4_f64,
        ld_volatile_param_b8, ld_volatile_param_b16, ld_volatile_param_b32, ld_volatile_param_b64,
        ld_volatile_param_u8, ld_volatile_param_u16, ld_volatile_param_u32, ld_volatile_param_u64,
        ld_volatile_param_s8, ld_volatile_param_s16, ld_volatile_param_s32, ld_volatile_param_s64,
        ld_volatile_param_f32, ld_volatile_param_f64,
        ld_volatile_param_v2_b8, ld_volatile_param_v2_b16, ld_volatile_param_v2_b32, ld_volatile_param_v2_b64,
        ld_volatile_param_v2_u8, ld_volatile_param_v2_u16, ld_volatile_param_v2_u32, ld_volatile_param_v2_u64,
        ld_volatile_param_v2_s8, ld_volatile_param_v2_s16, ld_volatile_param_v2_s32, ld_volatile_param_v2_s64,
        ld_volatile_param_v2_f32, ld_volatile_param_v2_f64,
        ld_volatile_param_v4_b8, ld_volatile_param_v4_b16, ld_volatile_param_v4_b32, ld_volatile_param_v4_b64,
        ld_volatile_param_v4_u8, ld_volatile_param_v4_u16, ld_volatile_param_v4_u32, ld_volatile_param_v4_u64,
        ld_volatile_param_v4_s8, ld_volatile_param_v4_s16, ld_volatile_param_v4_s32, ld_volatile_param_v4_s64,
        ld_volatile_param_v4_f32, ld_volatile_param_v4_f64,
        ld_volatile_shared_b8, ld_volatile_shared_b16, ld_volatile_shared_b32, ld_volatile_shared_b64,
        ld_volatile_shared_u8, ld_volatile_shared_u16, ld_volatile_shared_u32, ld_volatile_shared_u64,
        ld_volatile_shared_s8, ld_volatile_shared_s16, ld_volatile_shared_s32, ld_volatile_shared_s64,
        ld_volatile_shared_f32, ld_volatile_shared_f64,
        ld_volatile_shared_v2_b8, ld_volatile_shared_v2_b16, ld_volatile_shared_v2_b32, ld_volatile_shared_v2_b64,
        ld_volatile_shared_v2_u8, ld_volatile_shared_v2_u16, ld_volatile_shared_v2_u32, ld_volatile_shared_v2_u64,
        ld_volatile_shared_v2_s8, ld_volatile_shared_v2_s16, ld_volatile_shared_v2_s32, ld_volatile_shared_v2_s64,
        ld_volatile_shared_v2_f32, ld_volatile_shared_v2_f64,
        ld_volatile_shared_v4_b8, ld_volatile_shared_v4_b16, ld_volatile_shared_v4_b32, ld_volatile_shared_v4_b64,
        ld_volatile_shared_v4_u8, ld_volatile_shared_v4_u16, ld_volatile_shared_v4_u32, ld_volatile_shared_v4_u64,
        ld_volatile_shared_v4_s8, ld_volatile_shared_v4_s16, ld_volatile_shared_v4_s32, ld_volatile_shared_v4_s64,
        ld_volatile_shared_v4_f32, ld_volatile_shared_v4_f64,

        // st
        st_global_b8, st_global_b16, st_global_b32, st_global_b64,
        st_global_u8, st_global_u16, st_global_u32, st_global_u64,
        st_global_s8, st_global_s16, st_global_s32, st_global_s64,
        st_global_f32, st_global_f64,
        st_global_v2_b8, st_global_v2_b16, st_global_v2_b32, st_global_v2_b64,
        st_global_v2_u8, st_global_v2_u16, st_global_v2_u32, st_global_v2_u64,
        st_global_v2_s8, st_global_v2_s16, st_global_v2_s32, st_global_v2_s64,
        st_global_v2_f32, st_global_v2_f64,
        st_global_v4_b8, st_global_v4_b16, st_global_v4_b32, st_global_v4_b64,
        st_global_v4_u8, st_global_v4_u16, st_global_v4_u32, st_global_v4_u64,
        st_global_v4_s8, st_global_v4_s16, st_global_v4_s32, st_global_v4_s64,
        st_global_v4_f32, st_global_v4_f64,
        st_local_b8, st_local_b16, st_local_b32, st_local_b64,
        st_local_u8, st_local_u16, st_local_u32, st_local_u64,
        st_local_s8, st_local_s16, st_local_s32, st_local_s64,
        st_local_f32, st_local_f64,
        st_local_v2_b8, st_local_v2_b16, st_local_v2_b32, st_local_v2_b64,
        st_local_v2_u8, st_local_v2_u16, st_local_v2_u32, st_local_v2_u64,
        st_local_v2_s8, st_local_v2_s16, st_local_v2_s32, st_local_v2_s64,
        st_local_v2_f32, st_local_v2_f64,
        st_local_v4_b8, st_local_v4_b16, st_local_v4_b32, st_local_v4_b64,
        st_local_v4_u8, st_local_v4_u16, st_local_v4_u32, st_local_v4_u64,
        st_local_v4_s8, st_local_v4_s16, st_local_v4_s32, st_local_v4_s64,
        st_local_v4_f32, st_local_v4_f64,
        st_shared_b8, st_shared_b16, st_shared_b32, st_shared_b64,
        st_shared_u8, st_shared_u16, st_shared_u32, st_shared_u64,
        st_shared_s8, st_shared_s16, st_shared_s32, st_shared_s64,
        st_shared_f32, st_shared_f64,
        st_shared_v2_b8, st_shared_v2_b16, st_shared_v2_b32, st_shared_v2_b64,
        st_shared_v2_u8, st_shared_v2_u16, st_shared_v2_u32, st_shared_v2_u64,
        st_shared_v2_s8, st_shared_v2_s16, st_shared_v2_s32, st_shared_v2_s64,
        st_shared_v2_f32, st_shared_v2_f64,
        st_shared_v4_b8, st_shared_v4_b16, st_shared_v4_b32, st_shared_v4_b64,
        st_shared_v4_u8, st_shared_v4_u16, st_shared_v4_u32, st_shared_v4_u64,
        st_shared_v4_s8, st_shared_v4_s16, st_shared_v4_s32, st_shared_v4_s64,
        st_shared_v4_f32, st_shared_v4_f64,
        //
        st_volatile_global_b8, st_volatile_global_b16, st_volatile_global_b32, st_volatile_global_b64,
        st_volatile_global_u8, st_volatile_global_u16, st_volatile_global_u32, st_volatile_global_u64,
        st_volatile_global_s8, st_volatile_global_s16, st_volatile_global_s32, st_volatile_global_s64,
        st_volatile_global_f32, st_volatile_global_f64,
        st_volatile_global_v2_b8, st_volatile_global_v2_b16, st_volatile_global_v2_b32, st_volatile_global_v2_b64,
        st_volatile_global_v2_u8, st_volatile_global_v2_u16, st_volatile_global_v2_u32, st_volatile_global_v2_u64,
        st_volatile_global_v2_s8, st_volatile_global_v2_s16, st_volatile_global_v2_s32, st_volatile_global_v2_s64,
        st_volatile_global_v2_f32, st_volatile_global_v2_f64,
        st_volatile_global_v4_b8, st_volatile_global_v4_b16, st_volatile_global_v4_b32, st_volatile_global_v4_b64,
        st_volatile_global_v4_u8, st_volatile_global_v4_u16, st_volatile_global_v4_u32, st_volatile_global_v4_u64,
        st_volatile_global_v4_s8, st_volatile_global_v4_s16, st_volatile_global_v4_s32, st_volatile_global_v4_s64,
        st_volatile_global_v4_f32, st_volatile_global_v4_f64,
        st_volatile_local_b8, st_volatile_local_b16, st_volatile_local_b32, st_volatile_local_b64,
        st_volatile_local_u8, st_volatile_local_u16, st_volatile_local_u32, st_volatile_local_u64,
        st_volatile_local_s8, st_volatile_local_s16, st_volatile_local_s32, st_volatile_local_s64,
        st_volatile_local_f32, st_volatile_local_f64,
        st_volatile_local_v2_b8, st_volatile_local_v2_b16, st_volatile_local_v2_b32, st_volatile_local_v2_b64,
        st_volatile_local_v2_u8, st_volatile_local_v2_u16, st_volatile_local_v2_u32, st_volatile_local_v2_u64,
        st_volatile_local_v2_s8, st_volatile_local_v2_s16, st_volatile_local_v2_s32, st_volatile_local_v2_s64,
        st_volatile_local_v2_f32, st_volatile_local_v2_f64,
        st_volatile_local_v4_b8, st_volatile_local_v4_b16, st_volatile_local_v4_b32, st_volatile_local_v4_b64,
        st_volatile_local_v4_u8, st_volatile_local_v4_u16, st_volatile_local_v4_u32, st_volatile_local_v4_u64,
        st_volatile_local_v4_s8, st_volatile_local_v4_s16, st_volatile_local_v4_s32, st_volatile_local_v4_s64,
        st_volatile_local_v4_f32, st_volatile_local_v4_f64,
        st_volatile_shared_b8, st_volatile_shared_b16, st_volatile_shared_b32, st_volatile_shared_b64,
        st_volatile_shared_u8, st_volatile_shared_u16, st_volatile_shared_u32, st_volatile_shared_u64,
        st_volatile_shared_s8, st_volatile_shared_s16, st_volatile_shared_s32, st_volatile_shared_s64,
        st_volatile_shared_f32, st_volatile_shared_f64,
        st_volatile_shared_v2_b8, st_volatile_shared_v2_b16, st_volatile_shared_v2_b32, st_volatile_shared_v2_b64,
        st_volatile_shared_v2_u8, st_volatile_shared_v2_u16, st_volatile_shared_v2_u32, st_volatile_shared_v2_u64,
        st_volatile_shared_v2_s8, st_volatile_shared_v2_s16, st_volatile_shared_v2_s32, st_volatile_shared_v2_s64,
        st_volatile_shared_v2_f32, st_volatile_shared_v2_f64,
        st_volatile_shared_v4_b8, st_volatile_shared_v4_b16, st_volatile_shared_v4_b32, st_volatile_shared_v4_b64,
        st_volatile_shared_v4_u8, st_volatile_shared_v4_u16, st_volatile_shared_v4_u32, st_volatile_shared_v4_u64,
        st_volatile_shared_v4_s8, st_volatile_shared_v4_s16, st_volatile_shared_v4_s32, st_volatile_shared_v4_s64,
        st_volatile_shared_v4_f32, st_volatile_shared_v4_f64,

        // cvt
        cvt_u8_u8, cvt_sat_u8_u8, cvt_u8_u16, cvt_sat_u8_u16, cvt_u8_u32, cvt_sat_u8_u32, cvt_u8_u64, cvt_sat_u8_u64,
        cvt_u8_s8, cvt_sat_u8_s8, cvt_u8_s16, cvt_sat_u8_s16, cvt_u8_s32, cvt_sat_u8_s32, cvt_u8_s64, cvt_sat_u8_s64,
        cvt_u8_f16, cvt_sat_u8_f16, cvt_u8_f32, cvt_sat_u8_f32, cvt_u8_f64, cvt_sat_u8_f64,
        cvt_u16_u8, cvt_sat_u16_u8, cvt_u16_u16, cvt_sat_u16_u16, cvt_u16_u32, cvt_sat_u16_u32, cvt_u16_u64, cvt_sat_u16_u64,
        cvt_u16_s8, cvt_sat_u16_s8, cvt_u16_s16, cvt_sat_u16_s16, cvt_u16_s32, cvt_sat_u16_s32, cvt_u16_s64, cvt_sat_u16_s64,
        cvt_u16_f16, cvt_sat_u16_f16, cvt_u16_f32, cvt_sat_u16_f32, cvt_u16_f64, cvt_sat_u16_f64,
        cvt_u32_u8, cvt_sat_u32_u8, cvt_u32_u16, cvt_sat_u32_u16, cvt_u32_u32, cvt_sat_u32_u32, cvt_u32_u64, cvt_sat_u32_u64,
        cvt_u32_s8, cvt_sat_u32_s8, cvt_u32_s16, cvt_sat_u32_s16, cvt_u32_s32, cvt_sat_u32_s32, cvt_u32_s64, cvt_sat_u32_s64,
        cvt_u32_f16, cvt_sat_u32_f16, cvt_u32_f32, cvt_sat_u32_f32, cvt_u32_f64, cvt_sat_u32_f64,
        cvt_u64_u8, cvt_sat_u64_u8, cvt_u64_u16, cvt_sat_u64_u16, cvt_u64_u32, cvt_sat_u64_u32, cvt_u64_u64, cvt_sat_u64_u64,
        cvt_u64_s8, cvt_sat_u64_s8, cvt_u64_s16, cvt_sat_u64_s16, cvt_u64_s32, cvt_sat_u64_s32, cvt_u64_s64, cvt_sat_u64_s64,
        cvt_u64_f16, cvt_sat_u64_f16, cvt_u64_f32, cvt_sat_u64_f32, cvt_u64_f64, cvt_sat_u64_f64,
        //
        cvt_s8_u8, cvt_sat_s8_u8, cvt_s8_u16, cvt_sat_s8_u16, cvt_s8_u32, cvt_sat_s8_u32, cvt_s8_u64, cvt_sat_s8_u64,
        cvt_s8_s8, cvt_sat_s8_s8, cvt_s8_s16, cvt_sat_s8_s16, cvt_s8_s32, cvt_sat_s8_s32, cvt_s8_s64, cvt_sat_s8_s64,
        cvt_s8_f16, cvt_sat_s8_f16, cvt_s8_f32, cvt_sat_s8_f32, cvt_s8_f64, cvt_sat_s8_f64,
        cvt_s16_u8, cvt_sat_s16_u8, cvt_s16_u16, cvt_sat_s16_u16, cvt_s16_u32, cvt_sat_s16_u32, cvt_s16_u64, cvt_sat_s16_u64,
        cvt_s16_s8, cvt_sat_s16_s8, cvt_s16_s16, cvt_sat_s16_s16, cvt_s16_s32, cvt_sat_s16_s32, cvt_s16_s64, cvt_sat_s16_s64,
        cvt_s16_f16, cvt_sat_s16_f16, cvt_s16_f32, cvt_sat_s16_f32, cvt_s16_f64, cvt_sat_s16_f64,
        cvt_s32_u8, cvt_sat_s32_u8, cvt_s32_u16, cvt_sat_s32_u16, cvt_s32_u32, cvt_sat_s32_u32, cvt_s32_u64, cvt_sat_s32_u64,
        cvt_s32_s8, cvt_sat_s32_s8, cvt_s32_s16, cvt_sat_s32_s16, cvt_s32_s32, cvt_sat_s32_s32, cvt_s32_s64, cvt_sat_s32_s64,
        cvt_s32_f16, cvt_sat_s32_f16, cvt_s32_f32, cvt_sat_s32_f32, cvt_s32_f64, cvt_sat_s32_f64,
        cvt_s64_u8, cvt_sat_s64_u8, cvt_s64_u16, cvt_sat_s64_u16, cvt_s64_u32, cvt_sat_s64_u32, cvt_s64_u64, cvt_sat_s64_u64,
        cvt_s64_s8, cvt_sat_s64_s8, cvt_s64_s16, cvt_sat_s64_s16, cvt_s64_s32, cvt_sat_s64_s32, cvt_s64_s64, cvt_sat_s64_s64,
        cvt_s64_f16, cvt_sat_s64_f16, cvt_s64_f32, cvt_sat_s64_f32, cvt_s64_f64, cvt_sat_s64_f64,
        //
        cvt_f16_u8, cvt_sat_f16_u8, cvt_f16_u16, cvt_sat_f16_u16, cvt_f16_u32, cvt_sat_f16_u32, cvt_f16_u64, cvt_sat_f16_u64,
        cvt_f16_s8, cvt_sat_f16_s8, cvt_f16_s16, cvt_sat_f16_s16, cvt_f16_s32, cvt_sat_f16_s32, cvt_f16_s64, cvt_sat_f16_s64,
        cvt_f16_f16, cvt_sat_f16_f16, cvt_f16_f32, cvt_sat_f16_f32, cvt_f16_f64, cvt_sat_f16_f64,
        cvt_f32_u8, cvt_sat_f32_u8, cvt_f32_u16, cvt_sat_f32_u16, cvt_f32_u32, cvt_sat_f32_u32, cvt_f32_u64, cvt_sat_f32_u64,
        cvt_f32_s8, cvt_sat_f32_s8, cvt_f32_s16, cvt_sat_f32_s16, cvt_f32_s32, cvt_sat_f32_s32, cvt_f32_s64, cvt_sat_f32_s64,
        cvt_f32_f16, cvt_sat_f32_f16, cvt_f32_f32, cvt_sat_f32_f32, cvt_f32_f64, cvt_sat_f32_f64,
        cvt_f64_u8, cvt_sat_f64_u8, cvt_f64_u16, cvt_sat_f64_u16, cvt_f64_u32, cvt_sat_f64_u32, cvt_f64_u64, cvt_sat_f64_u64,
        cvt_f64_s8, cvt_sat_f64_s8, cvt_f64_s16, cvt_sat_f64_s16, cvt_f64_s32, cvt_sat_f64_s32, cvt_f64_s64, cvt_sat_f64_s64,
        cvt_f64_f16, cvt_sat_f64_f16, cvt_f64_f32, cvt_sat_f64_f32, cvt_f64_f64, cvt_sat_f64_f64,

        #endregion

        #region Texture Instruction

        // tex
        tex_1d_v4_u32_s32, tex_1d_v4_u32_f32,
        tex_1d_v4_s32_s32, tex_1d_v4_s32_f32,
        tex_1d_v4_f32_s32, tex_1d_v4_f32_f32,
        //
        tex_2d_v4_u32_s32, tex_2d_v4_u32_f32,
        tex_2d_v4_s32_s32, tex_2d_v4_s32_f32,
        tex_2d_v4_f32_s32, tex_2d_v4_f32_f32,
        //
        tex_3d_v4_u32_s32, tex_3d_v4_u32_f32,
        tex_3d_v4_s32_s32, tex_3d_v4_s32_f32,
        tex_3d_v4_f32_s32, tex_3d_v4_f32_f32,

        #endregion

        #region Control Flow Instructions

        // bra
        bra, bra_uni,

        // call
        call, call_uni,

        // ret
        ret, ret_uni,

        // exit
        exit,

        #endregion

        #region Parallel Synchronization and Communication Instructions

        // bar
        bar_sync,

        // atom
        atom_global_and_b32,
        atom_global_or_b32,
        atom_global_xor_b32,
        atom_global_cas_b32, atom_global_cas_b64,
        atom_global_exch_b32, atom_global_exch_b64,
        atom_global_add_u32, atom_global_add_u64,
        atom_global_add_s32,
        atom_global_add_f32,
        atom_global_inc_u32,
        atom_global_dec_u32,
        atom_global_min_u32,
        atom_global_min_s32,
        atom_global_min_f32,
        atom_global_max_u32,
        atom_global_max_s32,
        atom_global_max_f32,
        atom_shared_and_b32,
        atom_shared_or_b32,
        atom_shared_xor_b32,
        atom_shared_cas_b32, atom_shared_cas_b64,
        atom_shared_exch_b32, atom_shared_exch_b64,
        atom_shared_add_u32, atom_shared_add_u64,
        atom_shared_add_s32,
        atom_shared_add_f32,
        atom_shared_inc_u32,
        atom_shared_dec_u32,
        atom_shared_min_u32,
        atom_shared_min_s32,
        atom_shared_min_f32,
        atom_shared_max_u32,
        atom_shared_max_s32,
        atom_shared_max_f32,

        // red
        red_global_and_b32,
        red_global_or_b32,
        red_global_xor_b32,
        red_global_add_u32, red_global_add_u64,
        red_global_add_s32,
        red_global_add_f32,
        red_global_inc_u32,
        red_global_dec_u32,
        red_global_min_u32,
        red_global_min_s32,
        red_global_min_f32,
        red_global_max_u32,
        red_global_max_s32,
        red_global_max_f32,
        red_shared_and_b32,
        red_shared_or_b32,
        red_shared_xor_b32,
        red_shared_add_u32, red_shared_add_u64,
        red_shared_add_s32,
        red_shared_add_f32,
        red_shared_inc_u32,
        red_shared_dec_u32,
        red_shared_min_u32,
        red_shared_min_s32,
        red_shared_min_f32,
        red_shared_max_u32,
        red_shared_max_s32,
        red_shared_max_f32,

        // vote
        vote_all_pred, vote_any_pred, vote_uni_pred,

        #endregion

        #region Floating-Point Instructions

        // rcp
        rcp_f32, rcp_f64,

        // sqrt
        sqrt_f32, sqrt_f64,

        // rsqrt
        rsqrt_f32, rsqrt_f64,

        // sin
        sin_f32,

        // cos
        cos_f32,

        // lg2
        lg2_f32,

        // ex2
        ex2_f32,

        #endregion

        #region Miscellaneous Instructions

        // trap
        trap,

        // brkpt
        brkpt,

        #endregion
    }
}
