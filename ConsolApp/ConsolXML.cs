﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using Microsoft.Win32;

namespace ConsolApp
{
    public class ConsolXML
    {
        public string FormatXml(XNode xmlNode)
        {
            StringBuilder text = new StringBuilder();

            // We will use stringWriter to push the formated xml into our StringBuilder bob.
            using (StringWriter stringWriter = new StringWriter(text))
            {
                // We will use the Formatting of our xmlTextWriter to provide our indentation.
                using (XmlTextWriter xmlTextWriter = new XmlTextWriter(stringWriter))
                {
                    xmlTextWriter.Formatting = Formatting.Indented;
                    xmlNode.WriteTo(xmlTextWriter);
                }
            }

            return text.ToString();
        }
        public string GenerateXML()
        {
            // Have to add xmlns in a wierd way, found below:
            // http://www.mikesdotnetting.com/Article/111/RSS-Feeds-and-Google-Sitemaps-for-ASP.NET-MVC-with-LINQ-To-XML
            XNamespace ns = "http://www.keynetix.com/XSD/KeyLAB/Export";

            XDocument root = new XDocument(
                //new XDeclaration("1.0", "utf-8", "no"),
                new XComment("Produced using Fugro internal software"),
                new XElement(ns + "keylab",
                    new XAttribute("content", "schedule"),
                    new XAttribute("timestamp", DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss")),
                    new XElement("test-definitions",
                        new XElement("test-definition",
                            new XAttribute("name", "One Dimensional Consolidation ISO"),
                            new XAttribute("code", "OEDOISO"),
                            new XElement("properties",
                                new XElement("property",
                                    new XAttribute("name", "Stage_StageReadings_StagePasteMins1"),
                                    new XAttribute("unit", "")
                                    ),
                                new XElement("property",
                                    new XAttribute("name", "Stage_StageReadings_StagePasteDive1"),
                                    new XAttribute("unit", "")
                                    )
                                )
                            )
                        ),
                    new XElement("project",
                        new XAttribute("id", "Unknown"),
                        new XAttribute("name", "Unknown"),
                        new XElement("samples",
                            new XElement("sample",
                                new XAttribute("name", "Unknown"),
                                new XElement("test",
                                    new XAttribute("code", "OEDOISO"),
                                    new XAttribute("specimen", "1"),
                                    new XElement("stages", "TODO")
                                    )
                                )
                            )
                        )
                    )
                );

            // Create SaveFileDialog 
            SaveFileDialog dlg = new SaveFileDialog
            {
                // Set filter for file extension and default file extension 
                Filter = "XML file (*.xml)|*.xml",
                FilterIndex = 2,
                RestoreDirectory = true,
            };

            // Saves the XML file name and display XML in a TextBox 
            string filename;
            if (dlg.ShowDialog() == true)
            {
                filename = dlg.FileName;
                root.Save(filename, SaveOptions.None);

                string str = File.ReadAllText(filename);
                return str;
            }
            else 
            { 
                return "Nothing"; 
            }
        }
    }
}