using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Supermarket.Main.Areas.Management.Models
{
    public class ProductOperationsViewModel
    {
        public ProductOperationDetailsViewModel DetailsViewModel { get; set; }

        public string ActionToSendTo { get; set; }

        public string ControllerToSendTo { get; set; }

        public string Title { get; set; }
    }
}