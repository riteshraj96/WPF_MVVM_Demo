using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Office.Interop.Excel;

namespace MVVMDemo
{
    /// <summary>
    /// This is an iterface for every object which can have its data filtered given a certain string
    /// </summary>
    public interface IEnhancedListObject
    {
        /// <summary>
        /// Filter the data given a filter string
        /// </summary>
        /// <param name="strFilter"></param>
        /// <returns></returns>
        //bool Filter(string strFilter);
        /// <summary>
        /// Write the Header into the excel sheet
        /// </summary>
        /// <param name="worksheet">The worksheet to use</param>
        /// <param name="nRow">The row to start with</param>
        /// <param name="nColumn">The column to start with</param>
        /// <returns></returns>
        void WriteHeaderIntoExcelSheet(Worksheet worksheet, ref int nRow, ref int nColumn);
        /// <summary>
        /// Write the data into an Excel worksheet
        /// </summary>
        /// <param name="worksheet">The worksheet to use</param>
        /// <param name="nRow">The row to start with</param>
        /// <param name="nColumn">The column to start with</param>
        /// <returns></returns>
        void WriteDataIntoExcelSheet(Worksheet worksheet, ref int nRow, ref int nColumn);
    }
}
