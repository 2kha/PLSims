using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using System.ComponentModel;

namespace PLSimsEditor
{
    [Serializable]    
    public class Descriptors : List<Descriptor>
    {
        public Font Font { get; set; }
        public Color BackColor { get; set; }

        public Descriptor this[string Name]
        {
            get
            {
                return this.Find(des => des.Name == Name);
            }
            set
            {
                int index = this.FindIndex(des => des.Name == Name);
                this[index] = value;
            }
        }
       
    }

    [Serializable]    
    public class Descriptor
    {       
        public string Name { get; set; }        
        public Color ForeColor { get; set; }        
        public string Description { get; set; }
    }

    public static class Settings
    {
        public static void Save(string FileName)
        {
            Descriptors descriptors = new Descriptors();

            System.Xml.Serialization.XmlSerializer writer1 = new System.Xml.Serialization.XmlSerializer(descriptors.GetType());
            System.IO.StreamWriter file = new System.IO.StreamWriter(FileName);
            writer1.Serialize(file, descriptors);
            file.Close();
            file.Dispose();
        }

        public static Descriptors Load(string FileName)
        {
            Descriptors descriptors = new Descriptors();

            System.Xml.Serialization.XmlSerializer reader1 = new System.Xml.Serialization.XmlSerializer(descriptors.GetType());
            System.IO.StreamReader file = new System.IO.StreamReader(FileName);

            descriptors = (Descriptors)reader1.Deserialize(file);

            file.Close();
            file.Dispose();

            return descriptors;
        }     
    }
}
