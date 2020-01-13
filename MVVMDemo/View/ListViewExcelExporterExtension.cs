using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Reflection;
using System.Windows.Controls;
using System.Globalization;
using System.Threading;

using Microsoft.Office.Interop.Excel;



namespace MVVMDemo
{
    public class ListViewExcelExporterExtension
    {
        /// <summary>
        /// ExcelExporter Attached Dependency Property
        /// </summary>
        public static readonly DependencyProperty ExcelExporterProperty =
            DependencyProperty.RegisterAttached("ExcelExporter",
            typeof(ListView), typeof(ListViewExcelExporterExtension),
                new FrameworkPropertyMetadata(null,
                    new PropertyChangedCallback(OnExcelExportRequest)));


        /// <summary>
        /// Gets the ExcelExporter property. 
        /// </summary>
        public static ListView GetExcelExporter(DependencyObject d)
        {
            return (ListView)d.GetValue(ExcelExporterProperty);
        }

        /// <summary>
        /// Sets the ExcelExporter property. 
        /// </summary>
        public static void SetExcelExporter(DependencyObject d, ListView value)
        {
            d.SetValue(ExcelExporterProperty, value);
        }

        /// <summary>
        /// Handles changes to the FilterSource property.
        /// </summary>
        private static void OnExcelExportRequest(DependencyObject d,
            DependencyPropertyChangedEventArgs e)
        {
            System.Windows.Controls.Button button = d as System.Windows.Controls.Button;
            ListView listView = e.NewValue as ListView;

            if ((button != null) && (listView != null))
            {
                button.Click += delegate (object sender, RoutedEventArgs er)
                {
                    Microsoft.Office.Interop.Excel.Application application =
                        new Microsoft.Office.Interop.Excel.Application();
                    application.Visible = true;

                    // Workaround to fix MCST bug - The threads culture info must be en-US for the Excel sheet
                    //                              to open properly
                    CultureInfo currentCulture = Thread.CurrentThread.CurrentCulture;
                    Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");

                    Workbook workbook = application.Workbooks.Add(Missing.Value);
                    Worksheet worksheet = workbook.ActiveSheet as Worksheet;
                    if (worksheet != null)
                    {
                        // Set the display to show from left to right
                        worksheet._DisplayRightToLeft = 0;

                        if (listView.Items.Count > 0)
                        {
                            // Write the header
                            IEnhancedListObject vm = listView.Items[0] as IEnhancedListObject;
                            int nRow = 1;
                            int nColumn = 1;
                            if (vm != null)
                            {
                                vm.WriteHeaderIntoExcelSheet(worksheet, ref nRow, ref nColumn);
                            }
                            // Write all the elements in the view model
                            foreach (var item in listView.Items)
                            {
                                vm = item as IEnhancedListObject;
                                vm.WriteDataIntoExcelSheet(worksheet, ref nRow, ref nColumn);
                            }
                        }
                    }
                    // Work around MSFT bug - return the culture info to the previous set
                    Thread.CurrentThread.CurrentCulture = currentCulture;
                };
            }
        }
    }
}
