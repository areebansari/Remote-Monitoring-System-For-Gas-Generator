using System;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace SmsMon
{

    
    public class param
    {

        public int cust_id = 0;
        public String gen_id = null;
        public String org =null;
        public String dt;
        public int indx=0;
        public TabControl tc;
        public bool admin = false;
        public SqlConnection conn;
        public bool sts = false;
        public bool QueryOk = true;
        public bool PollChk = true;
        private static  param inst;
        
        private param() { }  //

        public  static param instance
        {
            get
            {
                if (inst == null)
                {
                    inst = new param();
                }
                return inst;
            }
        }
        
    }
}
