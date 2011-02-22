#if CODE_ANALYSIS
namespace SystemEx
#else
namespace System
#endif
{
    public class Plane3
    {
        public float[] normal = new float[3];
        public float dist;
        // This is for fast side tests, 0=xplane, 1=yplane, 2=zplane and 3=arbitrary.
        public byte type;
        // This represents signx + (signy<<1) + (signz << 1).
        public byte signbits; // signx + (signy<<1) + (signz<<1)
        public byte[] pad = { 0, 0 };

        public void Set(Plane3 c)
        {
            Math3D.Set(normal, c.normal);
            dist = c.dist;
            type = c.type;
            signbits = c.signbits;
            pad[0] = c.pad[0];
            pad[1] = c.pad[1];
        }

        public void Clear()
        {
            Math3D.VectorClear(normal);
            dist = 0;
            type = 0;
            signbits = 0;
            pad[0] = 0;
            pad[1] = 0;
        }
    }
}