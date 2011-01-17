using System;
namespace SystemEx
{
    public class JSArrayEx
    {
        public static void Clear(Array array, int index, int length)
        {
        }

        public static void Copy(object source, int sourceIndex, object destination, int destinationIndex, int length)
        {
            if ((source == null) || (destination == null))
                throw new Exception("NullPointerException:");
            ////Type srcType = source.GetType();
            ////Type destType = destination.GetType();
            ////if (!srcType.isArray() || !destType.isArray())
            ////    throw new Exception("ArrayStoreException: Must be array types");
            ////Type srcComp = srcType.getComponentType();
            ////Type destComp = destType.getComponentType();
            ////if ((srcComp.modifiers != destComp.modifiers) || (srcComp.isPrimitive() && (!srcComp == destComp)))
            ////    throw new Exception("ArrayStoreException: Array types must match");
            int sourceLength = (int)Script.Literal("{0}.length", source);
            int destinationLength = (int)Script.Literal("{0}.length", destination);
            if ((sourceIndex < 0) || (destinationIndex < 0) || (length < 0) || (sourceIndex + length > sourceLength) || (destinationIndex + length > destinationLength))
                throw new Exception("IndexOutOfBoundsException:");
            InternalNativeCopy((object[])source, sourceIndex, (object[])destination, destinationIndex, length);
        }

        // TODO(jgw): using Function.apply() blows up for large arrays (around 8k items at least).
        private static void InternalNativeCopy(object[] source, int sourceIndex, object[] destination, int destinationIndex, int length)
        {
            if ((source == destination) && (sourceIndex < destinationIndex))
            {
                sourceIndex += length;
                for (int index = destinationIndex + length; index-- > destinationIndex; )
                    destination[index] = source[--sourceIndex];
            }
            else
                for (int index = destinationIndex + length; destinationIndex < index; )
                    destination[destinationIndex++] = source[sourceIndex++];
            //Script.Literal(@"
            //if (({0} == {2}) && ({1} < {3}))
            //{
            //    {1} += {4};
            //    for (var index = {3} + {4}; index-- > {3};)
            //        {2}[index] = {0}[--{1}];
            //} else
            //    for (var index = {3} + {4}; {3} < index;)
            //        {2}[{3}++] = {0}[{1}++];
            //", source, sourceIndex, destination, destinationIndex, length);
        }
    }
}
