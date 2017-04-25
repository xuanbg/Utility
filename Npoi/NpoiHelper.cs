using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;

namespace Insight.Utils.Npoi
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
            if (table == null) return _Result;

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
                        var cell = row.GetCell(i);
                        switch (cell.CellType)
                        {
                            case CellType.Numeric:
                                // 如单元格格式为数字，判断列数据类型是否为DateTime
                                var type = table.Columns[i].DataType.Name;
                                if (type == "DateTime") dr[i] = cell.DateCellValue;
                                else dr[i] = cell.NumericCellValue;

                                break;
                            case CellType.Boolean:
                                dr[i] = cell.BooleanCellValue;
                                break;
                            case CellType.String:
                                dr[i] = cell.StringCellValue;
                                break;
                            default:
                                dr[i] = DBNull.Value;
                                break;
                        }
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

            // 创建标题行
            var row = sheet.CreateRow(0);
            var i = -1;
            foreach (DataColumn c in table.Columns)
            {
                var cell = row.CreateCell(i++, GetCellType(c.DataType));
                cell.SetCellValue(c.ColumnName);
            }

            // 创建数据行
            for (var j = 0; j < table.Rows.Count; j++)
            {
                row = sheet.CreateRow(j + 1);
                var r = -1;
                foreach (DataColumn c in table.Columns)
                {
                    var type = GetCellType(c.DataType);
                    var cell = row.CreateCell(r++, type);
                    var data = table.Rows[j].ItemArray[r++];
                    switch (type)
                    {
                        case CellType.Numeric:
                            cell.SetCellValue(Convert.ToDouble(data));
                            break;

                        case CellType.Boolean:
                            cell.SetCellValue(Convert.ToBoolean(data));
                            break;

                        default:
                            cell.SetCellValue(data.ToString());
                            break;
                    }
                }
            }
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
            foreach (var p in propertys)
            {
                var gta = p.PropertyType.GenericTypeArguments;
                var type = gta.Length > 0 ? gta[0] : p.PropertyType;
                dict.Add(Util.GetPropertyName(p), type);
            }

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
        /// 根据数据类型获取单元格格式
        /// </summary>
        /// <param name="type">数据类型</param>
        /// <returns>CellType</returns>
        private CellType GetCellType(Type type)
        {
            switch (type.Name)
            {
                case "Boolean":
                    return CellType.Boolean;

                case "Int16":
                case "Int32":
                case "Int64":
                case "Single":
                case "Double":
                case "Decimal":
                    return CellType.Numeric;

                default:
                    return CellType.String;
            }
        }
    }
}
