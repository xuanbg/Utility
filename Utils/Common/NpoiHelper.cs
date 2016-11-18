using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using Insight.Utils.Entity;
using NPOI.HSSF.UserModel;
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

            if (index >= book.NumberOfSheets)
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
            if (!list.Any())
            {
                _Result.NoContent();
                return _Result;
            }

            var book = new HSSFWorkbook();
            var sheet = book.CreateSheet();
            SetSheetData(sheet, list);
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
                        var type = table.Columns[i].DataType;
                        dr[i] = GetCellData(row.GetCell(i), type);
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
        /// 将List数据写入Sheet
        /// </summary>
        /// <param name="sheet">当前数据表</param>
        /// <param name="list">数据集合</param>
        private void SetSheetData(ISheet sheet, List<T> list)
        {
            var table = Util.ConvertToDataTable(list);
            //var row = sheet.CreateRow(0);
            //for (var i = 0; i < dict.Count; i++)
            //{
            //    var cell = row.CreateCell(i, CellType.String);
            //    cell.SetCellValue(dict["a"].);
            //}
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

            // 根据类型属性数据类型建立列数据类型字典
            var dict = new Dictionary<string, Type>();
            var propertys = typeof(T).GetProperties().ToList();
            propertys.ForEach(p => dict.Add(Util.GetPropertyName(p), p.PropertyType));

            // 使用列数据类型字典构建一个和Sheet一致的DataTable
            // 如Sheet中列与指定类型不匹配，则捕获异常后返回格式不正确的错误
            var table = new DataTable();
            try
            {
                title.Cells.ForEach(c => table.Columns.Add(c.StringCellValue, dict[c.StringCellValue]));
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
        /// <param name="type">列数据类型</param>
        /// <returns>object 单元格数据</returns>
        private object GetCellData(ICell cell, Type type)
        {
            switch (cell.CellType)
            {
                case CellType.Numeric:
                    if (GetType(type) == PropertyType.DateTime) return cell.DateCellValue;

                    return cell.NumericCellValue;

                case CellType.String:
                    switch (GetType(type))
                    {
                        case PropertyType.DateTime:
                            return cell.DateCellValue;

                        case PropertyType.Numeric:
                            return cell.NumericCellValue;

                        default:
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
        /// 获取数据类型
        /// </summary>
        /// <param name="type">数据类型</param>
        /// <returns>PropertyType</returns>
        private PropertyType GetType(Type type)
        {
            switch (type.Name)
            {
                case "DateTime":
                    return PropertyType.DateTime;

                case "bool":
                    return PropertyType.Boolean;

                case "double":
                case "float":
                case "int":
                case "decimal":
                    return PropertyType.Numeric;

                default:
                    return PropertyType.String;
            }
        }

        /// <summary>
        /// 属性类型
        /// </summary>
        internal enum PropertyType
        {
            Numeric,
            DateTime,
            String,
            Boolean
        }
    }
}
