#if !CODE_ANALYSIS
using System.Collections;
using System.Text;
namespace System.Interop.CSyntax
#else
using System;
using System.Collections;
namespace SystemEx.Interop.CSyntax
#endif
{
    public partial class PrintfFormat
    {
        private int cPos = 0;
        private ArrayList vFmt = new ArrayList();

        public PrintfFormat(string fmtArg)
        {
            int ePos = 0;
            ConversionSpecification sFmt = null;
            string unCS = nonControl(fmtArg, 0);
            if (unCS != null)
            {
                sFmt = new ConversionSpecification();
                sFmt.setLiteral(unCS);
                vFmt.Add(sFmt);
            }
            while (cPos != -1 && cPos < fmtArg.Length)
            {
                for (ePos = cPos + 1; ePos < fmtArg.Length; ePos++)
                {
                    char c = '\x0';
#if CODE_ANALYSIS
                    c = fmtArg.CharAt(ePos);
#else
                    c = fmtArg[ePos];
#endif
                    if (c == 'i')
                        break;
                    if (c == 'd')
                        break;
                    if (c == 'f')
                        break;
                    if (c == 'g')
                        break;
                    if (c == 'G')
                        break;
                    if (c == 'o')
                        break;
                    if (c == 'x')
                        break;
                    if (c == 'X')
                        break;
                    if (c == 'e')
                        break;
                    if (c == 'E')
                        break;
                    if (c == 'c')
                        break;
                    if (c == 's')
                        break;
                    if (c == '%')
                        break;
                }
                ePos = Math.Min(ePos + 1, fmtArg.Length);
                sFmt = new ConversionSpecification(fmtArg.Substring(cPos, ePos));
                vFmt.Add(sFmt);
                unCS = this.nonControl(fmtArg, ePos);
                if (unCS != null)
                {
                    sFmt = new ConversionSpecification();
                    sFmt.setLiteral(unCS);
                    vFmt.Add(sFmt);
                }
            }
        }

        private string nonControl(string s, int start)
        {
            cPos = s.IndexOf("%", start);
            if (cPos == -1)
                cPos = s.Length;
            return s.Substring(start, cPos);
        }

        public string SprintfArray(object[] o)
        {
            char c = '\x0';
            int i = 0;
            StringBuilder sb = new StringBuilder();
            foreach (ConversionSpecification cs in vFmt)
            {
                c = cs.getConversionCharacter();
                if (c == '\0')
                    sb.Append(cs.getLiteral());
                else if (c == '%')
                    sb.Append("%");
                else
                {
                    if (cs.isPositionalSpecification())
                    {
                        i = cs.getArgumentPosition() - 1;
                        if (cs.isPositionalFieldWidth())
                        {
                            int ifw = cs.getArgumentPositionForFieldWidth() - 1;
                            cs.setFieldWidthWithArg(((int)o[ifw]));
                        }
                        if (cs.isPositionalPrecision())
                        {
                            int ipr = cs.getArgumentPositionForPrecision() - 1;
                            cs.setPrecisionWithArg(((int)o[ipr]));
                        }
                    }
                    else
                    {
                        if (cs.isVariableFieldWidth())
                        {
                            cs.setFieldWidthWithArg(((int)o[i]));
                            i++;
                        }
                        if (cs.isVariablePrecision())
                        {
                            cs.setPrecisionWithArg(((int)o[i]));
                            i++;
                        }
                    }
                    if (o[i] is byte)
                        sb.Append(cs.internalsprintfInt32(((byte)o[i])));
                    else if (o[i] is short)
                        sb.Append(cs.internalsprintfInt32(((short)o[i])));
                    else if (o[i] is int)
                        sb.Append(cs.internalsprintfInt32(((int)o[i])));
                    else if (o[i] is long)
                        sb.Append(cs.internalsprintfInt64(((long)o[i])));
                    else if (o[i] is float)
                        sb.Append(cs.internalsprintfDouble(((float)o[i])));
                    else if (o[i] is double)
                        sb.Append(cs.internalsprintfDouble(((double)o[i])));
                    else if (o[i] is char)
                        sb.Append(cs.internalsprintfInt32(((char)o[i])));
                    else if (o[i] is string)
                        sb.Append(cs.internalsprintfString((string)o[i]));
                    else
                        sb.Append(cs.internalsprintfObject(o[i]));
                    if (!cs.isPositionalSpecification())
                        i++;
                }
            }
            return sb.ToString();
        }

        public string Sprintf()
        {
            char c = '\0';
            StringBuilder sb = new StringBuilder();
            foreach (ConversionSpecification cs in vFmt)
            {
                c = cs.getConversionCharacter();
                if (c == '\0')
                    sb.Append(cs.getLiteral());
                else if (c == '%')
                    sb.Append("%");
            }
            return sb.ToString();
        }

        public string SprintfInt32(int x)
        {
            char c = '\x0';
            StringBuilder sb = new StringBuilder();
            foreach (ConversionSpecification cs in vFmt)
            {
                c = cs.getConversionCharacter();
                if (c == '\0')
                    sb.Append(cs.getLiteral());
                else if (c == '%')
                    sb.Append("%");
                else
                    sb.Append(cs.internalsprintfInt32(x));
            }
            return sb.ToString();
        }

        public string SprintfInt64(long x)
        {
            char c = '\x0';
            StringBuilder sb = new StringBuilder();
            foreach (ConversionSpecification cs in vFmt)
            {
                c = cs.getConversionCharacter();
                if (c == '\0')
                    sb.Append(cs.getLiteral());
                else if (c == '%')
                    sb.Append("%");
                else
                    sb.Append(cs.internalsprintfInt64(x));
            }
            return sb.ToString();
        }
        public string SprintfDouble(double x)
        {
            char c = '\x0';
            StringBuilder sb = new StringBuilder();
            foreach (ConversionSpecification cs in vFmt)
            {
                c = cs.getConversionCharacter();
                if (c == '\0')
                    sb.Append(cs.getLiteral());
                else if (c == '%')
                    sb.Append("%");
                else
                    sb.Append(cs.internalsprintfDouble(x));
            }
            return sb.ToString();
        }

        public string SprintfString(string x)
        {
            char c = '\x0';
            StringBuilder sb = new StringBuilder();
            foreach (ConversionSpecification cs in vFmt)
            {
                c = cs.getConversionCharacter();
                if (c == '\0')
                    sb.Append(cs.getLiteral());
                else if (c == '%')
                    sb.Append("%");
                else
                    sb.Append(cs.internalsprintfString(x));
            }
            return sb.ToString();
        }

        public string SprintfObject(object x)
        {
            char c = '\x0';
            StringBuilder sb = new StringBuilder();
            foreach (ConversionSpecification cs in vFmt)
            {
                c = cs.getConversionCharacter();
                if (c == '\0')
                    sb.Append(cs.getLiteral());
                else if (c == '%')
                    sb.Append("%");
                else
                {
                    if (x is byte)
                        sb.Append(cs.internalsprintfInt32(((byte)x)));
                    else if (x is short)
                        sb.Append(cs.internalsprintfInt32(((short)x)));
                    else if (x is int)
                        sb.Append(cs.internalsprintfInt32(((int)x)));
                    else if (x is long)
                        sb.Append(cs.internalsprintfInt64(((long)x)));
                    else if (x is float)
                        sb.Append(cs.internalsprintfDouble(((float)x)));
                    else if (x is double)
                        sb.Append(cs.internalsprintfDouble(((double)x)));
                    else if (x is char)
                        sb.Append(cs.internalsprintfInt32(((char)x)));
                    else if (x is string)
                        sb.Append(cs.internalsprintfString((string)x));
                    else
                        sb.Append(cs.internalsprintfObject(x));
                }
            }
            return sb.ToString();
        }
    }
}
