﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using Insight.Utils.ExcelHelper.Attribute;
using Insight.Utils.ExcelHelper.Enum;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using static Insight.Utils.ExcelHelper.Enum.ExcelVer;
using static Insight.Utils.ExcelHelper.Enum.Policy;

namespace Insight.Utils.ExcelHelper
{
    public class NpoiHelper
    {
        /// <summary>
        /// 当前Sheet列标题
        /// </summary>
        private List<string> title;

        /// <summary>
        /// 类型成员字段信息集合
        /// </summary>
        private List<FieldInfo> fieldInfos;

        /// <summary>
        /// 类型成员字段信息集合
        /// </summary>
        private List<FieldInfo> exportFields;

        /// <summary>
        /// 工作簿
        /// </summary>
        private readonly IWorkbook workbook;

        /// <summary>
        /// 构造方法,用于导出Excel文件
        /// </summary>
        /// <param name="ver">导出的Excel文件版本，默认为2007版本</param>
        public NpoiHelper(ExcelVer ver = XLS)
        {
            switch (ver)
            {
                case XLS:
                    workbook = new HSSFWorkbook();
                    break;
                case XLSX:
                    workbook = new XSSFWorkbook();
                    break;
                default:
                    workbook = null;
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
        /// 构造方法,用于从字节数组导入数据
        /// </summary>
        /// <param name="data">输入字节流</param>
        public NpoiHelper(byte[] data) : this(new MemoryStream(data))
        {
        }

        /// <summary>
        /// 构造方法,用于从数据流导入数据
        /// </summary>
        /// <param name="stream">文件流</param>
        public NpoiHelper(Stream stream)
        {
            try
            {
                workbook = new XSSFWorkbook(stream);
            }
            catch (Exception)
            {
                workbook = new HSSFWorkbook(stream);
            }
        }

        /// <summary>
        /// 指定位置的Sheet是否存在
        /// </summary>
        /// <param name="sheetIndex">Sheet位置</param>
        /// <returns>Sheet是否存在</returns>
        public bool sheetIsExist(int sheetIndex)
        {
            return workbook.GetSheetAt(sheetIndex) != null;
        }

        /// <summary>
        /// 指定名称的Sheet是否存在
        /// </summary>
        /// <param name="sheetName">Sheet名称</param>
        /// <returns>Sheet是否存在</returns>
        public bool sheetIsExist(string sheetName)
        {
            return workbook.GetSheet(sheetName) != null;
        }

        /// <summary>
        /// 校验指定位置的Sheet是否包含关键列
        /// </summary>
        /// <param name="sheetIndex">Sheet位置</param>
        /// <param name="keys">关键列名称(英文逗号分隔)</param>
        /// <returns>是否通过校验</returns>
        public bool verifyColumns(int sheetIndex, string keys)
        {
            var sheetName = workbook.GetSheetName(sheetIndex);

            return verifyColumns(sheetName, keys);
        }

        /// <summary>
        /// 校验指定名称的Sheet是否包含关键列
        /// </summary>
        /// <param name="sheetName">Sheet名称</param>
        /// <param name="keys">关键列名称(英文逗号分隔)</param>
        /// <returns>是否通过校验</returns>
        public bool verifyColumns(string sheetName, string keys)
        {
            if (string.IsNullOrEmpty(keys)) return false;

            var sheet = workbook.GetSheet(sheetName);
            if (sheet == null) return false;

            initTitle(sheet);
            if (title == null || title.Count == 0) return false;

            return !keys.Split(',').Except(title).Any();
        }

        /// <summary>
        /// 校验指定位置的Sheet是否包含关键列
        /// </summary>
        /// <typeparam name="T">类型参数</typeparam>
        /// <param name="sheetIndex">Sheet位置</param>
        /// <returns>是否通过校验</returns>
        public bool verifyColumns<T>(int sheetIndex)
        {
            var sheetName = workbook.GetSheetName(sheetIndex);

            return verifyColumns<T>(sheetName);
        }

        /// <summary>
        /// 校验指定名称的Sheet是否包含关键列
        /// </summary>
        /// <typeparam name="T">类型参数</typeparam>
        /// <param name="sheetName">Sheet名称</param>
        /// <returns>是否通过校验</returns>
        public bool verifyColumns<T>(string sheetName)
        {
            var sheet = workbook.GetSheet(sheetName);
            if (sheet == null) return false;

            // 读取标题
            initTitle(sheet);
            if (title == null || title.Count == 0) return false;

            // 读取关键列到集合并取标题集合的差集
            var list = new List<string>();
            foreach (var property in typeof(T).GetProperties())
            {
                var attributes = property.GetCustomAttributes(typeof(ColumnName), false);
                if (attributes.FirstOrDefault() is ColumnName att && att.policy == REQUIRED)
                {
                    list.Add(att.name);
                }
            }

            return !list.Except(title).Any();
        }

        /// <summary>
        /// 导出工作簿到Excel文件
        /// </summary>
        /// <param name="file"></param>
        public void exportFile(string file)
        {
            if (workbook == null) return;

            using (var stream = new FileStream(file, FileMode.OpenOrCreate, FileAccess.Write))
            {
                var ms = exportStream();
                var data = ms.ToArray();
                stream.Write(data, 0, data.Length);
                stream.Flush();
            }
        }

        /// <summary>
        /// 使用指定的数据集生成指定名称的Sheet并导出工作簿到Excel文件
        /// </summary>
        /// <typeparam name="T">类型参数</typeparam>
        /// <param name="file">输出Excel文件(.xls|.xlsx)的路径及文件名</param>
        /// <param name="list">输入数据集合</param>
        /// <param name="sheetName">Sheet名称</param>
        public void exportFile<T>(string file, List<T> list, string sheetName = null)
        {
            createSheet(list, sheetName);
            exportFile(file);
        }

        /// <summary>
        /// 导出工作簿到数据流
        /// </summary>
        /// <returns>Stream 文件流</returns>
        public MemoryStream exportStream()
        {
            var stream = new MemoryStream();
            if (workbook == null) return stream;

            workbook.Write(stream);

            return stream;
        }

        /// <summary>
        /// 使用指定的数据集生成指定名称的Sheet并导出工作簿到数据流
        /// </summary>
        /// <typeparam name="T">类型参数</typeparam>
        /// <param name="list">输入数据集合</param>
        /// <param name="sheetName">Sheet名称</param>
        /// <returns>Stream 文件流</returns>
        public MemoryStream exportStream<T>(List<T> list, string sheetName = null)
        {
            createSheet(list, sheetName);

            return exportStream();
        }

        /// <summary>
        /// 导出工作簿到字节数组
        /// </summary>
        /// <returns>字节流</returns>
        public byte[] exportByteArray()
        {
            return exportStream().ToArray();
        }

        /// <summary>
        /// 使用指定的数据集生成Sheet并导出工作簿到字节数组
        /// </summary>
        /// <typeparam name="T">类型参数</typeparam>
        /// <param name="list">输入数据集合</param>
        /// <returns>字节流</returns>
        public byte[] exportByteArray<T>(List<T> list)
        {
            return exportStream(list).ToArray();
        }

        /// <summary>
        /// 使用指定的数据集生成指定名称的Sheet并导出工作簿到字节数组
        /// </summary>
        /// <typeparam name="T">类型参数</typeparam>
        /// <param name="list">输入数据集合</param>
        /// <param name="sheetName">Sheet名称</param>
        /// <returns>字节流</returns>
        public byte[] exportByteArray<T>(List<T> list, string sheetName)
        {
            return exportStream(list, sheetName).ToArray();
        }

        /// <summary>
        /// 导入Excel文件中第一个Sheet的数据到指定类型的集合
        /// </summary>
        /// <typeparam name="T">类型参数</typeparam>
        /// <returns>指定类型的集合</returns>
        public List<T> importSheet<T>() where T : new()
        {
            return importSheet<T>(0);
        }

        /// <summary>
        /// 导入指定位置的Sheet的数据到指定类型的集合
        /// </summary>
        /// <typeparam name="T">类型参数</typeparam>
        /// <param name="sheetIndex">Sheet位置</param>
        /// <returns>指定类型的集合</returns>
        public List<T> importSheet<T>(int sheetIndex) where T : new()
        {
            var sheetName = workbook.GetSheetName(sheetIndex);

            return importSheet<T>(sheetName);
        }

        /// <summary>
        /// 导入指定名称的Sheet的数据到指定类型的集合
        /// </summary>
        /// <typeparam name="T">类型参数</typeparam>
        /// <param name="sheetName">Sheet名称</param>
        /// <returns>指定类型的集合</returns>
        public List<T> importSheet<T>(string sheetName) where T : new()
        {
            var sheet = workbook.GetSheet(sheetName);

            return toList<T>(sheet);
        }

        /// <summary>
        /// 创建一个用于导入数据的模板Sheet
        /// </summary>
        /// <typeparam name="T">类型参数</typeparam>
        public void createTemplate<T>()
        {
            createTemplate<T>(null);
        }

        /// <summary>
        /// 创建一个用于导入数据且指定名称的模板Sheet
        /// </summary>
        /// <typeparam name="T">类型参数</typeparam>
        /// <param name="sheetName">Sheet名称</param>
        public void createTemplate<T>(string sheetName)
        {
            if (workbook == null) return;

            createTitle<T>(sheetName);
        }

        /// <summary>
        /// 使用指定的数据集在工作簿中创建一个指定名称的Sheet
        /// </summary>
        /// <typeparam name="T">类型参数</typeparam>
        /// <param name="list">输入数据集合</param>
        /// <param name="sheetName">Sheet名称</param>
        public void createSheet<T>(List<T> list, string sheetName = null)
        {
            if (workbook == null || list == null) return;

            // 创建Sheet并生成标题行
            var sheet = createTitle<T>(sheetName);

            // 根据字段类型设置单元格格式并生成数据
            var i = 1;
            foreach (var item in list)
            {
                if (item == null) continue;

                var row = sheet.CreateRow(i++);
                writeRow(row, item);
            }
        }

        /// <summary>
        /// 从Sheet导入数据到集合
        /// </summary>
        /// <typeparam name="T">类型参数</typeparam>
        /// <param name="sheet">Sheet</param>
        /// <returns>指定类型的集合</returns>
        private List<T> toList<T>(ISheet sheet) where T : new()
        {
            if (sheet == null) return null;

            // 初始化字段信息字典和标题字典
            initFieldsInfo<T>();
            initTitle(sheet);

            // 如标题为空,则返回一个空集合
            var table = new List<T>();
            if (title == null || title.Count == 0) return table;

            // 从第二行开始读取正文内容(第一行为标题行)
            for (var i = 1; i <= sheet.LastRowNum; i++)
            {
                var row = sheet.GetRow(i);
                var item = readRow<T>(row);
                table.Add(item);
            }

            return table;
        }

        /// <summary>
        /// 读取输入Row的数据到指定类型的对象实体
        /// </summary>
        /// <typeparam name="T">类型参数</typeparam>
        /// <param name="row">输入的行数据</param>
        /// <returns>T 指定类型的数据对象</returns>
        private T readRow<T>(IRow row) where T : new()
        {
            if (row == null) return default(T);

            // 顺序读取行内的每个单元格的数据并赋值给对应的字段
            var nullCount = 0;
            var item = new T();
            var propertys = typeof(T).GetProperties();
            for (var i = 0; i < title.Count; i++)
            {
                var colName = title[i];

                // 如当前单元格所在列未在指定类型中定义,则跳过该单元格
                var info = fieldInfos.FirstOrDefault(f => colName == f.columnName || colName == f.fieldName);
                if (info == null)
                {
                    nullCount++;
                    continue;
                }

                // 读取单元格数据,如该属性/字段不允许写入或单元格值为空,则跳过该单元格
                var cell = row.GetCell(i);
                var value = readCell(cell, info.typeName);
                var property = propertys.First(p => p.Name == info.fieldName);
                if (!property.CanWrite || value == null)
                {
                    nullCount++;
                    continue;
                }

                // 给字段赋值
                property.SetValue(item, value, null);
            }

            return nullCount < title.Count ? item : default(T);
        }

        /// <summary>
        /// 读取单元格数据
        /// </summary>
        /// <param name="cell"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        private static object readCell(ICell cell, string type)
        {
            if (cell == null) return null;

            switch (cell.CellType)
            {
                case CellType.String:
                    var str = cell.StringCellValue;
                    switch (type)
                    {
                        case "DateTime":
                            return DateTime.Parse(str);
                        case "Boolean":
                            return bool.Parse(str);
                        default:
                            return cell.StringCellValue;
                    }
                case CellType.Numeric:
                    var num = cell.NumericCellValue.ToString(CultureInfo.InvariantCulture);
                    switch (type)
                    {
                        case "Date":
                            return cell.DateCellValue;
                        case "Boolean":
                            return bool.Parse(num);
                        case "String":
                            return num;
                        default:
                            return (decimal)cell.NumericCellValue;
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
                            return (decimal) cell.NumericCellValue;
                    }
                case CellType.Boolean:
                    return cell.BooleanCellValue;
                default:
                    return null;
            }
        }

        /// <summary>
        /// 创建指定名称的Sheet并生成标题行
        /// </summary>
        /// <typeparam name="T">类型参数</typeparam>
        /// <param name="sheetName">Sheet名称</param>
        /// <returns>Sheet</returns>
        private ISheet createTitle<T>(string sheetName)
        {
            initFieldsInfo<T>();
            if (string.IsNullOrEmpty(sheetName)) sheetName = $"Sheet{workbook.NumberOfSheets + 1}";

            var sheet = workbook.CreateSheet(sheetName);
            var row = sheet.CreateRow(0);
            var i = 0;
            foreach (var field in exportFields)
            {
                var cell = row.CreateCell(i++, CellType.String);
                var columnName = field.columnName;
                cell.SetCellValue(columnName ?? field.fieldName);
            }

            return sheet;
        }

        /// <summary>
        /// 写入数据对象字段值到行数据
        /// </summary>
        /// <typeparam name="T">类型参数</typeparam>
        /// <param name="row">行数据</param>
        /// <param name="item">指定类型的数据对象</param>
        private void writeRow<T>(IRow row, T item)
        {
            var propertys = typeof(T).GetProperties();
            for (var i = 0; i < exportFields.Count; i++)
            {
                var field = exportFields[i];
                var cell = row.CreateCell(i, getCellType(field.typeName));
                var property = propertys.First(p => p.Name == field.fieldName);
                if (!property.CanRead) continue;

                var value = property.GetValue(item, null)?.ToString();
                if (string.IsNullOrEmpty(value)) continue;

                switch (field.typeName)
                {
                    case "DateTime":
                        var isFormat = !string.IsNullOrEmpty(field.dateFormat);
                        var val = isFormat ? DateTime.Parse(value).ToString(field.dateFormat) : value;
                        cell.SetCellValue(val);
                        break;

                    case "Int16":
                    case "Int32":
                    case "Int64":
                    case "Single":
                        cell.SetCellValue(long.Parse(value));
                        cell.SetCellType(CellType.Numeric);
                        break;

                    case "Float":
                    case "Double":
                    case "Decimal":
                        cell.SetCellValue(double.Parse(value));
                        cell.SetCellType(CellType.Numeric);
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
        private static CellType getCellType(string type)
        {
            switch (type)
            {
                case "Int16":
                case "Int32":
                case "Int64":
                case "Single":
                case "Float":
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
        private void initTitle(ISheet sheet)
        {
            title = new List<string>();
            var row = sheet.GetRow(0);
            if (row == null)
            {
                return;
            }

            for (var i = 0; i < row.LastCellNum; i++)
            {
                var cell = row.GetCell(i);
                title.Add(cell == null ? "" : cell.StringCellValue);
            }
        }

        /// <summary>
        /// 生成指定类型对应的字段信息集合
        /// </summary>
        /// <typeparam name="T">类型参数</typeparam>
        /// <returns>指定类型对应的需要导出的字段信息集合</returns>
        private void initFieldsInfo<T>()
        {
            fieldInfos = new List<FieldInfo>();
            var propertys = typeof(T).GetProperties();
            foreach (var property in propertys)
            {
                var info = new FieldInfo
                {
                    fieldName = property.Name,
                    typeName = property.PropertyType.Name
                };

                // 如读取到列名自定义特性
                var attributes = property.GetCustomAttributes(typeof(ColumnName), false);
                if (attributes.FirstOrDefault() is ColumnName att)
                {
                    info.columnName = att.name;
                    info.dateFormat = att.dateFormat;
                    info.columnPolicy = att.policy;
                }

                fieldInfos.Add(info);
            }

            exportFields = fieldInfos.Where(i => i.columnPolicy != IGNORABLE).ToList();
        }
    }
}