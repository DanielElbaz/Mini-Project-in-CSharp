using DalApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using DO;

namespace Dal
{
    internal class OrderItem:IOrderItem
    {

        const string orderItemPath = "OrderItem";
        static XElement config = XMLTools.LoadConfig();
        public int Add(DO.OrderItem entity)
        {
            List<DO.OrderItem?> listOrderItem = XMLTools.LoadListFromXMLSerializer<DO.OrderItem>(orderItemPath);
            int lastID = listOrderItem.Last()!.Value.ID; // take the id of the last 
            if (listOrderItem.FirstOrDefault(orderItem => orderItem?.ID == entity.ID) != null)
                throw new DO.DuplicateIDException("id already exist");

            //entity.ID = int.Parse(config.Element("OrderItemId")!.Value) + 1;
            if (entity.ID == 0)
                entity.ID = lastID + 1;
            listOrderItem.Add(entity);

            XMLTools.SaveListToXMLSerializer(listOrderItem, orderItemPath);

            return entity.ID;
        }

        public void Delete(int id)
        {
            List<DO.OrderItem?> listOrderItem = XMLTools.LoadListFromXMLSerializer<DO.OrderItem>(orderItemPath);

            if (listOrderItem.RemoveAll(lec => lec?.ID == id) == 0)
                throw new DO.MissingIDException("missing id");

            XMLTools.SaveListToXMLSerializer(listOrderItem, orderItemPath);
        }

        public DO.OrderItem GetBy(Func<DO.OrderItem?, bool> filter)
        {
            List<DO.OrderItem?> listOrder = XMLTools.LoadListFromXMLSerializer<DO.OrderItem>(orderItemPath);

            return (from item in listOrder
                    where filter(item)
                    select (DO.OrderItem)item).FirstOrDefault();
        }
        public IEnumerable<DO.OrderItem?> GetAll(Func<DO.OrderItem?, bool>? filter)
        {
            List<DO.OrderItem?> listOrderItem = XMLTools.LoadListFromXMLSerializer<DO.OrderItem>(orderItemPath);

            if (filter == null)
                return listOrderItem.Select(lec => lec).OrderBy(lec => lec?.ID);
            else
                return listOrderItem.Where(filter).OrderBy(lec => lec?.ID);
        }

        public DO.OrderItem GetByID(int id)
        {
            List<DO.OrderItem?> listOrder = XMLTools.LoadListFromXMLSerializer<DO.OrderItem>(orderItemPath);

            return (from item in listOrder
                    where id == item.Value.ID
                    select (DO.OrderItem)item).FirstOrDefault();
        }
        public void Update(int id, DO.OrderItem entity)
        {
            Delete(id);
            Add(entity);
            
        }
    }
}
