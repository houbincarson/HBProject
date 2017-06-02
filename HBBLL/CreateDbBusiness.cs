using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HBProject.DAL;
using HBProject.Model;

namespace HBProject.BLL
{
    public class CreateDbBusiness
    {
        DbHelper helper = new DbHelper();

        public CreateDbBusiness()
        {
            try
            {
                helper.CreateDbFile();
            }
            catch (Exception e)
            {
                throw e;
            }
            
        }

        public void CreateInitDb()
        {
            try
            {
                helper.CreateDb();
            }
            catch (Exception)
            {
                
                throw;
            }
           
        }
        public void CreateInitTable()
        {
            try
            {
                helper.CreateTable();   
            }
            catch (Exception)
            {
                
                throw;
            }
           
        }

        public object InsertUserInfo(UserInfo userinfo )
        {
            try
            {
                return helper.InsertUserInfo(userinfo);
            }
            catch (Exception)
            {
                
                throw;
            } 
        }

        public object DeleteUserInfo(UserInfo userinfo)
        {
            try
            {
               return  helper.DeleteUserInfo(userinfo);
            }
            catch (Exception)
            {
                
                throw;
            }
        }

        public DataSet QueryUserInfo()
        {
            try
            {
                return helper.QueryUserInfo();
            }
            catch (Exception)
            {
                
                throw;
            }
        }
         
    }
}
