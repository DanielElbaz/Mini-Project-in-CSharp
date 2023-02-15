using DalApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using DO;
using System.Runtime.CompilerServices;

namespace Dal
{
    internal class Product:IProduct
    {
        string productPath = @"Product";
        static XElement config = XMLTools.LoadConfig();
        static DO.Product? createProductfromXElement(XElement s)
        {
            var result = new DO.Product
            {

                // ID = Int32.Parse(s?.Element("ID")?.Value!),
                ID = (int)s.Element("ID")!,
                Name = s?.Element("Name")?.Value,
                // Category = (DO.Category)s.Element("Category"),
                Category = (DO.Category)Enum.Parse(typeof(DO.Category), s?.Element("Category")?.Value!),
                Price = (double)s?.Element("Price")!,
                InStock = (int)s?.Element("InStock")!
            };
            return result;
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public int Add(DO.Product product)
        {
            XElement product_root = XMLTools.LoadListFromXMLElement(productPath);
            if (product.ID == 0)
            {
                product.ID = int.Parse(config.Element("ProductId")!.Value) + 1;
                XMLTools.SaveConfigXElement("ProductId", product.ID);
            }
            XElement? stud = (from st in product_root.Elements()
                              where st.ToIntNullable("ID") == product.ID
                              select st).FirstOrDefault();
            if (stud != null)
                throw new DO.DuplicateIDException("ID already exist");


            product_root.Add(new XElement("Product",
                                       new XElement("ID", product.ID),
                                       new XElement("Name", product.Name),
                                       new XElement("Category", product.Category),
                                       new XElement("Price", product.Price),
                                       new XElement("InStock", product.InStock)
                                       ));

            XMLTools.SaveListToXMLElement(product_root, productPath);

            return product.ID; ;
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void Delete(int id)
        {
            XElement product_root = XMLTools.LoadListFromXMLElement(productPath);

            XElement? prod = (from st in product_root.Elements()
                              where (int?)st.Element("ID") == id
                              select st).FirstOrDefault() ?? throw new Exception("Missing ID");

            prod.Remove(); //<==>   Remove stud from studentsRootElem

            XMLTools.SaveListToXMLElement(product_root, productPath);
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public DO.Product GetBy(Func<DO.Product?, bool> filter)
        {
           // if (filter == null)
             //   throw new Exception("missing function");

            XElement product_root = XMLTools.LoadListFromXMLElement(productPath);
            return ((from p in product_root.Elements()
                     where filter(p.ConvertProduct_Xml_to_D0())
                     select p.ConvertProduct_Xml_to_D0()).FirstOrDefault());
        }


        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<DO.Product?> GetAll(Func<DO.Product?, bool>? filter)
        {
            XElement? product_root = XMLTools.LoadListFromXMLElement(productPath);


            if (filter != null)
            {
                return from s in product_root.Elements()
                       let prod = createProductfromXElement(s)
                       where filter(prod)
                       select (DO.Product?)prod;
            }
            else
            {
                var result = from s in product_root.Elements()
                       select createProductfromXElement(s);
                return result;
            }
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public DO.Product GetByID(int id)
        {
            List<DO.Product?> listOrder = XMLTools.LoadListFromXMLSerializer<DO.Product>(productPath);

            return (from item in listOrder
                    where id == item.Value.ID
                    select (DO.Product)item).FirstOrDefault();
        }


        [MethodImpl(MethodImplOptions.Synchronized)]
        public void Update(int id,DO.Product product)
        {
            Delete(id);
            Add(product);

        }

    }
}
