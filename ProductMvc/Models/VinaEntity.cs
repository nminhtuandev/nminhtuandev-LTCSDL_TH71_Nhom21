using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace ProductMvc.Models
{
    public class VinaEntity : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Category> Categorys { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
    public class User
    {
        [Key]
        public int UserID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }
    public class Category
    {
        [Key]
        public int CategoryID { get; set; }

        [Required(ErrorMessage = "Chưa nhập tên loại sản phẩm")]
        [Display(Name = "Loại Sản Phẩm")]
        public string CategoryName { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }
    public class Product
    {
        [Key]
        public int ProductID { get; set; }

        [Display(Name = "Tên Sản Phẩm")]
        public string ProductName { get; set; }

        [Display(Name = "Loại Sản Phẩm"), Required(ErrorMessage = "Chưa chọn loại sản phẩm")]
        public int CategoryID { get; set; }

        [Display(Name = "Giá")]
        public double Price { get; set; }

        [Display(Name = "Ảnh Minh Họa")]
        public string Image { get; set; }

        [DataType(DataType.MultilineText)]
        public string ShortDescription { get; set; }

        [Display(Name = "Mô Tả"), UIHint("CkEditor"), Column(TypeName = "ntext"), MaxLength]
        public string Description { get; set; }

        public virtual Category Category { get; set; }

    }
    public class Order
    {
        [Key]
        public int OrderID { get; set; }

        [Display(Name = "Tên Khách Hàng"), Required(ErrorMessage = "Chưa nhập tên")]
        public string Customer { get; set; }
        [Display(Name = "Email Liên Hệ"), Required(ErrorMessage = "Chưa nhập Email")]
        public string Email { get; set; }
        [Display(Name = "Số Điện Thoại"), Required(ErrorMessage = "Chưa nhập số điện thoại")]
        public string Phone { get; set; }
        [Display(Name = "Địa chỉ liên hệ")]
        public string Address { get; set; }
        [Display(Name = "Trạng thái đơn hàng")]
        public bool Active { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
    public class OrderDetail
    {
        [Key]
        public int ID { get; set; }
        public int OrderID { get; set; }
        public int ProductID { get; set; }
        public int Quantity { get; set; }
        public virtual Product Product { get; set; }
        public virtual Order Order { get; set; }
    }
}