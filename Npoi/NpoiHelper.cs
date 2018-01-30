using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Insight.Utils.Npoi.Attribute;
using Insight.Utils.Npoi.Enum;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;

namespace Insight.Utils.Npoi
{
    public class NpoiHelper<T> where T : new()
    {
        /// <summary>
        /// 当前Sheet列标题
        /// </summary>
        private List<string> _title;

        /// <summary>
        /// 类型成员字段信息集合
        /// </summary>
        private List<FieldInfo> _fieldInfos;

        /// <summary>
        /// 指定类型的属性集合
        /// </summary>
        private readonly PropertyInfo[] _propertys = typeof(T).GetProperties();

        /// <summary>
        /// 工作簿
        /// </summary>
        private readonly IWorkbook _workbook;

        /// <summary>
        /// 构造方法,用于导出
        /// </summary>
        /// <param name="ver">导出的Excel文件版本，默认为2007版本</param>
        public NpoiHelper(ExcelVer ver = ExcelVer.XLS)
        {
            switch (ver)
            {
                case ExcelVer.XLS:
                    _workbook = new HSSFWorkbook();
                    break;
                case ExcelVer.XLSX:
                    _workbook = new XSSFWorkbook();
                    break;
                default:
                    _workbook = null;
                    break;
            }
        }

        /// <summary>
        /// 构造方法,用于从文件导入数据
        /// </summary>
        /// <param name="file">输入Excel文件(.xls|.xlsx)的路径</param>
        public NpoiHelper(string file) : this(new FileStream(file, FileMode.Open, FileAccess.Read))
        {
        }

        /// <summary>
        /// 构造方法,用于从文件导入数据
        /// </summary>
        /// <param name="data">输入字节流</param>
        public NpoiHelper(byte[] data) : this(new MemoryStream(data))
        {
        }

        /// <summary>
        /// 构造方法,用于从文件流导入数据
        /// </summary>
        /// <param name="stream">文件流</param>
        public NpoiHelper(Stream stream)
        {
            try
            {
                _workbook = new XSSFWorkbook(stream);
            }
            catch (Exception)
            {
                _workbook = new HSSFWorkbook(stream);
            }
        }

        /// <summary>
        /// 导入Excel文件中第一个Sheet的数据到指定类型的集合
        /// </summary>
        /// <returns>指定类型的集合</returns>
        public List<T> importExcel()
        {
            return importSheet(0);
        }

        /// <summary>
        /// 导入指定位置的Sheet的数据到指定类型的集合
        /// </summary>
        /// <param name="sheetIndex">Sheet位置</param>
        /// <returns>指定类型的集合</returns>
        public List<T> importSheet(int sheetIndex)
        {
            var sheet = _workbook.GetSheetAt(sheetIndex);

            return toList(sheet);
        }

        /// <summary>
        /// 导入指定名称的Sheet的数据到指定类型的集合
        /// </summary>
        /// <param name="sheetName">Sheet名称</param>
        /// <returns>指定类型的集合</returns>
        public List<T> importSheet(string sheetName)
        {
            var sheet = _workbook.GetSheet(sheetName);

            return toList(sheet);
        }

        /// <summary>
        /// 导出工作簿到Excel文件
        /// </summary>
        /// <param name="file"></param>
        public void exportFile(string file)
        {
            if (_workbook == null) return;

            using (var stream = new FileStream(file, FileMode.OpenOrCreate, FileAccess.Write))
            {
                var ms = exportStream();
                var data = ms.ToArray();
                stream.Write(data, 0, data.Length);
                stream.Flush();
            }
        }

        /// <summary>
        /// 导入集合数据并导出工作簿到Excel文件
        /// </summary>
        /// <param name="list"></param>
        /// <param name="file"></param>
        public void exportFile(List<T> list, string file)
        {
            exportFile(file, list, null);
        }

        /// <summary>
        /// 导入集合数据到指定名称的Sheet并导出工作簿到Excel文件
        /// </summary>
        /// <param name="file">输出Excel文件(.xls|.xlsx)的路径及文件名</param>
        /// <param name="list">输入数据集合</param>
        /// <param name="sheetName">Sheet名称</param>
        public void exportFile(string file, List<T> list, string sheetName)
        {
            createSheet(list, sheetName);
            exportFile(file);
        }

