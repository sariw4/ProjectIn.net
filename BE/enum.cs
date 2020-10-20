using System;
using System.Collections.Generic;
using System.Text;

namespace BE
{

    public enum RequestStatus { Open, SiteClose, Expired };
    public enum AreaStatus { All, North, South, Center, Jerusalem };
    public enum TypeStatus { Zimmer, Hotel, Camping, Etc };
    public enum Additions { Necessary, Possible, Notinterested };
    public enum OrderStatus { NotYetAddressed, MailHasBeenSent, ClosesOutOfResponsiveness, ClosesWithResponse };
}
