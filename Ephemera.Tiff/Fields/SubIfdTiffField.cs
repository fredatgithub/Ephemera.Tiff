﻿using System.Collections.Generic;
using System.IO;

namespace Ephemera.Tiff
{
    internal class SubIfdTiffField : LongTiffField, ITiffFieldInternal
    {
        private readonly Dictionary<int, TiffDirectory> subIfds = new Dictionary<int, TiffDirectory>();

        public SubIfdTiffField(TiffReader reader) : base((ushort)TiffTag.SubIFDs, reader)
        {
            TypeNum = (ushort) TiffFieldType.IFD;
        }

        public SubIfdTiffField(TiffDirectory directory) : base((ushort) TiffTag.SubIFDs, 0)
        {
            subIfds[0] = new TiffDirectory(directory);
        }

        private SubIfdTiffField(SubIfdTiffField original) : base(original.TagNum, 0)
        {
            TypeNum = (ushort)TiffFieldType.IFD;
            ((ITiffFieldInternal)this).Offset = ((ITiffFieldInternal)original).Offset;
            Values = new List<uint>(original.Values);
            foreach (var subIfd in original.subIfds)
                subIfds.Add(subIfd.Key, new TiffDirectory(subIfd.Value));
        }

        public IEnumerable<TiffDirectory> SubIFDs => subIfds.Values;

        public void AddDirectory(TiffDirectory dir)
        {
            Values.Add(0);
            subIfds[Count] = new TiffDirectory(dir);
        }

        protected override void ReadTag(TiffReader reader)
        {
            base.ReadTag(reader);
            var pos = reader.BaseStream.Position;

            for (int i = 0; i < Count; ++i)
            {
                reader.BaseStream.Seek(Values[i], SeekOrigin.Begin);
                var directory = new TiffDirectory(reader);
                subIfds.Add(i, directory);
            }

            reader.BaseStream.Seek(pos, SeekOrigin.Begin);
        }

        public override void WriteData(Stream s)
        {
            base.WriteData(s);

            for (int i = 0; i < Count; ++i)
            {
                DirectoryBlock block = subIfds[i].Write(s);
                Values[i] = (uint)block.IFDPosition;
            }
        }

        ITiffFieldInternal ITiffFieldInternal.Clone()
        {
            return new SubIfdTiffField(this);
        }
    }
}