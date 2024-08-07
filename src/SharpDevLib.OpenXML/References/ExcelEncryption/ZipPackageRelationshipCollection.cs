﻿/*******************************************************************************
 * You may amend and distribute as you like, but don't remove this header!
 *
 * EPPlus provides server-side generation of Excel 2007/2010 spreadsheets.
 * See https://github.com/JanKallman/EPPlus for details.
 *
 * Copyright (C) 2011  Jan Källman
 *
 * This library is free software; you can redistribute it and/or
 * modify it under the terms of the GNU Lesser General Public
 * License as published by the Free Software Foundation; either
 * version 2.1 of the License, or (at your option) any later version.

 * This library is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  
 * See the GNU Lesser General Public License for more details.
 *
 * The GNU Lesser General Public License can be viewed at http://www.opensource.org/licenses/lgpl-license.php
 * If you unfamiliar with this license or have questions about it, here is an http://www.gnu.org/licenses/gpl-faq.html
 *
 * All code and executables are provided "as is" with no warranty either express or implied. 
 * The author accepts no liability for any damage or loss of business that this product may cause.
 *
 * Code change notes:
 * 
 * Author							Change						Date
 *******************************************************************************
 * Jan Källman		Added		25-Oct-2012
 *******************************************************************************/
using ICSharpCode.SharpZipLib.Zip;
using System.Security;
using System.Text;

namespace SharpDevLib.OpenXML.References.ExcelEncryption;

internal class ZipPackageRelationshipCollection : IEnumerable<ZipPackageRelationship>
{
    internal protected Dictionary<string, ZipPackageRelationship> _rels = new(StringComparer.OrdinalIgnoreCase);
    internal void Add(ZipPackageRelationship item) => _rels.Add(item.Id ?? string.Empty, item);
    public IEnumerator<ZipPackageRelationship> GetEnumerator() => _rels.Values.GetEnumerator();

    System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => _rels.Values.GetEnumerator();

    internal void WriteZip(ZipOutputStream os, string fileName)
    {
        var xml = new StringBuilder("<?xml version=\"1.0\" encoding=\"UTF-8\" standalone=\"yes\"?><Relationships xmlns=\"http://schemas.openxmlformats.org/package/2006/relationships\">");
        foreach (var rel in _rels.Values)
        {
            xml.AppendFormat("<Relationship Id=\"{0}\" Type=\"{1}\" Target=\"{2}\"{3}/>", SecurityElement.Escape(rel.Id), rel.RelationshipType, SecurityElement.Escape(rel.TargetUri?.OriginalString), rel.TargetMode == TargetMode.External ? " TargetMode=\"External\"" : "");
        }
        xml.Append("</Relationships>");

        os.PutNextEntry(new ZipEntry(fileName));
        var b = Encoding.UTF8.GetBytes(xml.ToString());
        os.Write(b, 0, b.Length);
    }

    public int Count => _rels.Count;
}
