using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;


namespace LabKanban
{
    public class Admin :User
    {

        public override string ToString()
        {
            return "Администратор " + base.ToString();
        }



        

    }
}
