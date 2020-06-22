using System.Collections.Generic;
using System.Data.Entity;

namespace ProductMvc.Models
{
    public class DefaultData : DropCreateDatabaseIfModelChanges<VinaEntity>
    {
        protected override void Seed(VinaEntity context)
        {
            #region Tạo dữ liệu User quản trị ( hỗ trợ mã hóa mật khẩu MD5)
            // Tên đăng nhập admin , pass : 123456 ( tương ứng với mã MD5 là e10adc3949ba59abbe56e057f20f883e
            new List<User> {
            new User{ UserName="admin",Password="e10adc3949ba59abbe56e057f20f883e"}
            }.ForEach(m => context.Users.Add(m));
            #endregion
            #region Tạo danh sách loại sản phẩm ( category)
            new List<Category> {
            new Category { CategoryID=1,CategoryName="Category 1"},       
            new Category { CategoryID=2,CategoryName="Category 2"},         
            new Category { CategoryID=3,CategoryName="Category 3"}    
                       }.ForEach(m => context.Categorys.Add(m));
            #endregion
            #region danh sách Product
            
            new List<Product> { 
            new Product {ProductID=1, ProductName ="San Pham 1",CategoryID=1,Price=100000,Image="/Content/Image/System/ProductImage.gif",ShortDescription="Short Description 1",Description="Mo Ta San Pham 1"},
            new Product {ProductID=2,ProductName ="San Pham 2",CategoryID=1,Price=100000,Image="/Content/Image/System/ProductImage.gif",ShortDescription="Short Description 2",Description="Mo Ta San Pham 2"},                    
            new Product {ProductID=3,ProductName ="San Pham 3",CategoryID=1,Price=100000,Image="/Content/Image/System/ProductImage.gif",ShortDescription="Short Description 3",Description="Mo Ta San Pham 3"},
            new Product {ProductID=4,ProductName ="San Pham 4",CategoryID=2,Price=100000,Image="/Content/Image/System/ProductImage.gif",ShortDescription="Short Description 4",Description="Mo Ta San Pham 4"},                    
            new Product {ProductID=5,ProductName ="San Pham 5",CategoryID=2,Price=100000,Image="/Content/Image/System/ProductImage.gif",ShortDescription="Short Description 5",Description="Mo Ta San Pham 5"}
            }.ForEach(m => context.Products.Add(m));
            #endregion
            #region Order and OrderDetails
            new List<Order>
            {
                    new Order { OrderID=1,Customer="Customer 1",Email="billsangvn@gmail.com",Phone="0985359218",Address="Hà Nội",Active=false},
                    new Order {OrderID=2,Customer="Customer 2",Email="billsangvn@gmail.com",Phone="0985359218",Address="Hà Nội",Active=false},
                    new Order {OrderID=3,Customer="Customer 3",Email="billsangvn@gmail.com",Phone="0985359218",Address="Hà Nội",Active=false}
            }.ForEach(m => context.Orders.Add(m));
            #endregion
            #region OrderDetail
            new List<OrderDetail>
            {
                     new OrderDetail {OrderID=1,ProductID =1, Quantity =1},
                     new OrderDetail {OrderID=1,ProductID =2, Quantity =1},
                     new OrderDetail {OrderID=2,ProductID =1, Quantity =1},
                     new OrderDetail {OrderID=2,ProductID =2, Quantity =1},
                     new OrderDetail {OrderID=3,ProductID =1, Quantity =1},
                     new OrderDetail {OrderID=3,ProductID =2, Quantity =1}
            }.ForEach(m => context.OrderDetails.Add(m));
            #endregion
        }

    }
}