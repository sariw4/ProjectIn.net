using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DAL
{
    public class DalFactory
    {
        public static IDal instance;

        protected DalFactory() { instance = null; }
        public static IDal getDal()
        {
            if (null == instance)
                instance = new dal_imp_try();
            return instance;
        }
    }
}
