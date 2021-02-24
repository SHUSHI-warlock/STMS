using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace Database
{
    class Program
    {
        SqlConnection conn = null;
        public void OpenDB()
        {
            string costring = "Data Source=.;Initial Catalog=user;Integrated Security=True";
            conn = new SqlConnection(costring);
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            else
            {
                System.Windows.Forms.MessageBox.Show("连接数据库失败", "错误");
            }
        }

        public void Insert(string sql)
        {
            SqlCommand cmd = new SqlCommand(sql, conn);
            int result = cmd.ExecuteNonQuery();             //执行sql语句
            if (result > 0)
                System.Windows.Forms.MessageBox.Show("添加成功", "提示");
            else
                System.Windows.Forms.MessageBox.Show("添加失败", "提示");
        }

        public void Change(string sql)
        {
            SqlCommand cmd = new SqlCommand(sql, conn);
            int result = cmd.ExecuteNonQuery();             //执行sql语句
            if (result > 0)
                System.Windows.Forms.MessageBox.Show("修改成功", "提示");
            else
                System.Windows.Forms.MessageBox.Show("修改失败", "提示");
        }

        public void Delete(string sql)
        {
            SqlCommand cmd = new SqlCommand(sql, conn);
            int result = cmd.ExecuteNonQuery();
            if (result > 0)
                System.Windows.Forms.MessageBox.Show("删除成功", "提示");
            else
                System.Windows.Forms.MessageBox.Show("删除失败", "提示");
        }


        public List<User> Searchlogin(string sql)
        {
            List<User> user = new List<User>();
            SqlCommand cmd = new SqlCommand(sql, conn);
            SqlDataReader read = cmd.ExecuteReader();     //查询
            while (read.Read())
            {
                User u = new User();
                u.Name = read["Name"].ToString();
                u.Id = read["Id"].ToString();
                u.Money = int.Parse(read["Money"].ToString());
                if (read["Role"].ToString().Equals("学生"))
                {
                    u.Role = "学生";
                }
                else
                {
                    u.Role = "管理员";
                }
                if (read["Sex"].ToString().Equals("男"))
                    u.Sex = "男";
                else
                    u.Sex = "女";
                user.Add(u);
            }
            return user;
        }


        public void CloseDB()
        {
            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
            else
            {
                System.Windows.Forms.MessageBox.Show("数据库无法断开", "错误");
            }
        }
    }
    public class User
{
    public User()
    {
        Name = Id = Password = Role = string.Empty;
    }
    public User(User u)
    {
        Name = u.Name;
        Id = u.Id;
        Password = u.Password;
        Sex = u.Sex;
        Role = u.Role;
        Money = u.Money;
    }
    public string Name { set; get; }
    public string Id { set; get; }
    public string Password { set; get; }
    public string Sex { set; get; }
    public string Role { set; get; }
    public int Money { set; get; }

}
   class Payment
{
    public string IdOfPayment { set; get; }
    public string Id { set; get; }
    public string Time { set; get; }
    public string WindowsName { set; get; }
    public int Pay { set; get; }
}
}