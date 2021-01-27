using Shop.Inferastructure.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Inferastructure.Repository
{
    public class CommandText : ICommandText
    {
        public string GetProducts => "Select Name,Price,Quantity,CreateDate From Product";
        public string GetProductById => "Select Name,Price,Quantity,CreateDate From Product Where Id=@Id";
        public string AddProduct => "Insert Into Product (Name,Price,Quantity,CreateDate) Values (@Name,@Price,@Quantity,@CreateDate)";
        public string UpdateProduct => "Update Product Set Name=@Name,Quantity=@Quantity,CreateDate=@CreateData Where Id=@Id";
        public string RemoveProduct => "Delete From Product Where Id=@Id";
    }
}
