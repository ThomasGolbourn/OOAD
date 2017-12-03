using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BCPA_OTS_Prototype
{
    public partial class SeatingLayoutUI : Form
    {
        //Seat layout this is representing
        private SeatingLayout seatingLayout;

        //Size values for various components, form is dynamic and adjusts itself
        private int
            marginSize = 8,
            topBarHeight = 31,
            buttonAndInfoWidth,
            buttonHeight,
            labelHeight,
            formBorderSize;

        //Used for privilege checking
        private bool isManager = false;
        private int  agentId   = -1;

        //Used for seat selection
        private bool            seatsSelected   = false;
        private List<Seat>      seatList;
        private List<SeatUI>    allSeatUIs;
        private List<Seat>      selectedSeats;
        public  List<SeatUI>    selectedSeatUIs;

        public SeatingLayoutUI() { }

        public SeatingLayoutUI(SeatingLayout seatLayout, bool managerModeEnabled, int loggedInAgentId = -1)
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;

            //Fill representitive value
            seatingLayout = seatLayout;

            //Fill privilege values
            isManager   = managerModeEnabled;
            agentId     = loggedInAgentId;

            //Fill calculated size values
            buttonAndInfoWidth  = btn_backToShow.Width;
            buttonHeight        = btn_backToShow.Height;
            labelHeight         = lbl_selectionInformation.Height;
            formBorderSize      = (Width - ClientSize.Width) / 2;

            //Create selected seat ui element list
            selectedSeatUIs = new List<SeatUI>();
            selectedSeats   = new List<Seat>();

            //Create seat list
            seatList = seatingLayout.seatList;
            allSeatUIs  = new List<SeatUI>();

            //Resize picture box to match seat layout given
            pb_layout.Size = new Size(seatLayout.seatLayoutImageSize.X, seatLayout.seatLayoutImageSize.Y);
            pb_layout.Location = new Point(marginSize, marginSize);

            //Resize form to match layout given
            Size = new Size(formBorderSize + marginSize + pb_layout.Size.Width + marginSize + buttonAndInfoWidth + marginSize + formBorderSize, topBarHeight + marginSize + seatLayout.seatLayoutImageSize.Y + marginSize + marginSize);

            //Move selection info label into place
            lbl_selectionInformation.Location = new Point(marginSize + pb_layout.Size.Width + marginSize, marginSize);

            //If manager interface, else customer interface
            if (managerModeEnabled) { LoadManagerInterface(seatLayout);  } else { LoadCustomerInterface(seatLayout); };
        }

        private void LoadManagerInterface(SeatingLayout seatLayout)
        {
            //Set window title
            Text = "Seating Layout Manager";

            //Hide customer interface controls
            btn_buySeats.Hide();    btn_help.Hide();   btn_backToShow.Hide();

            //Used for text box height
            int buttonCount = 8;

            //Move text box into place and resize height to fill space
            rtb_selectionInfo.Location  = new Point(marginSize + pb_layout.Size.Width + marginSize, marginSize + labelHeight + marginSize);
            rtb_selectionInfo.Height    = pb_layout.Height - labelHeight - (buttonHeight * buttonCount) - marginSize * (buttonCount + 1);

            //Move buttons, labels, and info text into place
            btn_addRow.Location             = new Point(marginSize + pb_layout.Size.Width + marginSize, marginSize + labelHeight + marginSize + rtb_selectionInfo.Height + marginSize + ((buttonHeight + marginSize) * 7));
            btn_addSeat.Location            = new Point(marginSize + pb_layout.Size.Width + marginSize, marginSize + labelHeight + marginSize + rtb_selectionInfo.Height + marginSize + ((buttonHeight + marginSize) * 6));
            btn_selectAllSeats.Location     = new Point(marginSize + pb_layout.Size.Width + marginSize, marginSize + labelHeight + marginSize + rtb_selectionInfo.Height + marginSize + ((buttonHeight + marginSize) * 5));
            btn_deleteSeats.Location        = new Point(marginSize + pb_layout.Size.Width + marginSize, marginSize + labelHeight + marginSize + rtb_selectionInfo.Height + marginSize + ((buttonHeight + marginSize) * 4));
            btn_modifySeats.Location        = new Point(marginSize + pb_layout.Size.Width + marginSize, marginSize + labelHeight + marginSize + rtb_selectionInfo.Height + marginSize + ((buttonHeight + marginSize) * 3));
            btn_saveLayout.Location         = new Point(marginSize + pb_layout.Size.Width + marginSize, marginSize + labelHeight + marginSize + rtb_selectionInfo.Height + marginSize + ((buttonHeight + marginSize) * 2));
            btn_loadSeatingLayout.Location  = new Point(marginSize + pb_layout.Size.Width + marginSize, marginSize + labelHeight + marginSize + rtb_selectionInfo.Height + marginSize + ((buttonHeight + marginSize) * 1));
            btn_promotionsManager.Location  = new Point(marginSize + pb_layout.Size.Width + marginSize, marginSize + labelHeight + marginSize + rtb_selectionInfo.Height + marginSize + ((buttonHeight + marginSize) * 0));

        }

        private void LoadCustomerInterface(SeatingLayout seatLayout)
        {
            //Set window title
            Text = "Seat Selection";

            //Hide manager interface controls
            btn_addRow.Hide();      btn_modifySeats.Hide();     btn_promotionsManager.Hide();
            btn_addSeat.Hide();     btn_deleteSeats.Hide();     btn_loadSeatingLayout.Hide();
            btn_saveLayout.Hide();  btn_selectAllSeats.Hide();

            //Used for text box height
            int buttonCount = 3;

            //Move text box into place and resize height to fill space
            rtb_selectionInfo.Location  = new Point(marginSize + pb_layout.Size.Width + marginSize, marginSize + labelHeight + marginSize);
            rtb_selectionInfo.Height    = pb_layout.Height - labelHeight - (buttonHeight * buttonCount) - marginSize * (buttonCount + 1);

            //Move buttons, labels, and info text into place
            btn_backToShow.Location = new Point(marginSize + pb_layout.Size.Width + marginSize, marginSize + labelHeight + marginSize + rtb_selectionInfo.Height + marginSize + ((buttonHeight + marginSize) * 2));
            btn_help.Location       = new Point(marginSize + pb_layout.Size.Width + marginSize, marginSize + labelHeight + marginSize + rtb_selectionInfo.Height + marginSize + ((buttonHeight + marginSize) * 1));
            btn_buySeats.Location   = new Point(marginSize + pb_layout.Size.Width + marginSize, marginSize + labelHeight + marginSize + rtb_selectionInfo.Height + marginSize + ((buttonHeight + marginSize) * 0));
        }

        private void ManagerClickedLayout(Point clickPos, MouseButtons mouseBtn)
        {
            //If no seat selected, create a seat
            if (!seatsSelected)
            {
                Seat newSeat                        = new Seat("NewSeat", clickPos.X, clickPos.Y, seatingLayout, Seat.SeatStates.Available, -1, -1);
                SeatUI newSeatUI                    = new SeatUI(newSeat, this);
                CreateModifySeatUI createSeatForm   = new CreateModifySeatUI(newSeat, newSeatUI);
                allSeatUIs.Add(newSeatUI);
                createSeatForm.Show();
            } else
            {
                //Stop seat moving
                seatMoving = false;

                //Deselect seats
                DeselectAllSeats();
            }
        }

        private bool seatMoving = false;

        private void ManagerClickedSeat(SeatUI clickedSeatUI, Seat clickedSeat, MouseButtons mouseBtn)
        {
            //If seat selected else if not selected
            if (selectedSeats.Contains(clickedSeat))
            {
                //Move seat
                if (!seatMoving)
                {
                    seatMoving = true;
                    Task.Run(() =>
                    {

                        while (seatMoving)
                        {
                            //Get cursor screen pos
                            Point cursorScreenPos = Cursor.Position;
                            //Convert to co-ords within control, use this as our 0 pos
                            Point cursorControlPos = this.PointToClient(new Point(cursorScreenPos.X, cursorScreenPos.Y));

                            //Get control dimensions so we can prevent them dragging object outside of ctrl
                            int maxPosX = pb_layout.Size.Width - (Cursor.Size.Width / 2); int maxPosY = pb_layout.Size.Height - (Cursor.Size.Height / 2);
                            if (cursorControlPos.X > maxPosX || cursorControlPos.X < 0 + (Cursor.Size.Width / 2)) { DeselectAllSeats(); };
                            if (cursorControlPos.Y > maxPosY || cursorControlPos.Y < 0 + (Cursor.Size.Height / 2)) { DeselectAllSeats(); };

                            foreach (Seat seatObj in selectedSeats)
                            {
                                seatObj.seatPosX = cursorControlPos.X - (Cursor.Size.Width / 2);
                                seatObj.seatPosY = cursorControlPos.Y - (Cursor.Size.Height / 2);
                            }
                            foreach (SeatUI seatUIObj in selectedSeatUIs)
                            {
                                seatUIObj.seatPicBox.Location = new Point((cursorControlPos.X - (Cursor.Size.Width / 2)), cursorControlPos.Y - (Cursor.Size.Height / 2));
                                seatUIObj.Refresh();
                            }

                            Thread.Sleep(10);
                        };
                    });
                } else {
                    seatMoving = false;
                };
            } else {
                //Stop seat move op
                seatMoving = false;

                //Select the seat
                SelectSeat(clickedSeatUI, clickedSeat);
            }
        }

        private void CustomerOrAgentClickedLayout(Point clickPos, MouseButtons mouseBtn)
        {
            
        }

        private void btn_deleteSeats_Click(object sender, EventArgs e)
        {
            foreach (SeatUI seatUI in selectedSeatUIs)
            { 
                allSeatUIs.Remove(seatUI);
                seatUI.Hide();
                seatUI.seatPicBox.Hide();
                seatUI.Dispose();
            };

            foreach (Seat seat in selectedSeats)
            {
                seatList.Remove(seat);
            }

            selectedSeatUIs.Clear();
            selectedSeats.Clear();
            seatsSelected = false;
        }

        private void CustomerOrAgentClickedSeat(SeatUI clickedSeatUI, MouseButtons mouseBtn)
        {

        }

        private void SelectSeat(SeatUI clickedSeatUI, Seat clickedSeat)
        {
            selectedSeatUIs.Add(clickedSeatUI);
            selectedSeats.Add(clickedSeat);
            clickedSeatUI.seatPicBox.BackColor = Color.Blue;
            seatsSelected = true;
        }

        private void DeselectAllSeats()
        {
            foreach (SeatUI seatUI in selectedSeatUIs) {
                seatUI.seatPicBox.BackColor = Color.Transparent;
            };

            selectedSeatUIs.Clear();
            selectedSeats.Clear();
            seatsSelected = false;
        }

        internal void SeatClicked(SeatUI clickedSeatUI, Seat clickedSeat, MouseButtons mouseBtn)
        {
            //Run manager / customer or agent method
            if (isManager)
            {
                ManagerClickedSeat(clickedSeatUI, clickedSeat, mouseBtn);
            }
            else
            {
                CustomerOrAgentClickedSeat(clickedSeatUI, mouseBtn);
            };
        }


        private void pb_layout_Click(object sender, EventArgs e)
        {
            MouseEventArgs mouseEvent   = e as MouseEventArgs;
            Point clickPos              = mouseEvent.Location;
            MouseButtons mouseBtn       = mouseEvent.Button;

            //Run manager / customer or agent method
            if (isManager) {
                ManagerClickedLayout(clickPos, mouseBtn);
            } else {
                CustomerOrAgentClickedLayout(clickPos, mouseBtn);
            };
        }



    }
}
