using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Excel = Microsoft.Office.Interop.Excel;


namespace Interop
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnClipboard_Click(object sender, RoutedEventArgs e)
        {
            var result = Win32NativeFunctions.GetClipboardData(0);
            txtResult.Text = result.ToString();

        }

        private void btnSpace_Click(object sender, RoutedEventArgs e)
        {
            ulong freeBytes, totalBytes, totalFreeBytes;
            if (!Win32NativeFunctions.GetDiskFreeSpaceEx(@"c:", out freeBytes, out totalBytes, out totalFreeBytes))
            {
                txtResult.Text = "Error occured: " + Marshal.GetExceptionForHR(Marshal.GetLastWin32Error()).Message;
            }
            else
            {
                txtResult.Text = "Free bytes: " + (freeBytes/1024/1024).ToString("n");
            }
        }

        private void btnGetText_Click(object sender, RoutedEventArgs e)
        {
            IntPtr hwnd = Win32NativeFunctions.FindWindow("dwm", null);

            var windows = Win32NativeFunctions.GetWindows();
            var wdExpress = Process.GetProcessesByName("wdexpress").First();

            var proc = Process.GetProcessById(6936);


            var all = Process.GetProcesses();
            var current = Process.GetCurrentProcess();

            Win32NativeFunctions.SetWindowText(proc.MainWindowHandle, "Albo niedziala");

            //var proc = Process.GetProcessesByName("dwm").First();
            //while (proc.MainWindowHandle == IntPtr.Zero)
            //{
            //    // Discard cached information about the process
            //    // because MainWindowHandle might be cached.
            //    proc.Refresh();

            //    Thread.Sleep(10);
            //}


            //var writer = new StringBuilder();
            //Win32NativeFunctions.GetWindowText(wdExpress.MainWindowHandle, writer, 1000000);

            //txtResult.Text = writer.ToString();
        }

        private void btnExcel_Click(object sender, RoutedEventArgs e)
        {
            Excel._Application excelApp = new Excel.Application();
            var workbook = excelApp.Workbooks.Add();
            Excel.Worksheet sheet = workbook.Worksheets[1];

            excelApp.Visible = true;

            sheet.Cells[1, 1].Value = "Value";
            sheet.Cells[1, 2].Value = "Value Squared";

            for (int i = 1; i <= 10; i++)
            {
                sheet.Cells[i + 1, 1].Value = i;
                sheet.Cells[i + 1, 2].Value = (i * i).ToString();
            }
            sheet.Columns[1].AutoFit();
            sheet.Columns[2].AutoFit();
        }
    }
}