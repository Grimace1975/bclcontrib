#if !CODE_ANALYSIS
namespace System
#else
using System;
namespace SystemEx
#endif
{
    /// <summary>
    /// ByteBuilder3DExtensions
    /// </summary>
    public partial class ByteBuilder
    {
        public void WriteCoord(float f)
        {
            WriteInt16((short)(f * 8));
        }

        public void WritePos(float[] pos)
        {
            WriteInt16((short)(pos[0] * 8));
            WriteInt16((short)(pos[1] * 8));
            WriteInt16((short)(pos[2] * 8));
        }

        public void WriteAngle(float f)
        {
            WriteByte((byte)(f * 256 / 360));
        }

        public void WriteAngle16(float f)
        {
            WriteInt16(Math3D.ANGLE2SHORT(f));
        }

        public void WriteDir(float[] dir)
        {
            if (dir == null)
            {
                WriteByte(0);
                return;
            }
            float bestd = 0;
            byte best = 0;
            for (byte index = 0; index < Math3D.VertexNormals.Length; index++)
            {
                float d = Math3D.DotProduct(dir, Math3D.VertexNormals[index]);
                if (d > bestd)
                {
                    bestd = d;
                    best = index;
                }
            }
            WriteByte(best);
        }

        public void ReadDir(float[] dir)
        {
            int value = ReadByte();
            if ((value >= Math3D.VertexNormals.Length) && (_errorHandler != null))
                _errorHandler(ErrorCode.ERR_DROP, "MSF_ReadDir: out of range", null);
            Math3D.VectorCopy(Math3D.VertexNormals[value], dir);
        }

        public float ReadCoord()
        {
            return ReadInt16() * (1.0f / 8);
        }

        public void ReadPos(float[] pos)
        {
            pos[0] = ReadInt16() * (1.0f / 8);
            pos[1] = ReadInt16() * (1.0f / 8);
            pos[2] = ReadInt16() * (1.0f / 8);
        }

        public float ReadAngle()
        {
            return ReadChar() * (360.0f / 256);
        }

        public float ReadAngle16()
        {
            return Math3D.SHORT2ANGLE(ReadInt16());
        }
    }
}
