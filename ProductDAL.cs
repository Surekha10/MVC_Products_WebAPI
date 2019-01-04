using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.Mvc;

namespace MVC_Product_Assignment.Models
{
    public class ProductDAL
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ConnectionString);
        public int AddProduct(ProductModel model)
        {
            SqlCommand com_add = new SqlCommand("proc_addproduct", con);
            com_add.Parameters.AddWithValue("@name", model.ProductName);
            com_add.Parameters.AddWithValue("@price", model.ProductPrice);
            com_add.Parameters.AddWithValue("@category", model.ProductCategory);
            com_add.Parameters.AddWithValue("@imgaddress", model.ProductImageAddress);
            com_add.CommandType = CommandType.StoredProcedure;
            SqlParameter para_ret = new SqlParameter();
            para_ret.Direction = ParameterDirection.ReturnValue;
            com_add.Parameters.Add(para_ret);
            con.Open();
            com_add.ExecuteNonQuery();
            con.Close();
            int id = Convert.ToInt32(para_ret.Value);
            return id;
        }
        public List<SelectListItem> GetCategory()
        {
            List<SelectListItem> category = new List<SelectListItem>();
            category.Add(new SelectListItem { Text = "Select", Value = "" });
            category.Add(new SelectListItem { Text = "HomeAppliances", Value = "HomeAppliances" });
            category.Add(new SelectListItem { Text = "Books", Value = "Books" });
            category.Add(new SelectListItem { Text = "Mobiles", Value = "Mobiles" });
            category.Add(new SelectListItem { Text = "Clothes", Value = "Clothes" });
            category.Add(new SelectListItem { Text = "Laptop", Value = "Laptop" });
            return category;

        }
        public List<ProductProjectionModel> Search(string key)
        {
            SqlCommand com_search = new SqlCommand("proc_search", con);
            com_search.Parameters.AddWithValue("@key", key);
            com_search.CommandType = CommandType.StoredProcedure;
            con.Open();
            SqlDataReader dr = com_search.ExecuteReader();
            List<ProductProjectionModel> list = new List<ProductProjectionModel>();
            while (dr.Read())
            {
                ProductProjectionModel model = new ProductProjectionModel();
                model.ProductID = dr.GetInt32(0);
                model.ProductName = dr.GetString(1);
                model.ProductImageAddress = dr.GetString(2);
                list.Add(model);
            }
            con.Close();
            return list;
        }
        public ProductModel Find(int id)
        {
            SqlCommand com_find = new SqlCommand("proc_find", con);
            com_find.Parameters.AddWithValue("@id", id);
            com_find.CommandType = CommandType.StoredProcedure;
            con.Open();
            SqlDataReader dr = com_find.ExecuteReader();
            if (dr.Read())
            {
                ProductModel model = new ProductModel();
                model.ProductID = dr.GetInt32(0);
                model.ProductName = dr.GetString(1);
                model.ProductPrice = dr.GetInt32(2);
                model.ProductCategory = dr.GetString(3);
                model.ProductImageAddress = dr.GetString(4);
                con.Close();
                return model;
            }
            else
            {
                con.Close();
                return null;
            }
        }
        public bool Update(int id, int price, string category)
        {
            SqlCommand com_update = new SqlCommand("proc_update", con);
            com_update.Parameters.AddWithValue("@id", id);
            com_update.Parameters.AddWithValue("@price", price);
            com_update.Parameters.AddWithValue("@category", category);
            com_update.CommandType = CommandType.StoredProcedure;
            SqlParameter para_return = new SqlParameter();
            para_return.Direction = ParameterDirection.ReturnValue;
            com_update.Parameters.Add(para_return);
            con.Open();
            com_update.ExecuteNonQuery();
            con.Close();
            int count = Convert.ToInt32(para_return.Value);
            if (count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}