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
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
namespace System.Interop.Cuda
{
    using z = PtxOpCode;
    public partial class PtxEmitter
    {
        private readonly StringWriter _w = new StringWriter();
        private readonly PtxComputeCapability _computeCapability;
        private HashSet<PtxOperand> _globalSymbols = new HashSet<PtxOperand>(new PtxOperand.EqualityComparer());

        public PtxEmitter()
        {
            _computeCapability = PtxComputeCapability.Compute11;
        }

        public String GetEmittedPtx()
        {
            var b = new StringBuilder(@"
	.version 1.2
	.target " + _computeCapability.GetEmitString() + @", map_f64_to_f32
\n");
            b.AppendLine(GetGlobalDeclarations(_globalSymbols));
            b.AppendLine(_w.GetStringBuilder().ToString());
            return b.ToString();
        }

        private static string GetGlobalDeclarations(IEnumerable<PtxOperand> globalSymbols)
        {
            var b = new StringBuilder();
            foreach (var symbol in globalSymbols)
            {
                //Utilities.DebugAssert(symbol.Type == VRegType.Address);
                b.Append("\t");
                b.Append(symbol.StateSpace.GetEmitString() + " ");
                b.Append(symbol.StackType.GetEmitString(false) + " ");
                b.Append(symbol.Name);
                var pointerInfo = symbol.PointerInfo;
                if ((pointerInfo != null) && (pointerInfo.ElementCount != 1))
                    b.Append("[" + pointerInfo.ElementCount + "]");
                b.AppendLine(";");
            }
            return b.ToString();
        }

        public void Write(TextWriter w, PtxEntry entry)
        {
            w.WriteLine("	.entry " + entry.Name + "\n\t{");
            foreach (var parameters in entry.Parameters)
            {
                //Utilities.DebugAssert(param.StateSpace == CudaStateSpace.Parameter, "vreg.Type == VRegType.Parameter");
                w.WriteLine("\t.param " + parameters.StackType.GetEmitString(false) + " " + parameters.Name + ";");
            }
            WriteNameAndDeclareLocals(w, entry.Blocks);
            WriteBlocks(w, entry.Blocks);
            w.WriteLine("	} // " + entry.Name);
            w.WriteLine();
        }

        private static void WriteBlocks(TextWriter w, List<PtxBlock> blocks)
        {
            foreach (var block in blocks)
            {
                w.WriteLine();
                w.WriteLine(block.Name + ":");
                foreach (var instruction in block.Instructions)
                    instruction.EmitInstruction(w);
            }
        }

        private void WriteNameAndDeclareLocals(TextWriter w, IEnumerable<PtxBlock> blocks)
        {
            // Count and assign names/indices to all register variables.
            var operands = new HashSet<PtxOperand>();
            foreach (var instruction in (from b in blocks from li in b.Instructions select li))
            {
                if (instruction.Destination != null)
                    operands.Add(instruction.Destination);
                if (instruction is PtxMethodInstruction)
                {
                    foreach (var operand in ((PtxMethodInstruction)instruction).Parameters)
                        operands.Add(operand);
                }
                else
                {
                    if (instruction.Source != null)
                        operands.Add(instruction.Source);
                    if (instruction.Source2 != null)
                        operands.Add(instruction.Source2);
                    if (instruction.Source3 != null)
                        operands.Add(instruction.Source3);
                }
            }

            foreach (var operand in operands)
                if (operand.Type == PtxOperand.PtxOperandType.Address)
                    _globalSymbols.Add(operand);

            // Register: Type == VRegType.Register, disregard StateSpace value.
            // Parameter: Type == VRegType.Address, disregard StateSpace value.
            foreach (var operandGroup in operands
                .Where(operand => operand.Type == PtxOperand.PtxOperandType.Register)
                .GroupBy(operand => new { operand.StateSpace, operand.StackType }))
            {
                var stackType = operandGroup.Key.StackType;
                string variablePrefix = stackType.GetPrefix();
                int variableIndex = 0;
                foreach (PtxOperand operand in operandGroup)
                {
                    //Utilities.DebugAssert(stackType != StackType.ValueType || reg.ReflectionType == typeof(PredicateValue));
                    operand.Name = variablePrefix + variableIndex.ToString();
                    variableIndex++;
                }
                w.WriteLine(@"	{0} {1} {2}<{3}>;", operandGroup.Key.StateSpace.GetEmitString(), stackType.GetEmitString(true), variablePrefix, variableIndex);
            }
        }
        
        private static string GetBasicOpcodePredicate(PtxInstruction instruction)
        {
            return (instruction.Predicate == null ? string.Empty : "@" + (instruction.HasPredicateNegation ? "!" : string.Empty) + instruction.Predicate.Name + " ");
        }

        private static void EmitBasicOpcode(TextWriter w, PtxInstruction instruction, string opCode)
        {
            w.WriteLine("\t" + GetBasicOpcodePredicate(instruction) + opCode);
            if (instruction.Destination != null)
                w.WriteLine(" " + instruction.Destination.GetAssemblyText());
            if (instruction.Source != null)
            {
                if (instruction.Destination != null)
                    w.WriteLine(", ");
                w.WriteLine(" " + instruction.Source.GetAssemblyText());
                if (instruction.Source2 != null)
                {
                    w.WriteLine(", " + instruction.Source2.GetAssemblyText());
                    if (instruction.Source3 != null)
                        w.WriteLine(", " + instruction.Source3.GetAssemblyText());
                }
            }
            w.WriteLine(";");
        }
    }
}
