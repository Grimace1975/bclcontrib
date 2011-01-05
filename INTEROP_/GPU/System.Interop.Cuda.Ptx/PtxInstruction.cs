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
	public class PtxInstruction
	{
        private readonly PtxOpCode _opCode;

        public PtxInstruction(PtxOpCode opCode)
        {
            _opCode = opCode;
        }
        public PtxInstruction(PtxOpCode opCode, object operand)
        {
            _opCode = opCode;
            Operand = operand;
        }

        ///// <summary>
        ///// Copy constructor. Next and previous are not copied.
        ///// </summary>
        //public ListInstruction(PtxCode opcode, ListInstruction template)
        //{
        //    _opCode = (int)opcode;
        //    // Yes, virtcalls here, so MethodCallListInstruction probably shouldn't use this constructor.
        //    Destination = template.Destination;
        //    Source1 = template.Source1;
        //    Source2 = template.Source2;
        //    Source3 = template.Source3;
        //    Predicate = template.Predicate;
        //    PredicateNegation = template.PredicateNegation;
        //    Operand = template.Operand;
        //}


		public PtxOpCode OpCode
		{
			get { return _opCode; }
		}

        public object Operand { get; set; }

        public virtual PtxOperand Source { get; set; }
        public virtual PtxOperand Source2 { get; set; }
        public virtual PtxOperand Source3 { get; set; }
        public PtxOperand Destination { get; set; }
        public PtxOperand Predicate { get; set; }
        public bool HasPredicateNegation { get; set; }
	}
}
