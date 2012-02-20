using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows;
using System.Windows.Interop;

namespace WpfApp
{
    public static class WindowExtensions
    {
        /// <summary>
        /// Stop flashing. The system restores the window to its original state
        /// </summary>
        private const UInt32 FLASHW_STOP = 0; 
        /// <summary>
        /// Flash only caption of the window
        /// </summary>
        private const UInt32 FLASHW_CAPTION = 1; 
        /// <summary>
        /// Flash only taskbar icon
        /// </summary>
        private const UInt32 FLASHW_TRAY = 2;
        /// <summary>
        /// Flash both window caption and the taskbar icon
        /// </summary>
        private const UInt32 FLASHW_ALL = 3; 
        /// <summary>
        /// Flash continuously until <see cref="FLASHW_STOP"/> flag is set
        /// </summary>
        private const UInt32 FLASHW_TIMER = 4; 
        /// <summary>
        /// Flash continuously until window comes to a foreground
        /// </summary>
        private const UInt32 FLASHW_TIMERNOFG = 12; 

        [StructLayout(LayoutKind.Sequential)]
        private struct FLASHWINFO
        {
            public UInt32 cbSize;
            public IntPtr hwnd;
            public UInt32 dwFlags;
            public UInt32 uCount;
            public UInt32 dwTimeout;
        }

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool FlashWindowEx(ref FLASHWINFO info);

        public static void FlashWindow(this Window window, UInt32 count = UInt32.MaxValue)
        {
            //if (window.IsActive)
            //{
            //    return;
            //}

            WindowInteropHelper helper = new WindowInteropHelper(window);
            var info = new FLASHWINFO()
                           {
                               hwnd = helper.Handle,
                               dwFlags = FLASHW_ALL | FLASHW_TIMER,
                               uCount = count,
                               dwTimeout = 0
                           };
            info.cbSize = Convert.ToUInt32(Marshal.SizeOf(info));
            FlashWindowEx(ref info);
        }

        public static void StopFlashingWindow(this Window window)
        {
            WindowInteropHelper helper = new WindowInteropHelper(window);
            var info = new FLASHWINFO()
            {
                hwnd = helper.Handle,
                dwFlags = FLASHW_STOP,
                uCount = UInt32.MaxValue,
                dwTimeout = 0
            };
            info.cbSize = Convert.ToUInt32(Marshal.SizeOf(info));
            FlashWindowEx(ref info);
        }
    }
}