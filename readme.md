# NpoiHelper 使用手册
## 构造方法
### 1、用于导出Excel文件
NpoiHelper(ExcelVer ver = XLS)
可使用参数决定生成的文件版本，默认为97-2004版本(.xls)。
### 2、用于导入Excel文件
NpoiHelper(string file)
NpoiHelper(byte[] data)
NpoiHelper(Stream stream)
三个方法分别适用于：本地文件路径、字节数组及数据流。
- - - -
## 导出数据的方法
|方法|功用|
|---|---|
|void exportFile(string file)|导出工作簿到Excel文件|
|void exportFile<T>(string file, List<T> list)|使用指定的数据集生成Sheet并导出工作簿到Excel文件|
|void exportFile<T>(string file, List<T> list, string sheetName)|使用指定的数据集生成指定名称的Sheet并导出工作簿到Excel文件|
|MemoryStream exportStream()|导出工作簿到数据流|
|MemoryStream exportStream<T>(List<T> list)|使用指定的数据集生成Sheet并导出工作簿到数据流|
|MemoryStream exportStream<T>(List<T> list, string sheetName)|使用指定的数据集生成指定名称的Sheet并导出工作簿到数据流|
|byte[] exportByteArray()|导出工作簿到字节数组|
|byte[] exportByteArray<T>(List<T> list)|使用指定的数据集生成Sheet并导出工作簿到字节数组|
|byte[] exportByteArray<T>(List<T> list, string sheetName)|使用指定的数据集生成指定名称的Sheet并导出工作簿到字节数组|
|void createTemplate<T>()|创建一个用于导入数据的模板Sheet|
|void createTemplate<T>(string sheetName)|创建一个用于导入数据且指定名称的Sheet模板|
|void createSheet<T>(List<T> list)|使用指定的数据集在工作簿中创建一个Sheet|
|void createSheet<T>(List<T> list, string sheetName)|使用指定的数据集在工作簿中创建一个指定名称的Sheet|
## 导入数据的方法
|方法|功用|
|---|---|
|List<T> importSheet<T>()|导入Excel文件中第一个Sheet的数据到指定类型的集合|
|List<T> importSheet<T>(int sheetIndex)|导入指定位置的Sheet的数据到指定类型的集合|
|List<T> importSheet<T>(string sheetName)|导入指定名称的Sheet的数据到指定类型的集合|
## 其他方法
|方法|功用|
|---|---|
|bool sheetIsExist(int sheetIndex)|指定位置的Sheet是否存在|
|bool sheetIsExist(string sheetName)|指定名称的Sheet是否存在|
|bool verifyColumns(int sheetIndex, string keys)|校验指定位置的Sheet是否包含关键列,关键列名称以英文逗号分隔|
|bool verifyColumns(string sheetName, string keys)|校验指定名称的Sheet是否包含关键列,关键列名称以英文逗号分隔|
|bool verifyColumns<T>(int sheetIndex)|校验指定位置的Sheet是否包含关键列|
|bool verifyColumns<T>(string sheetName)|校验指定名称的Sheet是否包含关键列|
- - - -
## 用于导出/导入数据的实体类属性的特性说明
|特性|功用|
|---|---|
|name(默认特性)|Excel中的对应的列名|
|dateFormat|列的时间/日期格式,默认为:yyyy-MM-dd|
|policy|Ignorable:导出时忽略(不会导出),Required:导入时文件必须包含此列,否则无法通过关键列校验|


示例如下：
```
public class Test
{
    [ColumnName(Policy.Ignorable)]
    public string id { get; set; }

    [ColumnName("名称")]
    public string name { get; set; }

    [ColumnName("更新时间", dateFormat = "yyyy-MM-dd hh:mm:ss", policy = Policy.Required)]
    public DateTime updateTime { get; set; }
}
```
