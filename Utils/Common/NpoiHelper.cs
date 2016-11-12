using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using Insight.Utils.Entity;
using NPOI.SS.UserModel;

namespace Insight.Utils.Common
{
    public class NpoiHelper<T> where T : new()
    {
        private readonly Result _Result = new Result();

        /// <summary>
        /// 导入Excel文件
        /// </summary>
        /// <param name="path">文件路径</param>
        /// <param name="index">Sheet索引</param>
        /// <returns>Result</returns>
        public Result Import(string path, int index = 0)
        {
            if (!File.Exists(path))
            {
                _Result.FileNotExists();
                return _Result;
            }

            IWorkbook book;
            using (var file = new FileStream(path, FileMode.Open, FileAccess.Read))
            {
                book = WorkbookFactory.Create(file);
            }

            if (index > book.NumberOfSheets)
            {
                _Result.SheetNotExists();
                return _Result;
            }

            var sheet = book.GetSheetAt(index);
            var table = GetSheetData(sheet);
            var list = Util.ConvertToList<T>(table);
            _Result.Success(list);
            return _Result;
        }

        /// <summary>
        /// 导出Excel文件
        /// </summary>
        /// <param name="list">数据集合</param>
        /// <returns>Result</returns>
        public Result Export(List<T> list)
        {
            return _Result;
        }

        /// <summary>
        /// 读取Sheet中的数据到DataTable
        /// </summary>
        /// <param name="sheet">当前数据表</param>
        /// <returns>DataTable</returns>
        private DataTable GetSheetData(ISheet sheet)
        {
            var table = InitTable(sheet);
            if (table == null) return null;

            var rows = sheet.GetEnumerator();
            while (rows.MoveNext())
            {
                var row = (IRow) rows.Current;
                if (row.RowNum == 0) continue;

                var dr = table.NewRow();
                for (var i = 0; i < table.Columns.Count; i++)
                {
                    try
                    {
                        dr[i] = GetCellData(row.GetCell(i));
                    }
                    catch (Exception)
                    {
                        dr[i] = DBNull.Value;
                    }

                }
                table.Rows.Add(dr);
            }

            return table;
        }

        /// <summary>
        /// 初始化DataTable
        /// </summary>
        /// <param name="sheet">当前数据表</param>
        /// <returns>DataTable</returns>
        private DataTable InitTable(ISheet sheet)
        {
            var title = sheet.GetRow(0);
            if (title == null)
            {
                _Result.NoRowsRead();
                return null;
            }

            try
            {
                var dict = GetDictionary();
                var table = new DataTable();
                foreach (var cell in title.Cells)
                {
                    var col_name = cell.StringCellValue;
                    var col_type = dict[col_name];
                    table.Columns.Add(cell.StringCellValue, col_type);
                }

                return table;
            }
            catch
            {
                _Result.IncorrectExcelFormat();
                return null;
            }
        }

        /// <summary>
        /// 读Excel单元格的数据
        /// </summary>
        /// <param name="cell">Excel单元格</param>
        /// <returns>T 单元格数据</returns>
        private object GetCellData(ICell cell)
        {
            switch (cell.CellType)
            {
                case CellType.Numeric:
                    try
                    {
                        return cell.DateCellValue;
                    }
                    catch (InvalidOperationException)
                    {
                        return cell.NumericCellValue;
                    }

                case CellType.String:
                    try
                    {
                        return cell.DateCellValue;
                    }
                    catch (InvalidOperationException)
                    {
                        return cell.StringCellValue;
                    }

                case CellType.Boolean:
                    return cell.BooleanCellValue;

                case CellType.Unknown:
                case CellType.Formula:
                case CellType.Blank:
                case CellType.Error:
                    return null;
                default:
                    return null;
            }
        }

        /// <summary>
        /// 获取指定类型的属性名称/类型字典
        /// </summary>
        /// <returns>Dictionary</returns>
        private Dictionary<string, Type> GetDictionary()
        {
            var dict = new Dictionary<string, Type>();
            var propertys = typeof(T).GetProperties();
            foreach (var p in propertys)
            {
                string name;
                var attributes = p.GetCustomAttributes(typeof(AliasAttribute), false);
                if (attributes.Length > 0)
                {
                    var type = (AliasAttribute)attributes[0];
                    name = type.Alias;
                }
                else
                {
                    name = p.Name;
                }

                dict.Add(name, p.PropertyType);
            }

            return dict;
        }
    }
}
