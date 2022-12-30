using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMP_Project_DAL
{
    public class DateConverter 
    {
        private const string _format = "dd-MM-yy";

        public object Convert(object value, Type targetType, object parameter)
        {
            DateTime date = (DateTime)value;

            return date.ToString(_format);
        }



    }
}
