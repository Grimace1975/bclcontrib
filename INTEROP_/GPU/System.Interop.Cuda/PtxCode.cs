using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace System.Interop.Cuda
{
	enum PtxCode
	{
		None,
		/// <summary>
		/// Not a PTX instruction, but this ensures that PTX and IR opcodes do not overlap and cause confusion.
		/// </summary>
		Ptx_First = 500,
		Add_S16,
		Add_S32,
		Add_F32,
		Sub_S32,
		Sub_F32,
		Mul_Lo_S32,
		Mul_F32,
		Div_S32,
		Div_U32,
		Div_F32,
		Rem_S32,
		Abs_S32,
		Abs_F32,
		Neg_S32,
		Neg_F32,
		Min_S32,
		Min_F32,
		Max_S32,
		Max_F32,
		Bra,
		Ld_Param_S16,
		Ld_Param_S32,
		Ld_Param_F32,
		St_Global_S16,
		St_Global_S32,
		St_Global_F32,
		Ld_Global_S16,
		Ld_Global_S32,
		Ld_Global_F32,
		Ld_Shared_S32,
		St_Shared_S32,
		Ld_Shared_F32,
		St_Shared_F32,
		Ret,
		Call,
		Exit,
		Mov_S32,
		Mov_F32,

		/// <summary>
		/// Pseudo value used to recognize setp opcodes.
		/// </summary>
		Setp_First,
		Setp_Gt_S32,
		Setp_Gt_F32,
		Setp_Lt_S32,
		Setp_Lt_F32,
		Setp_Lo_U32,
		Setp_Ltu_F32,
		Setp_Eq_S32,
		Setp_Eq_F32,
		Setp_Hi_U32,
		Setp_Gtu_F32,

		/// <summary>
		/// Pseudo value used to recognize setp opcodes.
		/// </summary>
		Setp_Last,

		Cvt_S32_U16,
		Bar_Sync,
		And_B32,
		Or_B32,
		Xor_B32,
		Shl_B32,
		Shr_S32,
		Shr_U32,
		Cvt_Rzi_S32_F32,
		Cvt_Rzi_U32_F32,
		Cvt_Rn_F32_S32,
		Selp_S32
	}
}
