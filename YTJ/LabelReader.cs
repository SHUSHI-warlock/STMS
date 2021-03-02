using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Text;

namespace MsgTransTest
{
    class LabelReader
    {
        public interface IReader
        {
            Label GetLabel();
        }
        class RFIDReader : IReader
        {
            SerialPort COM = null;
            bool PortExist;

            //SerialPortManager _spManager;

            public RFIDReader()
            {
                COM = new SerialPort();
                string[] ports = SerialPort.GetPortNames();
                if (ports.Length == 0)
                {
                    PortExist = false;
                }
                else
                {
                    PortExist = true;
                    /* 需要在资源中配置串口信息
                    if (ConfigurationManager.AppSettings["SerialPort"].ToString() == "null")
                        COM.PortName = ports[0];
                    else
                        COM.PortName = ConfigurationManager.AppSettings["SerialPort"].ToString();
                    COM.BaudRate = int.Parse(ConfigurationManager.AppSettings["SerialBaudRate"]);
                    */            
                }
            }

            //解析标签内容
            private Label PraseRFID2Label(string fileName)
            {
                Label label = new Label();
                label.SetId(fileName);
                return label;
            }
            private string PraseLabel2RFID(Label label)
            {
                string newfile = label.GetId();
                return newfile;
            }

            public Label GetLabel()
            {
                if (PortExist == false)
                    return null;
                COM.Open();
                string labels = COM.ReadLine();
                COM.Close();
                return PraseRFID2Label(labels);
            }
           
        }

        class FileReader : IReader
        {
            //读取标签的文件夹
            string readerFolder;

            public FileReader() { }
            public FileReader(string Folder)
            {
                WorkFolder = Folder;
            }

            public string WorkFolder
            {
                set { readerFolder = value; }
            }
            //开启读取区
            private DirectoryInfo ReadFolder()
            {
                if (!Directory.Exists(readerFolder))
                {
                    //默认创建一个
                    //string defaultFolder = ConfigurationManager.AppSettings["root"].ToString() + "\\Reader";
                    //设置自动保存位置，建议写到项目的资源文件中
                    string defaultFolder = @"E:\study\MyCode\vs\作业与上机\程序设计方法\STMS\MsgTransTest";
                    if (Directory.Exists(defaultFolder))
                        readerFolder = defaultFolder;
                    else
                        return Directory.CreateDirectory(defaultFolder);
                }
                return new DirectoryInfo(readerFolder);
            }
            //解析标签内容
            private Label PraseFile2Label(string fileName)
            {
                Label label = new Label();
                label.SetId(fileName);
                return label;
            }
            private string PraseLabel2File(Label label)
            {
                return label.GetId();
            }

            public Label GetLabel()
            {
                DirectoryInfo di = ReadFolder();
                FileInfo[] labels = di.GetFiles();
                if (labels.Length == 0)
                    return null;
                else
                    return PraseFile2Label(labels[0].Name);
            }
        }

        public class ReaderFactory
        {
            private ReaderFactory()
            {}
            public static IReader createReader(string type)
            {
                if (type == "FILE")
                    return new FileReader();
                else if (type == "RFID")
                    return new RFIDReader();
                else
                    return null;
            }
        }
    }
}

