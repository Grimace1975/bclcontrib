using System;
namespace SystemEx
{
    public class JSArrayEx
    {
        public static void Clear(Array array, int index, int length)
        {
        }

        // http://forum.unity3d.com/threads/28806-copying-arrays-in-javascript
        public static void Copy(Array sourceArray, int sourceIndex, Array destinationArray, int destinationIndex, int index)
        {
            Script.Literal("System.Arr", sourceArray, sourceIndex, destinationArray, destinationIndex, index);
        }
    }
}
