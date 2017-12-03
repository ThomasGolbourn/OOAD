using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace BCPA_OTS_Prototype
{
   public class SeatingLayout : Show
    {
        public Bitmap  seatLayoutImage;
        public Point   seatLayoutImageSize;
        public List<Seat> seatList;

        public SeatingLayout() { }

        public SeatingLayout(Bitmap layoutImage) {
            seatLayoutImage     = layoutImage;
            seatLayoutImageSize = new Point(layoutImage.Size.Width, layoutImage.Size.Height);

            //Create seat list
            seatList = new List<Seat>();
        }
    }
}
