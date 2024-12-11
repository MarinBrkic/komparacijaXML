using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace razlikeLINQ
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Load the two XML documents
            XElement xml1 = XElement.Load(@"C:\Users\Učenik\Documents\prvi.xml");
            XElement xml2 = XElement.Load(@"C:\Users\Učenik\Documents\drugi.xml");

            // Compare the XML documents and print differences
            CompareElements(xml1, xml2, "");

            Console.ReadLine();
        }
        static void CompareElements(XElement element1, XElement element2, string path)
        {
            // Compare element names
            if (element1.Name != element2.Name)
            {
                Console.WriteLine($"{path}: Different element names: '{element1.Name}' vs '{element2.Name}'");
            }

            // Compare element values
            if (element1.Value != element2.Value)
            {
                Console.WriteLine($"{path}: Different values: '{element1.Value}' vs '{element2.Value}'");
            }

            // Compare attributes
            var attributes1 = element1.Attributes().ToList();
            var attributes2 = element2.Attributes().ToList();

            // Check if the number of attributes is different
            if (attributes1.Count != attributes2.Count)
            {
                Console.WriteLine($"{path}: Different number of attributes");
            }

            // Compare each attribute
            foreach (var attr1 in attributes1)
            {
                var attr2 = attributes2.FirstOrDefault(a => a.Name == attr1.Name);
                if (attr2 == null)
                {
                    Console.WriteLine($"{path}: Attribute '{attr1.Name}' is missing in the second XML.");
                }
                else if (attr1.Value != attr2.Value)
                {
                    Console.WriteLine($"{path}: Attribute '{attr1.Name}' values are different: '{attr1.Value}' vs '{attr2.Value}'");
                }
            }

            // Check if there are any additional attributes in the second XML
            foreach (var attr2 in attributes2)
            {
                if (!attributes1.Any(a => a.Name == attr2.Name))
                {
                    Console.WriteLine($"{path}: Attribute '{attr2.Name}' is missing in the first XML.");
                }
            }

            // Compare child elements (recursive comparison)
            var children1 = element1.Elements().ToList();
            var children2 = element2.Elements().ToList();

            // Check if the number of children is different
            if (children1.Count != children2.Count)
            {
                Console.WriteLine($"{path}: Different number of child elements");
            }

            // Compare each child element
            for (int i = 0; i < Math.Min(children1.Count, children2.Count); i++)
            {
                CompareElements(children1[i], children2[i], $"{path}/{element1.Name}");
            }

            // Check if any extra child elements are present in the second XML
            foreach (var child2 in children2)
            {
                if (!children1.Any(c => c.Name == child2.Name))
                {
                    Console.WriteLine($"{path}: Child element '{child2.Name}' is missing in the first XML.");
                }
            }

            // Check if any extra child elements are present in the first XML
            foreach (var child1 in children1)
            {
                if (!children2.Any(c => c.Name == child1.Name))
                {
                    Console.WriteLine($"{path}: Child element '{child1.Name}' is missing in the second XML.");
                }
            }
        }
    }
}
