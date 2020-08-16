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

        /// <summary>Шаг промотки.</summary>
        protected double Step => RootXml.step;

        /// <summary>Обший размер полос.</summary>
        protected SegmentXml Size => RootXml.size;

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
    }
}
