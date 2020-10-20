using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using System.Windows.Data;
using BE;
using BL;
namespace UI
{
    public class Converter:IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            switch ((OrderStatus)value)
            {
                case OrderStatus.ClosesOutOfResponsiveness:
                    return "Closes Out Of Responsiveness";
                case OrderStatus.ClosesWithResponse:
                    return "Closes With Response";
                case OrderStatus.MailHasBeenSent:
                    return "Mail Has Been Sent";
                case OrderStatus.NotYetAddressed:
                    return "Not Yet Addressed";
                default:
                    throw new Exception("ERROR");
            }
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