        /// <summary>
        /// 导出工作簿到文件流
        /// </summary>
        /// <returns>Stream 文件流</returns>
        public MemoryStream exportStream()
        {
            var stream = new MemoryStream();
            if (_workbook == null) return stream;

            _workbook.Write(stream);

            return stream;
        }

        /// <summary>
        /// 导入集合数据并导出工作簿到文件流
        /// </summary>
        /// <param name="list">输入数据集合</param>
        /// <returns>Stream 文件流</returns>
        public MemoryStream exportStream(List<T> list)
        {
            return exportStream(list, null);
        }

        /// <summary>
        /// 导入集合数据到指定名称的Sheet并导出工作簿到文件流
        /// </summary>
        /// <param name="list">输入数据集合</param>
        /// <param name="sheetName">Sheet名称</param>
        /// <returns>Stream 文件流</returns>
        public MemoryStream exportStream(List<T> list, string sheetName)
        {
            createSheet(list, sheetName);

            return exportStream();
        }

        /// <summary>
        /// 工作簿中创建一个Sheet并从集合导入数据
        /// </summary>
        /// <param name="list">输入数据集合</param>
        public void createSheet(List<T> list)
        {
            createSheet(list, null);
        }

        /// <summary>
        /// 工作簿中创建一个指定名称的Sheet并从集合导入数据
        /// </summary>
        /// <param name="list">输入数据集合</param>
        /// <param name="sheetName">Sheet名称</param>
        public void createSheet(List<T> list, string sheetName)
        {
            if (_workbook == null || list == null) return;

            if (string.IsNullOrEmpty(sheetName)) sheetName = $"Sheet{_workbook.NumberOfSheets + 1}";

            // 创建Sheet并生成标题行
            var infos = initFieldsInfo();
            var sheet = _workbook.CreateSheet(sheetName);
            var row = sheet.CreateRow(0);
            var i = -1;
            foreach (var info in infos)
            {
                var cell = row.CreateCell(i++, CellType.String);
                var columnName = info.columnName;
                cell.SetCellValue(columnName ?? info.fieldName);
            }

            // 根据字段类型设置单元格格式并生成数据
            i = 0;
            foreach (var item in list)
            {
                if (item == null) continue;

                row = sheet.CreateRow(i++);
                writeRow(infos, row, item);
            }
        }

        /// <summary>
        /// 从Sheet导入数据到集合
        /// </summary>
        /// <param name="sheet">Sheet</param>
        /// <returns>指定类型的集合</returns>
        private List<T> toList(ISheet sheet)
        {
            if (sheet == null) return null;

            // 初始化字段信息字典和标题字典
            initFieldsInfo();
            initTitel(sheet);

            // 如标题为空,则返回一个空集合
            var table = new List<T>();
            if (_title == null || _title.Count == 0)
            {
                return table;
            }

            // 从第二行开始读取正文内容(第一行为标题行)
            for (var i = 1; i <= sheet.LastRowNum; i++)
            {
                var row = sheet.GetRow(i);
                var item = readRow(row);
                table.Add(item);
            }

            return table;
        }

        /// <summary>
        /// 读取输入Row的数据到指定类型的对象实体
        /// </summary>
        /// <param name="row">输入的行数据</param>
        /// <returns>T 指定类型的数据对象</returns>
        private T readRow(IRow row)
        {
            if (row == null) return default(T);

            // 顺序读取行内的每个单元格的数据并赋值给对应的字段
            var nullCount = 0;
            var item = new T();
            for (var i = 0; i < _title.Count; i++)
            {
                var colName = _title[i];

                // 如当前单元格所在列未在指定类型中定义,则跳过该单元格
                var info = _fieldInfos.FirstOrDefault(f => colName == f.columnName || colName == f.fieldName);
                if (info == null)
                {
                    nullCount++;
                    continue;
                }

                // 读取单元格数据,如该属性/字段不允许写入或单元格值为空,则跳过该单元格
                var cell = row.GetCell(i);
                var value = readCell(cell, info.typeName);
                var property = _propertys.First(p => p.Name == info.fieldName);
                if (!property.CanWrite || value == null)
                {
                    nullCount++;
                    continue;
                }

                // 给字段赋值
                property.SetValue(item, value);
            }

            return nullCount < _title.Count ? item : default(T);
        }

