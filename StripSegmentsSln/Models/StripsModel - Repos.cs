using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace Models
{
    // Часть Модели отвечающая за загрузку/сохранение данных.
    public partial class StripsModel
    {
        /// <summary>Имя XML-файла с данными</summary>
        public string FileName { get; }

        /// <summary>Конструктор с заданием имени файла с данными.</summary>
        /// <param name="fileName">Имя XML-файла с данными</param>
        public StripsModel(string fileName)
        {
            if (string.IsNullOrWhiteSpace(fileName))
                throw new ArgumentNullException(nameof(fileName));
            FileName = fileName;
        }

        /// <summary>Полный контейнер с данными.</summary>
        protected RootXml RootXml { get; set; }

        /// <summary>Диапазон фильтрации из последнего запроса.</summary>
        protected SegmentXml Range => RootXml.range;

        /// <summary>Список полос с сегментами.</summary>
        protected List<StripXml> Strips => RootXml.strips;

        /// <summary>Сериализатор XML-файла.</summary>
        protected static readonly XmlSerializer serlz
            = new XmlSerializer(typeof(RootXml));

        /// <summary>Загрузка данных.</summary>
        public void Load()
        {
            using (FileStream file = File.OpenRead(FileName))
                RootXml = (RootXml)serlz.Deserialize(file);

            //// Сортировка всех полос
            //foreach (StripXml strip in Strips)
            //    strip.segments.Sort(CompareSegmentXml);
        }

        /// <summary>Сохранение данных.</summary>
        public void Save()
        {
            using (FileStream file = File.Create(FileName))
                serlz.Serialize(file, RootXml);
        }

        //static int CompareSegmentXml(SegmentXml left, SegmentXml right)
        //{
        //    if (left == null)
        //        return right == null ? 0 : -1;
        //    if (right == null)
        //        return 1;
        //    return left.begin.CompareTo(right.begin);
        //}
    }
}
