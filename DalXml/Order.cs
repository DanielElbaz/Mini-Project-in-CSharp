using DalApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DO;
using System.Xml.Linq;

namespace Dal
{
    internal class Order:IOrder
    {
        const string orderPath = "Order";
        static XElement config = XMLTools.LoadConfig();

        public int Add(DO.Order entity)
        {
            List<DO.Order?> listOrder = XMLTools.LoadListFromXMLSerializer<DO.Order>(orderPath);

            int lastID = listOrder.Last()!.Value.ID; // take the id of the last
            if (listOrder.FirstOrDefault(order => order?.ID== entity.ID) != null)
                throw new DO.DuplicateIDException("id already exist");

            //  entity.ID = int.Parse(config.Element("OrderId")!.Value) + 1;
            entity.ID = lastID + 1;
            listOrder.Add(entity);

            XMLTools.SaveListToXMLSerializer(listOrder, orderPath);

            return entity.ID;
        }
        public void Delete(int id)
        {
            List<DO.Order?> listOrder = XMLTools.LoadListFromXMLSerializer<DO.Order>(orderPath);

            if (listOrder.RemoveAll(lec => lec?.ID == id) == 0)
                throw new DO.MissingIDException("missing id");

            XMLTools.SaveListToXMLSerializer(listOrder, orderPath);
        }
        public DO.Order GetBy(Func<DO.Order?, bool> filter)
        {
            List<DO.Order?> listOrder = XMLTools.LoadListFromXMLSerializer<DO.Order>(orderPath);

            return (from item in listOrder
                    where filter(item)
                    select (DO.Order)item).FirstOrDefault();
        }
        public IEnumerable<DO.Order?> GetAll(Func<DO.Order?, bool>? filter)
        {
            List<DO.Order?> listOrder = XMLTools.LoadListFromXMLSerializer<DO.Order>(orderPath);

            if (filter == null)
                return listOrder.Select(lec => lec).OrderBy(lec => lec?.ID);
            else
                return listOrder.Where(filter).OrderBy(lec => lec?.ID);
        }
        public DO.Order GetByID(int id)
        {
            List<DO.Order?> listOrder = XMLTools.LoadListFromXMLSerializer<DO.Order>(orderPath);

            return (from item in listOrder
                    where item.Value.ID == id
                    select (DO.Order)item).FirstOrDefault();
        }
        public void Update(int id,DO.Order order)
        {
            Delete(id);
            Add(order);
        }
    }
}
