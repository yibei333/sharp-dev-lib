/*******************************************************************************
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
 * Author							Change						         Date
 * ******************************************************************************
 * Jan Källman		    Initial Release		         2009-10-01
 * Jan Källman		    License changed GPL-->LGPL 2011-12-27
 * Eyal Seagull       Add "CreateComplexNode"    2012-04-03
 * Eyal Seagull       Add "DeleteTopNode"        2012-04-13
 *******************************************************************************/

using System.Globalization;
using System.Text;
using System.Xml;
namespace SharpDevLib.OpenXML.References.ExcelEncryption;
/// <summary>
/// Help class containing XML functions. 
/// Can be Inherited 
/// </summary>
internal abstract class XmlHelper
{
    internal XmlHelper(XmlNamespaceManager nameSpaceManager)
    {
        NameSpaceManager = nameSpaceManager;
        SchemaNodeOrder = Array.Empty<string>();
    }

    internal XmlHelper(XmlNamespaceManager nameSpaceManager, XmlNode topNode) : this(nameSpaceManager)
    {
        TopNode = topNode;
    }
    //internal bool ChangedFlag;
    internal XmlNamespaceManager NameSpaceManager { get; set; }
    internal XmlNode? TopNode { get; set; }
    /// <summary>
    /// Schema order list
    /// </summary>
    internal string[] SchemaNodeOrder { get; set; }
    /// <summary>
    /// Create the node path. Nodesa are inserted according to the Schema node oreder
    /// </summary>
    /// <param name="path">The path to be created</param>
    /// <param name="insertFirst">Insert as first child</param>
    /// <param name="addNew">Always add a new item at the last level.</param>
    /// <returns></returns>
    internal XmlNode? CreateNode(string path, bool insertFirst, bool addNew = false)
    {
        var node = TopNode;
        XmlNode? prependNode = null;
        if (path.StartsWith("/")) path = path.Substring(1);
        var subPaths = path.Split('/');
        for (int i = 0; i < subPaths.Length; i++)
        {
            var subPath = subPaths[i];
            var subNode = node?.SelectSingleNode(subPath, NameSpaceManager);
            if (subNode == null || (i == subPath.Length - 1 && addNew))
            {
                string nodeName;
                string nodePrefix;
                var nameSplit = subPath.Split(':');

                if (SchemaNodeOrder != null && subPath[0] != '@')
                {
                    insertFirst = false;
                    prependNode = GetPrependNode(subPath, node);
                }

                string? nameSpaceURI;
                if (nameSplit.Length > 1)
                {
                    nodePrefix = nameSplit[0];
                    if (nodePrefix[0] == '@') nodePrefix = nodePrefix.Substring(1);
                    nameSpaceURI = NameSpaceManager.LookupNamespace(nodePrefix);
                    nodeName = nameSplit[1];
                }
                else
                {
                    nodePrefix = "";
                    nameSpaceURI = "";
                    nodeName = nameSplit[0];
                }
                if (subPath.StartsWith("@"))
                {
                    var addedAtt = node?.OwnerDocument?.CreateAttribute(subPath.Substring(1), nameSpaceURI);  //nameSpaceURI
                    if (addedAtt != null) node?.Attributes?.Append(addedAtt);
                }
                else
                {
                    if (nodePrefix == "")
                    {
                        subNode = node?.OwnerDocument?.CreateElement(nodeName, nameSpaceURI);
                    }
                    else
                    {
                        if (nodePrefix == "" || (node?.OwnerDocument != null && node.OwnerDocument.DocumentElement != null && node.OwnerDocument.DocumentElement.NamespaceURI == nameSpaceURI && node.OwnerDocument.DocumentElement.Prefix == ""))
                        {
                            subNode = node?.OwnerDocument?.CreateElement(nodeName, nameSpaceURI);
                        }
                        else
                        {
                            subNode = node?.OwnerDocument?.CreateElement(nodePrefix, nodeName, nameSpaceURI);
                        }
                    }

                    if (prependNode != null)
                    {
                        if (subNode != null) node?.InsertBefore(subNode, prependNode);
                        prependNode = null;
                    }
                    else if (insertFirst)
                    {
                        if (subNode != null) node?.PrependChild(subNode);
                    }
                    else
                    {
                        if (subNode != null) node?.AppendChild(subNode);
                    }
                }
            }
            node = subNode;
        }
        return node;
    }

