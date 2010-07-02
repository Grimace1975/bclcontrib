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
using System.Runtime.InteropServices;
namespace System.Interopt.AudioVideo.Native_.DirectShow
{
    /// <summary>
    /// IVideoWindow
    /// </summary>
    [ComImport, Guid("56A868B4-0AD4-11CE-B03A-0020AF0BA770"), InterfaceType(ComInterfaceType.InterfaceIsDual)]
    public interface IVideoWindow
    {
        /// <summary>
        /// Put_s the caption.
        /// </summary>
        /// <param name="caption">The caption.</param>
        /// <returns></returns>
        [PreserveSig]
        int put_Caption(string caption);
        /// <summary>
        /// Get_s the caption.
        /// </summary>
        /// <param name="caption">The caption.</param>
        /// <returns></returns>
        [PreserveSig]
        int get_Caption([Out] out string caption);
        /// <summary>
        /// Put_s the window style.
        /// </summary>
        /// <param name="windowStyle">The window style.</param>
        /// <returns></returns>
        [PreserveSig]
        int put_WindowStyle(int windowStyle);
        /// <summary>
        /// Get_s the window style.
        /// </summary>
        /// <param name="windowStyle">The window style.</param>
        /// <returns></returns>
        [PreserveSig]
        int get_WindowStyle(out int windowStyle);
        /// <summary>
        /// Put_s the window style ex.
        /// </summary>
        /// <param name="windowStyleEx">The window style ex.</param>
        /// <returns></returns>
        [PreserveSig]
        int put_WindowStyleEx(int windowStyleEx);
        /// <summary>
        /// Get_s the window style ex.
        /// </summary>
        /// <param name="windowStyleEx">The window style ex.</param>
        /// <returns></returns>
        [PreserveSig]
        int get_WindowStyleEx(out int windowStyleEx);
        /// <summary>
        /// Put_s the auto show.
        /// </summary>
        /// <param name="autoShow">if set to <c>true</c> [auto show].</param>
        /// <returns></returns>
        [PreserveSig]
        int put_AutoShow([In, MarshalAs(UnmanagedType.Bool)] bool autoShow);
        /// <summary>
        /// Get_s the auto show.
        /// </summary>
        /// <param name="autoShow">if set to <c>true</c> [auto show].</param>
        /// <returns></returns>
        [PreserveSig]
        int get_AutoShow([Out, MarshalAs(UnmanagedType.Bool)] out bool autoShow);
        /// <summary>
        /// Put_s the state of the window.
        /// </summary>
        /// <param name="windowState">State of the window.</param>
        /// <returns></returns>
        [PreserveSig]
        int put_WindowState(int windowState);
        /// <summary>
        /// Get_s the state of the window.
        /// </summary>
        /// <param name="windowState">State of the window.</param>
        /// <returns></returns>
        [PreserveSig]
        int get_WindowState(out int windowState);
        /// <summary>
        /// Put_s the background palette.
        /// </summary>
        /// <param name="backgroundPalette">if set to <c>true</c> [background palette].</param>
        /// <returns></returns>
        [PreserveSig]
        int put_BackgroundPalette([In, MarshalAs(UnmanagedType.Bool)] bool backgroundPalette);
        /// <summary>
        /// Get_s the background palette.
        /// </summary>
        /// <param name="backgroundPalette">if set to <c>true</c> [background palette].</param>
        /// <returns></returns>
        [PreserveSig]
        int get_BackgroundPalette([Out, MarshalAs(UnmanagedType.Bool)] out bool backgroundPalette);
        /// <summary>
        /// Put_s the visible.
        /// </summary>
        /// <param name="visible">if set to <c>true</c> [visible].</param>
        /// <returns></returns>
        [PreserveSig]
        int put_Visible([In, MarshalAs(UnmanagedType.Bool)] bool visible);
        /// <summary>
        /// Get_s the visible.
        /// </summary>
        /// <param name="visible">if set to <c>true</c> [visible].</param>
        /// <returns></returns>
        [PreserveSig]
        int get_Visible([Out, MarshalAs(UnmanagedType.Bool)] out bool visible);
        /// <summary>
        /// Put_s the left.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <returns></returns>
        [PreserveSig]
        int put_Left(int left);
        /// <summary>
        /// Get_s the left.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <returns></returns>
        [PreserveSig]
        int get_Left(out int left);
        /// <summary>
        /// Put_s the width.
        /// </summary>
        /// <param name="width">The width.</param>
        /// <returns></returns>
        [PreserveSig]
        int put_Width(int width);
        /// <summary>
        /// Get_s the width.
        /// </summary>
        /// <param name="width">The width.</param>
        /// <returns></returns>
        [PreserveSig]
        int get_Width(out int width);
        /// <summary>
        /// Put_s the top.
        /// </summary>
        /// <param name="top">The top.</param>
        /// <returns></returns>
        [PreserveSig]
        int put_Top(int top);
        /// <summary>
        /// Get_s the top.
        /// </summary>
        /// <param name="top">The top.</param>
        /// <returns></returns>
        [PreserveSig]
        int get_Top(out int top);
        /// <summary>
        /// Put_s the height.
        /// </summary>
        /// <param name="height">The height.</param>
        /// <returns></returns>
        [PreserveSig]
        int put_Height(int height);
        /// <summary>
        /// Get_s the height.
        /// </summary>
        /// <param name="height">The height.</param>
        /// <returns></returns>
        [PreserveSig]
        int get_Height(out int height);
        /// <summary>
        /// Put_s the owner.
        /// </summary>
        /// <param name="owner">The owner.</param>
        /// <returns></returns>
        [PreserveSig]
        int put_Owner(IntPtr owner);
        /// <summary>
        /// Get_s the owner.
        /// </summary>
        /// <param name="owner">The owner.</param>
        /// <returns></returns>
        [PreserveSig]
        int get_Owner(out IntPtr owner);
        /// <summary>
        /// Put_s the message drain.
        /// </summary>
        /// <param name="drain">The drain.</param>
        /// <returns></returns>
        [PreserveSig]
        int put_MessageDrain(IntPtr drain);
        /// <summary>
        /// Get_s the message drain.
        /// </summary>
        /// <param name="drain">The drain.</param>
        /// <returns></returns>
        [PreserveSig]
        int get_MessageDrain(out IntPtr drain);
        /// <summary>
        /// Get_s the color of the border.
        /// </summary>
        /// <param name="color">The color.</param>
        /// <returns></returns>
        [PreserveSig]
        int get_BorderColor(out int color);
        /// <summary>
        /// Put_s the color of the border.
        /// </summary>
        /// <param name="color">The color.</param>
        /// <returns></returns>
        [PreserveSig]
        int put_BorderColor(int color);
        /// <summary>
        /// Get_s the full screen mode.
        /// </summary>
        /// <param name="fullScreenMode">if set to <c>true</c> [full screen mode].</param>
        /// <returns></returns>
        [PreserveSig]
        int get_FullScreenMode([Out, MarshalAs(UnmanagedType.Bool)] out bool fullScreenMode);
        /// <summary>
        /// Put_s the full screen mode.
        /// </summary>
        /// <param name="fullScreenMode">if set to <c>true</c> [full screen mode].</param>
        /// <returns></returns>
        [PreserveSig]
        int put_FullScreenMode([In, MarshalAs(UnmanagedType.Bool)] bool fullScreenMode);
        /// <summary>
        /// Sets the window foreground.
        /// </summary>
        /// <param name="focus">The focus.</param>
        /// <returns></returns>
        [PreserveSig]
        int SetWindowForeground(int focus);
        /// <summary>
        /// Notifies the owner message.
        /// </summary>
        /// <param name="hwnd">The HWND.</param>
        /// <param name="msg">The MSG.</param>
        /// <param name="wParam">The w param.</param>
        /// <param name="lParam">The l param.</param>
        /// <returns></returns>
        [PreserveSig]
        int NotifyOwnerMessage(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam);
        /// <summary>
        /// Sets the window position.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="top">The top.</param>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        /// <returns></returns>
        [PreserveSig]
        int SetWindowPosition(int left, int top, int width, int height);
        /// <summary>
        /// Gets the window position.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="top">The top.</param>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        /// <returns></returns>
        [PreserveSig]
        int GetWindowPosition(out int left, out int top, out int width, out int height);
        /// <summary>
        /// Gets the size of the min ideal image.
        /// </summary>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        /// <returns></returns>
        [PreserveSig]
        int GetMinIdealImageSize(out int width, out int height);
        /// <summary>
        /// Gets the size of the max ideal image.
        /// </summary>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        /// <returns></returns>
        [PreserveSig]
        int GetMaxIdealImageSize(out int width, out int height);
        /// <summary>
        /// Gets the restore position.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="top">The top.</param>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        /// <returns></returns>
        [PreserveSig]
        int GetRestorePosition(out int left, out int top, out int width, out int height);
        /// <summary>
        /// Hides the cursor.
        /// </summary>
        /// <param name="hideCursor">if set to <c>true</c> [hide cursor].</param>
        /// <returns></returns>
        [PreserveSig]
        int HideCursor([In, MarshalAs(UnmanagedType.Bool)] bool hideCursor);
        /// <summary>
        /// Determines whether [is cursor hidden] [the specified hide cursor].
        /// </summary>
        /// <param name="hideCursor">if set to <c>true</c> [hide cursor].</param>
        /// <returns></returns>
        [PreserveSig]
        int IsCursorHidden([Out, MarshalAs(UnmanagedType.Bool)] out bool hideCursor);
    }
}
