using System.Text;
using System.Collections.Generic;
namespace System.Interop.CSyntax
{
    public partial class PrintfFormat
    {
        private int cPos = 0;
        private List<ConversionSpecification> vFmt = new List<ConversionSpecification>();

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
                    c = fmtArg[ePos];
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

        public string sprintf(object[] o)
        {
            char c = '\x0';
            int i = 0;
            var sb = new StringBuilder();
            foreach (var cs in vFmt)
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
                        sb.Append(cs.internalsprintf(((byte)o[i])));
                    else if (o[i] is short)
                        sb.Append(cs.internalsprintf(((short)o[i])));
                    else if (o[i] is int)
                        sb.Append(cs.internalsprintf(((int)o[i])));
                    else if (o[i] is long)
                        sb.Append(cs.internalsprintf(((long)o[i])));
                    else if (o[i] is float)
                        sb.Append(cs.internalsprintf(((float)o[i])));
                    else if (o[i] is double)
                        sb.Append(cs.internalsprintf(((double)o[i])));
                    else if (o[i] is char)
                        sb.Append(cs.internalsprintf(((char)o[i])));
                    else if (o[i] is string)
                        sb.Append(cs.internalsprintf((string)o[i]));
                    else
                        sb.Append(cs.internalsprintf(o[i]));
                    if (!cs.isPositionalSpecification())
                        i++;
                }
            }
            return sb.ToString();
        }

        public string sprintf()
        {
            char c = '\0';
            var sb = new StringBuilder();
            foreach (var cs in vFmt)
            {
                c = cs.getConversionCharacter();
                if (c == '\0')
                    sb.Append(cs.getLiteral());
                else if (c == '%')
                    sb.Append("%");
            }
            return sb.ToString();
        }

        public string sprintf(int x)
        {
            char c = '\x0';
            var sb = new StringBuilder();
            foreach (var cs in vFmt)
            {
                c = cs.getConversionCharacter();
                if (c == '\0')
                    sb.Append(cs.getLiteral());
                else if (c == '%')
                    sb.Append("%");
                else
                    sb.Append(cs.internalsprintf(x));
            }
            return sb.ToString();
        }

        public string sprintf(long x)
        {
            char c = '\x0';
            var sb = new StringBuilder();
            foreach (var cs in vFmt)
            {
                c = cs.getConversionCharacter();
                if (c == '\0')
                    sb.Append(cs.getLiteral());
                else if (c == '%')
                    sb.Append("%");
                else
                    sb.Append(cs.internalsprintf(x));
            }
            return sb.ToString();
        }
        public string sprintf(double x)
        {
            char c = '\x0';
            var sb = new StringBuilder();
            foreach (var cs in vFmt)
            {
                c = cs.getConversionCharacter();
                if (c == '\0')
                    sb.Append(cs.getLiteral());
                else if (c == '%')
                    sb.Append("%");
                else
                    sb.Append(cs.internalsprintf(x));
            }
            return sb.ToString();
        }

        public string sprintf(string x)
        {
            char c = '\x0';
            var sb = new StringBuilder();
            foreach (var cs in vFmt)
            {
                c = cs.getConversionCharacter();
                if (c == '\0')
                    sb.Append(cs.getLiteral());
                else if (c == '%')
                    sb.Append("%");
                else
                    sb.Append(cs.internalsprintf(x));
            }
            return sb.ToString();
        }

        public string sprintf(object x)
        {
            char c = '\x0';
            var sb = new StringBuilder();
            foreach (var cs in vFmt)
            {
                c = cs.getConversionCharacter();
                if (c == '\0')
                    sb.Append(cs.getLiteral());
                else if (c == '%')
                    sb.Append("%");
                else
                {
                    if (x is byte)
                        sb.Append(cs.internalsprintf(((byte)x)));
                    else if (x is short)
                        sb.Append(cs.internalsprintf(((short)x)));
                    else if (x is int)
                        sb.Append(cs.internalsprintf(((int)x)));
                    else if (x is long)
                        sb.Append(cs.internalsprintf(((long)x)));
                    else if (x is float)
                        sb.Append(cs.internalsprintf(((float)x)));
                    else if (x is double)
                        sb.Append(cs.internalsprintf(((double)x)));
                    else if (x is char)
                        sb.Append(cs.internalsprintf(((char)x)));
                    else if (x is string)
                        sb.Append(cs.internalsprintf((string)x));
                    else
                        sb.Append(cs.internalsprintf(x));
                }
            }
            return sb.ToString();
        }
    }
}
