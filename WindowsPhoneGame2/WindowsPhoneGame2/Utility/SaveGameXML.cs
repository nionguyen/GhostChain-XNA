using System;
using System.Xml;
using System.Xml.Serialization;
using System.IO.IsolatedStorage;
using System.IO;

namespace WindowsPhoneGame2
{
    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public static class SaveGameXML
    {
        public static XmlWriterSettings xmlWriterSetting = new XmlWriterSettings();

        public static void SaveGame(Data object_Save)
        {
            using (IsolatedStorageFile isoStorage = IsolatedStorageFile.GetUserStoreForApplication())
            {
                using (IsolatedStorageFileStream Stream = new IsolatedStorageFileStream("saveGame.xml", FileMode.Create, isoStorage))
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(Data));
                    using (XmlWriter xmlWriter = XmlWriter.Create(Stream, xmlWriterSetting))
                    {
                        serializer.Serialize(xmlWriter, object_Save);
                    }
                }
            }
        }


        public static Data ReadDataFromXML()
        {
            Data source = new Data();
            using (IsolatedStorageFile File = IsolatedStorageFile.GetUserStoreForApplication())
            {
                using (IsolatedStorageFileStream FileStream = new IsolatedStorageFileStream("saveGame.xml", FileMode.OpenOrCreate, File))
                {
                    // Trong FileStream thực hiện Deserialize
                    XmlSerializer serialize = new XmlSerializer(typeof(Data));
                    // Ép kiểu về đúng kiểu ban đầu và Gán lại cho Source

                    try
                    {
                        source = (Data)serialize.Deserialize(FileStream);
                    }
                    catch (Exception)
                    {
                    }

                };
            };
            return source;
        }

    }


}