        /// <summary>
        /// 读取单元格数据
        /// </summary>
        /// <param name="cell"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        private object readCell(ICell cell, string type)
        {
            if (cell == null) return null;

            switch (cell.CellType)
            {
                case CellType.String:
                    var value = cell.StringCellValue;
                    switch (type)
                    {
                        case "DateTime":
                            return DateTime.Parse(value);
                        case "Boolean":
                            return bool.Parse(value);
                        case "String":
                            return cell.StringCellValue;
                        default:
                            return cell.StringCellValue;
                    }
                case CellType.Numeric:
                    switch (type)
                    {
                        case "Date":
                            return cell.DateCellValue;
                        default:
                            return cell.NumericCellValue;
                    }
                case CellType.Formula:
                    switch (type)
                    {
                        case "Boolean":
                            return cell.BooleanCellValue;
                        case "Date":
                            return cell.DateCellValue;
                        case "String":
                            return cell.StringCellValue;
                        default:
                            return cell.NumericCellValue;
                    }
                case CellType.Boolean:
                    return cell.BooleanCellValue;
                default:
                    return null;
            }
        }

        /// <summary>
        /// 写入数据对象字段值到行数据
        /// </summary>
        /// <param name="infos">字段信息字典</param>
        /// <param name="row">行数据</param>
        /// <param name="item">指定类型的数据对象</param>
        private void writeRow(List<FieldInfo> infos, IRow row, T item)
        {
            for (var i = 0; i < infos.Count; i++)
            {
                var info = infos[i];
                var cellType = getCellType(info.typeName);
                var cell = row.CreateCell(i, cellType);

                var property = _propertys.First(p => p.Name == info.fieldName);
                if (!property.CanRead) continue;

                var value = property.GetValue(item)?.ToString();
                if (string.IsNullOrEmpty(value))
                {
                    continue;
                }

                switch (info.typeName)
                {
                    case "DateTime":
                        cell.SetCellValue(DateTime.Parse(value));
                        break;
                    default:
                        cell.SetCellValue(value);
                        break;
                }
            }
        }

        /// <summary>
        /// 根据字段类型获取对应的单元格格式
        /// </summary>
        /// <param name="type">属性/字段类型</param>
        /// <returns>单元格格式</returns>
        private CellType getCellType(string type)
        {
            switch (type)
            {
                case "Int16":
                case "Int32":
                case "Int64":
                case "Single":
                case "Double":
                case "Decimal":
                    return CellType.Numeric;
                case "Boolean":
                    return CellType.Boolean;
                default:
                    return CellType.String;
            }
        }

        /// <summary>
        /// 读取标题,生成标题和对应的数据类型的字典
        /// </summary>
        /// <param name="sheet">数据表</param>
        private void initTitel(ISheet sheet)
        {
            _title = new List<string>();
            var row = sheet.GetRow(0);
            if (row == null)
            {
                return;
            }

            for (var i = 0; i < row.LastCellNum; i++)
            {
                var cell = row.GetCell(i);
                _title.Add(cell == null ? "" : cell.StringCellValue);
            }
        }

        /// <summary>
        /// 生成指定类型对应的字段信息集合
        /// </summary>
        /// <returns>指定类型对应的需要导出的字段信息集合</returns>
        private List<FieldInfo> initFieldsInfo()
        {
            _fieldInfos = new List<FieldInfo>();
            foreach (var property in _propertys)
            {
                var info = new FieldInfo
                {
                    fieldName = property.Name,
                    typeName = property.PropertyType.Name
                };

                // 如读取到列名自定义特性
                var attributes = typeof(T).GetCustomAttributes(typeof(ColumnName), false);
                if (attributes.Length > 0 && attributes[0] is ColumnName att)
                {
                    info.columnName = att.name;
                    info.dateFormat = att.dateFormat;
                    info.columnPolicy = att.policy;
                }

                _fieldInfos.Add(info);
            }

            return _fieldInfos.Where(i => i.columnPolicy != Policy.Ignorable).ToList();
        }
    }
}