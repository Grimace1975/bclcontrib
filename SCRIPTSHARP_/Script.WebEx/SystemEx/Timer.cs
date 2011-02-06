//#if !CODE_ANALYSIS
//namespace System
//#else
//using System;
//namespace SystemEx
//#endif
//{
//    #if CODE_ANALYSIS
//    public static class Timer
//    {
//        public static int Milliseconds()
//        {
//            return 0;
//        }
//    }
//#else
//    public static class Timer
//    {
//        public static int curtime = 0;
//        private static long baseTime = DateTime.Now.Ticks;

//        public static int Milliseconds()
//        {
//            long time = DateTime.Now.Ticks;
//            long delta = time - baseTime;
//            unchecked
//            {
//                if (delta < 0)
//                    delta += long.MaxValue + 1;
//            }
//            return curtime = (int)(delta);
//        }
//    }
//#endif
//}