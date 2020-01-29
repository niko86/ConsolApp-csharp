using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Xml.XPath;
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
            XNamespace ns = "http://www.keynetix.com/XSD/KeyLAB/Export"; // find out why this is adding xmlns to all subelements of (ns + "keylab"

            XDocument xml_doc = new XDocument(
                new XElement(ns + "keylab",
                    new XAttribute("content", "schedule"),
                    new XAttribute("timestamp", DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss")),
                    new XElement(ns + "test-definitions",
                        new XElement(ns + "test-definition",
                            new XAttribute("name", "One Dimensional Consolidation ISO"),
                            new XAttribute("code", "OEDOISO"),
                            new XElement(ns + "properties",
                                new XElement(ns + "property",
                                    new XAttribute("name", "Stage_StageReadings_StagePasteMins1"),
                                    new XAttribute("unit", "")
                                    ),
                                new XElement(ns + "property",
                                    new XAttribute("name", "Stage_StageReadings_StagePasteDive1"),
                                    new XAttribute("unit", "")
                                    )
                                )
                            )
                        ),
                    new XElement(ns + "project",
                        new XAttribute("id", "Unknown"),
                        new XAttribute("name", "Unknown"),
                        new XElement(ns + "samples",
                            new XElement(ns + "sample",
                                new XAttribute("id", "Unknown"),
                                new XElement(ns + "test",
                                    new XAttribute("code", "OEDOISO"),
                                    new XAttribute("specimen", "1"),
                                    new XElement(ns + "stages", string.Empty)
                                    )
                                )
                            )
                        )
                    )
                );

            // Must be a better way to use xpath and not get confused by namespace issues
            XElement stages = xml_doc.Root.Element(ns + "project").Element(ns + "samples").Element(ns + "sample").Element(ns + "test").Element(ns + "stages");

            for (int i = 0; i < MainWindow.FileNames.Length; i++)
            {
                XElement stage = new XElement(ns + "stage",
                    new XAttribute("number", (i+1).ToString()),
                    new XElement(ns + "parameters")
                    );

                stages.Add(stage);

                ConsolData consolData = new ConsolData();
                ConsolData.TestData testdata = consolData.ParseFile(MainWindow.FileNames[i]);

                for (int j = 0; j < testdata.Divs.Count; j++)
                {
                    XElement StagePasteDive1 = new XElement(ns + "parameter",
                        new XAttribute("name", "Stage_StageReadings_StagePasteDive1"),
                        new XAttribute("value", testdata.Divs[j].ToString()));

                    XElement StagePasteMins1 = new XElement(ns + "parameter",
                    new XAttribute("name", "Stage_StageReadings_StagePasteMins1"),
                    new XAttribute("value", testdata.Time[j].ToString()));

                    stage.Element(ns + "parameters").Add(StagePasteDive1);
                    stage.Element(ns + "parameters").Add(StagePasteMins1);
                }
            }

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
                xml_doc.Save(filename, SaveOptions.None);

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