    /// <summary>
    /// return Prepend node
    /// </summary>
    /// <param name="nodeName">name of the node to check</param>
    /// <param name="node">Topnode to check children</param>
    /// <returns></returns>
    private XmlNode? GetPrependNode(string nodeName, XmlNode? node)
    {
        if (node == null) return null;
        int pos = GetNodePos(nodeName);
        if (pos < 0)
        {
            return null;
        }
        XmlNode? prependNode = null;
        foreach (XmlNode childNode in node.ChildNodes)
        {
            int childPos = GetNodePos(childNode.Name);
            if (childPos > -1)  //Found?
            {
                if (childPos > pos) //Position is before
                {
                    prependNode = childNode;
                    break;
                }
            }
        }
        return prependNode;
    }
    private int GetNodePos(string nodeName)
    {
        int ix = nodeName.IndexOf(":");
        if (ix > 0)
        {
            nodeName = nodeName.Substring(ix + 1);
        }
        for (int i = 0; i < SchemaNodeOrder.Length; i++)
        {
            if (nodeName == SchemaNodeOrder[i])
            {
                return i;
            }
        }
        return -1;
    }
    internal void DeleteAllNode(string path)
    {
        var split = path.Split('/');
        var node = TopNode;
        foreach (string s in split)
        {
            node = node?.SelectSingleNode(s, NameSpaceManager);
            if (node == null) break;
            if (node is XmlAttribute)
            {
                (node as XmlAttribute)!.OwnerElement?.Attributes.Remove(node as XmlAttribute);
            }
            else
            {
                node.ParentNode?.RemoveChild(node);
            }
        }
    }
    internal void SetXmlNodeString(string path, string value)
    {
        SetXmlNodeString(TopNode, path, value, false, false);
    }
    internal void SetXmlNodeString(XmlNode? node, string path, string value, bool removeIfBlank, bool insertFirst)
    {
        if (node == null) return;

        if (value == "" && removeIfBlank)
        {
            DeleteAllNode(path);
        }
        else
        {
            var nameNode = node.SelectSingleNode(path, NameSpaceManager);
            if (nameNode == null)
            {
                CreateNode(path, insertFirst);
                nameNode = node.SelectSingleNode(path, NameSpaceManager);
            }
            //if (nameNode.InnerText != value) HasChanged();
            if (nameNode != null) nameNode.InnerText = value;
        }
    }
    internal int GetXmlNodeInt(string path) => int.TryParse(GetXmlNodeString(path), NumberStyles.Number, CultureInfo.InvariantCulture, out var i) ? i : int.MinValue;

    internal string GetXmlNodeString(XmlNode? node, string path)
    {
        if (node == null) return "";

        var nameNode = node.SelectSingleNode(path, NameSpaceManager);

        if (nameNode != null)
        {
            if (nameNode.NodeType == XmlNodeType.Attribute)
            {
                return nameNode.Value ?? "";
            }
            else
            {
                return nameNode.InnerText;
            }
        }
        else
        {
            return "";
        }
    }
    internal string GetXmlNodeString(string path)
    {
        return GetXmlNodeString(TopNode, path);
    }

    internal static void LoadXmlSafe(XmlDocument xmlDoc, Stream stream)
    {
        var settings = new XmlReaderSettings
        {
            //Disable entity parsing (to aviod xmlbombs, External Entity Attacks etc).
            DtdProcessing = DtdProcessing.Prohibit
        };

        using var reader = XmlReader.Create(stream, settings);
        xmlDoc.Load(reader);
    }
    internal static void LoadXmlSafe(XmlDocument xmlDoc, string xml, Encoding encoding)
    {
        using var stream = new MemoryStream(encoding.GetBytes(xml));
        LoadXmlSafe(xmlDoc, stream);
    }
}
