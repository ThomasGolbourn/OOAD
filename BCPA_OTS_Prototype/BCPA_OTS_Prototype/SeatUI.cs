using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BCPA_OTS_Prototype
{
    public class SeatUI : SeatingLayoutUI
    {
        public  Seat        seatObj;         //The seat object we are creating ui element for
        private Bitmap      seatBitmap;      //The seat image itself
        private ToolTip     seatTt;          //Tooltip interface element for when mouse hovers on seat
        public  PictureBox  seatPicBox;      //The seats picture box that displays the image
        public  PictureBox  layoutPicBox;    //The picture that holds the seats, the seating chart/layout
        public  SeatingLayoutUI parentUI;    //The seating layout UI object this object is a child of

        public SeatUI(Seat seatObject, SeatingLayoutUI layoutUI){

            //Fill values from input
            seatObj         = seatObject;
            seatBitmap      = Properties.Resources.SeatImage;
            parentUI        = layoutUI;
            layoutPicBox    = layoutUI.pb_layout;

            //Create seat picture box
            seatPicBox = new PictureBox
            {
                Parent = layoutPicBox,
                BackColor = Color.Transparent,
                SizeMode = PictureBoxSizeMode.Zoom,
                Location = new Point(seatObj.seatPosX, seatObj.seatPosY),
                Image = seatBitmap
            };
            
            //Set bounds of pic box / image size (image matches picbox size)
            seatPicBox.SetBounds(seatPicBox.Location.X, seatPicBox.Location.Y, 22, 19);
            
            //Add on-click event to seat
            seatPicBox.MouseClick += new MouseEventHandler(OnClick);

            //Add on-hover event to seat
            seatPicBox.MouseEnter += new EventHandler(OnMouseEnter);
            seatPicBox.MouseHover += new EventHandler(OnMouseHover);
            seatPicBox.MouseLeave += new EventHandler(OnMouseLeave);
 
            //Update seat colour to match status
            UpdateSeatColour();
        }

        /*====================================================================
        * Event Handlers
        * ==================================================================*/

        /*  On Mouse Click */
        private void OnClick(object sender, EventArgs eventArgs)
        {

            //Get mouse Arguments for mouse button used
            MouseEventArgs mouseEvent = eventArgs as MouseEventArgs;
            MouseButtons mouseBtn = mouseEvent.Button;
            
            //If Left click
            if (mouseBtn == MouseButtons.Left)
            {
                Console.WriteLine("Seat Left Clicked");

                //Send seat selection event to seating layout, so we can track multiple seats selected at a time
                parentUI.SeatClicked(this, seatObj, mouseBtn);
            };
        

            //If right click
            if (mouseBtn == MouseButtons.Right)
            {
                Console.WriteLine("Seat Right Clicked");

                //Load seat modification ui
                CreateModifySeatUI modifySeatUI = new CreateModifySeatUI(seatObj, this);
                modifySeatUI.Show();
            }
        }

        /* On Mouse Enter: When user hovers mouse over the seat, create a tooltip */
        private void OnMouseEnter(object sender, EventArgs eventArgs)
        {
            if (seatObj == null) { return; };

            //If seat is selected show the grab object cursor
            if (parentUI.selectedSeatUIs.Contains(this)){ this.parentUI.Cursor = Cursors.Hand; this.Cursor = Cursors.Hand; }

            //If seat is moving, exit, dont show tooltip
            if (parentUI.seatMoving) { return; };

            //Create and show balloon tooltip
            seatTt = new ToolTip();                 //create object
            seatTt.IsBalloon = true;                //set to balloon style
            seatTt.ToolTipIcon = ToolTipIcon.None;  //set icon to none
            seatTt.InitialDelay = 50;               //Set pop-up delay in ms

            string ttText = (                       //Set text
                "Seat ID:\t" + seatObj.seatID +
                "\nName:\t" + seatObj.seatName + 
                "\nStatus:\t" + seatObj.seatStatus +
                "\nPrice List:" +
                    "\n  Adult:\t  £"   + seatObj.GetSeatPrice(Seat.TicketTypes.Adult)   +
                    "\n  Student: £"    + seatObj.GetSeatPrice(Seat.TicketTypes.Student) +
                    "\n  Senior:\t  £"  + seatObj.GetSeatPrice(Seat.TicketTypes.Senior)  +
                    "\n  Child:\t  £"   + seatObj.GetSeatPrice(Seat.TicketTypes.Child)
            );

            int ttLines = ttText.Split('\n').Length - 1,                       //count amount of lines in text
                ttSizeX = 16,                                                  //tooltip width offset
                ttSizeY = 45 + (int)(ttLines * 17.33),                         //17.333 is y size increase per line
                ttPosX = seatObj.seatPosX + (seatPicBox.Bounds.Width / 2) - ttSizeX, //Use size & position of tooltip & picbox
                ttPosY = seatObj.seatPosY + (seatPicBox.Bounds.Height / 2) - ttSizeY; // to find perfect pos for tooltip display

            //Show tooltip, infinite duration while user keeps mouse over seat
            seatTt.Show(ttText, layoutPicBox, ttPosX, ttPosY);
        }

        /* On Mouse Hover: When user keeps hovering mouse over seat, this keeps firing */
        private void OnMouseHover(object sender, EventArgs eventArgs)
        {
            //If seat is selected show the grab object cursor
            if (parentUI.selectedSeatUIs.Contains(this)) { this.parentUI.Cursor = Cursors.Hand; this.Cursor = Cursors.Hand; }
        }

        /* On Mouse Leave: When users mouse stops hovering over the seat, destroy the tooltip */
        private void OnMouseLeave(object sender, EventArgs eventArgs) { if (seatTt != null) { seatTt.Dispose(); this.parentUI.Cursor = Cursors.Default; }; }

        /*====================================================================
         * Methods
         * ==================================================================*/

        /*-------------------------------------------------------------------
         *  Update Seat Position / Move Seat
         *      Inputs:
         *          (int) newX:     New X position relative to parent
         *          (int) newY:     New Y position relative to parent
         * -----------------------------------------------------------------*/
        private void UpdateSeatPos(int newX, int newY) { seatPicBox.Location = new Point(newX, newY); }
        /*-------------------------------------------------------------------
         * Update Seat Colour
         *      Sets seat colour based on seatStatus
         *          Available = Light Green
         *          Held      = Orange
         *          Booked    = Red
         *          Error     = Black
         * -----------------------------------------------------------------*/
        public void UpdateSeatColour()
        {
            if (seatObj == null) { return; };
            switch (seatObj.seatStatus)
            {
                case Seat.SeatStates.Available:
                    ReColourBitmap(Color.LightGreen);
                    Console.WriteLine("updatecol 1");

                    break;
                case Seat.SeatStates.Held:
                    ReColourBitmap(Color.Orange);
                    Console.WriteLine("updatecol 2");

                    break;
                case Seat.SeatStates.Booked:
                    ReColourBitmap(Color.Red);
                    Console.WriteLine("updatecol 3");

                    break;
                default:
                    ReColourBitmap(Color.Black);
                    Console.WriteLine("updatecol 4");
                    break;
            }
        }

        /*-------------------------------------------------------------------
         * Re-Colour Bitmap
         *   Takes all colour that is NOT argb 0,0,0,0 (transparent nothing)
         *   and replaces it with the input colour, used to recolour seats
         * -----------------------------------------------------------------*/
        private void ReColourBitmap(Color NewColour)
        {
            Color trans = Color.FromArgb(0, 0, 0, 0);
            for (int x = 0; x < seatBitmap.Width; x++)
            {
                for (int y = 0; y < seatBitmap.Height; y++)
                {
                    if (seatBitmap.GetPixel(x, y) != trans) { seatBitmap.SetPixel(x, y, NewColour); };
                };
            };

            //Redraw
            seatPicBox.Refresh();
        }
    }
}
